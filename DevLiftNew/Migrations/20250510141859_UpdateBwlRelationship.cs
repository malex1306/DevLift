using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLiftNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBwlRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizQuestionsBwl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BwlKategorie = table.Column<string>(type: "TEXT", nullable: false),
                    BwlFrageText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionsBwl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizAnswersBwl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BwlAntwortText = table.Column<string>(type: "TEXT", nullable: false),
                    BwlIstKorrekt = table.Column<bool>(type: "INTEGER", nullable: false),
                    bwlQuizQuestionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAnswersBwl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAnswersBwl_QuizQuestionsBwl_bwlQuizQuestionId",
                        column: x => x.bwlQuizQuestionId,
                        principalTable: "QuizQuestionsBwl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswersBwl_bwlQuizQuestionId",
                table: "QuizAnswersBwl",
                column: "bwlQuizQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAnswersBwl");

            migrationBuilder.DropTable(
                name: "QuizQuestionsBwl");
        }
    }
}
