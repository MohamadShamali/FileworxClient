using Elastic.Clients.Elasticsearch;
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
using FileworxDTOsLibrary;

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

            settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
            client = new ElasticsearchClient(settings);
        }


        public async override Task InsertAsync()
        {
            LastReceiveDate = DateTime.Now;

            // DB
            await base.InsertAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"INSERT INTO T_CONTACT (ID, C_TRANSMITLOCATION, C_RECEIVELOCATION, C_CONTACTDIRECTIONID, C_LASTRECEIVEDATE, C_ENABLED) " +
                               $"VALUES('{Id}', '{TransmitLocation}', '{ReceiveLocation}',{(int) Direction}, '{LastReceiveDate.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}', '{Enabled}');";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }

            // Elastic
            var contactDto = new clsContactDto(this);
            var response = await client.IndexAsync(contactDto, EditBeforeRun.ElasticContactsIndex);
            contactDto = null;
            if (!response.IsValidResponse)
            {
                Console.WriteLine(response.DebugInformation);
            }
            client.Indices.Refresh(EditBeforeRun.ElasticContactsIndex); // refresh index 
        }

        public override async Task DeleteAsync()
        {

            // DB
            await base.DeleteAsync();

            // Elastic
            var response = await client.DeleteAsync(EditBeforeRun.ElasticContactsIndex, Id);
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforeRun.ElasticContactsIndex);
        }

        public override async Task UpdateAsync()
        {

            // DB
            await base.UpdateAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();

                string query = $"UPDATE T_CONTACT SET C_TRANSMITLOCATION = '{TransmitLocation}', C_RECEIVELOCATION = '{ReceiveLocation}', " +
                               $"C_CONTACTDIRECTIONID= '{(int)Direction}', C_LASTRECEIVEDATE= '{LastReceiveDate.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}', " +
                               $"C_ENABLED= '{Enabled}' " +
                               $"WHERE Id = '{Id}';";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }

            // Elastic
            var contactDto = new clsContactDto(this);
            var response = await client.UpdateAsync<clsContactDto, clsContactDto>(EditBeforeRun.ElasticContactsIndex, Id, u => u.Doc(contactDto));
            contactDto = null;
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforeRun.ElasticContactsIndex);
        }

        public override async Task ReadAsync()
        {
            // DB
            await base.ReadAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"SELECT C_TRANSMITLOCATION, C_RECEIVELOCATION, C_CONTACTDIRECTIONID, C_LASTRECEIVEDATE, C_ENABLED " +
                               $"FROM {tableName} " +
                               $"WHERE Id = '{Id}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
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

        public override void Read()
        {
            // DB
            base.Read();
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
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
                            int d = (int)reader[2];
                            Direction = (ContactDirection)d;
                            LastReceiveDate = DateTime.Parse(reader[3].ToString());
                            Enabled = (bool)reader[4];

                        }
                    }
                }
            }
        }

        public void TransmitFile(clsFile file)
        {
            Guid TxGuid = Guid.NewGuid();
            string filePath = TransmitLocation + @"\" + TxGuid + ".txt";
            if (file is clsNews)
            {
                clsNews news = (clsNews) file;

                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(GetTxtFileContent(news));
                }
                fs.Close();
            }

            else
            {
                clsPhoto photo = (clsPhoto) file;

                File.Copy(photo.Location, TransmitLocation + @"\" + TxGuid + Path.GetExtension(photo.Location));
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(GetTxtFileContent(photo, TxGuid));
                }

                fs.Close();
            }

        }

        public async Task ReceiveFileIfItsNew(string filePath)
        {
            DateTime fileLastModification = File.GetLastWriteTime(filePath);
            
            if (fileLastModification > LastReceiveDate)
            {
                string record;
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                using (StreamReader reader = new StreamReader(fs))
                {
                    record = reader.ReadLine();
                }
                string[] content = record.Split(new string[] { EditBeforeRun.Separator }, StringSplitOptions.None);

                string format = "M/d/yyyy h:mm:ss tt";

                // News
                if (content[0] == $"{(int) Type.News}")
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

                    await news.InsertAsync();
                }

                // Photo
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

                    await photo.InsertAsync();
                }

                LastReceiveDate = fileLastModification;
                await this.UpdateAsync();
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

        public string GetTxtFileContent(clsNews news)
        {
            return $"{(int) news.Class}{EditBeforeRun.Separator}" +
                   $"{news.Description}{EditBeforeRun.Separator}" +
                   $"{news.CreationDate}{EditBeforeRun.Separator}" +
                   $"{news.Name}{EditBeforeRun.Separator}" + 
                   $"{news.Body}{EditBeforeRun.Separator}" + 
                   $"{news.Category}";
        }

        public string GetTxtFileContent(clsPhoto photo , Guid TxGuid)
        {
            return $"{(int) photo.Class}{EditBeforeRun.Separator}" +
                   $"{photo.Description}{EditBeforeRun.Separator}" +
                   $"{photo.CreationDate}{EditBeforeRun.Separator}" +
                   $"{photo.Name}{EditBeforeRun.Separator}" +
                   $"{photo.Body}{EditBeforeRun.Separator}" +
                   $"{TransmitLocation + @"\" + TxGuid + Path.GetExtension(photo.Location)}";
        }

    }
}
