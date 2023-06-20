using System.ComponentModel.DataAnnotations;

namespace API.Model.Entity
{
    public class UsuarioLogro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime FechaObtencion { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int LogroId { get; set; }
    }
}
