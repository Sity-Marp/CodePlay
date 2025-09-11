using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // ===== SEEDING QUIZZES =====
            var quizzes = new[]
            {
                // HTML Track
                new Quiz { Id = 1, Title = "HTML Grundläggande", Track = TrackType.Html, LevelNumber = 1, PassingScore = 70 },
                new Quiz { Id = 2, Title = "HTML Element och Attribut", Track = TrackType.Html, LevelNumber = 2, PassingScore = 75 },
                new Quiz { Id = 3, Title = "HTML Formulär och Tabeller", Track = TrackType.Html, LevelNumber = 3, PassingScore = 80 },
                
                // CSS Track
                new Quiz { Id = 4, Title = "CSS Grundläggande", Track = TrackType.Css, LevelNumber = 1, PassingScore = 70 },
                new Quiz { Id = 5, Title = "CSS Selectors och Layout", Track = TrackType.Css, LevelNumber = 2, PassingScore = 75 },
                new Quiz { Id = 6, Title = "CSS Flexbox och Grid", Track = TrackType.Css, LevelNumber = 3, PassingScore = 80 },
                
                // JavaScript Track
                new Quiz { Id = 7, Title = "JavaScript Grundläggande", Track = TrackType.JavaScript, LevelNumber = 1, PassingScore = 70 },
                new Quiz { Id = 8, Title = "JavaScript Funktioner och Objekt", Track = TrackType.JavaScript, LevelNumber = 2, PassingScore = 75 },
                new Quiz { Id = 9, Title = "JavaScript DOM och Events", Track = TrackType.JavaScript, LevelNumber = 3, PassingScore = 80 }
            };

            modelBuilder.Entity<Quiz>().HasData(quizzes);

            // ===== SEEDING QUESTIONS =====
            var questions = new[]
            {
                // HTML Quiz 1 - Grundläggande
                new Question { Id = 1, Text = "Vad står HTML för?", QuizId = 1, Order = 1 },
                new Question { Id = 2, Text = "Vilken HTML-tagg används för att skapa en rubrik?", QuizId = 1, Order = 2 },
                new Question { Id = 3, Text = "Vad är den korrekta syntaxen för att skapa en länk i HTML?", QuizId = 1, Order = 3 },
                new Question { Id = 4, Text = "Vilken tagg används för att skapa stycken i HTML?", QuizId = 1, Order = 4 },
                new Question { Id = 5, Text = "Vad är DOCTYPE-deklarationen till för?", QuizId = 1, Order = 5 },

                // HTML Quiz 2 - Element och Attribut
                new Question { Id = 6, Text = "Vilket attribut används för att ange en bild-URL?", QuizId = 2, Order = 1 },
                new Question { Id = 7, Text = "Vilken tagg används för att skapa en oordnad lista?", QuizId = 2, Order = 2 },
                new Question { Id = 8, Text = "Vad används class-attributet till?", QuizId = 2, Order = 3 },
                new Question { Id = 9, Text = "Vilken tagg används för att skapa en radbrytning?", QuizId = 2, Order = 4 },
                new Question { Id = 10, Text = "Vilket attribut används för att öppna en länk i nytt fönster?", QuizId = 2, Order = 5 },

                // CSS Quiz 1 - Grundläggande
                new Question { Id = 11, Text = "Vad står CSS för?", QuizId = 4, Order = 1 },
                new Question { Id = 12, Text = "Hur lägger man till CSS i ett HTML-dokument?", QuizId = 4, Order = 2 },
                new Question { Id = 13, Text = "Vilken CSS-egenskap används för att ändra textfärg?", QuizId = 4, Order = 3 },
                new Question { Id = 14, Text = "Vad är skillnaden mellan margin och padding?", QuizId = 4, Order = 4 },
                new Question { Id = 15, Text = "Vilken CSS-selektor har högst specificitet?", QuizId = 4, Order = 5 },

                // JavaScript Quiz 1 - Grundläggande
                new Question { Id = 16, Text = "Vad används 'var', 'let' och 'const' för i JavaScript?", QuizId = 7, Order = 1 },
                new Question { Id = 17, Text = "Vilken metod används för att skriva ut text i konsolen?", QuizId = 7, Order = 2 },
                new Question { Id = 18, Text = "Vad är skillnaden mellan '==' och '===' i JavaScript?", QuizId = 7, Order = 3 },
                new Question { Id = 19, Text = "Hur skapar man en funktion i JavaScript?", QuizId = 7, Order = 4 },
                new Question { Id = 20, Text = "Vad är JavaScript-datatyper?", QuizId = 7, Order = 5 }
            };

            modelBuilder.Entity<Question>().HasData(questions);

            // ===== SEEDING ANSWER OPTIONS =====
            var answerOptions = new[]
            {
                // Question 1: Vad står HTML för?
                new AnswerOption { Id = 1, Text = "HyperText Markup Language", IsCorrect = true, QuestionId = 1 },
                new AnswerOption { Id = 2, Text = "High Tech Modern Language", IsCorrect = false, QuestionId = 1 },
                new AnswerOption { Id = 3, Text = "Home Tool Markup Language", IsCorrect = false, QuestionId = 1 },
                new AnswerOption { Id = 4, Text = "Hyperlink and Text Markup Language", IsCorrect = false, QuestionId = 1 },

                // Question 2: Vilken HTML-tagg används för att skapa en rubrik?
                new AnswerOption { Id = 5, Text = "<h1> till <h6>", IsCorrect = true, QuestionId = 2 },
                new AnswerOption { Id = 6, Text = "<header>", IsCorrect = false, QuestionId = 2 },
                new AnswerOption { Id = 7, Text = "<title>", IsCorrect = false, QuestionId = 2 },
                new AnswerOption { Id = 8, Text = "<heading>", IsCorrect = false, QuestionId = 2 },

                // Question 3: Vad är den korrekta syntaxen för att skapa en länk i HTML?
                new AnswerOption { Id = 9, Text = "<a href=\"url\">Länktext</a>", IsCorrect = true, QuestionId = 3 },
                new AnswerOption { Id = 10, Text = "<link url=\"url\">Länktext</link>", IsCorrect = false, QuestionId = 3 },
                new AnswerOption { Id = 11, Text = "<a url=\"url\">Länktext</a>", IsCorrect = false, QuestionId = 3 },
                new AnswerOption { Id = 12, Text = "<href=\"url\">Länktext</href>", IsCorrect = false, QuestionId = 3 },

                // Question 4: Vilken tagg används för att skapa stycken i HTML?
                new AnswerOption { Id = 13, Text = "<p>", IsCorrect = true, QuestionId = 4 },
                new AnswerOption { Id = 14, Text = "<paragraph>", IsCorrect = false, QuestionId = 4 },
                new AnswerOption { Id = 15, Text = "<text>", IsCorrect = false, QuestionId = 4 },
                new AnswerOption { Id = 16, Text = "<div>", IsCorrect = false, QuestionId = 4 },

                // Question 5: Vad är DOCTYPE-deklarationen till för?
                new AnswerOption { Id = 17, Text = "Anger vilken version av HTML som används", IsCorrect = true, QuestionId = 5 },
                new AnswerOption { Id = 18, Text = "Anger sidans titel", IsCorrect = false, QuestionId = 5 },
                new AnswerOption { Id = 19, Text = "Anger sidans språk", IsCorrect = false, QuestionId = 5 },
                new AnswerOption { Id = 20, Text = "Anger sidans författare", IsCorrect = false, QuestionId = 5 },

                // Question 6: Vilket attribut används för att ange en bild-URL?
                new AnswerOption { Id = 21, Text = "src", IsCorrect = true, QuestionId = 6 },
                new AnswerOption { Id = 22, Text = "href", IsCorrect = false, QuestionId = 6 },
                new AnswerOption { Id = 23, Text = "url", IsCorrect = false, QuestionId = 6 },
                new AnswerOption { Id = 24, Text = "link", IsCorrect = false, QuestionId = 6 },

                // Question 7: Vilken tagg används för att skapa en oordnad lista?
                new AnswerOption { Id = 25, Text = "<ul>", IsCorrect = true, QuestionId = 7 },
                new AnswerOption { Id = 26, Text = "<ol>", IsCorrect = false, QuestionId = 7 },
                new AnswerOption { Id = 27, Text = "<list>", IsCorrect = false, QuestionId = 7 },
                new AnswerOption { Id = 28, Text = "<li>", IsCorrect = false, QuestionId = 7 },

                // Question 11: Vad står CSS för?
                new AnswerOption { Id = 29, Text = "Cascading Style Sheets", IsCorrect = true, QuestionId = 11 },
                new AnswerOption { Id = 30, Text = "Computer Style Sheets", IsCorrect = false, QuestionId = 11 },
                new AnswerOption { Id = 31, Text = "Creative Style Sheets", IsCorrect = false, QuestionId = 11 },
                new AnswerOption { Id = 32, Text = "Colorful Style Sheets", IsCorrect = false, QuestionId = 11 },

                // Question 12: Hur lägger man till CSS i ett HTML-dokument?
                new AnswerOption { Id = 33, Text = "Alla alternativen är korrekta", IsCorrect = true, QuestionId = 12 },
                new AnswerOption { Id = 34, Text = "Inline med style-attributet", IsCorrect = false, QuestionId = 12 },
                new AnswerOption { Id = 35, Text = "I <head> med <style>-tagg", IsCorrect = false, QuestionId = 12 },
                new AnswerOption { Id = 36, Text = "Externa CSS-filer med <link>", IsCorrect = false, QuestionId = 12 },

                // Question 16: Vad används 'var', 'let' och 'const' för i JavaScript?
                new AnswerOption { Id = 37, Text = "För att deklarera variabler", IsCorrect = true, QuestionId = 16 },
                new AnswerOption { Id = 38, Text = "För att skapa funktioner", IsCorrect = false, QuestionId = 16 },
                new AnswerOption { Id = 39, Text = "För att skapa loopar", IsCorrect = false, QuestionId = 16 },
                new AnswerOption { Id = 40, Text = "För att importera moduler", IsCorrect = false, QuestionId = 16 },

                // Question 17: Vilken metod används för att skriva ut text i konsolen?
                new AnswerOption { Id = 41, Text = "console.log()", IsCorrect = true, QuestionId = 17 },
                new AnswerOption { Id = 42, Text = "print()", IsCorrect = false, QuestionId = 17 },
                new AnswerOption { Id = 43, Text = "document.write()", IsCorrect = false, QuestionId = 17 },
                new AnswerOption { Id = 44, Text = "alert()", IsCorrect = false, QuestionId = 17 },

                // Question 18: Vad är skillnaden mellan '==' och '===' i JavaScript?
                new AnswerOption { Id = 45, Text = "== jämför värde, === jämför värde och datatyp", IsCorrect = true, QuestionId = 18 },
                new AnswerOption { Id = 46, Text = "== är för sträng, === är för nummer", IsCorrect = false, QuestionId = 18 },
                new AnswerOption { Id = 47, Text = "Ingen skillnad, båda gör samma sak", IsCorrect = false, QuestionId = 18 },
                new AnswerOption { Id = 48, Text = "=== är snabbare än ==", IsCorrect = false, QuestionId = 18 },

                // Question 19: Hur skapar man en funktion i JavaScript?
                new AnswerOption { Id = 49, Text = "function myFunction() { } eller const myFunction = () => { }", IsCorrect = true, QuestionId = 19 },
                new AnswerOption { Id = 50, Text = "def myFunction(): pass", IsCorrect = false, QuestionId = 19 },
                new AnswerOption { Id = 51, Text = "public void myFunction() { }", IsCorrect = false, QuestionId = 19 },
                new AnswerOption { Id = 52, Text = "function = myFunction() { }", IsCorrect = false, QuestionId = 19 },

                // Question 20: Vad är JavaScript-datatyper?
                new AnswerOption { Id = 53, Text = "string, number, boolean, object, undefined, null, symbol, bigint", IsCorrect = true, QuestionId = 20 },
                new AnswerOption { Id = 54, Text = "int, float, char, boolean", IsCorrect = false, QuestionId = 20 },
                new AnswerOption { Id = 55, Text = "text, nummer, sant/falskt", IsCorrect = false, QuestionId = 20 },
                new AnswerOption { Id = 56, Text = "string, integer, boolean, array", IsCorrect = false, QuestionId = 20 }
            };

            modelBuilder.Entity<AnswerOption>().HasData(answerOptions);
        }
    }
}