using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.Migrations
{
    public partial class uodate_dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Last_login_date",
                table: "VisittorRegister");

            migrationBuilder.DropColumn(
                name: "Last_logout_date",
                table: "VisittorRegister");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Last_login_date",
                table: "VisittorRegister",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Last_logout_date",
                table: "VisittorRegister",
                type: "datetime2",
                nullable: true);
        }
    }
}
