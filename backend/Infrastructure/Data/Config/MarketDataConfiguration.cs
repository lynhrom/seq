using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class MarketDataConfiguration : IEntityTypeConfiguration<MarketData>
    {
        public void Configure(EntityTypeBuilder<MarketData> builder)
        {
            builder.ToTable("MarketData");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
               .UseHiLo("market_data_hilo")
               .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Date).IsRequired(true);

            builder.HasOne(x => x.Ticker)
                .WithMany()
                .HasForeignKey(x => x.TickerId)
                .IsRequired(true);

            builder.HasOne(x => x.PriceSource)
                .WithMany()
                .HasForeignKey(x => x.PriceSourceId)
                .IsRequired(true);

        }
    }
}
