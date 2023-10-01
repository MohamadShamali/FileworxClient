using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.QueryDsl;
using FileworxDTOsLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FileworxDTOsLibrary.DTOs;
using Type = FileworxDTOsLibrary.DTOs.Type;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft;
using Newtonsoft.Json;
using FileworxDTOsLibrary.RabbitMQMessages;
using FileworxObjectClassLibrary.Models;

namespace FileworxObjectClassLibrary.Queries
{
    public class clsFileQuery
    {
        //Constants
        static string tableName = "T_FILE";

        // Properties
        public Type[] QClasses { get; set; } = { Type.News, Type.Photo };
        public QuerySource Source { get; set; }

        public async Task<List<clsFile>> RunAsync()
        {
            List<clsFile> allFiles = new List<clsFile>();

            if (Source == QuerySource.DB)
            {
                string[] conditions = new string[QClasses.Length];
                for (int i = 0; i < QClasses.Length; i++)
                {
                    conditions[i] = $"b1.C_CLASSID = {(int)QClasses[i]} OR ";
                    if (i == QClasses.Length - 1) conditions[i] = conditions[i].Replace("OR", "");
                }

                string conditionsString = string.Join(" ", conditions);

                using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
                {
                    connection.Open();

                    string query = $"SELECT b1.ID, b1.C_DESCRIPTION, b1.C_CREATIONDATE, b1.C_MODIFICATIONDATE, b1.C_CREATORID, b2.C_NAME AS CREATORNAME , " +
                                   $"b1.C_LASTMODIFIERID, b3.C_NAME AS LASTMODIFIERNAME, b1.C_NAME , b1.C_CLASSID, {tableName}.C_BODY " +
                                   $"FROM {tableName} " +
                                   $"INNER JOIN T_BUSINESSOBJECT b1 ON {tableName}.ID = b1.ID " +
                                   $"Left JOIN T_BUSINESSOBJECT b2 ON b1.C_CREATORID = b2.ID " +
                                   $"Left JOIN T_BUSINESSOBJECT b3 ON b1.C_LASTMODIFIERID = b3.ID " +
                                   $"WHERE " + conditionsString;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsFile file = new clsFile();

                                file.Id = new Guid(reader[0].ToString());

                                if (!string.IsNullOrEmpty(reader[1].ToString()))
                                {
                                    file.Description = reader[1].ToString();
                                }

                                if (!string.IsNullOrEmpty(reader[2].ToString()))
                                {
                                    file.CreationDate = DateTime.Parse(reader[2].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader[3].ToString()))
                                {
                                    file.ModificationDate = DateTime.Parse(reader[3].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader[4].ToString()))
                                {
                                    file.CreatorId = new Guid(reader[4].ToString());
                                }


                                if (!string.IsNullOrEmpty(reader[5].ToString()))
                                {
                                    file.CreatorName = reader[5].ToString();
                                }

                                if (!string.IsNullOrEmpty(reader[6].ToString()))
                                {
                                    file.LastModifierId = new Guid(reader[6].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader[7].ToString()))
                                {
                                    file.LastModifierName = reader[7].ToString();
                                }

                                if (!string.IsNullOrEmpty(reader[8].ToString()))
                                {
                                    file.Name = reader[8].ToString();
                                }

                                int c = (int)reader[9];
                                file.Class = (Type)c;

                                file.Body = reader[10].ToString();

                                allFiles.Add(file);
                            }
                        }
                    }
                }
            }

            else
            {
                var shouldQueries = new Action<QueryDescriptor<clsFile>>[QClasses.Length];

                for (int i = 0; i < shouldQueries.Length; i++)
                {
                    int capturedIndex = i; // Capture the current value of i
                    shouldQueries[i] = bs => bs.Term(p => p.Class, QClasses[capturedIndex].ToString().ToLower());
                }

                var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
                var client = new ElasticsearchClient(settings);

                var response = await client.SearchAsync<clsFile>(s => s
                            .Index(EditBeforeRun.ElasticFilesIndex)
                            .From(0)
                            .Size(10000)
                            .Query(q => q.Bool(b => b
                            .Should(shouldQueries))));

                if (response.IsValidResponse)
                {
                    var files = response.Documents;
                    foreach (var file in files)
                    {
                        allFiles.Add(file);
                    }
                }

                if (!response.IsValidResponse)
                {
                    throw new Exception("Error while working with Elastic");
                }
            }

            return allFiles;
        }



    }
}
