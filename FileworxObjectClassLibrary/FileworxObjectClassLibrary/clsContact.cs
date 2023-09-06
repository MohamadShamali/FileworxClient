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
        public bool Enabled { get; set; } = true;
        public clsContact()
        {
            Class = Type.Contact;

            settings = new ElasticsearchClientSettings(new Uri(EditBeforRun.ElasticUri));
            client = new ElasticsearchClient(settings);
        }

        public async override Task InsertAsync()
        {

            // DB
            await base.InsertAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"INSERT INTO T_CONTACT (ID, C_TRANSMITLOCATION, C_RECEIVELOCATION, C_CONTACTDIRECTIONID , C_ENABLED) " +
                               $"VALUES('{Id}', '{TransmitLocation}', '{ReceiveLocation}',{(int) Direction} , '{Enabled}');";
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

                string query = $"UPDATE T_CONTACT SET C_TRANSMITLOCATION = '{TransmitLocation}', C_RECEIVELOCATION = '{ReceiveLocation}', C_CONTACTDIRECTIONID='{(int)Direction}',C_ENABLED= '{Enabled}' " +
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
                string query = $"SELECT C_TRANSMITLOCATION, C_RECEIVELOCATION, C_CONTACTDIRECTIONID, C_ENABLED " +
                               $"FROM {tableName} " +
                               $"WHERE Id = '{Id}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            if (!String.IsNullOrEmpty(reader[1].ToString()))
                            {
                                TransmitLocation = (reader[0].ToString());
                                ReceiveLocation = (reader[1].ToString());
                                int d = (int) reader[2];
                                Direction = (ContactDirection) d;
                                Enabled= (bool) reader[3];
                            }
                        }
                    }
                }
            }
        }

        public void TransmitFile(clsFile file)
        {
            string filePath = TransmitLocation + @"\" + file.Id.ToString() + ".txt";
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

        public List<clsFile> ReceiveAllFiles()
        {
            if ((Direction & ContactDirection.Receive) != ContactDirection.Receive)
            {
                throw new Exception("This is not a Receive Contact");
            }

            List<clsFile> receivedFiles = new List<clsFile>();
            try
            {
                string[] fileNames = Directory.GetFiles(ReceiveLocation);

                foreach (string fileName in fileNames)
                {
                    receivedFiles.Add(ReceiveFile(fileName));
                }

                return receivedFiles;
            }

            catch (Exception ex)
            {
                throw new Exception("Error While Receiving Files");
            }
        }

        // _______________________________________________________________________

        static public clsFile ReceiveFile(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            string record;
            using (StreamReader reader = new StreamReader(fs))
            {
                record = reader.ReadLine();
            }
            string[] content = record.Split(new string[] { EditBeforRun.Separator }, StringSplitOptions.None);

            clsFile file = new clsFile();

            if (content[0] == "N")
            {
                clsNews news = new clsNews()
                {
                    Id = new Guid(content[1]),
                    Description = content[2],
                    CreationDate = DateTime.ParseExact(content[3], "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture),
                    ModificationDate = DateTime.ParseExact(content[4], "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture),
                    CreatorId = new Guid(content[5]),
                    CreatorName = content[6],
                    LastModifierId = new Guid(content[7]),
                    LastModifierName = content[8],
                    Name = content[9],
                    Class = (Type)Enum.Parse(typeof(Type), content[10]),
                    Body = content[11],
                    Category = content[12]
                };
                file = news;
            }

            else
            {
                clsPhoto photo = new clsPhoto()
                {
                    Id = new Guid(content[1]),
                    Description = content[2],
                    CreationDate = DateTime.ParseExact(content[3], "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture),
                    ModificationDate = DateTime.ParseExact(content[4], "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture),
                    CreatorId = new Guid(content[5]),
                    CreatorName = content[6],
                    LastModifierId = new Guid(content[7]),
                    LastModifierName = content[8],
                    Name = content[9],
                    Class = (Type)Enum.Parse(typeof(Type), content[10]),
                    Body = content[11],
                    Location = content[12]
                };
                file = photo;
            }

            return file;
        }

        private string txtFileContent(clsNews news)
        {
            return $"N{EditBeforRun.Separator}" +
                   $"{news.Id}{EditBeforRun.Separator}" +
                   $"{news.Description}{EditBeforRun.Separator}" +
                   $"{news.CreationDate}{EditBeforRun.Separator}" +
                   $"{news.ModificationDate}{EditBeforRun.Separator}" +
                   $"{news.CreatorId}{EditBeforRun.Separator}" +
                   $"{news.CreatorName}{EditBeforRun.Separator}" + 
                   $"{news.LastModifierId}{EditBeforRun.Separator}" +
                   $"{news.LastModifierName}{EditBeforRun.Separator}" + 
                   $"{news.Name}{EditBeforRun.Separator}" + 
                   $"{news.Class}{EditBeforRun.Separator}" +
                   $"{news.Body}{EditBeforRun.Separator}" + 
                   $"{news.Category}";
        }

        private string txtFileContent(clsPhoto photo)
        {
            return $"N{EditBeforRun.Separator}" +
                   $"{photo.Id}{EditBeforRun.Separator}" +
                   $"{photo.Description}{EditBeforRun.Separator}" +
                   $"{photo.CreationDate}{EditBeforRun.Separator}" +
                   $"{photo.ModificationDate}{EditBeforRun.Separator}" +
                   $"{photo.CreatorId}{EditBeforRun.Separator}" +
                   $"{photo.CreatorName}{EditBeforRun.Separator}" +
                   $"{photo.LastModifierId}{EditBeforRun.Separator}" +
                   $"{photo.LastModifierName}{EditBeforRun.Separator}" +
                   $"{photo.Name}{EditBeforRun.Separator}" +
                   $"{photo.Class}{EditBeforRun.Separator}" +
                   $"{photo.Body}{EditBeforRun.Separator}" +
                   $"{photo.Location}";
        }

    }
}
