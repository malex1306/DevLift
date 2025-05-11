using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLiftNew.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizResultQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizResultBwl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Punkte = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPunkte = table.Column<int>(type: "INTEGER", nullable: false),
                    Prozent = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResultBwl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizResultQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuizResultBwlId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuizQuestionBwlId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResultQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizResultQuestion_QuizQuestionsBwl_QuizQuestionBwlId",
                        column: x => x.QuizQuestionBwlId,
                        principalTable: "QuizQuestionsBwl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizResultQuestion_QuizResultBwl_QuizResultBwlId",
                        column: x => x.QuizResultBwlId,
                        principalTable: "QuizResultBwl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizResultQuestion_QuizQuestionBwlId",
                table: "QuizResultQuestion",
                column: "QuizQuestionBwlId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizResultQuestion_QuizResultBwlId",
                table: "QuizResultQuestion",
                column: "QuizResultBwlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizResultQuestion");

            migrationBuilder.DropTable(
                name: "QuizResultBwl");
        }
    }
}
