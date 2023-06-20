using System.ComponentModel.DataAnnotations;

namespace API.Model.Entity
{
    public class Medalla
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        [Required]

        public int UsuarioId { get; set; }
        public int NivelMedallaId { get; set; }
    }
}
