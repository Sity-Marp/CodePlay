using Backend.Data;
using Backend.Models;
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
                quiz.Track,        // t.ex. "HTML", "CSS", "JS"
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
        //    Bra när användaren har låst upp t.ex. level 2 och du vill hämta rätt quiz.

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
    }
}
