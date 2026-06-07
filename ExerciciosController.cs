using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P2_Desenv.Software.Data;
using P2_Desenv_Software.Models;

namespace P2_Desenv.Software
{
    [ApiController]
    [Route("exercicios")]
    public class ExerciciosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExerciciosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercicio>>> GetAll() =>
            await _context.Exercicios.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Exercicio>> GetById([FromRoute]int id) 
           {
            var exercicio = await _context.Exercicios.FindAsync(id);
            return exercicio == null ? NotFound() : Ok(exercicio);
            }

        [HttpPost]
        public async Task<ActionResult<Exercicio>> Post(Exercicio exercicio)
        {
            _context.Exercicios.Add(exercicio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = exercicio.Id }, exercicio);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Exercicio exercicio)
        {
            if (id != exercicio.Id) return BadRequest("ID do exercício não confere.");

            _context.Entry(exercicio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio == null) return NotFound();

            _context.Exercicios.Remove(exercicio);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

