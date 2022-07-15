using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Features;
using static WebApi.Features.GetPagedPriceQuery;

namespace IntegrationTests.ApplicationTests
{
    public class GetPagedPriceQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Repository<MarketData> _repository;
        private readonly IMapper _mockMapper;
        public GetPagedPriceQueryHandlerTests()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestUseInMemoryDatabase")
                .Options;
            _context = new ApplicationDbContext(dbOptions);
            _repository = new Repository<MarketData>(_context);
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile()); //your automapperprofile 
            });
            _mockMapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async void Query_Prices_By_Conditions_MatchMultipleItems()
        {
            //Arrange
            var tickers = new List<Ticker>{
                                        new Ticker ("IBM", "IBM UN" ),
                                        new Ticker ("GOOG","GOOG" ),
                                        new Ticker ("AAPL", "AAPL" )
                                        };
            _context.Tickers.AddRange(tickers);

            var priceSources = new List<PriceSource>{
                                        new PriceSource( "SRC1", "SRC1" ),
                                        new PriceSource ("SRC2", "SRC2" )
                                        };
            _context.PriceSources.AddRange(priceSources);
            _context.SaveChanges();

            var itemOnePrice = 2012m;
            var items = new List<MarketData>
                {
                    new MarketData(DateTime.Now,  0, 1, 1),
                    new MarketData(DateTime.Now.AddDays(1), itemOnePrice, 1, 2),
                    new MarketData(DateTime.Now.AddDays(2), 0, 1, 1),
                    new MarketData(DateTime.Now.AddDays(3), 0, 1, 2),
                    new MarketData(DateTime.Now.AddDays(4), 0, 1, 2),
                    new MarketData(DateTime.Now.AddDays(5), 0, 1, 2),
                    new MarketData(DateTime.Now.AddDays(6), 0, 1, 2),
                };
            _context.Prices.AddRange(items);
            _context.SaveChanges();
            //Act
            var request = new GetPagedPriceQuery { Request = new ListPagedPriceRequest(10, 0, 1, 1) };
            var spec = new Application.Specifications.TickerFilterSpecification(2, 1);
            var result = await _repository.ListAsync(spec);
            //Assert
            Assert.Equal(5, result.Count());
            Assert.Equal(1, result.Count(x=>x.Date.Date == DateTime.Now.AddDays(1).Date));
            Assert.Equal(1, result.Count(x=>x.Price == itemOnePrice));
        }
    }
}