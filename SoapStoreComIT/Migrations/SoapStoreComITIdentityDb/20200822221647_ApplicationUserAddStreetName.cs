using Microsoft.EntityFrameworkCore.Migrations;

namespace SoapStoreComIT.Migrations.SoapStoreComITIdentityDb
{
    public partial class ApplicationUserAddStreetName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StreetAdress",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetAdress",
                table: "AspNetUsers");
        }
    }
}
