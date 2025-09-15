using Backend.Data;
using Backend.Models;
using Backend.Content;
using Microsoft.AspNetCore.Mvc;     
using Microsoft.EntityFrameworkCore; 
using System.Linq;                  

namespace Backend.Controllers
{
    
    /// QuizController ansvarar för att leverera QUIZ-DATA till frontend:
    /// 1) Hämta ett specifikt quiz (utan IsCorrect)
    /// 2) Lista quiz per track (HTML/CSS/JS) och/eller nivå
   
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _db;

        public QuizController(AppDbContext db)
        {
            _db = db;
        }


        // 1) HÄMTA ETT QUIZ MED FRÅGOR + SVARSALTERNATIV (SÄKERT)
       // GET /api/quiz/{id}
        //    Viktigt: Skicka ALDRIG IsCorrect till klienten (anti-fusk).
       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var quiz = await _db.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.AnswerOptions)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
                return NotFound(new { message = "Quiz hittades inte." });

            
            var safeDto = new
            {
                quiz.Id,
                quiz.Title,
                quiz.Track,        
                quiz.LevelNumber,  // nivånumret inom tracken
                quiz.PassingScore, // hur många rätt som krävs för att bli godkänd
                Questions = quiz.Questions
                    .OrderBy(q => q.Id) 
                    .Select(q => new
                    {
                        q.Id,
                        q.Text,
                        AnswerOptions = q.AnswerOptions
                            .OrderBy(o => o.Id)
                            .Select(o => new
                            {
                                o.Id,
                                o.Text
                               
                            })
                    })
            };

            return Ok(safeDto);
        }


        // 2) LISTA QUIZ FÖR NAVIGERING I UI
        //    GET /api/quiz/
        //    GET /api/quiz/
        //    Används för att bygga meny/lista i frontend.
        
        [HttpGet("by-track")]
        public async Task<IActionResult> GetQuizzesByTrack([FromQuery] TrackType track, [FromQuery] int? maxLevel)
        {

            var query = _db.Quizzes.AsQueryable()
                .Where(q => q.Track == track);

            if (maxLevel.HasValue)
                query = query.Where(q => q.LevelNumber <= maxLevel.Value);

            var items = await query
                .OrderBy(q => q.LevelNumber)
                .Select(q => new
                {
                    q.Id,
                    q.Title,
                    q.Track,
                    q.LevelNumber,
                    q.PassingScore
                   
                })
                .ToListAsync();

            return Ok(items);
        }


        // 3) LISTA QUIZ FÖR EN SPECIFIK NIVÅ I ETT TRACK
        //    GET /api/quiz/ baserat på track + level
        //    Bra när användaren har låst upp 

        [HttpGet("by-level")]
        public async Task<IActionResult> GetQuizzesByLevel([FromQuery] TrackType track, [FromQuery] int level)
        {
           

            if (level <= 0)
                return BadRequest(new { message = "Level måste vara större än 0." });

            var items = await _db.Quizzes
                .Where(q => q.Track == track && q.LevelNumber == level)
                .OrderBy(q => q.Id)
                .Select(q => new
                {
                    q.Id,
                    q.Title,
                    q.Track,
                    q.LevelNumber,
                    q.PassingScore
                })
                .ToListAsync();

            return Ok(items);
        }
        // Hämta fakta för ett helt spår (HTML/CSS/JavaScript). 
        [HttpGet("facts/by-track")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFactsByTrack([FromQuery] TrackType track, [FromQuery] int? level)
        {
            // 1) Hämta alla frågor som tillhör angivet track (och ev. nivå)
            // Vi går via Quizzes för att slippa navigera från Question->Quiz och undvika null navs.
            var quizQuery = _db.Quizzes
                .Where(q => q.Track == track); 

            if (level.HasValue)
                quizQuery = quizQuery.Where(q => q.LevelNumber == level.Value);

            var questions = await quizQuery
                .SelectMany(q => q.Questions
                    .Select(ques => new
                    {
                        q.Id,                    // QuizId
                        q.LevelNumber,           // nivå
                        QuestionId = ques.Id,    // frågans Id från seeden
                        ques.Order               // ordning inom quizet
                    }))
                .OrderBy(x => x.LevelNumber)
                .ThenBy(x => x.Order)
                .ToListAsync();

            // 2) Mappa fråge-Id -> fakta (via vår in-memory-karta QuestionFacts)
            //    Vi tar bara med de som har fakta (skulle någon saknas blir den filtrerad bort).
            var items = questions
                .Where(x => QuestionFacts.Facts.ContainsKey(x.QuestionId))
                .Select(x => new
                {
                    x.QuestionId,
                    Fact = QuestionFacts.Facts[x.QuestionId]
                })
                .ToList();

            // 3) Returnera ett enkelt, frontend-vänligt svar
            return Ok(new
            {
                Track = track.ToString(),    
                Level = level,               
                Count = items.Count,
                Items = items                
            });
        }
    }
}
