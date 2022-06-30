using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Extension
{
    public static class FileManager
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }
        public static bool IsSize(this IFormFile file,int mb)
        {
            return file.Length / 1024 / 1024 <= mb;
        }
        public static string SaveImage(this IFormFile file,string root,string folder)
        {
            string rootPath = Path.Combine(root, folder);
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using(FileStream fs= new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return fileName;
        }
    }
}
