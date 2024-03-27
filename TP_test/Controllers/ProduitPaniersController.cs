using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TP_test.Configurations;
using TP_test.Database;
using TP_test.Models;

namespace TP_test.Controllers
{
    [Authorize]
    public class ProduitPaniersController : Controller
    {
        private readonly ProduitContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SmtpConfig _smtpConfig;
        private readonly SmtpClient _smtpClient;

        public ProduitPaniersController(ProduitContext context, UserManager<IdentityUser> userManager, IOptions<SmtpConfig> config)
        {
            _context = context;
            _userManager = userManager;

            //smtp
            _smtpConfig = config.Value;
            _smtpClient = new SmtpClient(_smtpConfig.Serveur, _smtpConfig.Port);
            _smtpClient.Credentials = new System.Net.NetworkCredential(_smtpConfig.Utilisateur, _smtpConfig.MotDePasse);
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.EnableSsl = true;
        }

        // GET: ProduitPaniers
        public async Task<IActionResult> Index()
        {
            if (_context.Produits == null)
            {
                return NotFound();
            }

            var current_User = _userManager.GetUserAsync(HttpContext.User).Result;
            string current_User_Id = (current_User != null) ? "" + current_User.Id : "";

            var panier = _context.Panier
                 .Where(x => x.UserGuid == current_User_Id && x.StatutAchat == false)
                 .FirstOrDefault();

            List<ProduitPanier>? produit;

            if (panier != null)
            {
                produit = await _context.ProduitPanier.Include(x => x.Panier)
                    .Where(x => x.PanierID! == panier.ID)
                    .Include(x => x.Produit)
                    .ThenInclude(x => x.Image)
                    .ToListAsync();
            }
            else
            {
                produit = null;
            }
            return View(produit);
        }

        // GET: ProduitPaniers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProduitPanier == null)
            {
                return NotFound();
            }
            var produitPanier = await _context.ProduitPanier.Include(x => x.Produit).ThenInclude(x => x.Image).FirstOrDefaultAsync(m => m.ID == id);

            if (produitPanier == null)
            {
                return NotFound();
            }

            return View(produitPanier);
        }

        // POST: ProduitPaniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProduitPanier == null)
            {
                return Problem("Entity set 'ProduitContext.ProduitPanier'  is null.");
            }

            var produitPanier = await _context.ProduitPanier.FindAsync(id);

            if (produitPanier != null)
            {
                _context.ProduitPanier.Remove(produitPanier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AcheterPanier(int? id)
        {
            //email message
            var current_User = _userManager.GetUserAsync(HttpContext.User).Result;
            string current_User_Id = (current_User != null) ? "" + current_User.Id : "";
            string email = (current_User != null) ? "" + current_User.Email : "";

            if (id == null)
            {
                return NotFound();
            }

            var produitsPanier = await _context.ProduitPanier
                .Include(x => x.Produit)
                .Where(x => x.PanierID == id)
                .ToListAsync();

            foreach (var produitPanier in produitsPanier)
            {
                var produit = produitPanier.Produit;
                var prod = await _context.Produits.Where(x => x.Id == produit!.Id).FirstAsync();

                prod!.Quantite -= produitPanier.Quantite;
                _context.Update(prod);
            }

            var panier = await _context.Panier.Where(x => x.ID == id).FirstAsync();

            panier.StatutAchat = true;
            panier.TimeAchat = DateTime.Now;
            _context.Update(panier);
            await _context.SaveChangesAsync();

            //email message
            if (email != string.Empty || email != null)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("root@cstjean.qc.ca", "Cégep Saint-Jean-Sur-Richelieu");
                mail.To.Add(new MailAddress(email));
                mail.Subject = "Vos résultats";
                mail.Body = "We've received you're order and will prepare it right away! " +
                "You can review your order here: " +
                "https://localhost:7198/Historiques/Details/" +
                panier.ID +
                " Thanks for your purchases!See you again! ";

                _smtpClient.Send(mail);
            }
            return View("Merci");
        }

        private bool ProduitPanierExists(int id)
        {
            return (_context.ProduitPanier?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
