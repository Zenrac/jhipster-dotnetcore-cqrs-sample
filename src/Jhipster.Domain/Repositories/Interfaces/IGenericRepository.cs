using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using Microsoft.EntityFrameworkCore.Query;

namespace Jhipster.Domain.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> : IReadOnlyGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateOrUpdateAsync(TEntity entity);
        Task DeleteByIdAsync(object id);
        Task DeleteAsync(TEntity entity);
        Task Clear();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        TEntity Add(TEntity entity);
        bool AddRange(params TEntity[] entities);
        TEntity Attach(TEntity entity);
        TEntity Update(TEntity entity);
        bool UpdateRange(params TEntity[] entities);
    }
}
