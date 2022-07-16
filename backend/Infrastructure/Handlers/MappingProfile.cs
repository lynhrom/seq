using AutoMapper;
using Domain.Entities;
using Infrastructure.Handlers.Markets;
using Infrastructure.Handlers.Sources;
using Infrastructure.Handlers.Tickers;

namespace Infrastructure.Handlers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticker, TickerDto>();
            CreateMap<PriceSource, PriceSourceDto>();
            CreateMap<MarketData, PriceDto>();

        }
    }
}
