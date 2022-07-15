using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TickerConfiguration : IEntityTypeConfiguration<Ticker>
    {
        public void Configure(EntityTypeBuilder<Ticker> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .UseHiLo("ticker_hilo")
               .IsRequired();

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
