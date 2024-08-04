using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update_receipt_item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ReceiptItems");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ReceiptItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxRate",
                table: "ReceiptItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ReceiptItems",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "ReceiptItems");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ReceiptItems");

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "ReceiptItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ReceiptItems",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
