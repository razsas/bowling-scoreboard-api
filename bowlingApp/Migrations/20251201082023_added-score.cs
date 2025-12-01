using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bowlingApp.Migrations
{
    /// <inheritdoc />
    public partial class addedscore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Frames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Frames");
        }
    }
}
