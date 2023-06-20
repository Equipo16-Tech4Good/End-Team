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
using Microsoft.CodeAnalysis;
using System.Net;
using NuGet.Protocol;
using API.Model.Responses;

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

        [HttpGet("GetUsuario/token_{token}")]
        public async Task<ActionResult<ResponseUser>> GetUsuario(string token)
        {
            ResponseUser badResponse = new ResponseUser
            {
                Mensaje = "Bad Request",
                Status = (int) HttpStatusCode.NotFound,
                Data = null
            };

            if (_context.Usuarios == null)
                return badResponse;

            string email = token;

            var usuario = GetByEmail(email);

            if (usuario == null)
                return badResponse;
            
            ResponseUser response = new ResponseUser
            {
                Mensaje = "Usuario ",
                Status = (int) HttpStatusCode.OK,
                Data = usuario
            };

            return response;
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

            return Ok(model.Email);
        }

       
        [HttpGet("Login")]
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

        private Usuario? GetByEmail(string email)
        {
            return _context.Usuarios.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
