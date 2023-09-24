using FileworxDTOsLibrary.DTOs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxDTOsLibrary.RabbitMQMessages
{
    public class clsMessage
    {
        // Properties
        [BsonId]
        public Guid Id { get; set; }
        public string Command { get; set; }
        public clsNewsDto NewsDto { get; set; }
        public clsPhotoDto PhotoDto { get; set; }
        public bool Processed { get; set; } = false;
        public clsContactDto Contact { get; set; }
        public DateTime ActionDate { get; set; }


        private IMongoDatabase db;
        private MongoClient client;
        private IMongoCollection<clsMessage> collection;

        public clsMessage()
        {
            client = new MongoClient();
            db = client.GetDatabase(EditBeforeRun.MessagesDBName);
            collection = db.GetCollection<clsMessage>(EditBeforeRun.MessagesCollectionName);
        }

        public async Task InsertAsync()
        {
            await collection.InsertOneAsync(this);
        }

        public void Read()
        {
            var filter = Builders<clsMessage>.Filter.Eq("Id",Id);

            var foundMessage = collection.Find(filter).First();

            this.Command = foundMessage.Command;
            this.NewsDto = foundMessage.NewsDto;
            this.PhotoDto = foundMessage.PhotoDto;
            this.Processed = foundMessage.Processed;
        }

        public async Task UpdateAsync()
        {
            var filter = Builders<clsMessage>.Filter.Eq("Id", Id);


            var result = await collection.ReplaceOneAsync(new BsonDocument ("_id",Id),
                                                            this,
                                                            new ReplaceOptions { IsUpsert = false });
        }

        public async Task DeleteAsync()
        {
            var filter = Builders<clsMessage>.Filter.Eq("Id", Id);

            var result = await collection.DeleteOneAsync(filter);
        }
    }
}
