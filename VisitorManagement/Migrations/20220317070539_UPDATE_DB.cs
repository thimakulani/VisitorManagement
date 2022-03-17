using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.Migrations
{
    public partial class UPDATE_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitor",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Visitor");

            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "Visitor",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitor",
                table: "Visitor",
                column: "Identification");

            migrationBuilder.CreateTable(
                name: "VisittorRegister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Last_login = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_logout = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_login_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_logout_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suspend_status = table.Column<bool>(type: "bit", nullable: false),
                    Asset_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Asset_num = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hc_fevor = table.Column<bool>(type: "bit", nullable: false),
                    Hc_shortness_breath = table.Column<bool>(type: "bit", nullable: false),
                    Hc_sore_throat = table.Column<bool>(type: "bit", nullable: false),
                    Hc_loss_taste = table.Column<bool>(type: "bit", nullable: false),
                    Hc_cough = table.Column<bool>(type: "bit", nullable: false),
                    Hc_muscle_pain = table.Column<bool>(type: "bit", nullable: false),
                    Hc_other = table.Column<bool>(type: "bit", nullable: false),
                    Hc_visit_gethering = table.Column<bool>(type: "bit", nullable: false),
                    Hc_gethering_place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hc_gething_dates = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hc_visit_health_facility = table.Column<bool>(type: "bit", nullable: false),
                    Hc_health_facility_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hc_health_facility_dates = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitorIdentification = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisittorRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisittorRegister_Visitor_VisitorIdentification",
                        column: x => x.VisitorIdentification,
                        principalTable: "Visitor",
                        principalColumn: "Identification");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisittorRegister_VisitorIdentification",
                table: "VisittorRegister",
                column: "VisitorIdentification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisittorRegister");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitor",
                table: "Visitor");

            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "Visitor",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Visitor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitor",
                table: "Visitor",
                column: "Id");
        }
    }
}
