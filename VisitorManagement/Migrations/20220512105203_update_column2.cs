using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.Migrations
{
    public partial class update_column2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisittorRegister_Visitor_VisitorId",
                table: "VisittorRegister");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisittorRegister",
                table: "VisittorRegister");

            migrationBuilder.RenameTable(
                name: "VisittorRegister",
                newName: "VisitorRegister");

            migrationBuilder.RenameIndex(
                name: "IX_VisittorRegister_VisitorId",
                table: "VisitorRegister",
                newName: "IX_VisitorRegister_VisitorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitorRegister",
                table: "VisitorRegister",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorRegister_Visitor_VisitorId",
                table: "VisitorRegister",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitorRegister_Visitor_VisitorId",
                table: "VisitorRegister");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitorRegister",
                table: "VisitorRegister");

            migrationBuilder.RenameTable(
                name: "VisitorRegister",
                newName: "VisittorRegister");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorRegister_VisitorId",
                table: "VisittorRegister",
                newName: "IX_VisittorRegister_VisitorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisittorRegister",
                table: "VisittorRegister",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisittorRegister_Visitor_VisitorId",
                table: "VisittorRegister",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
