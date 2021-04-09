using System;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Jhipster.Infrastructure.Configuration;

namespace Jhipster.Infrastructure.Data
{
    public class MongoDatabaseContext : IMongoDatabaseContext, IDisposable
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoDatabaseContext(IOptions<MongoDatabaseConfig> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.ConnectionString);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

        public long GetNextSequenceValue(string sequenceName)
        {
            var collection = _db.GetCollection<MongoSequence>("sequence");
            var filter = Builders<MongoSequence>.Filter.Eq(a => a.Name, sequenceName);
            var update = Builders<MongoSequence>.Update.Inc(a => a.Value, 1);
            var sequence = collection.FindOneAndUpdate(filter, update);
            if (sequence == null)
            {
                collection.InsertOne(new MongoSequence { Name = sequenceName, Value = 1 });
                return 1;
            }

            return sequence.Value;
        }

        public void Dispose()
        {
            this.Session.Dispose();
        }
    }
}
