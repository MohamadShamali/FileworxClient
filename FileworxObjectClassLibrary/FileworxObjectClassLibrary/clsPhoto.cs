﻿using System;
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
            Class=Type.Photo;
            photoUpdated = false;
        }

        public override void Insert()
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
        }

        public override void Delete()
        {
            base.Delete();

            if (File.Exists(location))
            {
                File.Delete(location);
            }
        }

        public override void Update()
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
