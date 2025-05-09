using Microsoft.AspNetCore.Mvc;
using DevLiftNew.Data;
using DevLiftNew.Models;

namespace DevLiftNew.Controllers
{
    [ApiController]
    [Route("api/quiz")]
    public class QuizApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddQuestion([FromBody] QuizQuestion question)
        {
            if (question == null || string.IsNullOrWhiteSpace(question.FrageText))
                return BadRequest("Ungültige Frage");

            _context.QuizQuestions.Add(question);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, id = question.Id });
        }
    }
}