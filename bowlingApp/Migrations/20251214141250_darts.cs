using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bowlingApp.Migrations
{
    /// <inheritdoc />
    public partial class darts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DartsGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DartsGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DartsFrames",
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
                    table.PrimaryKey("PK_DartsFrames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DartsFrames_DartsGames_GameId",
                        column: x => x.GameId,
                        principalTable: "DartsGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DartsFrames_GameId",
                table: "DartsFrames",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DartsFrames");

            migrationBuilder.DropTable(
                name: "DartsGames");
        }
    }
}
