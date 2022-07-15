using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class PriceSourceConfiguration : IEntityTypeConfiguration<PriceSource>
    {
        public void Configure(EntityTypeBuilder<PriceSource> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .UseHiLo("price_source_hilo")
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
