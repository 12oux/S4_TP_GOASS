using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GOASS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GOASS.Controllers
{
    [Authorize]
    public class PanierController : Controller
    {
        private readonly ProduitContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PanierController(ProduitContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Panier
        public async Task<IActionResult> Index()
        {
            return View(await _context.Panier.ToListAsync());
        }

        // GET: Panier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var current_User = _userManager.GetUserAsync(HttpContext.User).Result;
            string current_User_Id = (current_User != null) ? "" + current_User.Id : "";

            var panier = await _context.Panier.Include(x => x.ItemPanier).ThenInclude(x => x.Produit)
                .FirstOrDefaultAsync(m => m.UserGuid == current_User_Id);

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

            return View(panier);
        }

        // GET: Panier/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Panier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PanierID,UserGuid")] Panier panier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(panier);
        }

        // GET: Panier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier.FindAsync(id);
            if (panier == null)
            {
                return NotFound();
            }
            return View(panier);
        }

        // POST: Panier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PanierID,UserGuid")] Panier panier)
        {
            if (id != panier.PanierID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanierExists(panier.PanierID))
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
            return View(panier);
        }

        // GET: Panier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .FirstOrDefaultAsync(m => m.PanierID == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // POST: Panier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var panier = await _context.Panier.FindAsync(id);
            _context.Panier.Remove(panier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanierExists(int id)
        {
            return _context.Panier.Any(e => e.PanierID == id);
        }
    }
}
