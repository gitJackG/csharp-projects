using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedemailtojoke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JokeAuthor",
                table: "Joke",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JokeAuthor",
                table: "Joke");
        }
    }
}
