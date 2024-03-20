using Microsoft.AspNetCore.Mvc;
using PruebaExamen.Models;
using PruebaExamen.Repositories;

namespace PruebaExamen.ViewComponents
{
    public class MenuGenerosViewComponent : ViewComponent
    {
        private RepositoryPeliculas repo;

        public MenuGenerosViewComponent(RepositoryPeliculas repo)
        {
            this.repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = await this.repo.GetAllGenerosAsync();
            return View(generos);
        }
    }
}