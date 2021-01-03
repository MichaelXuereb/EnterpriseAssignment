using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Data.Migrations
{
    public partial class InitialMigrationCartAndCartProd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataPlaced = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartProd",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    ShoppingCartFk = table.Column<Guid>(nullable: false),
                    ProductFk = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProd", x => x.id);
                    table.ForeignKey(
                        name: "FK_CartProd_Products_ProductFk",
                        column: x => x.ProductFk,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProd_Cart_ShoppingCartFk",
                        column: x => x.ShoppingCartFk,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProd_ProductFk",
                table: "CartProd",
                column: "ProductFk");

            migrationBuilder.CreateIndex(
                name: "IX_CartProd_ShoppingCartFk",
                table: "CartProd",
                column: "ShoppingCartFk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProd");

            migrationBuilder.DropTable(
                name: "Cart");
        }
    }
}
