using AutoMapper;
using Domain.Entities;
using WebApi.Features.Markets;
using WebApi.Features.Sources;
using WebApi.Features.Tickers;

namespace WebApi.Features
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
