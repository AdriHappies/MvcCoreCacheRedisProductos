using MvcCoreCacheRedisProductos.Helpers;
using MvcCoreCacheRedisProductos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCoreCacheRedisProductos.Repositories
{
    public class RepositoryProductos
    {
        private PathProvider pathProvider;
        private XDocument document;

        public RepositoryProductos(PathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
            string path = this.pathProvider.MapPath("productos.xml", Folders.Documents);
            this.document = XDocument.Load(path);
        }

        public List<Producto> GetProductos()
        {
            var consulta = from datos in this.document.Descendants("producto")
                           select new Producto
                           {
                               IdProducto = int.Parse(datos.Element("idproducto").Value),
                               Nombre = datos.Element("nombre").Value,
                               Descripcion = datos.Element("descripcion").Value,
                               Precio = double.Parse(datos.Element("precio").Value),
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }

        public Producto FindProducto(int idproducto)
        {
            return this.GetProductos().SingleOrDefault(x => x.IdProducto == idproducto);
        }
    }
}
