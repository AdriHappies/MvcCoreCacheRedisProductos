using ClienteCacheRedis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteCacheRedis
{
    public class ServiceCacheRedis
    {
        private IDatabase database;

        public ServiceCacheRedis()
        {
            this.database = CacheRedisMultiplexer.GetConnection.GetDatabase();
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
    }
}
