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
using FileworxDTOsLibrary.DTOs;
using Type = FileworxDTOsLibrary.DTOs.Type;

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

    }
}
