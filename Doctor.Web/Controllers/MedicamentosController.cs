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
    public class MedicamentosController : ControllerBase
    {
        private readonly DbDocContext _context;

        public MedicamentosController(DbDocContext context)
        {
            _context = context;
        }

        // GET: api/Medicamentos/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<MedicamentoViewModel>> List()
        {
            var medicamentoList = await _context.Medicamentos.ToListAsync();

            return medicamentoList.Select(c => new MedicamentoViewModel
            {
                MedicamentoId = c.MedicamentoId,
                Nombre = c.Nombre
            });
        }

        // GET: api/Medicamentos/Show/5
        [HttpGet("[action]/{MedicamentoId}")]
        public async Task<ActionResult<Medicamento>> Show([FromRoute] int MedicamentoId)
        {
            var medicamento = await _context.Medicamentos.FindAsync(MedicamentoId);

            if (medicamento == null)//Si es que no existe
            {

                return NotFound(); //NotFound404
            }

            return Ok(new MedicamentoViewModel
            {
                MedicamentoId = medicamento.MedicamentoId,
                Nombre = medicamento.Nombre,
            });
        }
    }
}
