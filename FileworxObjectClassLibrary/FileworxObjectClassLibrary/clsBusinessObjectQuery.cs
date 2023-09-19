using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FileworxObjectClassLibrary
{
    public enum QuerySource {DB, ES}
    public class clsBusinessObjectQuery
    {
        // Constants
        static string tableName = "T_BUSINESSOBJECT";

        // Properties
        public Type[] QClasses { get; set; } = { Type.User, Type.News, Type.Photo, Type.Contact };
        public QuerySource Source { get; set; }

        public async Task<List<clsBusinessObject>> RunAsync()
        {
            List<clsBusinessObject> allBusinessObjects = new List<clsBusinessObject>();


            // DB
            if(Source == QuerySource.DB)
            {
                string[] conditions = new string[QClasses.Length];
                for(int i = 0; i< QClasses.Length; i++)
                {
                    conditions[i]= $"b1.C_CLASSID = {(int)QClasses[i]} OR ";
                    if (i == (QClasses.Length - 1)) conditions[i]= conditions[i].Replace("OR", "");
                }

                string conditionsString = string.Join(" ", conditions);

                using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
                {
                    connection.Open();

                    string query = $"SELECT b1.ID, b1.C_DESCRIPTION, b1.C_CREATIONDATE, b1.C_MODIFICATIONDATE, b1.C_CREATORID, b2.C_NAME AS CREATORNAME , " +
                                   $"b1.C_LASTMODIFIERID, b3.C_NAME AS LASTMODIFIERNAME, b1.C_NAME , b1.C_CLASSID " +
                                   $"FROM {tableName} b1 " +
                                   $"Left JOIN {tableName} b2 ON b1.C_CREATORID = b2.ID " +
                                   $"Left JOIN {tableName} b3 ON b1.C_LASTMODIFIERID = b3.ID " +
                                   $"WHERE " + conditionsString;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsBusinessObject businessObject = new clsBusinessObject();

                                businessObject.Id = new Guid(reader[0].ToString());

                                if (!String.IsNullOrEmpty(reader[1].ToString()))
                                {
                                    businessObject.Description = (reader[1].ToString());
                                }

                                if (!String.IsNullOrEmpty(reader[2].ToString()))
                                {
                                    businessObject.CreationDate = DateTime.Parse(reader[2].ToString());
                                }

                                if (!String.IsNullOrEmpty(reader[3].ToString()))
                                {
                                    businessObject.ModificationDate = DateTime.Parse(reader[3].ToString());
                                }

                                if (!String.IsNullOrEmpty(reader[4].ToString()))
                                {
                                    businessObject.CreatorId = new Guid(reader[4].ToString());
                                }

                                if (!String.IsNullOrEmpty(reader[5].ToString()))
                                {
                                    businessObject.CreatorName = reader[5].ToString();
                                }

                                if (!String.IsNullOrEmpty(reader[6].ToString()))
                                {
                                    businessObject.LastModifierId = new Guid(reader[6].ToString());
                                }

                                if (!String.IsNullOrEmpty(reader[7].ToString()))
                                {
                                    businessObject.LastModifierName = reader[7].ToString();
                                }

                                if (!String.IsNullOrEmpty(reader[8].ToString()))
                                {
                                    businessObject.Name = reader[8].ToString();
                                }

                                int c = (int)(reader[9]);
                                businessObject.Class = (Type)c;

                                allBusinessObjects.Add(businessObject);
                            }
                        }
                    }
                }
            }


            // ES
            else
            {
                var shouldQueries = new Action<QueryDescriptor<clsBusinessObject>>[QClasses.Length];

                for (int i = 0; i < shouldQueries.Length; i++)
                {
                    int capturedIndex = i; // Capture the current value of i
                    shouldQueries[i] = (bs => bs.Term(p => p.Class, QClasses[capturedIndex].ToString().ToLower()));
                }

                var settings = new ElasticsearchClientSettings(new Uri(EditBeforRun.ElasticUri));
                var client = new ElasticsearchClient(settings);

                var response = await client.SearchAsync<clsBusinessObject>(s => s
                            .Index(EditBeforRun.ElasticBusinessObjectAlias)
                            .From(0)
                            .Size(10000)
                            .Query(q => q.Bool(b => b
                            .Should(shouldQueries))));

                if (response.IsValidResponse)
                {
                    var objs = response.Documents;
                    foreach (var obj in objs)
                    {
                        allBusinessObjects.Add(obj);
                    }
                }

                if (!response.IsValidResponse)
                {
                    throw new Exception("Error while working with Elastic");
                }
            }

            return allBusinessObjects;
        }


    }
}
