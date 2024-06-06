using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Spabooking");

            migrationBuilder.AddColumn<string>(
                name: "HotelPackage",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelPackage",
                table: "Invoice");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Spabooking",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
