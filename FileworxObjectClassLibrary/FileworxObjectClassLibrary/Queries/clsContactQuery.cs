﻿using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.Fluent;
using System.Security.Claims;
using FileworxDTOsLibrary;
using FileworxDTOsLibrary.DTOs;
using Type = FileworxDTOsLibrary.DTOs.Type;
using FileworxObjectClassLibrary.Models;
using FileworxObjectClassLibrary.Others;

namespace FileworxObjectClassLibrary.Queries
{
    public class clsContactQuery
    {
        // Constants
        static string tableName = "T_CONTACT";

        // Properties
        public QuerySource Source { get; set; }
        public ContactDirection[] QDirection { get; set; } = { ContactDirection.Transmit, ContactDirection.Receive, ContactDirection.Transmit | ContactDirection.Receive };

        public async Task<List<clsContact>> RunAsync()
        {
            List<clsContact> allContacts = new List<clsContact>();

            if (Source == QuerySource.DB)
            {
                string[] conditions = new string[QDirection.Length];
                for (int i = 0; i < QDirection.Length; i++)
                {
                    conditions[i] = $"T_CONTACT.C_CONTACTDIRECTIONID = {(int)QDirection[i]} OR ";
                    if (i == QDirection.Length - 1) conditions[i] = conditions[i].Replace("OR", "");
                }

                string conditionsString = string.Join(" ", conditions);
                using (SqlConnection connection = new SqlConnection(EditBeforeRun.connectionString))
                {
                    connection.Open();

                    string query = $"SELECT b1.ID, b1.C_DESCRIPTION, b1.C_CREATIONDATE, b1.C_MODIFICATIONDATE, b1.C_CREATORID, b2.C_NAME AS CREATORNAME , " +
                                   $"b1.C_LASTMODIFIERID, b3.C_NAME AS LASTMODIFIERNAME, b1.C_NAME , b1.C_CLASSID, {tableName}.C_TRANSMITLOCATION , {tableName}.C_RECEIVELOCATION,  {tableName}.C_CONTACTDIRECTIONID ,{tableName}.C_LASTRECEIVEDATE ,{tableName}.C_ENABLED " +
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
                                clsContact contact = new clsContact();

                                contact.Id = new Guid(reader[0].ToString());

                                if (!string.IsNullOrEmpty(reader[1].ToString()))
                                {
                                    contact.Description = reader[1].ToString();
                                }

                                if (!string.IsNullOrEmpty(reader[2].ToString()))
                                {
                                    contact.CreationDate = DateTime.Parse(reader[2].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader[3].ToString()))
                                {
                                    contact.ModificationDate = DateTime.Parse(reader[3].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader[4].ToString()))
                                {
                                    contact.CreatorId = new Guid(reader[4].ToString());
                                }


                                if (!string.IsNullOrEmpty(reader[5].ToString()))
                                {
                                    contact.CreatorName = reader[5].ToString();
                                }

                                if (!string.IsNullOrEmpty(reader[6].ToString()))
                                {
                                    contact.LastModifierId = new Guid(reader[6].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader[7].ToString()))
                                {
                                    contact.LastModifierName = reader[7].ToString();
                                }

                                if (!string.IsNullOrEmpty(reader[8].ToString()))
                                {
                                    contact.Name = reader[8].ToString();
                                }

                                int c = (int)reader[9];
                                contact.Class = (Type)c;

                                contact.TransmitLocation = reader[10].ToString();
                                contact.ReceiveLocation = reader[11].ToString();
                                int d = (int)reader[12];
                                contact.Direction = (ContactDirection)d;

                                contact.LastReceiveDate = DateTime.Parse(reader[13].ToString());

                                contact.Enabled = (bool)reader[14];

                                allContacts.Add(contact);
                            }
                        }
                    }
                }
            }

            if (Source == QuerySource.ES)
            {
                var shouldQueries = new Action<QueryDescriptor<clsContactElasticDto>>[QDirection.Length];

                for (int i = 0; i < shouldQueries.Length; i++)
                {
                    int capturedIndex = i; // Capture the current value of i
                    shouldQueries[i] = bs => bs.Term(p => p.Direction, (int)QDirection[capturedIndex]);
                }

                var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
                var client = new ElasticsearchClient(settings);

                var response = await client.SearchAsync<clsContactElasticDto>(s => s
                                            .Index(EditBeforeRun.ElasticContactsIndex)
                                            .From(0)
                                            .Size(10000)
                                            .Query(q => q.Bool(b => b.
                                             Should(shouldQueries))));

                if (response.IsValidResponse)
                {
                    var dtoContacts = response.Documents;
                    foreach (var dtoContact in dtoContacts)
                    {
                        allContacts.Add(dtoMap(dtoContact));
                    }
                }
                if (!response.IsValidResponse)
                {
                    throw new Exception("Error while working with Elastic");
                }

            }
            return allContacts;
        }

        private clsContact dtoMap(clsContactElasticDto dto)
        {
            var contact = new clsContact()
            {
                Id = dto.Id,
                Description = dto.Description,
                CreationDate = dto.CreationDate,
                ModificationDate = dto.ModificationDate,
                CreatorId = dto.CreatorId,
                CreatorName = dto.CreatorName,
                LastModifierId = dto.LastModifierId,
                LastModifierName = dto.LastModifierName,
                Name = dto.Name,
                Class = dto.Class,
                TransmitLocation = dto.TransmitLocation,
                ReceiveLocation = dto.ReceiveLocation,
                Direction = (ContactDirection)dto.Direction,
                LastReceiveDate = dto.LastReceiveDate,
                Enabled = dto.Enabled,
            };

            return contact;
        }
    }
}
