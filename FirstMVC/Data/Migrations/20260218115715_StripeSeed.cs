using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class StripeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StripeProductId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_StripeProductId",
                table: "Product",
                column: "StripeProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_StripeProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "StripeProductId",
                table: "Product");
        }
    }
}
