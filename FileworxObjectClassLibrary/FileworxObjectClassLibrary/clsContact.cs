using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
            updateDirectories();

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
            deleteTransmitDirectory();
            deleteReceiveDirectory();

            await base.DeleteAsync();

            var response = await client.DeleteAsync(EditBeforRun.ElasticContactsIndex, Id);

            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforRun.ElasticContactsIndex);
        }

        public override async Task UpdateAsync()
        {
            updateDirectories();

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

        private void updateDirectories()
        {
            if ((Direction & ContactDirection.Transmit) == ContactDirection.Transmit)
            {
                TransmitLocation = EditBeforRun.TransmitFolder + @"\" + Id;
                createTransmitDirectory();
            }
            else
            {
                TransmitLocation = String.Empty;
                deleteTransmitDirectory();
            }

            if ((Direction & ContactDirection.Receive) == ContactDirection.Receive)
            {
                ReceiveLocation = EditBeforRun.ReceiveFolder + @"\" + Id;
                createReceiveDirectory();
            }
            else
            {
                ReceiveLocation = String.Empty;
                deleteReceiveDirectory();
            }
        }

        private void createTransmitDirectory()
        {
            if (!Directory.Exists(TransmitLocation))
            {
                Directory.CreateDirectory(TransmitLocation);
            }
        }

        private void deleteTransmitDirectory()
        {
            if (Directory.Exists(TransmitLocation))
            {
                Directory.Delete(TransmitLocation);
            }
        }

        private void createReceiveDirectory()
        {
            if (!Directory.Exists(ReceiveLocation))
            {
                Directory.CreateDirectory(ReceiveLocation);
            }
        }

        private void deleteReceiveDirectory()
        {
            if (Directory.Exists(ReceiveLocation))
            {
                Directory.Delete(ReceiveLocation);
            }
        }
    }
}
