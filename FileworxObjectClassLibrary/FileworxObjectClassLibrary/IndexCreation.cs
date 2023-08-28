using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxObjectClassLibrary
{
    public class IndexCreation
    {

        public static async void CreateIndex()
        {
            var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
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
    }
}
