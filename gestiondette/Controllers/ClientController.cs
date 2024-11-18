using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestiondette.Models;
using gestiondette.Enum;

namespace gestiondette.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {

            var clients = await _context.client
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();


            int totalClients = await _context.client.CountAsync();
            int totalPages = (int)Math.Ceiling(totalClients / (double)pageSize);

            // Passer les données et la pagination à la vue
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(clients);
        }


        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Surnom,Telephone,Adresse,Solde,Id,CreateAt,UpdateAt,User")] Client client)
        {

            Console.WriteLine(ModelState);
            if (ModelState.IsValid)
            {
                client.OnPrePersist();

                // Only process User if checkbox is checked
                if (Request.Form["CreateUser"] == "on" && client.User != null)
                {
                    client.User.Role = Role.CLIENT;
                    client.User.OnPrePersist();
                    _context.Add(client.User);
                }
                else
                {
                    client.User = null;
                }

                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }


        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Surnom,Telephone,Adresse,Solde,Id,CreateAt,UpdateAt")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.client
                .FirstOrDefaultAsync(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.client.FindAsync(id);
            if (client != null)
            {
                _context.client.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.client.Any(e => e.Id == id);
        }
    }
}
