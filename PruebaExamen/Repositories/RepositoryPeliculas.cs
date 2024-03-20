using Microsoft.EntityFrameworkCore;
using PruebaExamen.Data;
using PruebaExamen.Models;

namespace PruebaExamen.Repositories
{
    public class RepositoryPeliculas
    {
        private PeliculasContext context;

        public RepositoryPeliculas(PeliculasContext context)
        {
            this.context = context;
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            return await this.context.Peliculas.ToListAsync();
        }

        public async Task<Pelicula> FindPeliculaAsync(int idPelicula)
        {
            return await this.context.Peliculas.FirstOrDefaultAsync(x => x.IdPelicula == idPelicula);
        }

        public async Task<List<Pelicula>> PeliculasGeneroAsync(int idgenero)
        {
            var consulta = from datos in context.Peliculas
                           where datos.IdGenero == idgenero
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<List<Genero>> GetAllGenerosAsync()
        {
            var consulta = from datos in context.Generos
                           select datos;
            return await consulta.ToListAsync();
        }

        public List<Pelicula> GetPeliculasCarrito(List<int> idPelicula)
        {

            var consulta = from datos in context.Peliculas
                           where idPelicula.Contains(datos.IdPelicula)
                           select datos;
            return consulta.ToList();
        }

        public async Task<int> GetUltimaCompra()
        {
            var ultimoIdImagen = await this.context.Compras
                                            .MaxAsync(imagen => (int?)imagen.IdCompra);

            return ultimoIdImagen ?? 1;
        }

        public async Task ComprarProducto(Compra compra)
        {
            context.Compras.Add(compra);
        }
    }
}
