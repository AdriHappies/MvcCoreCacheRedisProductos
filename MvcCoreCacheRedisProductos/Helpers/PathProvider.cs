using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCacheRedisProductos.Helpers
{
    public enum Folders
    {
        Images = 0, Documents = 1, Temporal = 2
    }

    public class PathProvider
    {
        private IWebHostEnvironment environment;

        public PathProvider(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public string MapPath(string filename, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Documents)
            {
                carpeta = "documents";
            }else if (folder == Folders.Images)
            {
                carpeta = "images";
            }

            string path = Path.Combine(this.environment.WebRootPath, carpeta, filename);
            return path;
        }
    }
}
