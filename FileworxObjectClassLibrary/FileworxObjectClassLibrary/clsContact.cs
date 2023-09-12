﻿using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FileworxObjectClassLibrary
{
    public enum ContactDirection 
    {
        Transmit = 1,
        Receive = 2
    };

    public class clsContact : clsBusinessObject
    {
        // Constants
        static string tableName = "T_CONTACT";
        private ElasticsearchClientSettings settings;
        private ElasticsearchClient client;

        // Properties
        public string TransmitLocation { get; set; } = String.Empty;
        public string ReceiveLocation { get; set; } = String.Empty;
        public ContactDirection Direction {get; set;} = (ContactDirection.Transmit | ContactDirection.Receive);
        public DateTime LastReceiveDate { get; set; }
        public bool Enabled { get; set; } = true;
        public clsContact()
        {
            Class = Type.Contact;

            settings = new ElasticsearchClientSettings(new Uri(EditBeforRun.ElasticUri));
            client = new ElasticsearchClient(settings);
        }

        public async override Task InsertAsync()
        {
            LastReceiveDate = DateTime.Now;

            // DB
            await base.InsertAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"INSERT INTO T_CONTACT (ID, C_TRANSMITLOCATION, C_RECEIVELOCATION, C_CONTACTDIRECTIONID, C_LASTRECEIVEDATE, C_ENABLED) " +
                               $"VALUES('{Id}', '{TransmitLocation}', '{ReceiveLocation}',{(int) Direction}, '{LastReceiveDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{Enabled}');";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }

            // Elastic
            var contactDto = new clsContactDto(this);
            var response = await client.IndexAsync(contactDto, EditBeforRun.ElasticContactsIndex);
            contactDto = null;
            if (!response.IsValidResponse)
            {
                Console.WriteLine(response.DebugInformation);
            }
            client.Indices.Refresh(EditBeforRun.ElasticContactsIndex); // refresh index 
        }

        public override async Task DeleteAsync()
        {

            // DB
            await base.DeleteAsync();

            // Elastic
            var response = await client.DeleteAsync(EditBeforRun.ElasticContactsIndex, Id);
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforRun.ElasticContactsIndex);
        }

        public override async Task UpdateAsync()
        {

            // DB
            await base.UpdateAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                await connection.OpenAsync();

                string query = $"UPDATE T_CONTACT SET C_TRANSMITLOCATION = '{TransmitLocation}', C_RECEIVELOCATION = '{ReceiveLocation}', " +
                               $"C_CONTACTDIRECTIONID= '{(int)Direction}', C_LASTRECEIVEDATE= '{LastReceiveDate.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                               $"C_ENABLED= '{Enabled}' " +
                               $"WHERE Id = '{Id}';";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }

            // Elastic
            var contactDto = new clsContactDto(this);
            var response = await client.UpdateAsync<clsContactDto, clsContactDto>(EditBeforRun.ElasticContactsIndex, Id, u => u.Doc(contactDto));
            contactDto = null;
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforRun.ElasticContactsIndex);
        }

        public override void Read()
        {
            // DB
            base.Read();
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                connection.Open();
                string query = $"SELECT C_TRANSMITLOCATION, C_RECEIVELOCATION, C_CONTACTDIRECTIONID, C_LASTRECEIVEDATE, C_ENABLED " +
                               $"FROM {tableName} " +
                               $"WHERE Id = '{Id}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            if (!String.IsNullOrEmpty(reader[0].ToString())) TransmitLocation = (reader[0].ToString());
                            if (!String.IsNullOrEmpty(reader[1].ToString())) ReceiveLocation = (reader[1].ToString());
                                int d = (int) reader[2];
                                Direction = (ContactDirection) d;
                                LastReceiveDate= DateTime.Parse(reader[3].ToString());
                                Enabled = (bool) reader[4];
                            
                        }
                    }
                }
            }
        }

        public void TransmitFile(clsFile file)
        {
            string filePath = TransmitLocation + @"\" + file.Id + ".txt";
            if (file is clsNews)
            {
                clsNews news = (clsNews) file;

                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(txtFileContent(news));
                }
                fs.Close();
            }

            else
            {
                clsPhoto photo = (clsPhoto) file;

                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(txtFileContent(photo));
                }
                fs.Close();
            }

        }

        public async Task ReceiveFileIfItsNew(string filePath)
        {
            string record;
            string format = "M/d/yyyy h:mm:ss tt";

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (StreamReader reader = new StreamReader(fs))
            {
                record = reader.ReadLine();
            }
            string[] content = record.Split(new string[] { EditBeforRun.Separator }, StringSplitOptions.None);


            if (content[0] == "N")
            {
                clsNews news = new clsNews()
                {
                    Id = Guid.NewGuid(),
                    Description = content[1],
                    CreationDate = DateTime.ParseExact(content[2], format, System.Globalization.CultureInfo.InvariantCulture),
                    CreatorId = new Guid("ffd7c672-aa84-47b1-a9a3-c7875a503708"),
                    CreatorName = "admin",
                    Name = content[3],
                    Body = content[4],
                    Category = content[5]
                };
                DateTime fileLastWriteTime = news.CreationDate;

                if (fileLastWriteTime > LastReceiveDate)
                {
                    await news.InsertAsync();
                    LastReceiveDate = news.CreationDate;
                    await this.UpdateAsync();
                }
            }

            else
            {
                clsPhoto photo = new clsPhoto()
                {
                    Id = Guid.NewGuid(),
                    Description = content[1],
                    CreationDate = DateTime.ParseExact(content[2], format, System.Globalization.CultureInfo.InvariantCulture),
                    CreatorId = new Guid("ffd7c672-aa84-47b1-a9a3-c7875a503708"),
                    CreatorName = "admin",
                    Name = content[3],
                    Body = content[4],
                    Location = content[5]
                };
                DateTime fileLastWriteTime2 = photo.CreationDate;

                if (fileLastWriteTime2 > LastReceiveDate)
                {
                    await photo.InsertAsync();
                    LastReceiveDate = photo.CreationDate;
                    await this.UpdateAsync();
                }
            }

        }

        public async Task ReceiveAllFiles()
        {
            // Get all files in the directory and order them by modification date (oldest to newest)
            var files = Directory.GetFiles(ReceiveLocation).OrderBy(file => new FileInfo(file).LastWriteTime);

            foreach (var file in files)
            {
                await ReceiveFileIfItsNew(file);
            }
        }

        // _______________________________________________________________________

        private string txtFileContent(clsNews news)
        {
            return $"N{EditBeforRun.Separator}" +
                   $"{news.Description}{EditBeforRun.Separator}" +
                   $"{DateTime.Now}{EditBeforRun.Separator}" +
                   $"{news.Name}{EditBeforRun.Separator}" + 
                   $"{news.Body}{EditBeforRun.Separator}" + 
                   $"{news.Category}";
        }

        private string txtFileContent(clsPhoto photo)
        {
            return $"P{EditBeforRun.Separator}" +
                   $"{photo.Description}{EditBeforRun.Separator}" +
                   $"{DateTime.Now}{EditBeforRun.Separator}" +
                   $"{photo.Name}{EditBeforRun.Separator}" +
                   $"{photo.Body}{EditBeforRun.Separator}" +
                   $"{photo.Location}";
        }

    }
}
