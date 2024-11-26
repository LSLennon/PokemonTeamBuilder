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
                name: "HeldItem",
                columns: table => new
                {
                    HeldItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HeldItemName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeldItem", x => x.HeldItemId);
                });

            migrationBuilder.CreateTable(
                name: "PokeAbilities",
                columns: table => new
                {
                    PokeAbilityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AbilityName = table.Column<string>(type: "TEXT", nullable: false),
                    AbilityDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeAbilities", x => x.PokeAbilityId);
                });

            migrationBuilder.CreateTable(
                name: "PokeMethods",
                columns: table => new
                {
                    PokeMethodId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeMethods", x => x.PokeMethodId);
                });

            migrationBuilder.CreateTable(
                name: "PokeStats",
                columns: table => new
                {
                    PokeStatsId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_PokeStats", x => x.PokeStatsId);
                });

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
                name: "Pokemons",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokemonName = table.Column<string>(type: "TEXT", nullable: false),
                    PokedexEntry = table.Column<string>(type: "TEXT", nullable: false),
                    Height = table.Column<double>(type: "REAL", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    BaseStatsId = table.Column<int>(type: "INTEGER", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.PokemonId);
                    table.ForeignKey(
                        name: "FK_Pokemons_PokeStats_BaseStatsId",
                        column: x => x.BaseStatsId,
                        principalTable: "PokeStats",
                        principalColumn: "PokeStatsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeEffectivenesses",
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
                    table.PrimaryKey("PK_TypeEffectivenesses", x => x.TypeEffectivnessId);
                    table.ForeignKey(
                        name: "FK_TypeEffectivenesses_PokeTypes_AttackTypeId",
                        column: x => x.AttackTypeId,
                        principalTable: "PokeTypes",
                        principalColumn: "PokeTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypeEffectivenesses_PokeTypes_DefenceTypeId",
                        column: x => x.DefenceTypeId,
                        principalTable: "PokeTypes",
                        principalColumn: "PokeTypeId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "MPokemonToAbilities",
                columns: table => new
                {
                    MPokemonToAbilitiesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    PokeAbilityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MPokemonToAbilities", x => x.MPokemonToAbilitiesId);
                    table.ForeignKey(
                        name: "FK_MPokemonToAbilities_PokeAbilities_PokeAbilityId",
                        column: x => x.PokeAbilityId,
                        principalTable: "PokeAbilities",
                        principalColumn: "PokeAbilityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MPokemonToAbilities_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MPokemonToTypes",
                columns: table => new
                {
                    MPokemonToTypesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    PokeTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MPokemonToTypes", x => x.MPokemonToTypesId);
                    table.ForeignKey(
                        name: "FK_MPokemonToTypes_PokeTypes_PokeTypeId",
                        column: x => x.PokeTypeId,
                        principalTable: "PokeTypes",
                        principalColumn: "PokeTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MPokemonToTypes_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId",
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
                    BasePokemonId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonAbilityPokeAbilityId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomPokemonHeldItemHeldItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomPokemonEVsId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomPokemonIVsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomPokemons", x => x.CustomPokemonId);
                    table.ForeignKey(
                        name: "FK_CustomPokemons_HeldItem_CustomPokemonHeldItemHeldItemId",
                        column: x => x.CustomPokemonHeldItemHeldItemId,
                        principalTable: "HeldItem",
                        principalColumn: "HeldItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomPokemons_PokeAbilities_CustomPokemonAbilityPokeAbilityId",
                        column: x => x.CustomPokemonAbilityPokeAbilityId,
                        principalTable: "PokeAbilities",
                        principalColumn: "PokeAbilityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomPokemons_PokeStats_CustomPokemonEVsId",
                        column: x => x.CustomPokemonEVsId,
                        principalTable: "PokeStats",
                        principalColumn: "PokeStatsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomPokemons_PokeStats_CustomPokemonIVsId",
                        column: x => x.CustomPokemonIVsId,
                        principalTable: "PokeStats",
                        principalColumn: "PokeStatsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomPokemons_UserTeams_UserTeamId",
                        column: x => x.UserTeamId,
                        principalTable: "UserTeams",
                        principalColumn: "UserTeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokeMoves",
                columns: table => new
                {
                    PokeMoveId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoveName = table.Column<string>(type: "TEXT", nullable: false),
                    Accuracy = table.Column<int>(type: "INTEGER", nullable: true),
                    MoveTypePokeTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PP = table.Column<int>(type: "INTEGER", nullable: true),
                    Power = table.Column<int>(type: "INTEGER", nullable: true),
                    DamgeClass = table.Column<string>(type: "TEXT", nullable: false),
                    FlavourText = table.Column<string>(type: "TEXT", nullable: false),
                    CustomPokemonId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeMoves", x => x.PokeMoveId);
                    table.ForeignKey(
                        name: "FK_PokeMoves_CustomPokemons_CustomPokemonId",
                        column: x => x.CustomPokemonId,
                        principalTable: "CustomPokemons",
                        principalColumn: "CustomPokemonId");
                    table.ForeignKey(
                        name: "FK_PokeMoves_PokeTypes_MoveTypePokeTypeId",
                        column: x => x.MoveTypePokeTypeId,
                        principalTable: "PokeTypes",
                        principalColumn: "PokeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MPokemonToMoves",
                columns: table => new
                {
                    MPokemonToMovesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    PokeMoveId = table.Column<int>(type: "INTEGER", nullable: false),
                    PokeMethodId = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MPokemonToMoves", x => x.MPokemonToMovesId);
                    table.ForeignKey(
                        name: "FK_MPokemonToMoves_PokeMethods_PokeMethodId",
                        column: x => x.PokeMethodId,
                        principalTable: "PokeMethods",
                        principalColumn: "PokeMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MPokemonToMoves_PokeMoves_PokeMoveId",
                        column: x => x.PokeMoveId,
                        principalTable: "PokeMoves",
                        principalColumn: "PokeMoveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MPokemonToMoves_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "PokemonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_CustomPokemonAbilityPokeAbilityId",
                table: "CustomPokemons",
                column: "CustomPokemonAbilityPokeAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_CustomPokemonEVsId",
                table: "CustomPokemons",
                column: "CustomPokemonEVsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_CustomPokemonHeldItemHeldItemId",
                table: "CustomPokemons",
                column: "CustomPokemonHeldItemHeldItemId");

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
                name: "IX_MPokemonToAbilities_PokeAbilityId",
                table: "MPokemonToAbilities",
                column: "PokeAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MPokemonToAbilities_PokemonId",
                table: "MPokemonToAbilities",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_MPokemonToMoves_PokeMethodId",
                table: "MPokemonToMoves",
                column: "PokeMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MPokemonToMoves_PokemonId",
                table: "MPokemonToMoves",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_MPokemonToMoves_PokeMoveId",
                table: "MPokemonToMoves",
                column: "PokeMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MPokemonToTypes_PokemonId",
                table: "MPokemonToTypes",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_MPokemonToTypes_PokeTypeId",
                table: "MPokemonToTypes",
                column: "PokeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_BaseStatsId",
                table: "Pokemons",
                column: "BaseStatsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokeMoves_CustomPokemonId",
                table: "PokeMoves",
                column: "CustomPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokeMoves_MoveTypePokeTypeId",
                table: "PokeMoves",
                column: "MoveTypePokeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeEffectivenesses_AttackTypeId",
                table: "TypeEffectivenesses",
                column: "AttackTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeEffectivenesses_DefenceTypeId",
                table: "TypeEffectivenesses",
                column: "DefenceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_UserId",
                table: "UserTeams",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MPokemonToAbilities");

            migrationBuilder.DropTable(
                name: "MPokemonToMoves");

            migrationBuilder.DropTable(
                name: "MPokemonToTypes");

            migrationBuilder.DropTable(
                name: "TypeEffectivenesses");

            migrationBuilder.DropTable(
                name: "PokeMethods");

            migrationBuilder.DropTable(
                name: "PokeMoves");

            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "CustomPokemons");

            migrationBuilder.DropTable(
                name: "PokeTypes");

            migrationBuilder.DropTable(
                name: "HeldItem");

            migrationBuilder.DropTable(
                name: "PokeAbilities");

            migrationBuilder.DropTable(
                name: "PokeStats");

            migrationBuilder.DropTable(
                name: "UserTeams");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
