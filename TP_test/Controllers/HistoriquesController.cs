using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_test.Database;
using TP_test.Models;

namespace TP_test.Controllers
{
    [Authorize]
    public class HistoriquesController : Controller
    {
        private readonly ProduitContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HistoriquesController(ProduitContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Historiques Users
        public async Task<IActionResult> Index()
        {
            var current_User = _userManager.GetUserAsync(HttpContext.User).Result;
            string current_User_Id = (current_User != null) ? "" + current_User.Id : "";


            var paniers = await _context.Panier.Where(x => x.StatutAchat == true && x.UserGuid == current_User_Id).Include(x => x.ProduitPanier).ToListAsync();

            return _context != null ?
            View(paniers) :
            Problem("Entity set 'ProduitContext.Panier'  is null.");
        }

        // GET: Historiques Admin
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> IndexAdmin()
        {
            var paniers = await _context.Panier.Where(x => x.StatutAchat == true).Include(x => x.ProduitPanier).ToListAsync();

            return _context != null ?
            View("Index", paniers) :
            Problem("Entity set 'ProduitContext.Panier'  is null.");

        }

        // GET: Historiques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProduitPanier == null)
            {
                return NotFound();
            }

            var produits = await _context.ProduitPanier
                .Where(x => x.PanierID == id && x.Panier!.StatutAchat == true)
                .Include(x => x.Panier)
                .Include(x => x.Produit)
                .ThenInclude(x => x.Image)
                .ToListAsync();

            if (produits == null)
            {
                return NotFound();
            }

            return View(produits);
        }
    }
}
