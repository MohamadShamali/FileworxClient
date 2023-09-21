using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileworxDTOsLibrary;

namespace FileworxObjectClassLibrary
{
    public class IndexCreation
    {

        public static async void CreateIndex()
        {
            var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
            var client = new ElasticsearchClient(settings);

            var response = await client.Indices.CreateAsync("businessobject");

            if (response.IsValidResponse)
            {
                Console.WriteLine("index added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add index. Error: {response.ElasticsearchServerError}");
            }
        }

        public static async Task CreateUsersIndex()
        {
            var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
            var client = new ElasticsearchClient(settings);

            var response = await client.Indices.CreateAsync("users");

            if (response.IsValidResponse)
            {
                Console.WriteLine("index added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add index. Error: {response.ElasticsearchServerError}");
            }
        }

        public static async Task CreateFilesIndex()
        {
            var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
            var client = new ElasticsearchClient(settings);

            var response = await client.Indices.CreateAsync("files");

            if (response.IsValidResponse)
            {
                Console.WriteLine("index added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add index. Error: {response.ElasticsearchServerError}");
            }
        }

        public static async Task CreateContactsIndex()
        {
            var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
            var client = new ElasticsearchClient(settings);

            var response1 = await client.Indices.CreateAsync<clsContactDto>("contacts", c => c.Mappings(map => map.Properties(p => p.Text(t => t.Id)
                                                                                                                                  .Text(t => t.Description)
                                                                                                                                  .Date(d => d.CreationDate)
                                                                                                                                  .Date(d => d.ModificationDate)
                                                                                                                                  .Text(t => t.CreatorId)
                                                                                                                                  .Text(t => t.CreatorName)
                                                                                                                                  .Text(t => t.LastModifierId)
                                                                                                                                  .Text(t => t.LastModifierName)
                                                                                                                                  .Text(t => t.Name)
                                                                                                                                  .Text (t => t.Class)
                                                                                                                                  .Text(t => t.TransmitLocation)
                                                                                                                                  .Text(t => t.ReceiveLocation)
                                                                                                                                  .IntegerNumber(t => t.Direction) // Here
                                                                                                                                  .Boolean(t => t.Enabled))));

            if (response1.IsValidResponse)
            {
                Console.WriteLine("index added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add index. Error: {response1.ElasticsearchServerError}");
            }
            var response2 = await client.Indices.PutAliasAsync("contacts", "businessobjectalias");

            if (response2.IsValidResponse)
            {
                Console.WriteLine("Alias added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add alias. Error: {response1.ElasticsearchServerError} + {response2.ElasticsearchServerError}");
            }
        }

        public static async Task CreateBusinessObjectAlias()
        {
            var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
            var client = new ElasticsearchClient(settings);

            var response1 = await client.Indices.PutAliasAsync("users", "businessobjectalias");
            var response2 = await client.Indices.PutAliasAsync("files", "businessobjectalias");

            if (response1.IsValidResponse && response2.IsValidResponse)
            {
                Console.WriteLine("Alias added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add alias. Error: {response1.ElasticsearchServerError} + {response2.ElasticsearchServerError}");
            }
        }

        public static async Task CreateAdmin()
        {
            var user = new clsUser();
            user.Id = new Guid("ffd7c672-aa84-47b1-a9a3-c7875a503708");
            user.Description = "admin";
            user.Name = "admin";
            user.CreationDate = DateTime.Parse("2023-03-08 00:00:00.000");
            //user.CreatorId = null;
            //user.LastModifierId = null;
            //user.ModificationDate = null;
            user.Username = "admin";
            user.Password = "admin";
            user.IsAdmin = true;

            var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
            var client = new ElasticsearchClient(settings);
            var response = await client.IndexAsync(user, EditBeforeRun.ElasticUsersIndex);
            if (!response.IsValidResponse)
            {
                throw new Exception("Error while working with Elastic");
            }
        }

        public async static Task<string> tst()
        {
            var settings = new ElasticsearchClientSettings(new Uri(EditBeforeRun.ElasticUri));
            var client = new ElasticsearchClient(settings);

            var response = await client.SearchAsync<clsNews>(s => s
                                        .Index(EditBeforeRun.ElasticFilesIndex)
                                        .From(0)
                                        .Size(10000)
                                        .Query(q => q.Term(t => t.Class, Type.News)));

                var News = response.Documents;
                foreach (var news in News)
                {
                    return news.Name;
                }

            return "s";
            
        }
    }
}
