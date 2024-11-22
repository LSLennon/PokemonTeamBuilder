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
                name: "PokeTypes",
                columns: table => new
                {
                    PokeTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokeTypeName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeTypes", x => x.PokeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "stats",
                columns: table => new
                {
                    AppStatsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HP = table.Column<int>(type: "INTEGER", nullable: false),
                    Attack = table.Column<int>(type: "INTEGER", nullable: false),
                    Defence = table.Column<int>(type: "INTEGER", nullable: false),
                    SpAttack = table.Column<int>(type: "INTEGER", nullable: false),
                    SpDefence = table.Column<int>(type: "INTEGER", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stats", x => x.AppStatsId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Salt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "EffectivenessTypes",
                columns: table => new
                {
                    TypeEffectivnessId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DamageCalculation = table.Column<double>(type: "REAL", nullable: false),
                    AttackTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DefenceTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectivenessTypes", x => x.TypeEffectivnessId);
                    table.ForeignKey(
                        name: "FK_EffectivenessTypes_PokeTypes_AttackTypeId",
                        column: x => x.AttackTypeId,
                        principalTable: "PokeTypes",
                        principalColumn: "PokeTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectivenessTypes_PokeTypes_DefenceTypeId",
                        column: x => x.DefenceTypeId,
                        principalTable: "PokeTypes",
                        principalColumn: "PokeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokedexPokemons",
                columns: table => new
                {
                    PokedexPokemonId = table.Column<string>(type: "TEXT", nullable: false),
                    PokemonName = table.Column<string>(type: "TEXT", nullable: false),
                    DefenceType1PokeTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DefenceType2PokeTypeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokedexPokemons", x => x.PokedexPokemonId);
                    table.ForeignKey(
                        name: "FK_PokedexPokemons_PokeTypes_DefenceType1PokeTypeId",
                        column: x => x.DefenceType1PokeTypeId,
                        principalTable: "PokeTypes",
                        principalColumn: "PokeTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokedexPokemons_PokeTypes_DefenceType2PokeTypeId",
                        column: x => x.DefenceType2PokeTypeId,
                        principalTable: "PokeTypes",
                        principalColumn: "PokeTypeId");
                });

            migrationBuilder.CreateTable(
                name: "UserTeams",
                columns: table => new
                {
                    UserTeamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserTeamName = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    CustomPokemonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomPokemonNickname = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomPokemonMoves = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonAbility = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonHeldItem = table.Column<string>(type: "TEXT", nullable: false),
                    BasePokemonId = table.Column<string>(type: "TEXT", nullable: false),
                    UserTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomPokemonEVsId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomPokemonIVsId = table.Column<int>(type: "INTEGER", nullable: false)
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
                        principalColumn: "AppStatsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomPokemons_stats_CustomPokemonIVsId",
                        column: x => x.CustomPokemonIVsId,
                        principalTable: "stats",
                        principalColumn: "AppStatsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_CustomPokemonEVsId",
                table: "CustomPokemons",
                column: "CustomPokemonEVsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_CustomPokemonIVsId",
                table: "CustomPokemons",
                column: "CustomPokemonIVsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_UserTeamId",
                table: "CustomPokemons",
                column: "UserTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectivenessTypes_AttackTypeId",
                table: "EffectivenessTypes",
                column: "AttackTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectivenessTypes_DefenceTypeId",
                table: "EffectivenessTypes",
                column: "DefenceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokedexPokemons_DefenceType1PokeTypeId",
                table: "PokedexPokemons",
                column: "DefenceType1PokeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokedexPokemons_DefenceType2PokeTypeId",
                table: "PokedexPokemons",
                column: "DefenceType2PokeTypeId");

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
                name: "EffectivenessTypes");

            migrationBuilder.DropTable(
                name: "PokedexPokemons");

            migrationBuilder.DropTable(
                name: "UserTeams");

            migrationBuilder.DropTable(
                name: "stats");

            migrationBuilder.DropTable(
                name: "PokeTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
