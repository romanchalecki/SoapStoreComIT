using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoapStoreComIT.Migrations
{
    public partial class StoreItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Ingridients = table.Column<string>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreItem_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StoreItem_SubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreItem_CategoryId",
                table: "StoreItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItem_SubCategoryId",
                table: "StoreItem",
                column: "SubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreItem");
        }

        public static implicit operator StoreItem(Models.StoreItem v)
        {
            throw new NotImplementedException();
        }
    }
}
