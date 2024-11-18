using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestiondette.Models;

namespace gestiondette.Controllers
{
    public class PaiementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaiementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Paiement
        public async Task<IActionResult> Index(int id)
        {
            var paiements = await _context.paiement
                .Where(p => p.Dette.Id == id)
                .ToListAsync();

            ViewBag.DetteId = id; // Optionnel, pour utilisation dans la vue si besoin
            return View(paiements);
        }

        // GET: Paiement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paiement = await _context.paiement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paiement == null)
            {
                return NotFound();
            }

            return View(paiement);
        }

        public async Task<IActionResult> IndexByDette(int id)
        {
            var paiements = await _context.paiement
                .Where(p => p.Dette.Id == id)
                .ToListAsync();

            ViewBag.DetteId = id; // Optionnel, pour utilisation dans la vue si besoin
            return View(paiements);
        }

        // GET: Paiement/Create
        public IActionResult Create(int id)
        {
            if (id == null)
            {
                return NotFound("ID is missing.");
            }

            ViewData["DetteId"] = id;
            return View();
        }

        // POST: Paiement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Montant, int? id)
        {

            if (ModelState.IsValid)
            {
                var dette = await _context.dette.FindAsync(id);
                if (dette == null)
                {
                    return NotFound("Dette not found.");
                }
                if (Montant < dette.MontantRestant)
                {
                    Paiement paiement = new Paiement();

                    dette.MontantRestant -= Montant;
                    dette.MontantVerser += Montant;
                    paiement.DatePaiement = DateTime.UtcNow;
                    paiement.Montant = Montant;
                    paiement.Dette = dette;

                    _context.Add(paiement);

                    dette.Paiements.Add(paiement);
                    _context.dette.Update(dette);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("-------------------************************************------------------------------");
                    Console.WriteLine(paiement.Id);
                    Console.WriteLine("----------------------**********************************---------------------------");
                    // return NotFound(paiement.Id);
                    return RedirectToAction("Index", new { id = dette.Id });
                }
                else
                {

                    ModelState.AddModelError("Montant", "montant invalide");
                }




            }
            return View();
        }

        // GET: Paiement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paiement = await _context.paiement.FindAsync(id);
            if (paiement == null)
            {
                return NotFound();
            }
            return View(paiement);
        }

        // POST: Paiement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Montant,DatePaiement,Id,CreateAt,UpdateAt")] Paiement paiement)
        {
            if (id != paiement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paiement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaiementExists(paiement.Id))
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
            return View(paiement);
        }

        // GET: Paiement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paiement = await _context.paiement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paiement == null)
            {
                return NotFound();
            }

            return View(paiement);
        }

        // POST: Paiement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paiement = await _context.paiement.FindAsync(id);
            if (paiement != null)
            {
                _context.paiement.Remove(paiement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaiementExists(int id)
        {
            return _context.paiement.Any(e => e.Id == id);
        }
    }
}
