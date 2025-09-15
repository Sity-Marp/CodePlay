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
                new Quiz { Id = 1, Title = "HTML Grundläggande", Track = TrackType.Html, LevelNumber = 1, PassingScore = 5 },
                
                // CSS Track
                new Quiz { Id = 2, Title = "CSS Grundläggande", Track = TrackType.Css, LevelNumber = 1, PassingScore = 10 },
                
                // JavaScript Track
                new Quiz { Id = 3, Title = "JavaScript Grundläggande", Track = TrackType.JavaScript, LevelNumber = 1, PassingScore = 15 },

                // LastTrack
                new Quiz { Id = 4, Title = "Kombinerad Grundläggande", Track = TrackType.LastTrack, LevelNumber = 1, PassingScore = 20}
            };

            modelBuilder.Entity<Quiz>().HasData(quizzes);

            // ===== SEEDING QUESTIONS =====
            var questions = new[]
            {
                // HTML Quiz
                new Question { Id = 1, Text = "Vad står HTML för?", QuizId = 1, Order = 1 },
                new Question { Id = 2, Text = "Vilken tagg används för att skapa en rubrik?", QuizId = 1, Order = 2 },
                new Question { Id = 3, Text = "Vad är den korrekta syntaxen för att skapa en länk i HTML?", QuizId = 1, Order = 3 },
                new Question { Id = 4, Text = "Vilken tagg används för att skapa en oordnad lista?", QuizId = 1, Order = 4 },
                new Question { Id = 5, Text = "Vad är DOCTYPE-deklarationen till för?", QuizId = 1, Order = 5 },

                // CSS Quiz
                new Question { Id = 6, Text = "Vad står CSS för?", QuizId = 2, Order = 1 },
                new Question { Id = 7, Text = "Hur lägger man till CSS i ett HTML-dokument?", QuizId = 2, Order = 2 },
                new Question { Id = 8, Text = "Vilken CSS-egenskap används för att ändra textfärg?", QuizId = 2, Order = 3 },
                new Question { Id = 9, Text = "Vad är skillnaden mellan margin och padding?", QuizId = 2, Order = 4 },
                new Question { Id = 10, Text = "Vilken CSS-selektor har högst specificitet?", QuizId = 2, Order = 5 },

                // JavaScript Quiz
                new Question { Id = 11, Text = "Vad används 'var', 'let' och 'const' för i JavaScript?", QuizId = 3, Order = 1 },
                new Question { Id = 12, Text = "Vilken metod används för att skriva ut text i konsolen?", QuizId = 3, Order = 2 },
                new Question { Id = 13, Text = "Vad är skillnaden mellan '==' och '===' i JavaScript?", QuizId = 3, Order = 3 },
                new Question { Id = 14, Text = "Hur skapar man en funktion i JavaScript?", QuizId = 3, Order = 4 },
                new Question { Id = 15, Text = "Vad är skillnaden på 'let' och 'const'?", QuizId = 3, Order = 5 },

                // LastTrack quiz
                new Question { Id = 16, Text = "Du har flera knappar <button> på sidan och vill att alla ska ha blå bakgrund. Hur selektera du dem i CSS?", QuizId = 4, Order = 1 },
                new Question { Id = 17, Text = "Hur implementerar man dark mode på en webbsida?", QuizId = 4, Order = 2 },
                new Question { Id = 18, Text = "När du klickar på en knapp byter den färg. Vilket språk hanterar detta?", QuizId = 4, Order = 3 },
                new Question { Id = 19, Text = "En knapp ska ändra bakgrundsfärg när man för muspekaren över den. Vilket språk skapar denna hover-effekt?", QuizId = 4, Order = 4 },
                new Question { Id = 20, Text = "Du har flera knappar med klassen btn. Du vill göra alla knappar gröna. Vilken CSS-selektor används?", QuizId = 4, Order = 5 }
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

                // Question 4: Vilken tagg används för att skapa en oordnad lista?
                new AnswerOption { Id = 13, Text = "<ul>", IsCorrect = true, QuestionId = 4 },
                new AnswerOption { Id = 14, Text = "<ol>", IsCorrect = false, QuestionId = 4 },
                new AnswerOption { Id = 15, Text = "<li>", IsCorrect = false, QuestionId = 4 },
                new AnswerOption { Id = 16, Text = "<list>", IsCorrect = false, QuestionId = 4 },

                // Question 5: Vad är DOCTYPE-deklarationen till för?
                new AnswerOption { Id = 17, Text = "För att tala om för webbläsaren vilken version av HTML som används", IsCorrect = true, QuestionId = 5 },
                new AnswerOption { Id = 18, Text = "För att länka till externa CSS-filer", IsCorrect = false, QuestionId = 5 },
                new AnswerOption { Id = 19, Text = "För att definiera dokumentets titel", IsCorrect = false, QuestionId = 5 },
                new AnswerOption { Id = 20, Text = "För att importera JavaScript-filer", IsCorrect = false, QuestionId = 5 },

                // Question 6: Vad står CSS för?
                new AnswerOption { Id = 21, Text = "Cascading Style Sheets", IsCorrect = true, QuestionId = 6 },
                new AnswerOption { Id = 22, Text = "Computer Style Sheets", IsCorrect = false, QuestionId = 6 },
                new AnswerOption { Id = 23, Text = "Creative Style Sheets", IsCorrect = false, QuestionId = 6 },
                new AnswerOption { Id = 24, Text = "Colorful Style Sheets", IsCorrect = false, QuestionId = 6 },

                // Question 7: Hur lägger man till CSS i ett HTML-dokument?
                new AnswerOption { Id = 25, Text = "Inline, internal eller external", IsCorrect = true, QuestionId = 7 },
                new AnswerOption { Id = 26, Text = "Endast genom externa filer", IsCorrect = false, QuestionId = 7 },
                new AnswerOption { Id = 27, Text = "Endast genom style-attributet", IsCorrect = false, QuestionId = 7 },
                new AnswerOption { Id = 28, Text = "Endast genom <style>-taggen", IsCorrect = false, QuestionId = 7 },

                // Question 8: Vilken CSS-egenskap används för att ändra textfärg?
                new AnswerOption { Id = 29, Text = "color", IsCorrect = true, QuestionId = 8 },
                new AnswerOption { Id = 30, Text = "text-color", IsCorrect = false, QuestionId = 8 },
                new AnswerOption { Id = 31, Text = "font-color", IsCorrect = false, QuestionId = 8 },
                new AnswerOption { Id = 32, Text = "background-color", IsCorrect = false, QuestionId = 8 },

                // Question 9: Vad är skillnaden mellan margin och padding?
                new AnswerOption { Id = 33, Text = "Margin är utrymmet utanför elementet, padding är utrymmet inuti", IsCorrect = true, QuestionId = 9 },
                new AnswerOption { Id = 34, Text = "Padding är utrymmet utanför elementet, margin är utrymmet inuti", IsCorrect = false, QuestionId = 9 },
                new AnswerOption { Id = 35, Text = "De är samma sak", IsCorrect = false, QuestionId = 9 },
                new AnswerOption { Id = 36, Text = "Margin används för text, padding för bilder", IsCorrect = false, QuestionId = 9 },

                // Question 10: Vilken CSS-selektor har högst specificitet?
                new AnswerOption { Id = 37, Text = "ID-selektor (#id)", IsCorrect = true, QuestionId = 10 },
                new AnswerOption { Id = 38, Text = "Klass-selektor (.class)", IsCorrect = false, QuestionId = 10 },
                new AnswerOption { Id = 39, Text = "Element-selektor (div)", IsCorrect = false, QuestionId = 10 },
                new AnswerOption { Id = 40, Text = "Universal-selektor (*)", IsCorrect = false, QuestionId = 10 },

                // Question 11: Vad används 'var', 'let' och 'const' för i JavaScript?
                new AnswerOption { Id = 41, Text = "För att deklarera variabler", IsCorrect = true, QuestionId = 11 },
                new AnswerOption { Id = 42, Text = "För att skapa funktioner", IsCorrect = false, QuestionId = 11 },
                new AnswerOption { Id = 43, Text = "För att importera moduler", IsCorrect = false, QuestionId = 11 },
                new AnswerOption { Id = 44, Text = "För att skapa loopar", IsCorrect = false, QuestionId = 11 },

                // Question 12: Vilken metod används för att skriva ut text i konsolen?
                new AnswerOption { Id = 45, Text = "console.log()", IsCorrect = true, QuestionId = 12 },
                new AnswerOption { Id = 46, Text = "print()", IsCorrect = false, QuestionId = 12 },
                new AnswerOption { Id = 47, Text = "document.write()", IsCorrect = false, QuestionId = 12 },
                new AnswerOption { Id = 48, Text = "alert()", IsCorrect = false, QuestionId = 12 },

                // Question 13: Vad är skillnaden mellan '==' och '===' i JavaScript?
                new AnswerOption { Id = 49, Text = "'==' jämför värde, '===' jämför värde och typ", IsCorrect = true, QuestionId = 13 },
                new AnswerOption { Id = 50, Text = "'===' jämför värde, '==' jämför värde och typ", IsCorrect = false, QuestionId = 13 },
                new AnswerOption { Id = 51, Text = "De är identiska", IsCorrect = false, QuestionId = 13 },
                new AnswerOption { Id = 52, Text = "'==' används för siffror, '===' för text", IsCorrect = false, QuestionId = 13 },

                // Question 14: Hur skapar man en funktion i JavaScript?
                new AnswerOption { Id = 53, Text = "function namn() {} eller const namn = () => {}", IsCorrect = true, QuestionId = 14 },
                new AnswerOption { Id = 54, Text = "def namn():", IsCorrect = false, QuestionId = 14 },
                new AnswerOption { Id = 55, Text = "create function namn()", IsCorrect = false, QuestionId = 14 },
                new AnswerOption { Id = 56, Text = "new function namn()", IsCorrect = false, QuestionId = 14 },

                // Question 15: Vad är skillnaden på 'let' och 'const'?
                new AnswerOption { Id = 57, Text = "'let' kan ändras, 'const' är konstant", IsCorrect = true, QuestionId = 15 },
                new AnswerOption { Id = 58, Text = "'const' kan ändras, 'let' är konstant", IsCorrect = false, QuestionId = 15 },
                new AnswerOption { Id = 59, Text = "De är identiska", IsCorrect = false, QuestionId = 15 },
                new AnswerOption { Id = 60, Text = "'let' används för siffror, 'const' för text", IsCorrect = false, QuestionId = 15 },

                // Question 16: Du har flera knappar <button> på sidan och vill att alla ska ha blå bakgrund. Hur selektera du dem i CSS?
                new AnswerOption { Id = 61, Text = "button { background-color: blue; }", IsCorrect = true, QuestionId = 16 },
                new AnswerOption { Id = 62, Text = "#button { background-color: blue; }", IsCorrect = false, QuestionId = 16 },
                new AnswerOption { Id = 63, Text = ".button { background-color: blue; }", IsCorrect = false, QuestionId = 16 },
                new AnswerOption { Id = 64, Text = "<button style=\"background-color: blue;\">", IsCorrect = false, QuestionId = 16 },

                // Question 17: Hur implementerar man dark mode på en webbsida?
                new AnswerOption { Id = 65, Text = "Med CSS variabler och media queries eller JavaScript", IsCorrect = true, QuestionId = 17 },
                new AnswerOption { Id = 66, Text = "Endast med HTML-attribut", IsCorrect = false, QuestionId = 17 },
                new AnswerOption { Id = 67, Text = "Webbläsaren hanterar det automatiskt", IsCorrect = false, QuestionId = 17 },
                new AnswerOption { Id = 68, Text = "Det går inte att implementera", IsCorrect = false, QuestionId = 17 },

                // Question 18: När du klickar på en knapp byter den färg. Vilket språk hanterar detta?
                new AnswerOption { Id = 69, Text = "JavaScript", IsCorrect = true, QuestionId = 18 },
                new AnswerOption { Id = 70, Text = "HTML", IsCorrect = false, QuestionId = 18 },
                new AnswerOption { Id = 71, Text = "CSS", IsCorrect = false, QuestionId = 18 },
                new AnswerOption { Id = 72, Text = "PHP", IsCorrect = false, QuestionId = 18 },

                // Question 19: En knapp ska ändra bakgrundsfärg när man för muspekaren över den. Vilket språk skapar denna hover-effekt?
                new AnswerOption { Id = 73, Text = "CSS", IsCorrect = true, QuestionId = 19 },
                new AnswerOption { Id = 74, Text = "JavaScript", IsCorrect = false, QuestionId = 19 },
                new AnswerOption { Id = 75, Text = "HTML", IsCorrect = false, QuestionId = 19 },
                new AnswerOption { Id = 76, Text = "PHP", IsCorrect = false, QuestionId = 19 },

                // Question 20: Du har flera knappar med klassen btn. Du vill göra alla knappar gröna. Vilken CSS-selektor används?
                new AnswerOption { Id = 77, Text = ".btn { background-color: green; }", IsCorrect = true, QuestionId = 20 },
                new AnswerOption { Id = 78, Text = "#btn { background-color: green; }", IsCorrect = false, QuestionId = 20 },
                new AnswerOption { Id = 79, Text = "btn { background-color: green; }", IsCorrect = false, QuestionId = 20 },
                new AnswerOption { Id = 80, Text = "button.btn { color: green; }", IsCorrect = false, QuestionId = 20 }
            };

            modelBuilder.Entity<AnswerOption>().HasData(answerOptions);
        }
    }
}