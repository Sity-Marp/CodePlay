using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Add_QuizAttempt_NoCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizAttemptId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "QuizAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    Track = table.Column<int>(type: "int", nullable: false),
                    LevelNumber = table.Column<int>(type: "int", nullable: false),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    CorrectCount = table.Column<int>(type: "int", nullable: false),
                    WrongCount = table.Column<int>(type: "int", nullable: false),
                    Passed = table.Column<bool>(type: "bit", nullable: false),
                    PlayedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAttempts_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizAttempts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { 100, "testuser@example.com", "n3NeDfmh3ccCvwoae4MDP59xU6AMKd6CztrcmVcomwU=", "testuser" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuizAttemptId",
                table: "UserAnswers",
                column: "QuizAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_QuizId",
                table: "QuizAttempts",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_Track_LevelNumber_PlayedAt",
                table: "QuizAttempts",
                columns: new[] { "Track", "LevelNumber", "PlayedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_UserId_PlayedAt",
                table: "QuizAttempts",
                columns: new[] { "UserId", "PlayedAt" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuizAttempts_QuizAttemptId",
                table: "UserAnswers",
                column: "QuizAttemptId",
                principalTable: "QuizAttempts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuizAttempts_QuizAttemptId",
                table: "UserAnswers");

            migrationBuilder.DropTable(
                name: "QuizAttempts");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_QuizAttemptId",
                table: "UserAnswers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DropColumn(
                name: "QuizAttemptId",
                table: "UserAnswers");
        }
    }
}
