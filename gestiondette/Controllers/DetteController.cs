using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestiondette.Models;
using gestiondette.Enum;
using gestiondette.Helpers;

namespace gestiondette.Controllers
{
    public class DetteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dette
        public async Task<IActionResult> Index(string status, int pageNumber = 1, int pageSize = 3)
        {
            var dettes = from d in _context.dette
                         select d;

            // Filter by status
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "archiver")
                {
                    dettes = dettes.Where(d => d.StateDette == StateDette.ARCHIVER);
                }
                else if (status == "desarchiver")
                {
                    dettes = dettes.Where(d => d.StateDette == StateDette.DESARCHIVER);
                }
                else
                {
                    dettes = from d in _context.dette
                             select d; ;
                }
            }

            // Calculate total number of filtered items
            int totalClients = await dettes.CountAsync();

            // Apply pagination if pageSize > 0
            if (pageSize > 0)
            {
                dettes = dettes.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

            // Calculate total pages based on pageSize
            int totalPages = (int)Math.Ceiling(totalClients / (double)pageSize);

            // Pass total pages and current page to ViewBag
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            // Return the view with the paginated and filtered data
            return View(await dettes.ToListAsync());
        }



        // GET: Dette/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dette = await _context.dette
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dette == null)
            {
                return NotFound();
            }


            return View(dette);
        }

        // GET: Dette/Create
        public async Task<IActionResult> Create()
        {

            var clients = await _context.client.ToListAsync();
            var articles = await _context.article.ToListAsync();
            ViewBag.Clients = clients;
            ViewBag.Articles = articles;
            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier") ?? new Panier();
            // HttpContext.Session.Remove("panier");
            ViewBag.panier = panier;
            // ViewBag.neub = "enabled";
            if (TempData["selectedClient"] != null)
            {

                int selectedClient = (int)TempData["selectedClient"];
                ViewBag.selectedClient = selectedClient;
                Console.WriteLine("******************************* cac'est mon idddddddddddddd  **************************");
                Console.WriteLine($"ClientId: {ViewBag.selectedClient}");
                Console.WriteLine("******************************* **************************");

                Client client = _context.client.FirstOrDefault(c => c.Id == selectedClient);

                Console.WriteLine("******************************* cac'est mon client  **************************");
                Console.WriteLine($"ClientId: {client}");
                Console.WriteLine("******************************* **************************");
                panier.client = client;
                HttpContext.Session.SetObjectAsJson("panier", panier);
                Console.WriteLine("******************************* cac'est mon panier  **************************");
                Console.WriteLine($"ClientId: {panier.client}");
                Console.WriteLine("******************************* **************************");
                // ViewBag.neub = "disabled";
                // Console.WriteLine(selectedClient);

            }




            return View();
        }

        [HttpPost]
        public async Task<IActionResult> create()
        {


            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier") ?? new Panier();
            var dette = new Dette();
            dette.StateDette = StateDette.DESARCHIVER;
            dette.CreateAt = DateTime.UtcNow;
            Console.WriteLine("******************************* **************************");
            Console.WriteLine("je suis la mon bbebebebebebbebebebebebebebbebbebe");
            Console.WriteLine(panier.client);
            Console.WriteLine("*************************************************************");

            int ClientId = panier.client.Id;

            dette.Client = _context.client.FirstOrDefault(c => c.Id == ClientId);
            double MontantTotal = 0.0;
            foreach (DetailDette detailDette in panier.detailDettes)
            {
                detailDette.Dette = dette;
                detailDette.ArticleId = detailDette.Article.Id;
                _context.Add(detailDette);
                MontantTotal += detailDette.Total;
            }
            dette.Montant = MontantTotal;
            dette.MontantRestant = MontantTotal;
            dette.MontantVerser = 0.0;
            dette.EtatDette = EtatDette.ENCOURS;
            dette.UpdateAt = DateTime.UtcNow;
            _context.Add(dette);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("panier");
            return RedirectToAction(nameof(Index));
        }


        public IActionResult AjouterArticle(int articleId, int qte, string ClientId)
        {

            var article = _context.article.FirstOrDefault(a => a.Id == articleId);
            Console.WriteLine("'''''''''''''*************************************''''''''''''''''''''''''");
            Console.WriteLine($"Article: {article}, Quantity: {qte}");
            Console.WriteLine("''''''''''''************************************************************'''''''''''''''''''''''''");
            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier") ?? new Panier();


            panier.AjouterArticles(article, qte);
            HttpContext.Session.SetObjectAsJson("panier", panier);

            // Faire un dump du panier

            if (ViewBag.selectedClient == null)
            {
                ViewBag.selectedClient = Convert.ToInt32(ClientId);
                TempData["selectedClient"] = ViewBag.selectedClient; // Sto
                Console.WriteLine("******************************* **************************");
                Console.WriteLine($"ClientId: {ViewBag.selectedClient}");
                Console.WriteLine("******************************* **************************");
            }

            return RedirectToAction("Create");
        }

        // POST: Dette/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create(Dette dette)
        // {
        //     var id = int.Parse(Request.Form["ClientId"]!);


        //     if (ModelState.IsValid)
        //     {

        //         var client = await _context.client.FindAsync(id);

        //         if (client == null)
        //         {
        //             // Handle the case where the client is not found
        //             ModelState.AddModelError("", "Client not found.");
        //             return View(dette);
        //         }

        //         // Now set the full Client object to the Dette model
        //         dette.Client = client;
        //         dette.MontantVerser = 0.0;
        //         dette.MontantRestant = dette.Montant;
        //         dette.EtatDette = EtatDette.VALIDER;
        //         dette.StateDette = StateDette.DESARCHIVER;

        //         _context.Add(dette);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }

        //     return View(dette);
        // }


        // GET: Dette/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dette = await _context.dette.FindAsync(id);
            if (dette == null)
            {
                return NotFound();
            }
            return View(dette);
        }

        // POST: Dette/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MontantRestant,MontantVerser,EtatDette,Montant,StateDette,Id,CreateAt,UpdateAt")] Dette dette)
        {
            if (id != dette.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetteExists(dette.Id))
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
            return View(dette);
        }

        // GET: Dette/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dette = await _context.dette
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dette == null)
            {
                return NotFound();
            }

            return View(dette);
        }

        // POST: Dette/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dette = await _context.dette.FindAsync(id);
            if (dette != null)
            {
                _context.dette.Remove(dette);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetteExists(int id)
        {
            return _context.dette.Any(e => e.Id == id);
        }
    }
}
