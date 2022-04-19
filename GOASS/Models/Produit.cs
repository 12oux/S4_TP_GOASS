using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class Produit
    {
        public int ProduitID { get; set; }

        [DisplayName("Nom")]
        public string NomProduit { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Marque")]
        public string Marque { get; set; }

        [DisplayName("Taille")]
        public int Taille { get; set; }

        [DisplayName("Quantité")]
        public int Quantité { get; set; }

        [DisplayName("Sport")]
        public string Sport { get; set; }
        public int? ImageID { get; set; }
        public Image Image { get; set; }

        [DisplayName("Prix")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Prix { get; set; }

        public DbSet<Produit> Produits { get; set; }
        public List<ItemPanier> ItemPanier { get; set; }

    }
}
