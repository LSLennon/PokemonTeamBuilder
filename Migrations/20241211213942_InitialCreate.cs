using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InclamentEmeraldTeamBuilder.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    AbilityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AbilityName = table.Column<string>(type: "TEXT", nullable: false),
                    AbilitySourceName = table.Column<string>(type: "TEXT", nullable: false),
                    AbilityDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.AbilityId);
                });

            migrationBuilder.CreateTable(
                name: "MoveEffects",
                columns: table => new
                {
                    MoveEffectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoveEffectName = table.Column<string>(type: "TEXT", nullable: false),
                    MoveEffectSourceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveEffects", x => x.MoveEffectId);
                });

            migrationBuilder.CreateTable(
                name: "MoveStatuses",
                columns: table => new
                {
                    MoveStatusId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoveStatusName = table.Column<string>(type: "TEXT", nullable: false),
                    MoveStatusSourceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveStatuses", x => x.MoveStatusId);
                });

            migrationBuilder.CreateTable(
                name: "MoveTargets",
                columns: table => new
                {
                    MoveTargetId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoveTargetName = table.Column<string>(type: "TEXT", nullable: false),
                    MoveTargetSourceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveTargets", x => x.MoveTargetId);
                });

            migrationBuilder.CreateTable(
                name: "PokeTypes",
                columns: table => new
                {
                    PokeTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokeTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    PokeTypeSourceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeTypes", x => x.PokeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    MoveId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoveName = table.Column<string>(type: "TEXT", nullable: false),
                    MoveSourceName = table.Column<string>(type: "TEXT", nullable: false),
                    MoveDescription = table.Column<string>(type: "TEXT", nullable: false),
                    MoveEffectId = table.Column<int>(type: "INTEGER", nullable: false),
                    Power = table.Column<int>(type: "INTEGER", nullable: false),
                    PokeTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Accuracy = table.Column<int>(type: "INTEGER", nullable: false),
                    PP = table.Column<int>(type: "INTEGER", nullable: false),
                    SecondaryEffectChance = table.Column<int>(type: "INTEGER", nullable: false),
                    MoveTargetId = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    MoveStatusId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.MoveId);
                    table.ForeignKey(
                        name: "FK_Moves_MoveEffects_MoveEffectId",
                        column: x => x.MoveEffectId,
                        principalTable: "MoveEffects",
                        principalColumn: "MoveEffectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moves_MoveStatuses_MoveStatusId",
                        column: x => x.MoveStatusId,
                        principalTable: "MoveStatuses",
                        principalColumn: "MoveStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moves_MoveTargets_MoveTargetId",
                        column: x => x.MoveTargetId,
                        principalTable: "MoveTargets",
                        principalColumn: "MoveTargetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moves_PokeTypes_PokeTypeId",
                        column: x => x.PokeTypeId,
                        principalTable: "PokeTypes",
                        principalColumn: "PokeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveFlags",
                columns: table => new
                {
                    MoveFlagId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoveFlagName = table.Column<string>(type: "TEXT", nullable: false),
                    MoveFlagSourceName = table.Column<string>(type: "TEXT", nullable: false),
                    MoveId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveFlags", x => x.MoveFlagId);
                    table.ForeignKey(
                        name: "FK_MoveFlags_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "MoveId");
                });

            migrationBuilder.CreateTable(
                name: "MMoveToMoveFlags",
                columns: table => new
                {
                    MMoveToMoveFlagId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoveId = table.Column<int>(type: "INTEGER", nullable: false),
                    MoveFlagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MMoveToMoveFlags", x => x.MMoveToMoveFlagId);
                    table.ForeignKey(
                        name: "FK_MMoveToMoveFlags_MoveFlags_MoveFlagId",
                        column: x => x.MoveFlagId,
                        principalTable: "MoveFlags",
                        principalColumn: "MoveFlagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MMoveToMoveFlags_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "MoveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MMoveToMoveFlags_MoveFlagId",
                table: "MMoveToMoveFlags",
                column: "MoveFlagId");

            migrationBuilder.CreateIndex(
                name: "IX_MMoveToMoveFlags_MoveId",
                table: "MMoveToMoveFlags",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveFlags_MoveId",
                table: "MoveFlags",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_MoveEffectId",
                table: "Moves",
                column: "MoveEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_MoveStatusId",
                table: "Moves",
                column: "MoveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_MoveTargetId",
                table: "Moves",
                column: "MoveTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_PokeTypeId",
                table: "Moves",
                column: "PokeTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "MMoveToMoveFlags");

            migrationBuilder.DropTable(
                name: "MoveFlags");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "MoveEffects");

            migrationBuilder.DropTable(
                name: "MoveStatuses");

            migrationBuilder.DropTable(
                name: "MoveTargets");

            migrationBuilder.DropTable(
                name: "PokeTypes");
        }
    }
}
