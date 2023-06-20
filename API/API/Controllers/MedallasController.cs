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
    public class MedallasController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MedallasController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetByToken/token_{token}")]
        public async Task<ActionResult<ResponseMedallas>> GetMedallasCount(string token)
        {
            if (_context.Usuarios == null || _context.Medallas == null)
            {
                return new ResponseMedallas
                {
                    Mensaje = "Bad Request",
                    Status = (int)HttpStatusCode.NotFound,
                    Data = null
                };
            }

            string email = token;

            var usuario = await _context.Usuarios
                .Where(x => x.Email == email)
                .Include(x => x.Medallas)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            MedallasDeUsuarioDTO medallas = new MedallasDeUsuarioDTO();

            usuario.Medallas.ForEach(m =>
            {
                NivelMedalla? nm = _context.NivelesMedallas.Where(x => x.Id== m.NivelMedallaId).FirstOrDefault();

                if (nm != null)
                {
                    if (m.Titulo == "Oro")
                        medallas.CountOro++;
                    else if (m.Titulo == "Plata")
                        medallas.CountPlata++;
                    else
                        medallas.CountBronce++;
                }
            });

            return new ResponseMedallas
            {
                Mensaje = "Recuento de medallas",
                Status = (int) HttpStatusCode.OK,
                Data = medallas
            };
        }

        [HttpPost("Post")]
        public async Task<ActionResult<ResponseBoolean>> Post(MedallaDTO model)
        {
            ResponseBoolean badRequest = new ResponseBoolean
            {
                Mensaje = "Bad Request",
                Status = (int)HttpStatusCode.NotFound,
                Data = false
            };

            if (_context.Medallas == null)
            {
                return badRequest;
            }

            Usuario usuario = _context.Usuarios.Where(x => x.Email == model.UsuarioToken).FirstOrDefault();
            Medalla m = new Medalla
            {
                UsuarioId = usuario.Id,
                NivelMedallaId = model.NivelMedallaId
            };

            _context.Medallas.Add(m);
            await _context.SaveChangesAsync();

            return new ResponseBoolean
            {
                Mensaje = "Medalla creada de manera satisfactoria",
                Status = (int) HttpStatusCode.OK,
                Data = true
            };
        }
    }
}
