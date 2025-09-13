using System.Collections.Generic;
//central källa för faktatexter kopplade till Question.Id (från seeden).
namespace Backend.Content
{
    public static class QuestionFacts
    {
        public static readonly IReadOnlyDictionary<int, string> Facts = new Dictionary<int, string>
        {
            [1] = @"HTML står för HyperText Markup Language: hypertext = länkade dokument, markup = taggar som beskriver struktur.",
            [2] = @"Rubriker i HTML görs med <h1> till <h6>. <h1> är störst och används för sidans huvudrubrik.",
            [3] = @"Länkar skapas med <a> och adressen sätts i href: <a href=""https://exempel.se"">Länktext</a>.
Skriv en beskrivande länktext (inte bara “klicka här”). Du kan länka externt, internt (#ankare) eller till e-post (mailto:).",
            [4] = "Oordnade listor görs med <ul> (unordered list), och varje punkt är ett <li>. Använd <ul> när ordningen inte spelar roll (inköpslista). Om ordningen är viktig (receptsteg) använder du <ol>.",
            [5] = @"DOCTYPE berättar för webbläsaren vilken HTML-standard som används. I dag: <!DOCTYPE html> för standardläge.",
            [6] = @"CSS betyder Cascading Style Sheets: stilar “rinner ner” och avgör hur HTML visas.",
            [7] = @"CSS kan läggas inline (style), internt (<style>) eller externt (länkad .css). Externa filer är mest återanvändbara.",
            [8] = @"Egenskapen color styr textfärgen. background-color styr bakgrundsfärgen.",
            [9] = @"Margin är utrymmet UTANFÖR elementets kant; padding är utrymmet INNANFÖR kanten.",
            [10] = @"ID-selektorn (#id) har högre specificitet än .klass och elementselektor.",
            [11] = @"var/let/const deklarerar variabler. let/const har block-scope; const kan inte ombindas.",
            [12] = @"console.log() skriver till webbläsarens utvecklarkonsol och används för felsökning.",
            [13] = @"== jämför värden med typomvandling; === jämför värde OCH typ (ingen automatisk omvandling).",
            [14] = @"Funktion skapas med function namn() { } eller arrow: const namn = () => { }.",
            [15] = @"let kan ombindas; const är en konstant referens (kan inte ombindas).",
            [16] = @"Typselektorn button matchar alla <button>: button { background-color: blue; }.",
            [17] = @"Dark mode kan göras med media queryn prefers-color-scheme (följer användarens systemtema) eller genom att toggla en .dark-klass via JavaScript.
Sätt gärna färger som CSS-variabler så blir teman enkla att byta.",
            [18] = @"Interaktivt färgbyte vid klick hanteras av JavaScript via händelser (addEventListener).",
            [19] = @"Hover-effekter görs i CSS med :hover, t.ex. button:hover { ... }.",
            [20] = @"Klass-selektorn .btn matchar alla element med class=""btn"": .btn { background-color: green; }."
        };
    }
}
