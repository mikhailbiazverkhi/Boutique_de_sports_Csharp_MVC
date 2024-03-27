using Microsoft.EntityFrameworkCore;
using TP_test.Models;

namespace TP_test.Database
{
    public class ProduitContext : DbContext
    {
        public ProduitContext(DbContextOptions<ProduitContext> options) : base(options)
        {
        }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Panier> Panier { get; set; }
        public DbSet<ProduitPanier> ProduitPanier { get; set; }
    }
}
