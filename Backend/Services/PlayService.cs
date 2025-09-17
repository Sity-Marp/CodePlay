

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;              // <-- AppDbContext
using Backend.DTOs;              // <-- SubmitAnswersDto, AnswerSubmissionDto, SubmitResult
using Backend.Models;            // <-- Quiz, Question, AnswerOption, UserAnswer, QuizAttempt, UserProgress
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class PlayService
    {
        private readonly AppDbContext _db;

        // DI: DbContext injiceras
        public PlayService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<SubmitResult> SubmitAsync(int userId, SubmitAnswersDto dto)
        {
            // ========== 1) Hämta quiz + frågor + svarsalternativ ==========
            var quiz = await _db.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.AnswerOptions)
                .FirstOrDefaultAsync(q => q.Id == dto.QuizId);

            if (quiz == null)
                throw new InvalidOperationException("Quiz hittades inte.");

            if (dto.Answers == null || dto.Answers.Count == 0)
                throw new InvalidOperationException("Inga svar skickades in.");

            // ========== 2) Skapa en QuizAttempt (sammanfattning av hela rundan) ==========
            var attempt = new QuizAttempt
            {
                UserId = userId,
                QuizId = quiz.Id,
                Track = quiz.Track,            // enum TrackType
                LevelNumber = quiz.LevelNumber,      // t.ex. 1, 2, 3 ...
                PlayedAt = DateTime.UtcNow
            };
            _db.QuizAttempts.Add(attempt);

            // ========== 3) Rätta svaren och spara UserAnswers kopplade till attemptet ==========
            int correct = 0;
            int incorrect = 0;
            var incorrectQuestionIds = new List<int>();

            foreach (var ans in dto.Answers) // ans = AnswerSubmissionDto (QuestionId, AnswerOptionId)
            {
                // 3.1 Hitta frågan i detta quiz
                var question = quiz.Questions.FirstOrDefault(q => q.Id == ans.QuestionId);
                if (question == null)
                    continue; // defensivt – ignorera ev. skräp

                // 3.2 Hitta valt alternativ
                var option = question.AnswerOptions.FirstOrDefault(o => o.Id == ans.AnswerOptionId);
                if (option == null)
                    throw new InvalidOperationException($"Ogiltigt alternativ för fråga {question.Id}.");

                var isCorrect = option.IsCorrect;
                if (isCorrect) correct++;
                else
                {
                    incorrect++;
                    incorrectQuestionIds.Add(question.Id);
                }

                // 3.3 Spara rad-för-rad och koppla till attemptet
                _db.UserAnswers.Add(new UserAnswer
                {
                    UserId = userId,
                    QuestionId = question.Id,
                    AnswerOptionId = option.Id,
                    SelectedOption = option.Text,         // spara texten – robust om alternativen ändras senare
                    IsCorrect = isCorrect,
                    QuizAttempt = attempt              // FK sätts via navigation -> QuizAttemptId
                    
                });
            }

            // ========== 4) Fyll attempt-summeringen ==========
            attempt.TotalQuestions = correct + incorrect;
            attempt.CorrectCount = correct;
            attempt.WrongCount = incorrect;
            attempt.Passed = correct >= quiz.PassingScore;   // godkänt enligt quizets gräns

            // ========== 5) Uppdatera UserProgress för denna track ==========
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

            // 5.1 Räkna upp totals (enkel poäng: 1 poäng per rätt)
            progress.TotalCorrect += correct;
            progress.TotalIncorrect += incorrect;
            progress.TotalPoints += correct;
            progress.LastUpdated = DateTime.UtcNow;

            // 5.2 Lås upp nästa nivå om attemptet blev godkänt
            if (attempt.Passed && quiz.LevelNumber >= progress.HighestUnlockedLevel)
            {
                progress.HighestUnlockedLevel = quiz.LevelNumber + 1;

                // Flytta fram CurrentLevel om vi nu låst upp längre än var vi står
                if (progress.CurrentLevel < progress.HighestUnlockedLevel)
                    progress.CurrentLevel = progress.HighestUnlockedLevel;
            }

            // ========== 6) Spara ALLT ==========
            await _db.SaveChangesAsync();

            // ========== 7) Returnera ett resultatpaket till frontend ==========
            return new SubmitResult
            {
                AttemptId = attempt.Id,
                Correct = correct,
                Incorrect = incorrect,
                Passed = attempt.Passed,
                NextLevelUnlocked = attempt.Passed ? progress.HighestUnlockedLevel : (int?)null,
                IncorrectQuestionIds = incorrectQuestionIds,
                QuizTitle = quiz.Title,
                LevelNumber = quiz.LevelNumber
            };
        }
    }
}
