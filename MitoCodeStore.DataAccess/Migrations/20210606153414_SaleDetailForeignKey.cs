using Microsoft.EntityFrameworkCore.Migrations;

namespace MitoCodeStore.DataAccess.Migrations
{
    public partial class SaleDetailForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "SaleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SaleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_SaleId",
                table: "SaleDetail",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Sales_SaleId",
                table: "SaleDetail",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Sales_SaleId",
                table: "SaleDetail");

            migrationBuilder.DropTable(
                name: "SaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetail_SaleId",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "SaleDetail");
        }
    }
}
