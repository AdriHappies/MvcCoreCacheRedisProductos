using MvcCoreCacheRedisProductos.Helpers;
using MvcCoreCacheRedisProductos.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCacheRedisProductos.Services
{
    public class ServiceCacheRedis
    {
        private IDatabase database;

        public ServiceCacheRedis()
        {
            this.database = CacheRedisMultiplexer.GetConnection.GetDatabase();
        }

        //NECESITAMOS ALMACENAR VARIOS PRODUCTOS.
        //TENDREMOS UN METODO QUE RECIBA UN PRODUCTO Y LO GUARDA EN UNA COLECCION
        public void AlmacenarProductoCache(Producto producto)
        {
            //CACHE REDIS FUNCIONA CON CLAVES DENTRO DE SU INTERIOR
            //DICHAS CLAVES DEBEN SER UNICAS, SINO SOBREESCRIBEN EL VALOR
            //LO QUE ALMACENAMOS DEBE SER EN FORMATO JSON
            //DEBEMOS RECUPERAR EL JSON QUE REPRESENTA TODOS LOS PRODUCTOS FAVORITOS
            string jsonProductos = this.database.StringGet("favoritos");
            //NOSOTROS ALMACENAREMOS UNA COLECCION DE PRODUCTOS
            List<Producto> favoritos;
            //SI NO HEMOS ALMACENADO FAVORITOS jsonProductos NO TENDRA VALOR
            if (jsonProductos == null)
            {
                //CREAMOS UNA NUEVA LISTA
                favoritos = new List<Producto>();
            }
            else
            {
                //YA TENEMOS PRODUCTOS EN CACHE REDIS
                //EXTRAEMOS
                favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }
            //AÑADIMOS EL NUEVO PRODUCTO A CACHE
            favoritos.Add(producto);
            //CONVERTIMOS A JSON
            jsonProductos = JsonConvert.SerializeObject(favoritos);
            //ALMACENAMOS DE NUEVO EN CACHE LOS DATOS
            this.database.StringSet("favoritos", jsonProductos);
        }

        public List<Producto> GetFavoritosCache()
        {
            string jsonProductos = this.database.StringGet("favoritos");
            if(jsonProductos == null)
            {
                return null;
            }
            else
            {
                List<Producto> favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                return favoritos;
            }
        }

        public void EliminarFavoritosCache(int idproducto)
        {
            string jsonProductos = this.database.StringGet("favoritos");
            if(jsonProductos != null)
            {
                List<Producto> favoritos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                Producto favorito = favoritos.FirstOrDefault(x => x.IdProducto == idproducto);
                favoritos.Remove(favorito);
                if(favoritos.Count == 0)
                {
                    this.database.KeyDelete("favoritos");
                }
                else
                {
                    jsonProductos = JsonConvert.SerializeObject(favoritos);
                    this.database.StringSet("favoritos", jsonProductos, TimeSpan.FromMinutes(15));
                }
            }
        }
    }
}
