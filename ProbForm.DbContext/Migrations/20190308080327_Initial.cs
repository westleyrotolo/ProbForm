using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProbForm.DBContext.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    TeamId = table.Column<string>(maxLength: 50, nullable: false),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => new { x.Name, x.TeamId });
                    table.UniqueConstraint("AK_Players_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Mister = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Day = table.Column<int>(nullable: false),
                    HomeTeamId = table.Column<string>(maxLength: 50, nullable: false),
                    AwayTeamId = table.Column<string>(maxLength: 50, nullable: false),
                    HomeModule = table.Column<string>(nullable: true),
                    AwayModule = table.Column<string>(nullable: true),
                    MatchTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => new { x.Day, x.HomeTeamId, x.AwayTeamId });
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayers",
                columns: table => new
                {
                    TeamPlayerId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    playerName = table.Column<string>(nullable: true),
                    playerTeamId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Info = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayers", x => x.TeamPlayerId);
                    table.ForeignKey(
                        name: "FK_TeamPlayers_Teams_TeamName",
                        column: x => x.TeamName,
                        principalTable: "Teams",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamPlayers_Players_playerName_playerTeamId",
                        columns: x => new { x.playerName, x.playerTeamId },
                        principalTable: "Players",
                        principalColumns: new[] { "Name", "TeamId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_TeamName",
                table: "TeamPlayers",
                column: "TeamName");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_playerName_playerTeamId",
                table: "TeamPlayers",
                columns: new[] { "playerName", "playerTeamId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "TeamPlayers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
