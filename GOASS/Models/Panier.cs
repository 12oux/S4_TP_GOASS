using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class Panier
    {
        public int PanierID { get; set; }
        public string UserGuid { get; set; }

        public List<ItemPanier> ItemPanier { get; set; }
    }
}
