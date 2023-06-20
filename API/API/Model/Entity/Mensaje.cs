using System.ComponentModel.DataAnnotations;

namespace API.Model.Entity
{
    public class Mensaje
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        [Required]
        public string Titulo { get; set; }
        [StringLength(250)]
        [Required]
        public string Nombre { get; set; }
    }
}
