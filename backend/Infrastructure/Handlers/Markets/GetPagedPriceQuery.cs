using Application.Interfaces;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Markets
{
    public class GetPagedPriceQuery : IRequest<ListPagedPriceResponse>
    {
        public ListPagedPriceRequest Request { get; set; }
        public class GetPagedPriceQueryHandler : IRequestHandler<GetPagedPriceQuery, ListPagedPriceResponse>
        {
            private readonly IReadRepository<MarketData> _tickerRepository;
            private readonly IMapper _mapper;

            public GetPagedPriceQueryHandler(IReadRepository<MarketData> tickerRepository, IMapper mapper)
            {
                _tickerRepository = tickerRepository ?? throw new ArgumentNullException(nameof(tickerRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<ListPagedPriceResponse> Handle(GetPagedPriceQuery query, CancellationToken cancellationToken)
            {
                var result = new ListPagedPriceResponse(query.Request.CorrelationId());
                var filterSpec = new TickerFilterSpecification(query.Request.PriceSourceId, query.Request.TickerId);
                var pagedSpec = new TickerFilterPaginatedSpecification(query.Request.PageIndex, query.Request.PageSize, query.Request.PriceSourceId, query.Request.TickerId);
                var items = (await _tickerRepository.ListAsync(pagedSpec));
                int totalItems = await _tickerRepository.CountAsync(filterSpec);
                result.Items.AddRange(items.Select(_mapper.Map<PriceDto>));
                if (query.Request.PageSize > 0)
                {
                    result.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / query.Request.PageSize).ToString());
                }

                return result;
            }
        }
    }
}
