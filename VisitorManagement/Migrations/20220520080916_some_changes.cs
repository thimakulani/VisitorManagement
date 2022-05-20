using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.Migrations
{
    public partial class some_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthCheckId",
                table: "EmployeeRegister",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeathCheckId",
                table: "EmployeeRegister",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegister_HealthCheckId",
                table: "EmployeeRegister",
                column: "HealthCheckId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRegister_HealthCheck_HealthCheckId",
                table: "EmployeeRegister",
                column: "HealthCheckId",
                principalTable: "HealthCheck",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRegister_HealthCheck_HealthCheckId",
                table: "EmployeeRegister");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeRegister_HealthCheckId",
                table: "EmployeeRegister");

            migrationBuilder.DropColumn(
                name: "HealthCheckId",
                table: "EmployeeRegister");

            migrationBuilder.DropColumn(
                name: "HeathCheckId",
                table: "EmployeeRegister");
        }
    }
}
