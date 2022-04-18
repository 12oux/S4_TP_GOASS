using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class Commande
    {
        public int CommandeID { get; set; }
        public string UserGuid { get; set; }
        public List<ItemCommande> Items { get; set; }

        //public ItemCommande ItemCommande { get; set; }
    }
}
