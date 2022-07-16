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
            CreateMap<MarketData, PriceDto>()
                    .ForMember(dto => dto.Ticker, options => options.MapFrom(src => src.Ticker.Code))
                    .ForMember(dto => dto.Source, options => options.MapFrom(src => src.PriceSource.Code));

        }
    }
}
