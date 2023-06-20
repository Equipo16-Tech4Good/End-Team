using API.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;


namespace API.Context
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medalla> Medallas { get; set; }
        public DbSet<Logro> Logros { get; set; }
        public DbSet<NivelMedalla> NivelesMedallas { get; set; }
        public DbSet<UsuarioLogro> UsuariosLogros { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
    }
}
