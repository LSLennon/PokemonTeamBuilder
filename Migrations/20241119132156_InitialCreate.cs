using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonTeamBuilder.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stats",
                columns: table => new
                {
                    StatsId = table.Column<string>(type: "TEXT", nullable: false),
                    HP = table.Column<int>(type: "INTEGER", nullable: false),
                    Attack = table.Column<int>(type: "INTEGER", nullable: false),
                    Defence = table.Column<int>(type: "INTEGER", nullable: false),
                    SpAttack = table.Column<int>(type: "INTEGER", nullable: false),
                    SpDefence = table.Column<int>(type: "INTEGER", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stats", x => x.StatsId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Salt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserTeams",
                columns: table => new
                {
                    UserTeamId = table.Column<string>(type: "TEXT", nullable: false),
                    UserTeamName = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeams", x => x.UserTeamId);
                    table.ForeignKey(
                        name: "FK_UserTeams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomPokemons",
                columns: table => new
                {
                    CustomPokemonId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonNickname = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomPokemonMoves = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonAbility = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonHeldItem = table.Column<string>(type: "TEXT", nullable: false),
                    BasePokemonId = table.Column<string>(type: "TEXT", nullable: false),
                    UserTeamId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonEVsId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonIVsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomPokemons", x => x.CustomPokemonId);
                    table.ForeignKey(
                        name: "FK_CustomPokemons_UserTeams_UserTeamId",
                        column: x => x.UserTeamId,
                        principalTable: "UserTeams",
                        principalColumn: "UserTeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomPokemons_stats_CustomPokemonEVsId",
                        column: x => x.CustomPokemonEVsId,
                        principalTable: "stats",
                        principalColumn: "StatsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomPokemons_stats_CustomPokemonIVsId",
                        column: x => x.CustomPokemonIVsId,
                        principalTable: "stats",
                        principalColumn: "StatsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_CustomPokemonEVsId",
                table: "CustomPokemons",
                column: "CustomPokemonEVsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_CustomPokemonIVsId",
                table: "CustomPokemons",
                column: "CustomPokemonIVsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_UserTeamId",
                table: "CustomPokemons",
                column: "UserTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_UserId",
                table: "UserTeams",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomPokemons");

            migrationBuilder.DropTable(
                name: "UserTeams");

            migrationBuilder.DropTable(
                name: "stats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
