using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bowlingApp.Migrations
{
    /// <inheritdoc />
    public partial class generica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.CreateTable(
                name: "BowlingGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BowlingGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BowlingFrames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    FrameIndex = table.Column<int>(type: "int", nullable: false),
                    Roll1 = table.Column<int>(type: "int", nullable: false),
                    Roll2 = table.Column<int>(type: "int", nullable: true),
                    Roll3 = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BowlingFrames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BowlingFrames_BowlingGames_GameId",
                        column: x => x.GameId,
                        principalTable: "BowlingGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BowlingFrames_GameId",
                table: "BowlingFrames",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BowlingFrames");

            migrationBuilder.DropTable(
                name: "BowlingGames");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrameIndex = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Roll1 = table.Column<int>(type: "int", nullable: false),
                    Roll2 = table.Column<int>(type: "int", nullable: true),
                    Roll3 = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frames_GameId",
                table: "Frames",
                column: "GameId");
        }
    }
}
