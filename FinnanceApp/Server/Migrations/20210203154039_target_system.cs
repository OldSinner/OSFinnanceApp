using Microsoft.EntityFrameworkCore.Migrations;

namespace FinnanceApp.Server.Migrations
{
    public partial class target_system : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "targetValue",
                table: "Users",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "targetValue",
                table: "Users");
        }
    }
}
