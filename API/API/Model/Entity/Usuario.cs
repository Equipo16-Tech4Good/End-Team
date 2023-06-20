using API.Model.DTOs;
using System.ComponentModel.DataAnnotations;

namespace API.Model.Entity
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Email { get; set; }
        [StringLength(50)]
        [Required]
        public string Nombre { get; set; }
        [StringLength(50)]
        [Required]
        public string Psswrd { get; set; }
        [Required]
        public int NivelEstanque { get; set; } = 0;
        [Required]
        public int RachaConexion { get; set; } = 0;
        [Required]
        public DateTime FechaRegistro { get; set; }

        public List<Medalla> Medallas { get; set; } = new List<Medalla>();
        public List<UsuarioLogro> UsuarioLogros { get; set; } = new List<UsuarioLogro>();

        public void AddModelInfo(UsuarioDTO model)
        {
            Email = model.Email;
            Nombre = model.Nombre;
            Psswrd = model.Psswrd;
            FechaRegistro = model.FechaRegistro;
        }
    }
}
