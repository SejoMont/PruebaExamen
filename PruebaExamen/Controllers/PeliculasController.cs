﻿using Microsoft.AspNetCore.Mvc;
using PruebaExamen.Extensions;
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
        public IActionResult GuardarPeliculaCarrito(int idPelicula, int? idGenero)
        {
            if (idPelicula != null)
            //Guardamos el producto en el carrito
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("CARRITO") == null)
                {
                    carrito = new List<int>();
                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                }
                carrito.Add(idPelicula);
                HttpContext.Session.SetObject("CARRITO", carrito);
            }
            if (idGenero != null)
            {
                return RedirectToAction("PeliculasGenero", "Peliculas", new { idgenero = idGenero });

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Carrito(int? idPeliculaEliminar)
        {
            //Le pasamos el carrito
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            //Tienes que crear para añadir datos al carrito
            if (carrito == null)
            {
                return View();
            }
            else
            {
                if (idPeliculaEliminar != null)
                {
                    carrito.Remove(idPeliculaEliminar.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);
                }
                List<Pelicula> peliculas = this.repo.GetPeliculasCarrito(carrito);
                return View(peliculas);
            }
        }

        [AuthorizeUsuarios]
        public IActionResult Pedidos()
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            List<Pelicula> peliculas = this.repo.GetPeliculasCarrito(carrito);
            HttpContext.Session.Remove("CARRITO");
            return View(peliculas);
        }

        [AuthorizeUsuarios]
        public IActionResult ComprasUsuario(int iduser)
        {
            List<Compra> compras = this.repo.GetComprasUsuario(iduser);

            return View(compras);
        }

        [HttpPost]
        public async Task<IActionResult> Compra(List<int> peliculas, List<int> cantidades, int iduser)
        {
            int idcompra = await repo.GetUltimaCompra();
            for (int i = 0; i < peliculas.Count; i++)
            {
                int peli = peliculas[i];
                int cantidad = cantidades[i];
                
                idcompra++;
                Compra nuevaCompra = new Compra()
                {
                    IdCompra = idcompra,
                    IdPelicula = peli,
                    FechaCompra = DateTime.Now,
                    IdUsuario = iduser,
                    Cantidad = cantidad 
                };
                
                await repo.ComprarProducto(nuevaCompra);
                
            }
            HttpContext.Session.Remove("CARRITO");
            return RedirectToAction("ComprasUsuario", "Peliculas", new {iduser = iduser});
        }
    }
}
