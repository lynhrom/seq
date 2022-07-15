using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "market_data_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "price_source_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "ticker_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "PriceSources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceSourceId = table.Column<long>(type: "bigint", nullable: false),
                    TickerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketData_PriceSources_PriceSourceId",
                        column: x => x.PriceSourceId,
                        principalTable: "PriceSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketData_Tickers_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Tickers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketData_PriceSourceId",
                table: "MarketData",
                column: "PriceSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketData_TickerId",
                table: "MarketData",
                column: "TickerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketData");

            migrationBuilder.DropTable(
                name: "PriceSources");

            migrationBuilder.DropTable(
                name: "Tickers");

            migrationBuilder.DropSequence(
                name: "market_data_hilo");

            migrationBuilder.DropSequence(
                name: "price_source_hilo");

            migrationBuilder.DropSequence(
                name: "ticker_hilo");
        }
    }
}
