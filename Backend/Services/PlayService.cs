

using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Services
{
    public class PlayService
    {
        private readonly AppDbContext _db;

        public PlayService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<SubmitResult> SubmitAsync(int userId, SubmitAnswersDto dto)
        {
            // 1) Ladda quiz inkl. frågor + svarsalternativ
            var quiz = await _db.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.AnswerOptions)
                .FirstOrDefaultAsync(q => q.Id == dto.QuizId);

            if (quiz == null)
                throw new Exception("Quiz hittades inte.");

            int correct = 0;
            int incorrect = 0;
            var incorrectQuestionIds = new List<int>();

            // 2) Rätta varje inskickat svar + spara i UserAnswers
            foreach (var ans in dto.Answers)
            {
                // 2.1 Hitta frågan (med samma Id som frontend skickade)
                var question = quiz.Questions.FirstOrDefault(q => q.Id == ans.QuestionId);
                if (question == null)
                    continue; // om frontend råkar skicka skräp, hoppa över (påverkar inte resten)

                // 2.2 Hitta valt alternativ via ID (robustare än text)
                var selectedOption = question.AnswerOptions.FirstOrDefault(o => o.Id == ans.AnswerOptionId);
                if (selectedOption == null)
                    throw new Exception($"Ogiltigt alternativ för fråga {question.Id}.");

                // 2.3 Är detta alternativet rätt?
                var isCorrect = selectedOption.IsCorrect;

                if (isCorrect)
                {
                    correct++;
                }
                else
                {
                    incorrect++;
                    incorrectQuestionIds.Add(question.Id);
                }
                // Spara userens svar
                _db.UserAnswers.Add(new UserAnswer
                {
                    UserId = userId,
                    QuestionId = question.Id,
                    AnswerOptionId= selectedOption.Id,
                    IsCorrect = isCorrect,
                    
                    SelectedOption = selectedOption.Text

                });
            }

            await _db.SaveChangesAsync();

            // 3) Uppdatera/Skapa UserProgress för quizets Track
            var progress = await _db.UserProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.Track == quiz.Track);

            if (progress == null)
            {
                progress = new UserProgress
                {
                    UserId = userId,
                    Track = quiz.Track,
                    CurrentLevel = 1,
                    HighestUnlockedLevel = 1,
                    TotalPoints = 0,
                    TotalCorrect = 0,
                    TotalIncorrect = 0,
                    LastUpdated = DateTime.UtcNow
                };
                _db.UserProgresses.Add(progress);
            }

            // Enkel poängstrategi
            progress.TotalCorrect += correct;
            progress.TotalIncorrect += incorrect;
            progress.TotalPoints += correct;
            progress.LastUpdated = DateTime.UtcNow;

            // Klarade användaren quizet?
            bool passed = correct >= quiz.PassingScore;

            // Lås upp nästa nivå vid pass
            if (passed && quiz.LevelNumber >= progress.HighestUnlockedLevel)
            {
                progress.HighestUnlockedLevel = quiz.LevelNumber + 1;
                progress.CurrentLevel = Math.Max(progress.CurrentLevel, progress.HighestUnlockedLevel);
            }

            await _db.SaveChangesAsync();

            return new SubmitResult
            {
                Correct = correct,
                Incorrect = incorrect,
                Passed = passed,
                NextLevelUnlocked = passed ? progress.HighestUnlockedLevel : (int?)null,
                IncorrectQuestionIds = incorrectQuestionIds
            };
        }
    }

    // Svarspaket till frontend
    public class SubmitResult
    {
        public int Correct { get; set; }
        public int Incorrect { get; set; }
        public bool Passed { get; set; }
        public int? NextLevelUnlocked { get; set; }
        public List<int> IncorrectQuestionIds { get; set; } = new();
    }
}
