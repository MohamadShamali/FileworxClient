using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace FileworxObjectClassLibrary
{
    public class clsContactQuery
    {
        // Constants
        static string tableName = "T_CONTACT";

        // Properties
        public QuerySource Source { get; set; }
        public ContactDirection[] QDirection { get; set; } = {ContactDirection.Transmit, ContactDirection.Receive, ContactDirection.TransmitAndReceive };
        public clsContactQuery()
        {
            
        }
        public async Task<List<clsContact>> Run()
        {
            List<clsContact> allContacts = new List<clsContact>();

            if (Source == QuerySource.DB)
            {
                using (SqlConnection connection = new SqlConnection(EditBeforRun.connectionString))
                {
                    connection.Open();

                    string query = $"SELECT b1.ID, b1.C_DESCRIPTION, b1.C_CREATIONDATE, b1.C_MODIFICATIONDATE, b1.C_CREATORID, b2.C_NAME AS CREATORNAME , " +
                                   $"b1.C_LASTMODIFIERID, b3.C_NAME AS LASTMODIFIERNAME, b1.C_NAME , b1.C_CLASSID, {tableName}.C_LOCATION,  {tableName}.C_CONTACTDIRECTIONID " +
                                   $"FROM {tableName} " +
                                   $"INNER JOIN T_BUSINESSOBJECT b1 ON {tableName}.ID = b1.ID " +
                                   $"Left JOIN T_BUSINESSOBJECT b2 ON b1.C_CREATORID = b2.ID " +
                                   $"Left JOIN T_BUSINESSOBJECT b3 ON b1.C_LASTMODIFIERID = b3.ID ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsContact Contact = new clsContact();

                                Contact.Id = new Guid(reader[0].ToString());

                                if (!String.IsNullOrEmpty(reader[1].ToString()))
                                {
                                    Contact.Description = (reader[1].ToString());
                                }

                                if (!String.IsNullOrEmpty(reader[2].ToString()))
                                {
                                    Contact.CreationDate = DateTime.Parse(reader[2].ToString());
                                }

                                if (!String.IsNullOrEmpty(reader[3].ToString()))
                                {
                                    Contact.ModificationDate = DateTime.Parse(reader[3].ToString());
                                }

                                if (!String.IsNullOrEmpty(reader[4].ToString()))
                                {
                                    Contact.CreatorId = new Guid(reader[4].ToString());
                                }


                                if (!String.IsNullOrEmpty(reader[5].ToString()))
                                {
                                    Contact.CreatorName = reader[5].ToString();
                                }

                                if (!String.IsNullOrEmpty(reader[6].ToString()))
                                {
                                    Contact.LastModifierId = new Guid(reader[6].ToString());
                                }

                                if (!String.IsNullOrEmpty(reader[7].ToString()))
                                {
                                    Contact.LastModifierName = reader[7].ToString();
                                }

                                if (!String.IsNullOrEmpty(reader[8].ToString()))
                                {
                                    Contact.Name = reader[8].ToString();
                                }

                                int c = (int)(reader[9]);
                                Contact.Class = (Type)c;

                                Contact.Location = reader[10].ToString();

                                int d = (int)(reader[11]);
                                Contact.Direction = (ContactDirection)d;

                                allContacts.Add(Contact);
                            }
                        }
                    }
                }
            }

            if (Source == QuerySource.ES)
            {
                var shouldQueries = new Action<QueryDescriptor<clsContact>>[QDirection.Length];

                for (int i = 0; i < shouldQueries.Length; i++)
                {
                    int capturedIndex = i; // Capture the current value of i
                    shouldQueries[i] = (bs => bs.Term(p => p.DirectionID, (int)QDirection[capturedIndex]));
                }

                var settings = new ElasticsearchClientSettings(new Uri(EditBeforRun.ElasticUri));
                var client = new ElasticsearchClient(settings);

                var response = await client.SearchAsync<clsContact>(s => s
                                            .Index(EditBeforRun.ElasticContactsIndex)
                                            .From(0)
                                            .Size(10000)
                                            .Query(q => q.Bool( b=> b.
                                             Should(shouldQueries))));

                if (response.IsValidResponse)
                {
                    var contacts = response.Documents;
                    foreach (var contact in contacts)
                    {
                        allContacts.Add(contact);
                    }
                }
                if (!response.IsValidResponse)
                {
                    throw new Exception("Error while working with Elastic");
                }

            }
            return allContacts;
        }
    }
}
