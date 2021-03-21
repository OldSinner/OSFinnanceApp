using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FinnanceApp.Server.Migrations
{
    public partial class lastlogeduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "lastLogged",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "MontlyBills",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastLogged",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "description",
                table: "MontlyBills");
        }
    }
}
