using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Context;
using API.Model.Entity;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MensajesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Mensaje>> RandomTip()
        {
            Mensaje? randomMessage = _context.Mensajes.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            if (randomMessage != null)
            {
                return randomMessage;
            }
            else
            {
                return NotFound(); // Si no se encuentra ningún mensaje en la base de datos
            }
        }

        private bool MensajeExists(int id)
        {
          return (_context.Mensajes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
