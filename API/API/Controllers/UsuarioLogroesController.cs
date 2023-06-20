using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Context;
using API.Model.Entity;
using API.Model.DTOs;
using API.Model.Responses;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioLogroesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public UsuarioLogroesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetByToken/token_{token}")]
        public async Task<ActionResult<ResponseLogros>> GetLogrosByUsuario(string token)
        {
            ResponseLogros badRequest = new ResponseLogros
            {
                Mensaje = "Bad Request",
                Status = (int)HttpStatusCode.NotFound,
                Data = null
            };

            if (_context.Usuarios == null || _context.UsuariosLogros == null)
            {
                return badRequest;
            }

            string email = token;

            var usuario = await _context.Usuarios
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return badRequest;
            }

            List<LogroDTO> logros = new List<LogroDTO>();

            _context.UsuariosLogros.Where(x => x.UsuarioId == usuario.Id).ToList().ForEach(x =>
            {
                Logro? l = _context.Logros.Where(logro => logro.Id == x.LogroId).FirstOrDefault();

                if (l != null)
                {
                    LogroDTO ldto = new LogroDTO();
                    ldto.AddContentLogro(l);
                    logros.Add(ldto);
                }
            });

            return new ResponseLogros
            {
                Mensaje = "Se han encontrado todos los logros del token",
                Status = (int)HttpStatusCode.OK,
                Data = logros
            }; ;
        }


        [HttpPost("Post")]
        public async Task<ActionResult<ResponseBoolean>> Post(UsuarioLogroDTO model)
        {
            ResponseBoolean badRequest = new ResponseBoolean
            {
                Mensaje = "Bad Request",
                Status = (int)HttpStatusCode.NotFound,
                Data = false
            };

            if (_context.UsuariosLogros == null)
            {
                return badRequest;
            }

            Usuario usuario = _context.Usuarios.Where(x => x.Email == model.UsuarioToken).FirstOrDefault();
            UsuarioLogro ul = new UsuarioLogro
            {
                UsuarioId = usuario.Id,
                LogroId = model.LogroId
            };

            _context.UsuariosLogros.Add(ul);
            await _context.SaveChangesAsync();

            return new ResponseBoolean
            {
                Mensaje = "UsuarioLogro creado de manera satisfactoria",
                Status = (int)HttpStatusCode.OK,
                Data = true
            };
        }
        private bool UsuarioLogroExists(int id)
        {
          return (_context.UsuariosLogros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
