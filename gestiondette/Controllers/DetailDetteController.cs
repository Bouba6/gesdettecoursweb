using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestiondette.Models;
using gestiondette.Helpers;
namespace gestiondette.Controllers
{
    public class DetailDetteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetailDetteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetailDette
        public async Task<IActionResult> Index()
        {
            return View(await _context.detaildette.ToListAsync());
        }

        // GET: DetailDette/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailDette = await _context.detaildette
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailDette == null)
            {
                return NotFound();
            }

            return View(detailDette);
        }

        // GET: DetailDette/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetailDette/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Qte,CreateAt,UpdateAt")] DetailDette detailDette)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detailDette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detailDette);
        }
        public IActionResult AddDetailToSession(int articleId, int quantity)
        {
            // Retrieve the existing details from the session or create a new list
            var detailsList = HttpContext.Session.GetObjectFromJson<List<DetailDette>>("DetailDettes") ?? new List<DetailDette>();

            // Assuming Client and Article are already fetched by their IDs

            var article = _context.article.FirstOrDefault(a => a.Id == articleId);

            // Create a new DetailDette and add it to the list
            if (article != null)
            {
                var detail = new DetailDette
                {

                    Article = article,
                    Qte = quantity
                };
                detailsList.Add(detail);

                // Update the session with the new list
                HttpContext.Session.SetObjectAsJson("DetailDettes", detailsList);
            }

            return RedirectToAction("Create", "Dette");
        }


        // GET: DetailDette/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailDette = await _context.detaildette.FindAsync(id);
            if (detailDette == null)
            {
                return NotFound();
            }
            return View(detailDette);
        }

        // POST: DetailDette/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Qte,CreateAt,UpdateAt")] DetailDette detailDette)
        {
            if (id != detailDette.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detailDette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailDetteExists(detailDette.Id))
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
            return View(detailDette);
        }

        // GET: DetailDette/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailDette = await _context.detaildette
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailDette == null)
            {
                return NotFound();
            }

            return View(detailDette);
        }

        // POST: DetailDette/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detailDette = await _context.detaildette.FindAsync(id);
            if (detailDette != null)
            {
                _context.detaildette.Remove(detailDette);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailDetteExists(int id)
        {
            return _context.detaildette.Any(e => e.Id == id);
        }
    }
}
