using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bowlingApp.Migrations
{
    /// <inheritdoc />
    public partial class betterlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FrameId",
                table: "AppLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "AppLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputData",
                table: "AppLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrameId",
                table: "AppLogs");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "AppLogs");

            migrationBuilder.DropColumn(
                name: "InputData",
                table: "AppLogs");
        }
    }
}
