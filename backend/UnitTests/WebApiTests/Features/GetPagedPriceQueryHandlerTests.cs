using Application.Interfaces;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using Moq;
using Infrastructure.Handlers;
using Infrastructure.Handlers.Markets;
using static Infrastructure.Handlers.Markets.GetPagedPriceQuery;

namespace UnitTests.WebApiTests.Features
{
    public class GetPagedPriceQueryHandlerTests
    {
        private readonly Mock<IReadRepository<MarketData>> _mockTickerRepository;
        private readonly IMapper _mockMapper;

        public GetPagedPriceQueryHandlerTests()
        {
            var items = new List<MarketData>
                {
                    new MarketData(DateTime.Now.AddDays(3), 0, 1, 1),
                    new MarketData(DateTime.Now.AddDays(2), 0, 1, 1),
                    new MarketData(DateTime.Now.AddDays(1), 0, 1, 1),
                    new MarketData(DateTime.Now,  0, 1, 1),
                };
            _mockTickerRepository = new Mock<IReadRepository<MarketData>>();
            _mockTickerRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<MarketData>>(), default)).ReturnsAsync(items);

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile()); //your automapperprofile 
            });
            _mockMapper = mockMapper.CreateMapper();
        }
        [Fact]
        public async Task When_Called_NotReturnNullIfConditionsNotMatch()
        {
            var request = new GetPagedPriceQuery { Request = new ListPagedPriceRequest(10,0,10,10)};
            var handler = new GetPagedPriceQueryHandler(_mockTickerRepository.Object, _mockMapper);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task When_Called_ResutAlreadyOrderByDescending()
        {
            var request = new GetPagedPriceQuery { Request = new ListPagedPriceRequest(10, 0, 1, 1) };
            var handler = new GetPagedPriceQueryHandler(_mockTickerRepository.Object, _mockMapper);
            var result = await handler.Handle(request, CancellationToken.None);
            var itemsOrdered = result.Items.OrderByDescending(x=>x.Date);
            Assert.Equal(itemsOrdered, result.Items);
            Assert.Equal(4, result.Items.Count);
        }
    }
}
