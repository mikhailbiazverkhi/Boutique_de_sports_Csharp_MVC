using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using TP_test.Configurations;
using TP_test.Database;
using TP_test.Models;


namespace TP_test.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly ProduitContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProduitsController(ProduitContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Produits
        public async Task<IActionResult> Index(CategorieType? categorie)
        {
            if (categorie != null)
            {
                return _context.Produits != null ?
                  View("Index", await _context.Produits.Include(x => x.Image).Where(x => x.Categorie == categorie).ToListAsync()) :
                  Problem("Entity set 'ProduitContext.Produits'  is null.");
            }
            else
            {
                return _context.Produits != null ?
                  View("Index", await _context.Produits.Include(x => x.Image).ToListAsync()) :
                  Problem("Entity set 'ProduitContext.Produits'  is null.");
            }
        }

        // GET: Produits/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produits == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits.Include(x => x.Image).SingleOrDefaultAsync(m => m.Id == id);

            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // GET: Produits/Create
        [Authorize(Roles = "Administrateur")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Marque,Taille,Categorie,Quantite,ImageID")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                var files = Request.Form.Files;
                if (files != null && files.Count > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await files.First().CopyToAsync(ms);
                        produit.Image = new Image()
                        {
                            NomImage = files.First().FileName,
                            ImageData = ms.ToArray(),
                            ContentType = files.First().ContentType
                        };
                    }
                }

                _context.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produit);
        }

        // GET: Produits/Edit/5
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produits == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits.Include(x => x.Image).SingleOrDefaultAsync(m => m.Id == id);

            if (produit == null)
            {
                return NotFound();
            }
            return View(produit);
        }

        // POST: Produits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Marque,Taille,Categorie,Quantite,ImageID")] Produit produit)
        {
            if (id != produit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var files = Request.Form.Files;
                    if (files != null && files.Count > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await files.First().CopyToAsync(ms);
                            produit.Image = new Image()
                            {
                                NomImage = files.First().FileName,
                                ImageData = ms.ToArray(),
                                ContentType = files.First().ContentType
                            };
                        }
                    }

                    if (produit.ImageID != null)
                    {
                        var image = _context.Images.Find(produit.ImageID);
                        if (image != null)
                        {
                            _context.Images.Remove(image);
                        }
                    }

                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.Id))
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

        // GET: Produits/Delete/5
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produits == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits.Include(x => x.Image).SingleOrDefaultAsync(m => m.Id == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produits == null)
            {
                return Problem("Entity set 'ProduitContext.Produits'  is null.");
            }

            var produit = await _context.Produits.FindAsync(id);

            if (produit != null)
            {
                if (produit.ImageID != null)
                {
                    var image = _context.Images.Find(produit.ImageID);
                    if (image != null)
                    {
                        _context.Images.Remove(image);
                    }
                }
                _context.Produits.Remove(produit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Produits/AjoutPanier
        [HttpPost]
        public async Task<IActionResult> AjoutPanier(int? id, int quantiteProduitWish)
        {
            if (id == null)
            {
                return NotFound();
            }

            var current_User = _userManager.GetUserAsync(HttpContext.User).Result;
            string current_User_Id = (current_User != null) ? "" + current_User.Id : "";

            var panier = _context.Panier.Where(x => x.StatutAchat != true).FirstOrDefault(x => x.UserGuid == current_User_Id);

            if (panier == null)
            {
                panier = new Panier()
                {
                    UserGuid = current_User_Id,
                    ProduitPanier = new List<ProduitPanier>()
                };
                _context.Add(panier);
                await _context.SaveChangesAsync();
            }

            var quantiteInventaire = _context.Produits.Where(x => x.Id == id.Value).FirstOrDefault()!.Quantite;

            if (quantiteInventaire > 0)
            {
                var item = _context.ProduitPanier.FirstOrDefault(x => x.PanierID == panier.ID && x.ProduitID == id.Value);

                if (item == null)
                {
                    if (quantiteProduitWish <= 0)
                    {
                        quantiteProduitWish = 1;
                    }
                    else if (quantiteProduitWish > quantiteInventaire)
                    {
                        quantiteProduitWish = quantiteInventaire;
                    }                  
                    item = new ProduitPanier() { PanierID = panier.ID, ProduitID = id.Value, Quantite = quantiteProduitWish };
                    _context.Add(item);
                }
                else
                {
                    if (item.Quantite + quantiteProduitWish > quantiteInventaire)
                    {
                        item.Quantite = quantiteInventaire;
                    } 
                    else if (item.Quantite + quantiteProduitWish <= 0)
                    {
                        item.Quantite = 1;
                    }
                    else
                    {
                        item.Quantite = item.Quantite + quantiteProduitWish;
                    }
                    _context.Update(item);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "ProduitPaniers");
        }

        private bool ProduitExists(int id)
        {
            return (_context.Produits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
