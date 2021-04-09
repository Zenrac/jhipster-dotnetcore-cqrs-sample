using MongoDB.Driver;

namespace Jhipster.Infrastructure.Data
{
    public interface IMongoDatabaseContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
        void Dispose();
    }
}
