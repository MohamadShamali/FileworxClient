using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxObjectClassLibrary
{
    public class clsPhoto : clsFile
    {
        // Constants
        static string tableName = "T_PHOTO";
        private ElasticsearchClientSettings settings;
        private ElasticsearchClient client;

        // Properties
        private string location;

        private bool photoUpdated { get; 
            set; }
        public string Location 
        { 
            get { return location; }
            set
            {
                string directoryPath = Path.GetDirectoryName(value);

                if (File.Exists(value))
                {
                    if(directoryPath != EditBeforRun.PhotosLocation)
                    {
                        if (File.Exists(location) && (location != value))
                        {
                            File.Delete(location);
                            photoUpdated = true;
                        }
                    }
                    location = value;
                }

                else
                {
                    throw new InvalidOperationException("The specified file does not exist.");
                }
            } 
        }

        public clsPhoto()
        {
            settings = new ElasticsearchClientSettings(new Uri(EditBeforRun.ElasticUri));
            client = new ElasticsearchClient(settings);

            Class =Type.Photo;
            photoUpdated = false;
        }

        public async override Task InsertAsync()
        {
            await base.InsertAsync();
            copyImage();
            photoUpdated = false;
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"INSERT INTO T_PHOTO (ID, C_LOCATION) " +
                               $"VALUES('{Id}', '{Location}')";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            var response = await client.IndexAsync(this, EditBeforRun.ElasticFilesIndex);
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforRun.ElasticFilesIndex);
        }

        public async override Task DeleteAsync()
        {
            await base.DeleteAsync();

            if (File.Exists(location))
            {
                File.Delete(location);
            }

            var response = await client.DeleteAsync(EditBeforRun.ElasticFilesIndex, Id);
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforRun.ElasticFilesIndex);
        }

        public async override Task UpdateAsync()
        {
            await base.UpdateAsync();
            if (photoUpdated)
            {
                copyImage();
                photoUpdated = false;
            }

            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                await connection.OpenAsync();

                string query = $"UPDATE {tableName} " +
                               $"SET C_LOCATION = '{Location}' " +
                               $"WHERE Id = '{Id}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }

            var response = await client.UpdateAsync<clsPhoto, clsPhoto>(EditBeforRun.ElasticFilesIndex, Id, u => u.Doc(this));
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
            client.Indices.Refresh(EditBeforRun.ElasticFilesIndex);
        }

        public override async Task ReadAsync()
        {
            await base.ReadAsync();
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                await connection.OpenAsync();

                string query = $"SELECT ID, C_LOCATION " +
                               $"FROM {tableName} " +
                               $"WHERE Id = '{Id}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            Location = (reader[1].ToString());
                        }
                    }
                }
            }
        }

        public override void Read()
        {
            base.Read();
            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                connection.Open();

                string query = $"SELECT ID, C_LOCATION " +
                               $"FROM {tableName} " +
                               $"WHERE Id = '{Id}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Location = (reader[1].ToString());
                        }
                    }
                }
            }
        }

        private void copyImage()
        {
            string photoextention = Path.GetExtension(location);
            if(!File.Exists(EditBeforRun.PhotosLocation + @"\" + Id.ToString() + photoextention))
            {
                File.Copy(location, EditBeforRun.PhotosLocation + @"\" + Id.ToString() + photoextention);
                location = EditBeforRun.PhotosLocation + @"\" + Id.ToString() + photoextention;
            }
        }
    }
}
