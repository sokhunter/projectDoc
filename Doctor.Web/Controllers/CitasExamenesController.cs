using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Doctor.Data;
using Doctor.Entities;

namespace Doctor.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasExamenesController : ControllerBase
    {
        private readonly DbDocContext _context;

        public CitasExamenesController(DbDocContext context)
        {
            _context = context;
        }

        // GET: api/CitasExamen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaExamen>>> GetCitaExamen()
        {
            return await _context.CitasExamenes.ToListAsync();
        }

        // GET: api/CitasExamen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitaExamen>> GetCitaExamen(int id)
        {
            var citaExamen = await _context.CitasExamenes.FindAsync(id);

            if (citaExamen == null)
            {
                return NotFound();
            }

            return citaExamen;
        }

        // PUT: api/CitasExamen/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitaExamen(int id, CitaExamen citaExamen)
        {
            if (id != citaExamen.CitaExamenId)
            {
                return BadRequest();
            }

            _context.Entry(citaExamen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExamenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CitasExamen
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CitaExamen>> PostCitaExamen(CitaExamen citaExamen)
        {
            _context.CitasExamenes.Add(citaExamen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitaExamen", new { id = citaExamen.CitaExamenId }, citaExamen);
        }

        // DELETE: api/CitasExamen/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CitaExamen>> DeleteCitaExamen(int id)
        {
            var citaExamen = await _context.CitasExamenes.FindAsync(id);
            if (citaExamen == null)
            {
                return NotFound();
            }

            _context.CitasExamenes.Remove(citaExamen);
            await _context.SaveChangesAsync();

            return citaExamen;
        }

        private bool CitaExamenExists(int id)
        {
            return _context.CitasExamenes.Any(e => e.CitaExamenId == id);
        }
    }
}
