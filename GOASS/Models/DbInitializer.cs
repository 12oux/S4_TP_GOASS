using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ProduitContext context)
        {
            if (context.Produit.Count() > 1)
                return;
            var produits = new List<Produit>()
            {
                new Produit{ NomProduit="Patins",Description="Patins qui vont vite vite vite",Marque="ZZM", Taille=10, Quantité=5, Sport="Hockey", Prix=15, ImageID=1},
                new Produit{ NomProduit="Ballon",Description="Beau ballon de soccer",Marque="Niki", Taille=5, Quantité=15, Sport="Soccer", Prix=10.25, ImageID=1},
                new Produit{ NomProduit="Bicycle",Description="Vélo 26 vitesses",Marque="VILO", Taille=0, Quantité=2, Sport="Velo", Prix=20, ImageID=1},
                new Produit{ NomProduit="Baton Baseball",Description="Baton en bois",Marque="Rebook",Taille=0,Quantité=235,Sport="Baseball", Prix=15.21, ImageID=1}
            };
            foreach (Produit item in produits)
            {
                context.Produit.Add(item);
            }
            context.SaveChanges();
        }
    }
}
