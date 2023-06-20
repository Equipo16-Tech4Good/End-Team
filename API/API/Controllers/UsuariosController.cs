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
    public class UsuariosController : Controller
    {
        private readonly ApplicationDBContext _context;

        public UsuariosController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("token_{token}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string token)
        {
            if (_context.Usuarios == null)
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

            return usuario;
        }

        [HttpGet("email_{email}")]
        public async Task<ActionResult<bool>> EmailExist(string email)
        {
            Usuario? u = GetByEmail(email);

            if (u != null)
                return true;
            else return false;
        }


        [HttpPost("SingUp")]
        public async Task<ActionResult<string>> SignUp(UsuarioDTO model)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Usuarios'  is null.");
            }

            Usuario usuario = new Usuario();
            usuario.AddModelInfo(model);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return model.Email;
        }

       
        [HttpGet("login")]
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Usuarios'  is null.");
            }

            Usuario? usuario = GetByEmail(loginDTO.Email);

            if (usuario == null)
            {
                if (usuario.Psswrd == loginDTO.Psswrd)
                    return loginDTO.Email;
            }
            return "Manolo";
        }
        

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private Usuario? GetByEmail(string email)
        {
            return _context.Usuarios.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
