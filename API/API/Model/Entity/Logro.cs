using System.ComponentModel.DataAnnotations;

namespace API.Model.Entity
{
    public class Logro
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Titulo { get; set; }
        [StringLength(150)]
        [Required]
        public string Descripcion { get; set; }
        [StringLength(150)]
        [Required]
        public string Imagen { get; set; }

        public List<UsuarioLogro> UsuarioLogros { get; set; } = new List<UsuarioLogro>();
    }
}
