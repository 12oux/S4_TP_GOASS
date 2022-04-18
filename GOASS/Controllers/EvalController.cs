using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GOASS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GOASS.Controllers
{
    [Authorize]
    public class EvalController : Controller
    {
        private readonly EvalContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EvalController(EvalContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Eval
        public async Task<IActionResult> Index(int? id)
        {
            var current_User = _userManager.GetUserAsync(HttpContext.User).Result;
            string current_User_Id = (current_User != null) ? "" + current_User.Id : "";

            return View(await _context.Eval.ToListAsync());
        }

        // GET: Eval/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eval = await _context.Eval
                .FirstOrDefaultAsync(m => m.EvalID == id);
            if (eval == null)
            {
                return NotFound();
            }

            return View(eval);
        }

        // GET: Eval/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eval/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvalID,NomClient,PrenomClient,Email,Comment")] Eval eval)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eval);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eval);
        }

        // GET: Eval/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eval = await _context.Eval.FindAsync(id);
            if (eval == null)
            {
                return NotFound();
            }
            return View(eval);
        }

        // POST: Eval/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EvalID,NomClient,PrenomClient,Email,Comment")] Eval eval)
        {
            if (id != eval.EvalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eval);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvalExists(eval.EvalID))
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
            return View(eval);
        }

        // GET: Eval/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eval = await _context.Eval
                .FirstOrDefaultAsync(m => m.EvalID == id);
            if (eval == null)
            {
                return NotFound();
            }

            return View(eval);
        }

        // POST: Eval/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eval = await _context.Eval.FindAsync(id);
            _context.Eval.Remove(eval);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvalExists(int id)
        {
            return _context.Eval.Any(e => e.EvalID == id);
        }
    }
}
