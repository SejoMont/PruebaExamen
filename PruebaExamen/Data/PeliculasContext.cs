using Microsoft.EntityFrameworkCore;
using PruebaExamen.Models;

namespace PruebaExamen.Data
{
    public class PeliculasContext : DbContext
    {
        public PeliculasContext(DbContextOptions<PeliculasContext> options)
            : base(options)
        {
        }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
