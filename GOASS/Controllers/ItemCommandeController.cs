using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GOASS.Models;
using System.Collections;

namespace GOASS.Controllers
{
    public class ItemCommandeController : Controller
    {
        private readonly ProduitContext _context;

        public ItemCommandeController(ProduitContext context)
        {
            _context = context;
        }

        // GET: ItemCommande
        public async Task<IActionResult> Index(int ID, int UserGuid)
        {

            var produitContext = _context.ItemCommande.Include(i => i.Commande).Include(i => i.Produit).ThenInclude(i => i.Image);

            return View(await produitContext.ToListAsync());

        }

        // GET: ItemCommande/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCommande = await _context.ItemCommande
                .Include(i => i.Commande)
                .Include(i => i.Produit)
                .ThenInclude(i => i.Image)
                .FirstOrDefaultAsync(m => m.ItemCommandeID == id);
            if (itemCommande == null)
            {
                return NotFound();
            }

            return View(itemCommande);
        }

        // GET: ItemCommande/Create
        public IActionResult Create()
        {
            ViewData["CommandeID"] = new SelectList(_context.Commande, "CommandeID", "CommandeID");
            ViewData["ProduitID"] = new SelectList(_context.Produit, "ProduitID", "ProduitID");
            return View();
        }

        // POST: ItemCommande/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemCommandeID,ProduitID,CommandeID,Quantite,MontantUnitaire")] ItemCommande itemCommande)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemCommande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommandeID"] = new SelectList(_context.Commande, "CommandeID", "CommandeID", itemCommande.CommandeID);
            ViewData["ProduitID"] = new SelectList(_context.Produit, "ProduitID", "ProduitID", itemCommande.ProduitID);
            return View(itemCommande);
        }

        // GET: ItemCommande/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCommande = await _context.ItemCommande.FindAsync(id);
            if (itemCommande == null)
            {
                return NotFound();
            }
            ViewData["CommandeID"] = new SelectList(_context.Commande, "CommandeID", "CommandeID", itemCommande.CommandeID);
            ViewData["ProduitID"] = new SelectList(_context.Produit, "ProduitID", "ProduitID", itemCommande.ProduitID);
            return View(itemCommande);
        }

        // POST: ItemCommande/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemCommandeID,ProduitID,CommandeID,Quantite,MontantUnitaire")] ItemCommande itemCommande)
        {
            if (id != itemCommande.ItemCommandeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCommande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCommandeExists(itemCommande.ItemCommandeID))
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
            ViewData["CommandeID"] = new SelectList(_context.Commande, "CommandeID", "CommandeID", itemCommande.CommandeID);
            ViewData["ProduitID"] = new SelectList(_context.Produit, "ProduitID", "ProduitID", itemCommande.ProduitID);
            return View(itemCommande);
        }

        // GET: ItemCommande/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCommande = await _context.ItemCommande
                .Include(i => i.Commande)
                .Include(i => i.Produit)
                .FirstOrDefaultAsync(m => m.ItemCommandeID == id);
            if (itemCommande == null)
            {
                return NotFound();
            }

            return View(itemCommande);
        }

        // POST: ItemCommande/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemCommande = await _context.ItemCommande.FindAsync(id);
            _context.ItemCommande.Remove(itemCommande);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCommandeExists(int id)
        {
            return _context.ItemCommande.Any(e => e.ItemCommandeID == id);
        }
    }
}
