using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxObjectClassLibrary
{
    public enum ContactDirection { Transmit= 1, Receive= 2, TransmitAndReceive= 3 };
    public class clsContact : clsBusinessObject
    {
        // Constants
        static string tableName = "T_CONTACT";
        private ElasticsearchClientSettings settings;
        private ElasticsearchClient client;

        // Properties
        public string Location { get; set; }
        public ContactDirection Direction { get { return direction; } set { direction = value; DirectionID = (int) value;  } }
        private ContactDirection direction;
        public int DirectionID { get; set; }
        public clsContact()
        {
            Class = Type.Contact;

            settings = new ElasticsearchClientSettings(new Uri(EditBeforRun.ElasticUri));
            client = new ElasticsearchClient(settings);
        }

        public async override Task InsertAsync()
        {
            await base.InsertAsync();

            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"INSERT INTO T_CONTACT (ID, C_LOCATION, C_CONTACTDIRECTIONID) " +
                               $"VALUES('{Id}', '{Location}',{(int) Direction});";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            var response = await client.IndexAsync(this, EditBeforRun.ElasticContactsIndex);

            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforRun.ElasticContactsIndex); // refresh index 
        }

        public override async Task DeleteAsync()
        {
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
            await base.UpdateAsync();

            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                await connection.OpenAsync();

                string query = $"UPDATE T_CONTACT SET C_LOCATION = '{Location}, C_CONTACTDIRECTIONID={(int)Direction}' " +
                               $"WHERE Id = '{Id}';";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }

            var response = await client.UpdateAsync<clsContact, clsContact>(EditBeforRun.ElasticContactsIndex, Id, u => u.Doc(this));

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
                string query = $"SELECT C_LOCATION, C_CONTACTDIRECTIONID " +
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
                                Location = (reader[0].ToString());
                                int d = (int)(reader[1]);
                                Direction = (ContactDirection) d;
                            }
                        }
                    }
                }
            }
        }
    }
}
