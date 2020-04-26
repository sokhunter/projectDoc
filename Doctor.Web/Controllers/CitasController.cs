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
    public class CitasController : ControllerBase
    {
        private readonly DbDocContext _context;

        public CitasController(DbDocContext context)
        {
            _context = context;
        }

    }
}
