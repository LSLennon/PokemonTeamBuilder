using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonTeamBuilder.Migrations
{
    /// <inheritdoc />
    public partial class methodCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovesLearnedByPokemons_PokeMethods_MoveLearnMethodId",
                table: "MovesLearnedByPokemons");

            migrationBuilder.DropColumn(
                name: "LastVersion",
                table: "PokeMoves");

            migrationBuilder.RenameColumn(
                name: "MoveLearnMethodId",
                table: "PokeMethods",
                newName: "PokeMethodId");

            migrationBuilder.RenameColumn(
                name: "MoveLearnMethodId",
                table: "MovesLearnedByPokemons",
                newName: "PokeMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_MovesLearnedByPokemons_MoveLearnMethodId",
                table: "MovesLearnedByPokemons",
                newName: "IX_MovesLearnedByPokemons_PokeMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovesLearnedByPokemons_PokeMethods_PokeMethodId",
                table: "MovesLearnedByPokemons",
                column: "PokeMethodId",
                principalTable: "PokeMethods",
                principalColumn: "PokeMethodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovesLearnedByPokemons_PokeMethods_PokeMethodId",
                table: "MovesLearnedByPokemons");

            migrationBuilder.RenameColumn(
                name: "PokeMethodId",
                table: "PokeMethods",
                newName: "MoveLearnMethodId");

            migrationBuilder.RenameColumn(
                name: "PokeMethodId",
                table: "MovesLearnedByPokemons",
                newName: "MoveLearnMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_MovesLearnedByPokemons_PokeMethodId",
                table: "MovesLearnedByPokemons",
                newName: "IX_MovesLearnedByPokemons_MoveLearnMethodId");

            migrationBuilder.AddColumn<string>(
                name: "LastVersion",
                table: "PokeMoves",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_MovesLearnedByPokemons_PokeMethods_MoveLearnMethodId",
                table: "MovesLearnedByPokemons",
                column: "MoveLearnMethodId",
                principalTable: "PokeMethods",
                principalColumn: "MoveLearnMethodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
