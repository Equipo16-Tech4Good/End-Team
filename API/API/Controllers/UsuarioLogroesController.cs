﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Context;
using API.Model.Entity;
using API.Model.DTOs;

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

        [HttpGet("token_{token}")]
        public async Task<ActionResult<IEnumerable<LogroDTO>>> GetLogrosByUsuario(string token)
        {
            if (_context.Usuarios == null || _context.UsuariosLogros == null)
            {
                return NotFound();
            }

            string email = token;

            var usuario = await _context.Usuarios
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
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

            return logros;
        }

        private bool UsuarioLogroExists(int id)
        {
          return (_context.UsuariosLogros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
