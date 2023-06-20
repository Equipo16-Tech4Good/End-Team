using Microsoft.AspNetCore.Mvc;
using API.Context;
using API.Model.Entity;
using API.Model.DTOs;
using Microsoft.CodeAnalysis;
using System.Net;
using API.Model.Responses;
using Microsoft.EntityFrameworkCore;

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

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return badResponse;
            }

            ResponseUser response = new ResponseUser
            {
                Mensaje = "Usuario Encontrado de manera satisfactoria",
                Status = (int) HttpStatusCode.OK,
                Data = usuario
            };

            return response;
        }


        [HttpPost("SingUp")]
        public async Task<ActionResult<ResponseBoolean>> SignUp(UsuarioDTO model)
        {
            if (_context.Usuarios == null)
            {
                return new ResponseBoolean
                {
                    Mensaje = "Bad request ",
                    Status = (int)HttpStatusCode.NotFound,
                    Data = false
                };
            }

            if (GetByEmail(model.Email) != null)
            {
                return new ResponseBoolean
                {
                    Mensaje = "Email ya esta registrado",
                    Status = (int) HttpStatusCode.NotFound,
                    Data = false
                };
            }

            Usuario usuario = new Usuario();
            usuario.AddModelInfo(model);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new ResponseBoolean
            {
                Mensaje = "Usuario creado de manera satisfactoria",
                Status = (int ) HttpStatusCode.OK,
                Data = true
            };
        }

     
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseToken>> Login(LoginDTO loginDTO)
        {
            ResponseToken badRequest = new ResponseToken
            {
                Status = (int)HttpStatusCode.NotFound,
                Data = null
            };


            if (_context.Usuarios == null)
            {
                badRequest.Mensaje = "Bad Request";
                return badRequest;
            }

            Usuario? usuario = GetByEmail(loginDTO.Email);

            if (usuario == null)
            {
                if (usuario.Psswrd == loginDTO.Psswrd)
                {
                    return new ResponseToken
                    {
                        Mensaje = "Usuario encontrado de manera satisfactoria",
                        Status = (int)HttpStatusCode.OK,
                        Data = usuario.Email
                    };
                }
                else
                {
                    badRequest.Mensaje = "Password Erronea";
                    return badRequest;
                }
            }

            badRequest.Mensaje = "Usuario no encontrado";
            return badRequest;
        }


        [HttpPut("Update")]
        public async Task<ActionResult<ResponseBoolean>> Update(UpdateUserDTO updatedUser)
        {
            ResponseBoolean badRequest = new ResponseBoolean
            {
                Status = (int)HttpStatusCode.NotFound,
                Data = false
            };

            if (_context.Usuarios == null)
            {
                badRequest.Mensaje = "Bad Request";
                return badRequest;
            }

            string email = updatedUser.Token;
            Usuario? u = GetByEmail(email);

            if (u == null)
            {
                badRequest.Mensaje = "Usuario no encontrado";
                return badRequest;
            }

            u.RachaConexion = updatedUser.RachaConexion;
            u.NivelEstanque = updatedUser.NivelEstanque;

            return new ResponseBoolean
            {
                Mensaje = "Se ha actuualoizado el usuario de manera satisfactoria",
                Status = (int)HttpStatusCode.OK,
                Data = true
            };
        }

        private Usuario? GetByEmail(string email)
        {
            return _context.Usuarios.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
