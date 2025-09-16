
using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        //Koppling till användaren
        public int UserId { get; set; }

        public User User { get; set; } // Navigation property to User
        //Koppling till quizet
        public Quiz Quiz { get; set; }
        public int QuizId { get; set; }
        public TrackType Track { get; set; } // matcha din enum från UserProgress
        public int LevelNumber { get; set; }

        // Sammanfattning för denna runda
        public int TotalQuestions { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public bool Passed { get; set; }
        public DateTime PlayedAt { get; set; } = DateTime.UtcNow;

        // Navigation (alla UserAnswers som hör till denna runda)
        public List<UserAnswer> Answers { get; set; } = new();
    }
}

