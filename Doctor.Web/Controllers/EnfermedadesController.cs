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
    public class EnfermedadesController : ControllerBase
    {
        private readonly DbDocContext _context;

        public EnfermedadesController(DbDocContext context)
        {
            _context = context;
        }
        // GET: api/Enfermedades/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<EnfermedadViewModel>> List()
        {
            var enfermedadList = await _context.Enfermedades.ToListAsync();

            return enfermedadList.Select(c => new EnfermedadViewModel
            {
                EnfermedadId = c.EnfermedadId,
                Nombre = c.Nombre
            });
        }

        // GET: api/Enfermedades/Show/5
        [HttpGet("[action]/{EnfermedadId}")]
        public async Task<ActionResult<Enfermedad>> Show([FromRoute] int EnfermedadId)
        {
            var enfermedad = await _context.Enfermedades.FindAsync(EnfermedadId);

            if (enfermedad == null)//Si es que no existe
            {

                return NotFound(); //NotFound404
            }

            return Ok(new EnfermedadViewModel
            {
                EnfermedadId = enfermedad.EnfermedadId,
                Nombre = enfermedad.Nombre,
            });
        }
    }
}
