using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Helper
{
    public class Helpers
    {
        public static void DeleteImage(string root,string folder,string file)
        {
            string fullPath = Path.Combine(root, folder, file);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
