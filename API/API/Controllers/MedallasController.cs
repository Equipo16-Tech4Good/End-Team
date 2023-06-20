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

        [HttpGet("token_{token}")]
        public async Task<ActionResult<MedallasDTO>> GetMedallasCount(string token)
        {
            if (_context.Usuarios == null || _context.Medallas == null)
            {
                return NotFound();
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

            MedallasDTO medallas = new MedallasDTO();

            usuario.Medallas.ForEach(m =>
            {
                if (m.NivelMedallaId == 1)
                    medallas.CountOro++;
                else if (m.NivelMedallaId == 2)
                    medallas.CountPlata++;
                else 
                    medallas.CountBronce++;
            });

            return medallas;
        }

        private bool MedallaExists(int id)
        {
          return (_context.Medallas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
