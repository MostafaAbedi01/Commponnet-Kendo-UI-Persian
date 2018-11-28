using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr
{
    public class DefaultPathResolver : IPathResolver
    {
        public static DefaultPathResolver Instance = new DefaultPathResolver();

        public string Resolve(string filePath) { return Environment.CurrentDirectory + "/" + filePath; }
    }
}
