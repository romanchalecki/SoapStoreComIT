using Microsoft.EntityFrameworkCore.Migrations;

namespace SoapStoreComIT.Migrations.SoapStoreComITIdentityDb
{
    public partial class ApplicationUserAddColumnsFirstNameSecondName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetAdress",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StreetAdress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
