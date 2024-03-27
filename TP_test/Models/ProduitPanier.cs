using System.ComponentModel;

namespace TP_test.Models
{
    public class ProduitPanier
    {
        public int ID { get; set; }
        public int ProduitID { get; set; }
        public int PanierID { get; set; }
        [DisplayName("Nb items")]
        public int Quantite { get; set; }
        public Panier? Panier { get; set; }
        public Produit? Produit { get; set; }
    }
}
