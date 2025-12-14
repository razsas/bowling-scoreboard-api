using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bowlingApp.Migrations
{
    /// <inheritdoc />
    public partial class anotherlogimprovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppLogs_FrameId",
                table: "AppLogs",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLogs_GameId",
                table: "AppLogs",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLogs_BowlingFrames_FrameId",
                table: "AppLogs",
                column: "FrameId",
                principalTable: "BowlingFrames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppLogs_BowlingGames_GameId",
                table: "AppLogs",
                column: "GameId",
                principalTable: "BowlingGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppLogs_BowlingFrames_FrameId",
                table: "AppLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppLogs_BowlingGames_GameId",
                table: "AppLogs");

            migrationBuilder.DropIndex(
                name: "IX_AppLogs_FrameId",
                table: "AppLogs");

            migrationBuilder.DropIndex(
                name: "IX_AppLogs_GameId",
                table: "AppLogs");
        }
    }
}
