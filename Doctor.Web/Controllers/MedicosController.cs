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
    public class MedicosController : ControllerBase
    {
        private readonly DbDocContext _context;

        public MedicosController(DbDocContext context)
        {
            _context = context;
        }

        // GET: api/Medicos/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<MedicoViewModel>> List()
        {
            var medicoList = await _context.Medicos.ToListAsync();

            return medicoList.Select(c => new MedicoViewModel
            {
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Usuario = c.Usuario,
                Clinica = c.Clinica
            });
        }

        // GET: api/Medicos/Show/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Medico>> Show([FromRoute] int id)
        {
            var medico = await _context.Medicos.FindAsync(id);

            if (medico == null)//Si es que no existe
            {
                return NotFound(); //NotFound404
            }

            if (medico.Nombre == null)
            {
                medico.Nombre = "Null";
            }

            if (medico.Apellido == null)
            {
                medico.Apellido = "Null";
            }

            if (medico.Usuario == null)
            {
                medico.Usuario = "Null";
            }

            if (medico.Password == null)
            {
                medico.Password = "Null";
            }
            return Ok(new MedicoViewModel
            {

                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Usuario = medico.Usuario,
                Clinica = medico.Clinica
            });
        }

        // GET: api/Medicos/obtenerNombre/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Medico>> obtenerNombre([FromRoute] int id)
        {
            var medico = await _context.Medicos.FindAsync(id);

            if (medico == null)//Si es que no existe
            {
                return NotFound(); //NotFound404
            }

            if (medico.Nombre == null)
            {
                medico.Nombre = "Null";
            }

            return Ok(new obtenerNombreViewModel
            {

                Nombre = medico.Nombre,
            });
        }


        // GET: api/Medicos/Login/correo=?&contrasena=? 
        [HttpGet("[action]/correo={CorreoUsuario}&contrasena={Contrasena}")]
        public async Task<ActionResult<Medico>> Login([FromRoute] string Usuario, [FromRoute] string Password)
        {
            // Proyecto proyecto = _context.Proyectos.Where(p => p.NombreProyecto.Equals(pNombreProyecto)).FirstOrDefault();

            var medico = from m in _context.Medicos select m;

            if (!String.IsNullOrEmpty(Usuario) && !String.IsNullOrEmpty(Password))
            {
                medico = medico.Where(s => s.Usuario.Equals(Usuario) && s.Password.Equals(Password));
            }


            if (medico.Count() == 0)//Si es que no existe
            {
                return NotFound(); //NotFound404
            }

            return Ok(await medico.ToListAsync());

        }


        // GET: api/Medicos/Show/R00t_5layer
        [HttpGet("[action]/{NombreUsuario}")]
        public async Task<ActionResult<Medico>> ShowName([FromRoute] string Usuario)
        {
            var medico = await _context.Medicos.FindAsync(Usuario);

            if (medico == null)//Si es que no existe
            {
                return NotFound(); //NotFound404
            }

            return Ok(new MedicoViewModel
            {
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Usuario = medico.Usuario,
                Clinica = medico.Clinica
            });
        }

        // PUT: api/Medicos/Update/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UUpdate([FromBody] UpdateMedicoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //error 404
            }

            if (model.MedicoId <= 0)
            {
                return BadRequest();
            }

            var use = await _context.Medicos.FirstOrDefaultAsync(c => c.MedicoId == model.MedicoId); //FirstOrDefaultAsync el primer objeto que coincide

            if (use == null)
            {
                return NotFound();
            }

            use.Nombre = model.Nombre;
            use.Apellido = model.Apellido;
            use.Clinica = model.Clinica;
            use.Usuario = model.Usuario;
            use.Password = model.Password;

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


        // POST: api/Medicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("[action]")]
        public async Task<ActionResult> Create([FromBody] CreateMedicoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //error 404
            }

            Medico use = new Medico
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Clinica = model.Clinica,
                Usuario = model.Usuario,
                Password = model.Password,
            };

            _context.Medicos.Add(use); //agregamos, el objeto lo pongo en un insert

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


    }
}
