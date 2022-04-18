using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class Produit
    {
        public int ProduitID { get; set; }
        public string NomProduit { get; set; }
        public string Description { get; set; }
        public string Marque { get; set; }
        public int Taille { get; set; }
        public int Quantité { get; set; }
        public string Sport { get; set; }
        public int? ImageID { get; set; }
        public Image Image { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Prix { get; set; }

        public DbSet<Produit> Produits { get; set; }
        public List<ItemPanier> ItemPanier { get; set; }

    }
}
