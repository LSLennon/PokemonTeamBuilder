using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonTeamBuilder.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomPokemons_UserBox_UserBoxId",
                table: "CustomPokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBox_Users_UserId",
                table: "UserBox");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBox",
                table: "UserBox");

            migrationBuilder.RenameTable(
                name: "UserBox",
                newName: "UsersBox");

            migrationBuilder.RenameIndex(
                name: "IX_UserBox_UserId",
                table: "UsersBox",
                newName: "IX_UsersBox_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersBox",
                table: "UsersBox",
                column: "UserBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomPokemons_UsersBox_UserBoxId",
                table: "CustomPokemons",
                column: "UserBoxId",
                principalTable: "UsersBox",
                principalColumn: "UserBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersBox_Users_UserId",
                table: "UsersBox",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomPokemons_UsersBox_UserBoxId",
                table: "CustomPokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersBox_Users_UserId",
                table: "UsersBox");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersBox",
                table: "UsersBox");

            migrationBuilder.RenameTable(
                name: "UsersBox",
                newName: "UserBox");

            migrationBuilder.RenameIndex(
                name: "IX_UsersBox_UserId",
                table: "UserBox",
                newName: "IX_UserBox_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBox",
                table: "UserBox",
                column: "UserBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomPokemons_UserBox_UserBoxId",
                table: "CustomPokemons",
                column: "UserBoxId",
                principalTable: "UserBox",
                principalColumn: "UserBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBox_Users_UserId",
                table: "UserBox",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
