using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TP_test.Models
{
    public class Panier
    {
        public int ID { get; set; }
        [DisplayName("Id de l'utilisateur")]
        public required string UserGuid { get; set; }
        public bool StatutAchat { get; set; }
        [DisplayName("Moment de l'achat")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm:ss}")]
        public DateTime? TimeAchat { get; set; }
        public List<ProduitPanier>? ProduitPanier { get; set; }

        public Panier()
        {
            StatutAchat = false;
        }

    }
}
