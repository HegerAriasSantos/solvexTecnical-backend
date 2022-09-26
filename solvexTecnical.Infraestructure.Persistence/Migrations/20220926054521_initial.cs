using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace solvexTecnical.Infraestructure.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuperMarket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperMarket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Cedula = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SuperMarketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_SuperMarket_SuperMarketId",
                        column: x => x.SuperMarketId,
                        principalTable: "SuperMarket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsBrand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SuperMarketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsBrand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsBrand_SuperMarket_SuperMarketId",
                        column: x => x.SuperMarketId,
                        principalTable: "SuperMarket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SuperMarketId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingList_SuperMarket_SuperMarketId",
                        column: x => x.SuperMarketId,
                        principalTable: "SuperMarket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingList_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FinalProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalProducts_ProductsBrand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "ProductsBrand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    ShoppingListId = table.Column<int>(nullable: false),
                    FinalProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListProducts_FinalProducts_FinalProductId",
                        column: x => x.FinalProductId,
                        principalTable: "FinalProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShoppingListProducts_ShoppingList_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinalProducts_BrandId",
                table: "FinalProducts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalProducts_ProductId",
                table: "FinalProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SuperMarketId",
                table: "Product",
                column: "SuperMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsBrand_SuperMarketId",
                table: "ProductsBrand",
                column: "SuperMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingList_SuperMarketId",
                table: "ShoppingList",
                column: "SuperMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingList_UserId",
                table: "ShoppingList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListProducts_FinalProductId",
                table: "ShoppingListProducts",
                column: "FinalProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListProducts_ShoppingListId",
                table: "ShoppingListProducts",
                column: "ShoppingListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingListProducts");

            migrationBuilder.DropTable(
                name: "FinalProducts");

            migrationBuilder.DropTable(
                name: "ShoppingList");

            migrationBuilder.DropTable(
                name: "ProductsBrand");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "SuperMarket");
        }
    }
}
