using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Doctor.Data;
using Doctor.Entities;
using Doctor.Web.Models;

namespace Doctor.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenesController : ControllerBase
    {
        private readonly DbDocContext _context;

        public ExamenesController(DbDocContext context)
        {
            _context = context;
        }

        // GET: api/Examenes/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<ExamenViewModel>> List()
        {
            var examenList = await _context.Examenes.ToListAsync();

            return examenList.Select(c => new ExamenViewModel
            {
                ExamenId = c.ExamenId,
                Nombre = c.Nombre
            });
        }

        // GET: api/Examenes/Show/5
        [HttpGet("[action]/{ExamenId}")]
        public async Task<ActionResult<Examen>> Show([FromRoute] int ExamenId)
        {
            var examen = await _context.Examenes.FindAsync(ExamenId);

            if (examen == null)//Si es que no existe
            {

                return NotFound(); //NotFound404
            }

            return Ok(new ExamenViewModel
            {
                ExamenId = examen.ExamenId,
                Nombre = examen.Nombre,
            });
        }
    }
}
