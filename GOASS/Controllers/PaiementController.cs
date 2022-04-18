using GOASS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Controllers
{
    public class PaiementController : Controller
    {
        private readonly ProduitContext _context;
        public PaiementController(ProduitContext context)
        {
            _context = context;
        }

        public IActionResult Index(int ID)
        {
            var panier = _context.Panier.Include(x => x.ItemPanier).ThenInclude(x => x.Produit).SingleOrDefault(x => x.PanierID == ID);
            ViewBag.PanierID = ID;
            ViewBag.Montant = panier.ItemPanier.Sum(x => x.Produit.Prix * x.Quantite);
            ViewBag.TotalCentimes = Convert.ToInt64(Math.Round(ViewBag.Montant, 2) * 100);
            return View();
        }
        [HttpPost]
        public IActionResult TraiterPaiement(string stripeToken, string stripeEmail, string UserName, string Tel, int PanierID)
        {
            var panier = _context.Panier.Include(x => x.ItemPanier).ThenInclude(x => x.Produit).SingleOrDefault(x => x.PanierID == PanierID);
            var optionsCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = UserName,
                Phone = Tel
            };

            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionsCust);

            var optionsCharge = new ChargeCreateOptions
            {
                Amount = Convert.ToInt64(panier.ItemPanier.Sum(x => x.Produit.Prix * x.Quantite) * 100),
                Currency = "CAD",
                Description = "Item de sports",
                Source = stripeToken,
                ReceiptEmail = stripeEmail
            };

            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);

            if(charge.Status == "succeeded")
            {
                Commande commande = new Commande
                {
                    UserGuid = panier.UserGuid
                };
                _context.Add(commande);
                _context.SaveChanges();

                var options = new ChargeUpdateOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        {"order_id", commande.CommandeID.ToString()}
                    }
                };
                serviceCharge.Update(charge.Id, options);

                foreach(var itemPanier in panier.ItemPanier)
                {
                    var itemCommande = new ItemCommande
                    {
                        MontantUnitaire = itemPanier.Produit.Prix,
                        Quantite = itemPanier.Quantite,
                        CommandeID = commande.CommandeID,
                        ProduitID = itemPanier.ProduitID
                    };
                    _context.Add(itemCommande);
                    _context.Remove(itemPanier);
                }
                _context.SaveChanges();

                ViewBag.NoCommande = commande.CommandeID;
                ViewBag.MontantPaye = Convert.ToDecimal(charge.Amount) % 100 / 100 + (charge.Amount) / 100;
                ViewBag.Client = customer.Name;

            }
            return View();
        }

        //public IActionResult Historique(int CommandeID, int ItemCommandeID, int UserGuid)
        //{
        //    var commande = _context.Commande.Include(x => x.CommandeID);
        //    Historique historique = new Historique
        //    {

        //        UserGuid = commande.UserGuid,
        //        CommandeID = commande.CommandeID,
        //        ItemCommandeID = commande.ItemCommandeID
        //    };

        //    ViewBag.CommandeID = commande.CommandeID;
        //    ViewBag.UserGuid = commande.UserGuid;

        //}

    }
}
