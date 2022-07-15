using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Features.Tickers
{
    public class GetAllTickerQuery : IRequest<ListTickerResponse>
    {

        public class GetAllTickersQueryHandler : IRequestHandler<GetAllTickerQuery, ListTickerResponse>
        {
            private readonly IReadRepository<Ticker> _tickerRepository;
            private readonly IMapper _mapper;

            public GetAllTickersQueryHandler(IReadRepository<Ticker> tickerRepository, IMapper mapper)
            {
                _tickerRepository = tickerRepository ?? throw new ArgumentNullException(nameof(tickerRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<ListTickerResponse> Handle(GetAllTickerQuery query, CancellationToken cancellationToken)
            {
                var result = new ListTickerResponse();
                var items = await _tickerRepository.ListAsync();
                result.Items.AddRange(items.Select(_mapper.Map<TickerDto>));
                return result;
            }
        }
    }
}
