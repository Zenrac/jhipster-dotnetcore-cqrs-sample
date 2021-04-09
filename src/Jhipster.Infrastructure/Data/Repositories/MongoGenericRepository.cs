using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jhipster.Domain.Repositories.Interfaces;
using Jhipster.Infrastructure.Data.Extensions;
using Jhipster.Infrastructure.Data;
using System.Linq.Expressions;
using JHipsterNet.Core.Pagination;

namespace Jhipster.Infrastructure.Data.Repositories
{
    public abstract class MongoGenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class
    {
        protected readonly IMongoDatabaseContext _mongoContext;
        protected IMongoCollection<TEntity> _dbCollection;

            protected MongoGenericRepository(IMongoDatabaseContext context)
            {
                _mongoContext = context;
                _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
            }

            public virtual async Task<TEntity> GetOneAsync(object id)
            {
                var objectId = new ObjectId(id.ToString());
                FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);
                return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
            }

            public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
            {
                var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
                return await all.ToListAsync();
            }

            public virtual async Task<TEntity> CreateOrUpdateAsync(TEntity obj)
            {
                if (obj == null) throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
                await _dbCollection.InsertOneAsync(obj);
                return obj;
            }

            public virtual async Task UpdateAsync(string id, TEntity obj)
            {
                await _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), obj);
            }
            public virtual TEntity Update(TEntity obj)
            {
                // Use TKey... _dbCollection.ReplaceOne(Builders<TEntity>.Filter.Eq("_id", id), obj);
                return obj;
            }

            public virtual async Task DeleteByIdAsync(object id)
            {
                var objectId = new ObjectId(id.ToString());
                await _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));
            }

            public virtual async Task DeleteAsync(TEntity obj)
            {
                if (obj == null) throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
                // Use TKey... await _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.Id));
            }

            public virtual TEntity Add(TEntity entity)
            {
                _dbCollection.InsertOne(entity);
                return entity;
            }

            public virtual bool AddRange(params TEntity[] entities)
            {
                _dbCollection.InsertMany(entities);
                return true;
            }

            public virtual bool UpdateRange(params TEntity[] entities)
            {
                foreach (TEntity entity in entities)
                {
                    this.Update(entity);
                }
                return true;
            }

            public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
            {
                var result = await _dbCollection.FindAsync(predicate);
                return result.Any();
            }

            public virtual Task<int> CountAsync()
            {
                return Task.FromResult(Convert.ToInt32(_dbCollection.CountDocuments(Builders<TEntity>.Filter.Empty)));
            }

            public virtual async Task Clear()
            {
                await _dbCollection.DeleteManyAsync(Builders<TEntity>.Filter.Empty);
            }

            public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                return Task.FromResult(0);
            }

            public void Dispose()
            {
                this._mongoContext.Dispose();
            }

            public TEntity Attach(TEntity entity) => throw new NotImplementedException();
            public Task<IPage<TEntity>> GetPageAsync(IPageable pageable) => throw new NotImplementedException();
            public IFluentRepository<TEntity> QueryHelper() => throw new NotImplementedException();
    }
}
