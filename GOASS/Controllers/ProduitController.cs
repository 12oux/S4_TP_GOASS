using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GOASS.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace GOASS.Controllers
{
    public class ProduitController : Controller
    {
        private readonly ProduitContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProduitController(ProduitContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Produit
        public async Task<IActionResult> Index()
        {

            return View(await _context.Produit.Include(x => x.Image).ToListAsync());
        }

        //public async Task<IActionResult> Hockey(Produit produit)
        //{
        //    var produits = Produit.All();
        //    var queryHockey = from items in produits
        //                      where Produit.Sport == "Hockey"
        //                      select items;
        //    return View(await _context.Produit.Include(x => x.Image).ToListAsync());
        //}
        // GET: Produit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit.Include(x => x.Image).SingleOrDefaultAsync(m => m.ProduitID == id);
 
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // GET: Produit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProduitID,NomProduit,Description,Marque,Taille,Quantité,Sport, ImageID")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                if(Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files.SingleOrDefault();
                    Image img = new Image()
                    {
                        NomImage = file.FileName,
                        ContentType = file.ContentType
                    };
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);

                    img.ImageData = ms.ToArray();
                    ms.Close();
                    ms.Dispose();

                    _context.Add(img);
                    _context.SaveChanges();

                    produit.ImageID = img.ImageID;
                }

                _context.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produit);
        }

        // GET: Produit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }
            return View(produit);
        }


        // POST: Produit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProduitID,NomProduit,Description,Marque,Taille,Quantité,Sport, ImageID")] Produit produit)
        {
            if (id != produit.ProduitID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        var file = Request.Form.Files.SingleOrDefault();
                        Image img = new Image()
                        {
                            NomImage = file.FileName,
                            ContentType = file.ContentType
                        };

                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);

                        img.ImageData = ms.ToArray();
                        ms.Close();
                        ms.Dispose();

                        _context.Add(img);

                        if (produit.ImageID != null)
                        {
                            var imageASupprimer = await _context.Image.FindAsync(produit.ImageID);
                            produit.ImageID = null;
                            _context.SaveChanges();
                            _context.Remove(imageASupprimer);
                        }
                        _context.SaveChanges();

                        produit.ImageID = img.ImageID;
                    }

                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.ProduitID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produit);
        }

        // GET: Produit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit
                .FirstOrDefaultAsync(m => m.ProduitID == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        public async Task<IActionResult> AjoutPanier(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var current_User = _userManager.GetUserAsync(HttpContext.User).Result;
            string current_User_Id = (current_User != null) ? "" + current_User.Id : "";

            var panier = _context.Panier.FirstOrDefault(x => x.UserGuid == current_User_Id);

            if (panier == null)
            {
                panier = new Panier()
                {
                    UserGuid = current_User_Id,
                    ItemPanier = new List<ItemPanier>()
                };
                _context.Add(panier);
                await _context.SaveChangesAsync();
            }
            var item = _context.ItemPanier.FirstOrDefault(x => x.PanierID == panier.PanierID && x.ProduitID == id.Value);

            if (item == null)
            {
                item = new ItemPanier() { PanierID = panier.PanierID, ProduitID = id.Value, Quantite = 1 };
                _context.Add(item);
            }
            else
            {
                item.Quantite++;
                _context.Update(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Produit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produit = await _context.Produit.FindAsync(id);
            _context.Produit.Remove(produit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduitExists(int id)
        {
            return _context.Produit.Any(e => e.ProduitID == id);
        }

    }
}
