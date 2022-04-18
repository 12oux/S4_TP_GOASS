using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public string NomImage { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
        //public DbSet<Image> Images { get; set; }

    }
}