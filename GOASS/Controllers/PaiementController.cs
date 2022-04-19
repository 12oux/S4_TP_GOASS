using GOASS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GOASS.Controllers
{
    public class PaiementController : Controller
    {
        private readonly ProduitContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public PaiementController(ProduitContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

                var current_User = _userManager.GetUserAsync(HttpContext.User).Result;

                SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("courrielTI@cstjean.qc.ca", "3ordercharactercorn67winwest");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("courrielTI@cstjean.qc.ca", "Courriel TI");
                mail.To.Add(new MailAddress(current_User.Email));
                mail.Subject = "Votre confirmation de commande chez GOASS";
                mail.Body = "Votre commande:" + commande.CommandeID + "     Votre identifiant unique:" + commande.UserGuid + "     Vous pouvez accéder aux détails de la commande sur votre portail!" + "    Merci d'avoir magasiner chez GOASS, au plaisir de refaire affaires avec vous!"
                    ;
                smtpClient.Send(mail);

                foreach (var itemPanier in panier.ItemPanier)
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

    }
}
