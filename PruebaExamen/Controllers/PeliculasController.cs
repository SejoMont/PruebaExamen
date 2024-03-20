using Microsoft.AspNetCore.Mvc;
using PruebaExamen.Filters;
using PruebaExamen.Models;
using PruebaExamen.Repositories;

namespace PruebaExamen.Controllers
{
    public class PeliculasController : Controller
    {
        private RepositoryPeliculas repo;
        public PeliculasController(RepositoryPeliculas repo)
        {
            this.repo = repo;
        }

        [AuthorizeUsuarios]
        public IActionResult PerfilUsuario()
        {
            return View();
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> Index()
        {
            List<Pelicula> peliculas = await this.repo.GetPeliculasAsync();
            return View(peliculas);
        }

        public async Task<IActionResult> PeliculasGenero(int idgenero)
        {
            List<Pelicula> peliculasGenero = await this.repo.PeliculasGeneroAsync(idgenero);
            return View(peliculasGenero);
        }

        public async Task<IActionResult> DetallesPelicula(int id)
        {
            Pelicula peli = await this.repo.FindPeliculaAsync(id);
            return View(peli);
        }
    }
}
