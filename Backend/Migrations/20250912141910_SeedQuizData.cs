using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedQuizData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerOptionId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "LevelNumber", "PassingScore", "Title", "Track" },
                values: new object[,]
                {
                    { 1, 1, 5, "HTML Grundläggande", 1 },
                    { 2, 1, 10, "CSS Grundläggande", 2 },
                    { 3, 1, 15, "JavaScript Grundläggande", 3 },
                    { 4, 1, 20, "Kombinerad Grundläggande", 4 }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Order", "QuizId", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, "Vad står HTML för?" },
                    { 2, 2, 1, "Vilken tagg används för att skapa en rubrik?" },
                    { 3, 3, 1, "Vad är den korrekta syntaxen för att skapa en länk i HTML?" },
                    { 4, 4, 1, "Vilken tagg används för att skapa en oordnad lista?" },
                    { 5, 5, 1, "Vad är DOCTYPE-deklarationen till för?" },
                    { 6, 1, 2, "Vad står CSS för?" },
                    { 7, 2, 2, "Hur lägger man till CSS i ett HTML-dokument?" },
                    { 8, 3, 2, "Vilken CSS-egenskap används för att ändra textfärg?" },
                    { 9, 4, 2, "Vad är skillnaden mellan margin och padding?" },
                    { 10, 5, 2, "Vilken CSS-selektor har högst specificitet?" },
                    { 11, 1, 3, "Vad används 'var', 'let' och 'const' för i JavaScript?" },
                    { 12, 2, 3, "Vilken metod används för att skriva ut text i konsolen?" },
                    { 13, 3, 3, "Vad är skillnaden mellan '==' och '===' i JavaScript?" },
                    { 14, 4, 3, "Hur skapar man en funktion i JavaScript?" },
                    { 15, 5, 3, "Vad är skillnaden på 'let' och 'const'?" },
                    { 16, 1, 4, "Du har flera knappar <button> på sidan och vill att alla ska ha blå bakgrund. Hur selektera du dem i CSS?" },
                    { 17, 2, 4, "Hur implementerar man dark mode på en webbsida?" },
                    { 18, 3, 4, "När du klickar på en knapp byter den färg. Vilket språk hanterar detta?" },
                    { 19, 4, 4, "En knapp ska ändra bakgrundsfärg när man för muspekaren över den. Vilket språk skapar denna hover-effekt?" },
                    { 20, 5, 4, "Du har flera knappar med klassen btn. Du vill göra alla knappar gröna. Vilken CSS-selektor används?" }
                });

            migrationBuilder.InsertData(
                table: "AnswerOptions",
                columns: new[] { "Id", "IsCorrect", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, true, 1, "HyperText Markup Language" },
                    { 2, false, 1, "High Tech Modern Language" },
                    { 3, false, 1, "Home Tool Markup Language" },
                    { 4, false, 1, "Hyperlink and Text Markup Language" },
                    { 5, true, 2, "<h1> till <h6>" },
                    { 6, false, 2, "<header>" },
                    { 7, false, 2, "<title>" },
                    { 8, false, 2, "<heading>" },
                    { 9, true, 3, "<a href=\"url\">Länktext</a>" },
                    { 10, false, 3, "<link url=\"url\">Länktext</link>" },
                    { 11, false, 3, "<a url=\"url\">Länktext</a>" },
                    { 12, false, 3, "<href=\"url\">Länktext</href>" },
                    { 13, true, 4, "<ul>" },
                    { 14, false, 4, "<ol>" },
                    { 15, false, 4, "<li>" },
                    { 16, false, 4, "<list>" },
                    { 17, true, 5, "För att tala om för webbläsaren vilken version av HTML som används" },
                    { 18, false, 5, "För att länka till externa CSS-filer" },
                    { 19, false, 5, "För att definiera dokumentets titel" },
                    { 20, false, 5, "För att importera JavaScript-filer" },
                    { 21, true, 6, "Cascading Style Sheets" },
                    { 22, false, 6, "Computer Style Sheets" },
                    { 23, false, 6, "Creative Style Sheets" },
                    { 24, false, 6, "Colorful Style Sheets" },
                    { 25, true, 7, "Inline, internal eller external" },
                    { 26, false, 7, "Endast genom externa filer" },
                    { 27, false, 7, "Endast genom style-attributet" },
                    { 28, false, 7, "Endast genom <style>-taggen" },
                    { 29, true, 8, "color" },
                    { 30, false, 8, "text-color" },
                    { 31, false, 8, "font-color" },
                    { 32, false, 8, "background-color" },
                    { 33, true, 9, "Margin är utrymmet utanför elementet, padding är utrymmet inuti" },
                    { 34, false, 9, "Padding är utrymmet utanför elementet, margin är utrymmet inuti" },
                    { 35, false, 9, "De är samma sak" },
                    { 36, false, 9, "Margin används för text, padding för bilder" },
                    { 37, true, 10, "ID-selektor (#id)" },
                    { 38, false, 10, "Klass-selektor (.class)" },
                    { 39, false, 10, "Element-selektor (div)" },
                    { 40, false, 10, "Universal-selektor (*)" },
                    { 41, true, 11, "För att deklarera variabler" },
                    { 42, false, 11, "För att skapa funktioner" },
                    { 43, false, 11, "För att importera moduler" },
                    { 44, false, 11, "För att skapa loopar" },
                    { 45, true, 12, "console.log()" },
                    { 46, false, 12, "print()" },
                    { 47, false, 12, "document.write()" },
                    { 48, false, 12, "alert()" },
                    { 49, true, 13, "'==' jämför värde, '===' jämför värde och typ" },
                    { 50, false, 13, "'===' jämför värde, '==' jämför värde och typ" },
                    { 51, false, 13, "De är identiska" },
                    { 52, false, 13, "'==' används för siffror, '===' för text" },
                    { 53, true, 14, "function namn() {} eller const namn = () => {}" },
                    { 54, false, 14, "def namn():" },
                    { 55, false, 14, "create function namn()" },
                    { 56, false, 14, "new function namn()" },
                    { 57, true, 15, "'let' kan ändras, 'const' är konstant" },
                    { 58, false, 15, "'const' kan ändras, 'let' är konstant" },
                    { 59, false, 15, "De är identiska" },
                    { 60, false, 15, "'let' används för siffror, 'const' för text" },
                    { 61, true, 16, "button { background-color: blue; }" },
                    { 62, false, 16, "#button { background-color: blue; }" },
                    { 63, false, 16, ".button { background-color: blue; }" },
                    { 64, false, 16, "<button style=\"background-color: blue;\">" },
                    { 65, true, 17, "Med CSS variabler och media queries eller JavaScript" },
                    { 66, false, 17, "Endast med HTML-attribut" },
                    { 67, false, 17, "Webbläsaren hanterar det automatiskt" },
                    { 68, false, 17, "Det går inte att implementera" },
                    { 69, true, 18, "JavaScript" },
                    { 70, false, 18, "HTML" },
                    { 71, false, 18, "CSS" },
                    { 72, false, 18, "PHP" },
                    { 73, true, 19, "CSS" },
                    { 74, false, 19, "JavaScript" },
                    { 75, false, 19, "HTML" },
                    { 76, false, 19, "PHP" },
                    { 77, true, 20, ".btn { background-color: green; }" },
                    { 78, false, 20, "#btn { background-color: green; }" },
                    { 79, false, 20, "btn { background-color: green; }" },
                    { 80, false, 20, "button.btn { color: green; }" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "AnswerOptionId",
                table: "UserAnswers");
        }
    }
}
