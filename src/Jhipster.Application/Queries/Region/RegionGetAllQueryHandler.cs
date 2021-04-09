
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Dto;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace Jhipster.Application.Queries
{
    public class RegionGetAllQueryHandler : IRequestHandler<RegionGetAllQuery, Page<RegionDto>>
    {
        private IReadOnlyRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionGetAllQueryHandler(
            IMapper mapper,
            IReadOnlyRegionRepository regionRepository)
        {
            _mapper = mapper;
            _regionRepository = regionRepository;
        }

        public async Task<Page<RegionDto>> Handle(RegionGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _regionRepository.QueryHelper()
                .GetPageAsync(request.page);
            return new Page<RegionDto>(page.Content.Select(entity => _mapper.Map<RegionDto>(entity)).ToList(), request.page, page.TotalElements);
        }
    }
}
