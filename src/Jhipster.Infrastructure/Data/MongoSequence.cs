using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Jhipster.Infrastructure.Data
{
    class MongoSequence
    {
        [BsonId]
        public ObjectId _Id { get; set; }

        public string Name { get; set; }

        public long Value { get; set; }
    }
}
