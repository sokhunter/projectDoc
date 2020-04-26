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
    public class PacientesController : ControllerBase
    {
        private readonly DbDocContext _context;

        public PacientesController(DbDocContext context)
        {
            _context = context;
        }

        // GET: api/Pacientes/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<PacienteViewModel>> List()
        {
            var pacienteList = await _context.Pacientes.ToListAsync();

            return pacienteList.Select(c => new PacienteViewModel
            {
                PacienteId = c.PacienteId,
                Nombre = c.Nombre,
                ApellidoPaterno = c.ApellidoPaterno,
                ApellidoMaterno = c.ApellidoMaterno,
                TipoDocumentoId = c.TipoDocumentoId,
                NumeroDocumento = c.NumeroDocumento,
                Direccion = c.Direccion,
                Sexo = c.Sexo,
                FechaNacimiento = c.FechaNacimiento,
                Telefono = c.Telefono,
                Celular = c.Celular,
                Correo = c.Correo,
            });
        }

        // GET: api/Pacientes/Show/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Paciente>> Show([FromRoute] int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)//Si es que no existe
            {

                return NotFound(); //NotFound404
            }

            return Ok(new PacienteViewModel
            {
                PacienteId = paciente.PacienteId,
                Nombre = paciente.Nombre,
                ApellidoPaterno = paciente.ApellidoPaterno,
                ApellidoMaterno = paciente.ApellidoMaterno,
                TipoDocumentoId = paciente.TipoDocumentoId,
                NumeroDocumento = paciente.NumeroDocumento,
                Direccion = paciente.Direccion,
                Sexo = paciente.Sexo,
                FechaNacimiento = paciente.FechaNacimiento,
                Telefono = paciente.Telefono,
                Celular = paciente.Celular,
                Correo = paciente.Correo
            });
        }

        // GET: api/Pacientes/ShowName/R00t 
        [HttpGet("[action]/{Nombre}")]
        public async Task<ActionResult<Paciente>> ShowName([FromRoute] string Nombre)
        {
            // Proyecto proyecto = _context.Proyectos.Where(p => p.NombreProyecto.Equals(pNombreProyecto)).FirstOrDefault();

            var paciente = from m in _context.Pacientes select m;

            if (!String.IsNullOrEmpty(Nombre))
            {
                paciente = paciente.Where(s => s.Nombre.Equals(Nombre));
            }


            if (paciente.Count() == 0)//Si es que no existe
            {
                return NotFound(); //NotFound404
            }

            return Ok(await paciente.ToListAsync());

        }


        // PUT: api/Pacientes/PUpdate/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("[action]/{PacienteId}")]
        public async Task<IActionResult> PUpdate([FromBody] UpdatePacienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //error 404
            }

            if (model.PacienteId <= 0)
            {
                return BadRequest();
            }

            var cat = await _context.Pacientes.FirstOrDefaultAsync(c => c.PacienteId == model.PacienteId); //FirstOrDefaultAsync el primer objeto que coincide

            if (cat == null)
            {
                return NotFound();
            }

            cat.Nombre = model.Nombre;
            cat.ApellidoPaterno = model.ApellidoPaterno;
            cat.ApellidoMaterno = model.ApellidoMaterno;
            cat.Direccion = model.Direccion;
            cat.Telefono = model.Telefono;
            cat.Celular = model.Celular;
            cat.Correo = model.Correo;

            try
            {//await es para que espere
                await _context.SaveChangesAsync(); //Para guardar los cambios
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
            return Ok();
        }

        // POST: api/Pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("[action]")]
        public async Task<ActionResult> Create([FromBody] CreatePacienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //error 404
            }

            Paciente pro = new Paciente
            {
                Nombre = model.Nombre,
                ApellidoPaterno = model.ApellidoPaterno,
                ApellidoMaterno = model.ApellidoMaterno,
                TipoDocumentoId = model.TipoDocumentoId,
                NumeroDocumento = model.NumeroDocumento,
                Direccion = model.Direccion,
                Sexo = model.Sexo,
                FechaNacimiento = model.FechaNacimiento,
                Telefono = model.Telefono,
                Celular = model.Celular,
                Correo = model.Correo,
            };

            _context.Pacientes.Add(pro); //agregamos, el objeto lo pongo en un insert

            try
            {
                await _context.SaveChangesAsync(); //acá guarda en db recién, acá ejecuta el insert recién
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
            return Ok();
        }

        // DELETE: api/Pacientes/Delete/5
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //error 404
            }

            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }
            _context.Pacientes.Remove(paciente); //pone la query

            try
            {
                await _context.SaveChangesAsync(); //acá guarda en db recién, acá ejecuta el el remove recién
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
            return Ok();
        }

    }
}
