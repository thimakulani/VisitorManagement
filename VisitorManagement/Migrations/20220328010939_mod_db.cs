using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.Migrations
{
    public partial class mod_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisittorRegister_Visitor_VisitorId",
                table: "VisittorRegister");

            migrationBuilder.AlterColumn<string>(
                name: "VisitorId",
                table: "VisittorRegister",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VisittorRegister_Visitor_VisitorId",
                table: "VisittorRegister",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisittorRegister_Visitor_VisitorId",
                table: "VisittorRegister");

            migrationBuilder.AlterColumn<string>(
                name: "VisitorId",
                table: "VisittorRegister",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_VisittorRegister_Visitor_VisitorId",
                table: "VisittorRegister",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id");
        }
    }
}
