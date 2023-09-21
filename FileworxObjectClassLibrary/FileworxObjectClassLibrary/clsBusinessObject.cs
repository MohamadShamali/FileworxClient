using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileworxDTOsLibrary;

namespace FileworxObjectClassLibrary
{
    public enum Type { User = 1, News = 2, Photo = 3, Contact=4 }
    public class clsBusinessObject
    {
        // Constants
        static string tableName = "T_BUSINESSOBJECT";

        // Properties
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid CreatorId { get; set; }
        public string CreatorName { get; set; } = String.Empty;
        public Guid? LastModifierId { get; set; } = null;
        public string LastModifierName { get; set; } = String.Empty;
        public string Name { get; set; }
        public Type Class { get; set; }
        public clsBusinessObject()
        {
            CreatorId = new Guid("ffd7c672-aa84-47b1-a9a3-c7875a503708"); // to remove
            LastModifierId = new Guid("ffd7c672-aa84-47b1-a9a3-c7875a503708"); // to remove
        }

        public virtual async Task InsertAsync()
        {
            if (Description != null) 
            Description = Description.Replace("'", "''");
            Name = Name.Replace("'", "''");
            if(CreationDate == DateTime.MinValue)
            CreationDate = DateTime.Now;

            ModificationDate = CreationDate;
            LastModifierId = CreatorId;
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"INSERT INTO {tableName}(ID, C_DESCRIPTION, C_CREATIONDATE, C_CREATORID, C_NAME, C_CLASSID)" +
                               $"VALUES('{Id}', '{Description}', '{CreationDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{CreatorId}', '{Name}', {(int) Class});";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public virtual async Task DeleteAsync()
        {
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();
                string query = $"DELETE FROM {tableName} WHERE Id = '{Id}'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public virtual async Task UpdateAsync()
        {
            if (Description != null)
            Description = Description.Replace("'", "''");
            Name = Name.Replace("'", "''");
            ModificationDate = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();

                string query = $"UPDATE {tableName} SET C_DESCRIPTION = '{Description}', C_CREATIONDATE = '{CreationDate.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                               $"C_MODIFICATIONDATE = '{ModificationDate.ToString("yyyy-MM-dd HH:mm:ss")}', C_CREATORID= '{CreatorId}', C_LASTMODIFIERID= '{LastModifierId}', " +
                               $"C_NAME= '{Name}'  WHERE Id = '{Id}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public virtual async Task ReadAsync()
        {
            if(Id == null)
            {
                throw new Exception("No ID was specified");
            }
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                await connection.OpenAsync();
                                        //0
                string query = $"SELECT b1.C_DESCRIPTION, b1.C_CREATIONDATE, b1.C_MODIFICATIONDATE, b1.C_CREATORID, b2.C_NAME AS CREATORNAME , " +
                               $"b1.C_LASTMODIFIERID, b3.C_NAME AS LASTMODIFIERNAME, b1.C_NAME , b1.C_CLASSID " +
                               $"FROM {tableName} b1 " +
                               $"Left JOIN {tableName} b2 ON b1.C_CREATORID = b2.ID " +
                               $"Left JOIN {tableName} b3 ON b1.C_LASTMODIFIERID = b3.ID "+
                               $"WHERE b1.ID= '{Id}';";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {

                            if (!String.IsNullOrEmpty(reader[0].ToString()))
                            {
                                Description = (reader[0].ToString());
                            }

                            if (!String.IsNullOrEmpty(reader[1].ToString()))
                            {
                                CreationDate = DateTime.Parse(reader[1].ToString());
                            }

                            if (!String.IsNullOrEmpty(reader[2].ToString()))
                            {
                                ModificationDate = DateTime.Parse(reader[2].ToString());
                            }

                            if (!String.IsNullOrEmpty(reader[3].ToString()))
                            {
                                CreatorId = new Guid(reader[3].ToString());
                            }

                            if (!String.IsNullOrEmpty(reader[4].ToString()))
                            {
                                CreatorName = reader[4].ToString();
                            }

                            if (!String.IsNullOrEmpty(reader[5].ToString()))
                            {
                                LastModifierId = new Guid(reader[5].ToString());
                            }


                            if (!String.IsNullOrEmpty(reader[6].ToString()))
                            {
                                LastModifierName = reader[6].ToString();
                            }

                            if (!String.IsNullOrEmpty(reader[7].ToString()))
                            {
                                Name = reader[7].ToString();
                            }

                            int c = (int)(reader[8]);
                            Class = (Type)c;
                        }
                    }
                }
            }
        }

        public virtual void Read()
        {
            if (Id == null)
            {
                throw new Exception("No ID was specified");
            }
            using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
            {
                connection.Open();
                //0
                string query = $"SELECT b1.C_DESCRIPTION, b1.C_CREATIONDATE, b1.C_MODIFICATIONDATE, b1.C_CREATORID, b2.C_NAME AS CREATORNAME , " +
                               $"b1.C_LASTMODIFIERID, b3.C_NAME AS LASTMODIFIERNAME, b1.C_NAME , b1.C_CLASSID " +
                               $"FROM {tableName} b1 " +
                               $"Left JOIN {tableName} b2 ON b1.C_CREATORID = b2.ID " +
                               $"Left JOIN {tableName} b3 ON b1.C_LASTMODIFIERID = b3.ID " +
                               $"WHERE b1.ID= '{Id}';";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            if (!String.IsNullOrEmpty(reader[0].ToString()))
                            {
                                Description = (reader[0].ToString());
                            }

                            if (!String.IsNullOrEmpty(reader[1].ToString()))
                            {
                                CreationDate = DateTime.Parse(reader[1].ToString());
                            }

                            if (!String.IsNullOrEmpty(reader[2].ToString()))
                            {
                                ModificationDate = DateTime.Parse(reader[2].ToString());
                            }

                            if (!String.IsNullOrEmpty(reader[3].ToString()))
                            {
                                CreatorId = new Guid(reader[3].ToString());
                            }

                            if (!String.IsNullOrEmpty(reader[4].ToString()))
                            {
                                CreatorName = reader[4].ToString();
                            }

                            if (!String.IsNullOrEmpty(reader[5].ToString()))
                            {
                                LastModifierId = new Guid(reader[5].ToString());
                            }


                            if (!String.IsNullOrEmpty(reader[6].ToString()))
                            {
                                LastModifierName = reader[6].ToString();
                            }

                            if (!String.IsNullOrEmpty(reader[7].ToString()))
                            {
                                Name = reader[7].ToString();
                            }

                            int c = (int)(reader[8]);
                            Class = (Type)c;
                        }
                    }
                }
            }
        }

    }
}
