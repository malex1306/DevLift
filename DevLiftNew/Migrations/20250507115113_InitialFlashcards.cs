using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLiftNew.Migrations
{
    /// <inheritdoc />
    public partial class InitialFlashcards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcard_AspNetUsers_CreatedById",
                table: "Flashcard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flashcard",
                table: "Flashcard");

            migrationBuilder.RenameTable(
                name: "Flashcard",
                newName: "Flashcards");

            migrationBuilder.RenameIndex(
                name: "IX_Flashcard_CreatedById",
                table: "Flashcards",
                newName: "IX_Flashcards_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flashcards",
                table: "Flashcards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_AspNetUsers_CreatedById",
                table: "Flashcards",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_AspNetUsers_CreatedById",
                table: "Flashcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flashcards",
                table: "Flashcards");

            migrationBuilder.RenameTable(
                name: "Flashcards",
                newName: "Flashcard");

            migrationBuilder.RenameIndex(
                name: "IX_Flashcards_CreatedById",
                table: "Flashcard",
                newName: "IX_Flashcard_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flashcard",
                table: "Flashcard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcard_AspNetUsers_CreatedById",
                table: "Flashcard",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
