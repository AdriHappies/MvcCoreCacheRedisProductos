using Microsoft.AspNetCore.Mvc;
using MvcCoreCacheRedisProductos.Models;
using MvcCoreCacheRedisProductos.Repositories;
using MvcCoreCacheRedisProductos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCacheRedisProductos.Controllers
{
    public class ProductosController : Controller
    {
        private RepositoryProductos repo;
        private ServiceCacheRedis service;

        public ProductosController(RepositoryProductos repo, ServiceCacheRedis service)
        {
            this.repo = repo;
            this.service = service;
        }


        public IActionResult Index()
        {
            List<Producto> productos = this.repo.GetProductos();
            return View(productos);
        }

        public IActionResult Details(int id)
        {
            Producto producto = this.repo.FindProducto(id);
            return View(producto);
        }

        public IActionResult Favoritos()
        {
            List<Producto> favoritos = this.service.GetFavoritosCache();
            if(favoritos == null)
            {
                return View();
            }
            else
            {
                return View(favoritos);
            }
            
        }

        public IActionResult SeleccionarFavorito(int id)
        {
            Producto favorito = this.repo.FindProducto(id);
            this.service.AlmacenarProductoCache(favorito);
            return RedirectToAction("Favoritos");
        }

        public IActionResult EliminarFavorito(int id)
        {
            this.service.EliminarFavoritosCache(id);
            return RedirectToAction("Favoritos");
        }
    }
}
