using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furlencode.Core.Model
{
    public class StoreImages
    {
        public string Name { get; set; }
        public long? Size { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string ThumbnailPath { get; set; }

    }
}
