using System.ComponentModel.DataAnnotations;

namespace API.Model.Entity
{
    public class NivelMedalla
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string Titulo { get; set; }
        [StringLength(150)]
        [Required]
        public string Imagen { get; set; }

        public List<Medalla> Medallas { get; set; } = new List<Medalla>();
    }
}
