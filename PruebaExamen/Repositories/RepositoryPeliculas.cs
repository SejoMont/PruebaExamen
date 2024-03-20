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
            return await this.context.Peliculas.Where(p => p.IdGenero == idgenero).ToListAsync();
        }

        public async Task<List<Genero>> GetAllGenerosAsync()
        {
            return await this.context.Generos.ToListAsync();
        }

        public List<Pelicula> GetPeliculasCarrito(List<int> idPelicula)
        {
            return context.Peliculas.Where(p => idPelicula.Contains(p.IdPelicula)).ToList();
        }

        public List<Compra> GetComprasUsuario(int userid)
        {
            return context.Compras.Where(c => c.IdUsuario == userid).ToList();
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
            await context.SaveChangesAsync();
        }
    }
}
