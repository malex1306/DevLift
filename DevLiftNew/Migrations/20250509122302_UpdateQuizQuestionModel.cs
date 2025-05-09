using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLiftNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizQuestionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAnswers_QuizQuestions_QuizQuestionId1",
                table: "QuizAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuizAnswers_QuizQuestionId1",
                table: "QuizAnswers");

            migrationBuilder.DropColumn(
                name: "QuizQuestionId1",
                table: "QuizAnswers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizQuestionId1",
                table: "QuizAnswers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswers_QuizQuestionId1",
                table: "QuizAnswers",
                column: "QuizQuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAnswers_QuizQuestions_QuizQuestionId1",
                table: "QuizAnswers",
                column: "QuizQuestionId1",
                principalTable: "QuizQuestions",
                principalColumn: "Id");
        }
    }
}
