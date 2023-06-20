using System.ComponentModel.DataAnnotations;

namespace API.Model.DTOs
{
    public class UsuarioDTO
    {
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Psswrd { get; set; }
    }
}