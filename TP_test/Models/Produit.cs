using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TP_test.Models
{
    public enum CategorieType
    {
        Hockey,
        Baseball,
        Soccer,
        Velo
    }

    public class Produit
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Le champ nom doit être fournit.")]
        public string Nom { get; set; }
        
        [Required(ErrorMessage = "Le champ description doit être fournit.")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Le champ marque doit être fournit.")]
        public string Marque { get; set; }
        
        [DisplayName("Taille / Format")]
        public string? Taille { get; set; }
        
        [DisplayName("Catégorie")]
        [EnumDataType(typeof(CategorieType))]
        public CategorieType Categorie { get; set; }

        [DisplayName("Quantité")]
        [Required(ErrorMessage = "Le champ quantite doit être fournit.")]
        public int Quantite { get; set; }
        public int? ImageID { get; set; }
        public Image? Image { get; set; }
        public List<ProduitPanier>? ProduitPanier { get; set; }

        public Produit()
        {
            Nom = "";
            Description = "";
            Marque = "";
            Quantite = 0;
        }

    }
}
