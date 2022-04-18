using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class Historique
    {
        public int CommandeID { get; set; }
        public int ItemCommandeID { get; set; }
        public string UserGuid { get; set; }
        public Commande Commande { get; set; }
        public ItemCommande ItemCommande { get; set; }
    }
}
