using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.BackgroundJobs
{
    public class JobSchedulerService : IJobSchedulerService
    {
        private readonly IRepository<Ticker> _tickerRepository;
        private readonly IRepository<PriceSource> _priceSourceRepository;
        private readonly IRepository<MarketData> _marketDataRepository;
        private readonly ILogger<JobSchedulerService> _logger;
        private readonly IHubContext<NotificationHub> _hubcontext;

        public JobSchedulerService(IRepository<Ticker> tickerRepository, IRepository<PriceSource> priceSourceRepository, IRepository<MarketData> marketDataRepository, ILogger<JobSchedulerService> logger, IHubContext<NotificationHub> hubcontext)
        {
            _tickerRepository = tickerRepository ?? throw new ArgumentNullException(nameof(tickerRepository));
            _priceSourceRepository = priceSourceRepository ?? throw new ArgumentNullException(nameof(priceSourceRepository));
            _marketDataRepository = marketDataRepository ?? throw new ArgumentNullException(nameof(marketDataRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubcontext = hubcontext ?? throw new ArgumentNullException(nameof(hubcontext));
        }

        public async Task SyncData()
        {
            _logger.LogInformation("Start Sync Data at " + DateTime.Now);
            try
            {
                var sources = await _priceSourceRepository.ListAsync();
                var tickers = await _tickerRepository.ListAsync();

                var rnd = new Random();
                var dictSimulator = new Dictionary<string, decimal>
                {
                    {"IBM",  137.46m},
                    {"GOOG",  2223.91m},
                    {"AAPL",  146.95m},
                };
                foreach (var source in sources)
                {
                    foreach (var ticker in tickers)
                    {
                        var percentPriceChanges = rnd.Next(1, 10);
                        var latestPrice = dictSimulator[ticker.Code];
                        var rdPrice = latestPrice + latestPrice * percentPriceChanges / 100;
                        var entity = new MarketData(DateTime.Now, rdPrice, ticker.Id, source.Id);
                        await _marketDataRepository.AddAsync(entity);
                    }
                }
                
                await _marketDataRepository.SaveChangesAsync();
                _logger.LogInformation("End Sync Data at " + DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error ==> " + ex.Message);
            }
            
            await Task.FromResult(1);
        }
    }
}
