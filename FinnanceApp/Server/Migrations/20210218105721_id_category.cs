using Microsoft.EntityFrameworkCore.Migrations;

namespace FinnanceApp.Server.Migrations
{
    public partial class id_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MontlyBills_Category_categoryid",
                table: "MontlyBills");

            migrationBuilder.RenameColumn(
                name: "categoryid",
                table: "MontlyBills",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MontlyBills_categoryid",
                table: "MontlyBills",
                newName: "IX_MontlyBills_categoryId");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "MontlyBills",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "categoryId",
                table: "MontlyBills",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MontlyBills_Category_categoryId",
                table: "MontlyBills",
                column: "categoryId",
                principalTable: "Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MontlyBills_Category_categoryId",
                table: "MontlyBills");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "MontlyBills",
                newName: "categoryid");

            migrationBuilder.RenameIndex(
                name: "IX_MontlyBills_categoryId",
                table: "MontlyBills",
                newName: "IX_MontlyBills_categoryid");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "MontlyBills",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "categoryid",
                table: "MontlyBills",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_MontlyBills_Category_categoryid",
                table: "MontlyBills",
                column: "categoryid",
                principalTable: "Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
