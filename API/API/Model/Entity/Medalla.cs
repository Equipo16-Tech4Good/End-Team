using System.ComponentModel.DataAnnotations;

namespace API.Model.Entity
{
    public class Medalla
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string Titulo { get; set; }
        [StringLength(150)]
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        public int UsuarioId { get; set; }
        public int NivelMedallaId { get; set; }
    }
}
