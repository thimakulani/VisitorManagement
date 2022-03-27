using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Visitor");

            migrationBuilder.AddColumn<string>(
                name: "AppointmentWith",
                table: "VisittorRegister",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "VisittorRegister",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentWith",
                table: "VisittorRegister");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "VisittorRegister");

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Visitor",
                type: "float",
                nullable: true);
        }
    }
}
