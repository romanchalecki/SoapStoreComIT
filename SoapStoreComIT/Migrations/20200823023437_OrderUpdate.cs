using Microsoft.EntityFrameworkCore.Migrations;

namespace SoapStoreComIT.Migrations
{
    public partial class OrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Discriminator",
            //    table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Order",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Order");

            //migrationBuilder.AddColumn<string>(
            //    name: "Discriminator",
            //    table: "AspNetUsers",
            //    nullable: false,
            //    defaultValue: "");
        }
    }
}
