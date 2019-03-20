using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProbForm.DBContext.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayers",
                columns: table => new
                {
                    MatchDay = table.Column<int>(nullable: false),
                    MatchHomeTeamId = table.Column<string>(maxLength: 50, nullable: false),
                    MatchAwayTeamId = table.Column<string>(maxLength: 50, nullable: false),
                    PlayerNameId = table.Column<string>(maxLength: 50, nullable: false),
                    PlayerTeamId = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Info = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayers", x => new { x.MatchDay, x.MatchHomeTeamId, x.MatchAwayTeamId, x.PlayerNameId, x.PlayerTeamId });
                    table.ForeignKey(
                        name: "FK_TeamPlayers_Teams_TeamName",
                        column: x => x.TeamName,
                        principalTable: "Teams",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamPlayers_Players_PlayerNameId_PlayerTeamId",
                        columns: x => new { x.PlayerNameId, x.PlayerTeamId },
                        principalTable: "Players",
                        principalColumns: new[] { "Name", "TeamId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPlayers_Matches_MatchDay_MatchHomeTeamId_MatchAwayTeamId",
                        columns: x => new { x.MatchDay, x.MatchHomeTeamId, x.MatchAwayTeamId },
                        principalTable: "Matches",
                        principalColumns: new[] { "Day", "HomeTeamId", "AwayTeamId" },
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_TeamName",
                table: "TeamPlayers",
                column: "TeamName");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_PlayerNameId_PlayerTeamId",
                table: "TeamPlayers",
                columns: new[] { "PlayerNameId", "PlayerTeamId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamPlayers");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
