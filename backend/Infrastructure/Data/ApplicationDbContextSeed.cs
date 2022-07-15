using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedCatalogAsync(ApplicationDbContext context,
            ILogger logger,
            int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }

                var tickers = new List<Ticker>{
                                        new Ticker ("IBM", "IBM UN" ),
                                        new Ticker ("GOOG","GOOG" ),
                                        new Ticker ("AAPL", "AAPL" )
                                        };

                var priceSources = new List<PriceSource>{
                                        new PriceSource( "SRC1", "SRC1" ),
                                        new PriceSource ("SRC2", "SRC2" )
                                        };

                if (!await context.Tickers.AnyAsync())
                {
                    await context.Tickers.AddRangeAsync(tickers);
                    await context.SaveChangesAsync();
                }

                if (!await context.PriceSources.AnyAsync())
                {
                    await context.PriceSources.AddRangeAsync(priceSources);
                    await context.SaveChangesAsync();
                }

                if (!await context.Prices.AnyAsync())
                {
                    var currentDate = DateTime.Now;
                    var rnd = new Random();
                    int seconds = rnd.Next(5,59);
                    var currentPriceOfIBM = 137.46m;
                    var currentPriceOfGOOG = 2223.91m;
                    var currentPriceOfAAPL = 146.95m;

                    var prices = new List<MarketData>();
                    int i = 1;
                    foreach (var ticker in tickers)
                    {
                        foreach (var source in priceSources)
                        {
                            for (var j = 1; j <= 15; j++)
                            {
                                decimal rdPrice = 0;
                                var percentPriceChanges = rnd.Next(1, 10) / 100;
                                switch (ticker.Code)
                                {
                                    case "IBM":
                                        rdPrice = currentPriceOfIBM + currentPriceOfIBM * percentPriceChanges;
                                        break;
                                    case "GOOG":
                                        rdPrice = currentPriceOfGOOG + currentPriceOfGOOG * percentPriceChanges;
                                        break;
                                    case "AAPL":
                                        rdPrice = currentPriceOfAAPL + currentPriceOfAAPL * percentPriceChanges;
                                        break;
                                    default:
                                        break;
                                }

                                var rdDate = currentDate.AddSeconds(rnd.Next(5, 120));
                                prices.Add(new MarketData (rdDate, rdPrice, ticker.Id, source.Id ));

                                i++;
                            }
                        }
                    }  
                    
                    await context.Prices.AddRangeAsync(prices);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 3) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedCatalogAsync(context, logger, retryForAvailability);
                throw;
            }
        }
    }
}
