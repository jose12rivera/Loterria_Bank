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
    public class LoteriasController : ControllerBase
    {
        private readonly Contexto _context;

        public LoteriasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Loterias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loterias>>> GetLoterias()
        {
            return await _context.Loterias.ToListAsync();
        }

        // GET: api/Loterias/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Loterias>> GetLoteria(int id)
        {
            var loteria = await _context.Loterias.FindAsync(id);

            if (loteria == null)
                return NotFound();

            return loteria;
        }

        // PUT: api/Loterias/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutLoteria(int id, Loterias loteria)
        {
            if (id != loteria.LoteriaId)
                return BadRequest("El ID de la URL no coincide con el de la entidad.");

            _context.Entry(loteria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoteriaExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // POST: api/Loterias
        [HttpPost]
        public async Task<ActionResult<Loterias>> PostLoteria(Loterias loteria)
        {
            _context.Loterias.Add(loteria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLoteria), new { id = loteria.LoteriaId }, loteria);
        }

        // DELETE: api/Loterias/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLoteria(int id)
        {
            var loteria = await _context.Loterias.FindAsync(id);
            if (loteria == null)
                return NotFound();

            _context.Loterias.Remove(loteria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoteriaExists(int id)
        {
            return _context.Loterias.Any(e => e.LoteriaId == id);
        }
    }
}
