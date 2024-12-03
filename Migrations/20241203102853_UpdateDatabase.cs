using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonTeamBuilder.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomPokemons_UserTeams_UserTeamId",
                table: "CustomPokemons");

            migrationBuilder.AlterColumn<int>(
                name: "UserTeamId",
                table: "CustomPokemons",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "CustomPokemons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserBoxId",
                table: "CustomPokemons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MCustomToTeams",
                columns: table => new
                {
                    MCustomToTeamsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomPokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserTeamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCustomToTeams", x => x.MCustomToTeamsId);
                    table.ForeignKey(
                        name: "FK_MCustomToTeams_CustomPokemons_CustomPokemonId",
                        column: x => x.CustomPokemonId,
                        principalTable: "CustomPokemons",
                        principalColumn: "CustomPokemonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MCustomToTeams_UserTeams_UserTeamId",
                        column: x => x.UserTeamId,
                        principalTable: "UserTeams",
                        principalColumn: "UserTeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrainerName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.TrainerId);
                });

            migrationBuilder.CreateTable(
                name: "UserBox",
                columns: table => new
                {
                    UserBoxId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserBoxName = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBox", x => x.UserBoxId);
                    table.ForeignKey(
                        name: "FK_UserBox_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_TrainerId",
                table: "CustomPokemons",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPokemons_UserBoxId",
                table: "CustomPokemons",
                column: "UserBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_MCustomToTeams_CustomPokemonId",
                table: "MCustomToTeams",
                column: "CustomPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_MCustomToTeams_UserTeamId",
                table: "MCustomToTeams",
                column: "UserTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBox_UserId",
                table: "UserBox",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomPokemons_Trainers_TrainerId",
                table: "CustomPokemons",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomPokemons_UserBox_UserBoxId",
                table: "CustomPokemons",
                column: "UserBoxId",
                principalTable: "UserBox",
                principalColumn: "UserBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomPokemons_UserTeams_UserTeamId",
                table: "CustomPokemons",
                column: "UserTeamId",
                principalTable: "UserTeams",
                principalColumn: "UserTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomPokemons_Trainers_TrainerId",
                table: "CustomPokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomPokemons_UserBox_UserBoxId",
                table: "CustomPokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomPokemons_UserTeams_UserTeamId",
                table: "CustomPokemons");

            migrationBuilder.DropTable(
                name: "MCustomToTeams");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "UserBox");

            migrationBuilder.DropIndex(
                name: "IX_CustomPokemons_TrainerId",
                table: "CustomPokemons");

            migrationBuilder.DropIndex(
                name: "IX_CustomPokemons_UserBoxId",
                table: "CustomPokemons");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "CustomPokemons");

            migrationBuilder.DropColumn(
                name: "UserBoxId",
                table: "CustomPokemons");

            migrationBuilder.AlterColumn<int>(
                name: "UserTeamId",
                table: "CustomPokemons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomPokemons_UserTeams_UserTeamId",
                table: "CustomPokemons",
                column: "UserTeamId",
                principalTable: "UserTeams",
                principalColumn: "UserTeamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
