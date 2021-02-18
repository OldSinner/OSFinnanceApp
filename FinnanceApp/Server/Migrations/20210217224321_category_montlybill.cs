using Microsoft.EntityFrameworkCore.Migrations;

namespace FinnanceApp.Server.Migrations
{
    public partial class category_montlybill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryid",
                table: "MontlyBills",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MontlyBills_categoryid",
                table: "MontlyBills",
                column: "categoryid");

            migrationBuilder.AddForeignKey(
                name: "FK_MontlyBills_Category_categoryid",
                table: "MontlyBills",
                column: "categoryid",
                principalTable: "Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MontlyBills_Category_categoryid",
                table: "MontlyBills");

            migrationBuilder.DropIndex(
                name: "IX_MontlyBills_categoryid",
                table: "MontlyBills");

            migrationBuilder.DropColumn(
                name: "categoryid",
                table: "MontlyBills");
        }
    }
}
