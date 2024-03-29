﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220716221056_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("market_data_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("price_source_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("ticker_hilo")
                .IncrementsBy(10);

            modelBuilder.Entity("Domain.Entities.MarketData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "market_data_hilo");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("PriceSourceId")
                        .HasColumnType("bigint");

                    b.Property<long>("TickerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PriceSourceId");

                    b.HasIndex("TickerId");

                    b.ToTable("MarketData", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PriceSource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "price_source_hilo");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("PriceSources");
                });

            modelBuilder.Entity("Domain.Entities.Ticker", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<long>("Id"), "ticker_hilo");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Tickers");
                });

            modelBuilder.Entity("Domain.Entities.MarketData", b =>
                {
                    b.HasOne("Domain.Entities.PriceSource", "PriceSource")
                        .WithMany()
                        .HasForeignKey("PriceSourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Ticker", "Ticker")
                        .WithMany()
                        .HasForeignKey("TickerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceSource");

                    b.Navigation("Ticker");
                });
#pragma warning restore 612, 618
        }
    }
}
