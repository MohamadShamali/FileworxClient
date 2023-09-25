using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxDTOsLibrary.RabbitMQMessages
{
    public class clsMessageQuery
    {
        // MongoDB Class

        //public string[] QCommandsFilter { get; set; }
        //public bool[] QProcessedFilter { get; set; } = { false };

        //private IMongoDatabase db;
        //private MongoClient client;
        //public clsMessageQuery()
        //{
        //    client = new MongoClient();
        //    db = client.GetDatabase(EditBeforeRun.MessagesDBName);
        //}

        //public async Task<List<clsMessage>> RunAsync()
        //{
        //    var collection = db.GetCollection<clsMessage>(EditBeforeRun.MessagesCollectionName);

        //    var filter = Builders<clsMessage>.Filter.And(
        //        Builders<clsMessage>.Filter.In("Command", QCommandsFilter),
        //        Builders<clsMessage>.Filter.In("Processed", QProcessedFilter)
        //    );

        //    var cursor = await collection.FindAsync(filter);

        //    var messages = await cursor.ToListAsync();
        //    return messages;
        //}

    }
}