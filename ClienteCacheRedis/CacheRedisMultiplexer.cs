using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteCacheRedis
{
    public static  class CacheRedisMultiplexer
    {
        private static Lazy<ConnectionMultiplexer> CreateConnection =
            new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("cacheredisproductosafg.redis.cache.windows.net:6380,password=yCNCLmCTbOS9Gr0mO6zkUh0F2c2457aP4AzCaOxtKps=,ssl=True,abortConnect=False");
            });

        public static ConnectionMultiplexer GetConnection
        {
            get
            {
                return CreateConnection.Value;
            }
        }
    }
}
