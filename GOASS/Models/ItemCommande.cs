using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class ItemCommande
    {
        public int ItemCommandeID { get; set; }
        public int ProduitID { get; set; }
        public int CommandeID { get; set; }
        public int Quantite { get; set; }

        [DisplayName("Montant Unitaire")]
        public double MontantUnitaire { get; set; }
        public Commande Commande { get; set; }
        public Produit Produit { get; set; }
        public Image Image { get; set; }

    }
}
