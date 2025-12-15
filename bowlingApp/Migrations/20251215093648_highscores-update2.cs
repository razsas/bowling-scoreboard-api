using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bowlingApp.Migrations
{
    /// <inheritdoc />
    public partial class highscoresupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DartsCounter",
                table: "DartsGames");

            migrationBuilder.DropColumn(
                name: "DartsCount",
                table: "DartsFrames");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DartsCounter",
                table: "DartsGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DartsCount",
                table: "DartsFrames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
