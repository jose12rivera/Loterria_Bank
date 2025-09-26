using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Loterria_Bank.DAL;

namespace Loterria_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadasController : ControllerBase
    {
        private readonly Contexto _context;

        public JugadasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Jugadas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Jugadas>>> GetJugadas()
        {
            return await _context.Jugadas
                                 .Include(j => j.Loteria)
                                 .Include(j => j.Detalles)
                                 .ToListAsync();
        }

        // GET: api/Jugadas/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Jugadas>> GetJugada(int id)
        {
            var jugada = await _context.Jugadas
                                       .Include(j => j.Loteria)
                                       .Include(j => j.Detalles)
                                       .FirstOrDefaultAsync(j => j.JugadaId == id);

            if (jugada == null)
                return NotFound();

            return jugada;
        }

        // PUT: api/Jugadas/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutJugada(int id, Jugadas jugada)
        {
            if (id != jugada.JugadaId)
                return BadRequest("El ID de la URL no coincide con el de la entidad.");

            _context.Entry(jugada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JugadaExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Jugadas
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Jugadas>> PostJugada(Jugadas jugada)
        {
            _context.Jugadas.Add(jugada);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJugada), new { id = jugada.JugadaId }, jugada);
        }

        // DELETE: api/Jugadas/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteJugada(int id)
        {
            var jugada = await _context.Jugadas.FindAsync(id);
            if (jugada == null)
                return NotFound();

            _context.Jugadas.Remove(jugada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JugadaExists(int id)
        {
            return _context.Jugadas.Any(e => e.JugadaId == id);
        }
    }
}
