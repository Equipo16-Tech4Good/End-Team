using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Context;
using API.Model.Entity;
using API.Model.Responses;
using System.Net;

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

        [HttpGet("RandomTip")]
        public async Task<ActionResult<ResponseMensaje>> RandomTip()
        {
            Mensaje? randomMessage = _context.Mensajes.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            if (randomMessage != null)
            {
                return new ResponseMensaje
                {
                    Mensaje = "Mensaje Encontrado",
                    Status = (int)HttpStatusCode.OK,
                    Data = randomMessage
                };
            }
            else
            {
                return new ResponseMensaje
                {
                    Mensaje = "Bad Request",
                    Status = (int)HttpStatusCode.NotFound,
                    Data = null
                };
            }
        }
    }
}
