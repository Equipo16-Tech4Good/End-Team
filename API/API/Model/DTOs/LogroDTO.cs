using API.Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace API.Model.DTOs
{
    public class LogroDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }

        public void AddContentLogro(Logro logro)
        {
            Titulo= logro.Titulo;
            Descripcion= logro.Descripcion;
            Imagen= logro.Imagen;
        }
    }
}
