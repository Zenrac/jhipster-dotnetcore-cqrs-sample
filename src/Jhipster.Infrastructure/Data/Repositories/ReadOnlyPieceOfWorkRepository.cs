using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using Jhipster.Infrastructure.Data.Extensions;

namespace Jhipster.Infrastructure.Data.Repositories
{
    public class ReadOnlyPieceOfWorkRepository : ReadOnlyGenericRepository<PieceOfWork>, IReadOnlyPieceOfWorkRepository
    {
        public ReadOnlyPieceOfWorkRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
