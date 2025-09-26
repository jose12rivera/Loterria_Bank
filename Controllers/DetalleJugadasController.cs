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
    public class DetalleJugadasController : ControllerBase
    {
        private readonly Contexto _context;

        public DetalleJugadasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/DetalleJugadas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DetalleJugadas>>> GetDetalleJugadas()
        {
            return await _context.DetalleJugadas
                                 .Include(d => d.Jugada)
                                 .ToListAsync();
        }

        // GET: api/DetalleJugadas/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetalleJugadas>> GetDetalleJugada(int id)
        {
            var detalle = await _context.DetalleJugadas
                                        .Include(d => d.Jugada)
                                        .FirstOrDefaultAsync(d => d.DetalleJugadaId == id);

            if (detalle == null)
                return NotFound();

            return detalle;
        }

        // PUT: api/DetalleJugadas/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutDetalleJugada(int id, DetalleJugadas detalleJugadas)
        {
            if (id != detalleJugadas.DetalleJugadaId)
                return BadRequest("El ID de la URL no coincide con el de la entidad.");

            _context.Entry(detalleJugadas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleJugadaExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/DetalleJugadas
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<DetalleJugadas>> PostDetalleJugada(DetalleJugadas detalleJugadas)
        {
            _context.DetalleJugadas.Add(detalleJugadas);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalleJugada), new { id = detalleJugadas.DetalleJugadaId }, detalleJugadas);
        }

        // DELETE: api/DetalleJugadas/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDetalleJugada(int id)
        {
            var detalleJugadas = await _context.DetalleJugadas.FindAsync(id);
            if (detalleJugadas == null)
                return NotFound();

            _context.DetalleJugadas.Remove(detalleJugadas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleJugadaExists(int id)
        {
            return _context.DetalleJugadas.Any(e => e.DetalleJugadaId == id);
        }
    }
}
