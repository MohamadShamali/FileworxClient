﻿using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileworxDTOsLibrary;
using FileworxDTOsLibrary.DTOs;
using Type = FileworxDTOsLibrary.DTOs.Type;
using FileworxObjectClassLibrary.Models;

namespace FileworxObjectClassLibrary.Queries
{
    public class clsNewsQuery
    {
        // Constants
        static string tableName = "T_NEWS";

        // Properties
        public QuerySource Source { get; set; }

        public async Task<List<clsNews>> RunAsync()
        {
            List<clsNews> allNews = new List<clsNews>();

            if (Source == QuerySource.DB)
            {
                using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
                {
                    connection.Open();

                    string query = $"SELECT b1.ID, b1.C_DESCRIPTION, b1.C_CREATIONDATE, b1.C_MODIFICATIONDATE, b1.C_CREATORID, b2.C_NAME AS CREATORNAME , " +
                                   $"b1.C_LASTMODIFIERID, b3.C_NAME AS LASTMODIFIERNAME, b1.C_NAME , b1.C_CLASSID, f1.C_BODY, {tableName}.C_CATEGORY " +
                                   $"FROM {tableName} " +
                                   $"INNER JOIN T_BUSINESSOBJECT b1 ON {tableName}.ID = b1.ID " +
                                   $"INNER JOIN T_FILE f1 ON {tableName}.ID = f1.ID " +
                                   $"Left JOIN T_BUSINESSOBJECT b2 ON b1.C_CREATORID = b2.ID " +
                                   $"Left JOIN T_BUSINESSOBJECT b3 ON b1.C_LASTMODIFIERID = b3.ID ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsNews news = new clsNews();

                                news.Id = new Guid(reader[0].ToString());

                                if (!string.IsNullOrEmpty(reader[1].ToString()))
                                {
                                    news.Description = reader[1].ToString();
                                }

                                if (!string.IsNullOrEmpty(reader[2].ToString()))
                                {
                                    news.CreationDate = DateTime.Parse(reader[2].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader[3].ToString()))
                                {
                                    news.ModificationDate = DateTime.Parse(reader[3].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader[4].ToString()))
                                {
                                    news.CreatorId = new Guid(reader[4].ToString());
                                }


                                if (!string.IsNullOrEmpty(reader[5].ToString()))
                                {
                                    news.CreatorName = reader[5].ToString();
                                }

                                if (!string.IsNullOrEmpty(reader[6].ToString()))
                                {
                                    news.LastModifierId = new Guid(reader[6].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader[7].ToString()))
                                {
                                    news.LastModifierName = reader[7].ToString();
                                }

                                if (!string.IsNullOrEmpty(reader[8].ToString()))
                                {
                                    news.Name = reader[8].ToString();
                                }

                                int c = (int)reader[9];
                                news.Class = (Type)c;

                                news.Body = reader[10].ToString();

                                news.Category = reader[11].ToString();

                                allNews.Add(news);
                            }
                        }
                    }
                }
            }

            if (Source == QuerySource.ES)
            {
                var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
                var client = new ElasticsearchClient(settings);

                var response = await client.SearchAsync<clsNews>(s => s
                                            .Index(EditBeforeRun.ElasticFilesIndex)
                                            .From(0)
                                            .Size(10000)
                                            .Query(q => q.Term(t => t.Class, Type.News.ToString().ToLower())));

                if (response.IsValidResponse)
                {
                    var News = response.Documents;
                    foreach (var news in News)
                    {
                        allNews.Add(news);
                    }
                }
                if (!response.IsValidResponse)
                {
                    throw new Exception("Error while working with Elastic");
                }

            }
            return allNews;
        }
    }
}
