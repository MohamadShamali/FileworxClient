﻿using Elastic.Clients.Elasticsearch;
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

        private bool photoUpdated;
        public string Location 
        { 
            get { return location; }
            set
            {
                if (File.Exists(value))
                {
                    if(File.Exists(location))
                    {
                        File.Delete(location);
                        photoUpdated = true;
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
            settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
            client = new ElasticsearchClient(settings);

            Class =Type.Photo;
            photoUpdated = false;
        }

        public async override void Insert()
        {
            base.Insert();
            copyImage();

            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO T_PHOTO (ID, C_LOCATION) " +
                               $"VALUES('{Id}', '{Location}')";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            var response = await client.IndexAsync(this, "businessobject");
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
        }

        public async override void Delete()
        {
            base.Delete();

            if (File.Exists(location))
            {
                File.Delete(location);
            }

            var response = await client.DeleteAsync("businessobject", Id);
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
        }

        public async override void Update()
        {
            base.Update();
            if (photoUpdated)
            {
                copyImage();
                photoUpdated = false;
            }

            using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
            {
                connection.Open();

                string query = $"UPDATE {tableName} " +
                               $"SET C_LOCATION = '{Location}' " +
                               $"WHERE Id = '{Id}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            var response = await client.UpdateAsync<clsPhoto, clsPhoto>("businessobject", Id, u => u.Doc(this));
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
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
            string photoName = Path.GetFileNameWithoutExtension(location);
            string photoextention = Path.GetExtension(location);
            if(!File.Exists(EditBeforRun.PhotosLocation + @"\" + Id.ToString() + photoextention))
            {
                File.Copy(location, EditBeforRun.PhotosLocation + @"\" + Id.ToString() + photoextention);
                location = EditBeforRun.PhotosLocation + @"\" + Id.ToString() + photoextention;
            }
        }
    }
}
