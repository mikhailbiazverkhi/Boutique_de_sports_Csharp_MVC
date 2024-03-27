using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TP_test.Database;
using TP_test.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Hosting;


namespace TP_test.Models
{
    public static class DBInitializer
    {
        public static void Initialize(ProduitContext context)
        {
            if (context.Produits.Any() || context.Images.Any())
                return;

            var produits = new List<Produit>()
            {
                //Catégorie : Hockey
                new Produit
                {
                    Nom = "Bâton de hockey Easton Synergy",
                    Description = "Bâton de hockey en composite léger avec une lame renforcée.",
                    Marque = "Easton",
                    Taille = "38 pouces",
                    Categorie = CategorieType.Hockey,
                    Quantite = 100,
                    ImageID = 1
                },
                new Produit
                {
                    Nom = "Casque de hockey Bauer IMS 9.0",
                    Description = "Casque de hockey avec coque en polycarbonate et système de protection",
                    Marque = "Bauer",
                    Taille = "s",
                    Categorie = CategorieType.Hockey,
                    Quantite = 100,
                    ImageID = 2
                },
                new Produit
                {
                    Nom = "Gants de hockey CCM Tacks",
                    Description = "Gants de hockey offrant une grande protection et une excellente prise en main.",
                    Marque = "CCM",
                    Taille = "14 pouces",
                    Categorie = CategorieType.Hockey,
                    Quantite = 100,
                    ImageID = 3
                },

                //Catégorie : Baseball
                new Produit
                {
                    Nom = "Batte de baseball DeMarini Voodoo",
                    Description = "Batte de baseball en alliage d'aluminium avec un grand sweet spot.",
                    Marque = "DeMarini",
                    Taille = "33 pouces",
                    Categorie = CategorieType.Baseball,
                    Quantite = 100,
                    ImageID = 4
                },
                new Produit
                {
                    Nom = "Gant de baseball Wilson A2000",
                    Description = "Gant de baseball en cuir Pro Stock offrant une grande durabilité.",
                    Marque = "Wilson",
                    Taille = "12 pouces",
                    Categorie = CategorieType.Baseball,
                    Quantite = 100,
                    ImageID = 5
                },
                new Produit
                {
                    Nom = "Balle de baseball Rawlings Official League",
                    Description = "Balle de baseball officielle en cuir synthétique.",
                    Marque = "Rawlings",
                    Taille = "9 pouces",
                    Categorie = CategorieType.Baseball,
                    Quantite = 100,
                    ImageID = 6
                },

                //Catégorie : Soccer
                new Produit
                {
                    Nom = "Ballon de soccer Adidas Tango",
                    Description = "Ballon de soccer réplique du Tango original, idéal pour l'entraînement.",
                    Marque = "Adidas",
                    Taille ="4",
                    Categorie = CategorieType.Soccer,
                    Quantite = 100,
                    ImageID = 7
                },
                new Produit
                {
                    Nom = "Chaussures de football Puma Future Z",
                    Description = "Chaussures de football avec une tige en tricot pour un meilleur contrôle de la balle.",
                    Marque = "Puma",
                    Taille ="9",
                    Categorie = CategorieType.Soccer,
                    Quantite = 100,
                    ImageID = 8
                },
                new Produit
                {
                    Nom = "Protège-tibias Nike Mercurial Lite",
                    Description = "Protège-tibias légers avec une coque en plastique durable.",
                    Marque = "Nike",
                    Taille ="M",
                    Categorie = CategorieType.Soccer,
                    Quantite = 100,
                    ImageID = 9
                },

                //Catégorie : Velo
                new Produit
                {
                    Nom = "Vélo de route Specialized Tarmac",
                    Description = "Vélo de route en carbone avec transmission Shimano Ultegra.",
                    Marque = "Specialized",
                    Taille ="56 cm",
                    Categorie = CategorieType.Velo,
                    Quantite = 100,
                    ImageID = 10
                },
                new Produit
                {
                    Nom = "Casque de vélo Giro Synthe",
                    Description = "Casque de vélo aérodynamique avec système de refroidissement.",
                    Marque = "Giro",
                    Taille ="L",
                    Categorie = CategorieType.Velo,
                    Quantite = 100,
                    ImageID = 11
                },
                new Produit
                {
                    Nom = "Cuissard de vélo Pearl Izumi Elite",
                    Description = "Cuissard de vélo avec rembourrage Elite In-R-Cool pour un confort optimal.",
                    Marque = "Pearl Izumi",
                    Taille ="s",
                    Categorie = CategorieType.Velo,
                    Quantite = 100,
                    ImageID = 12
                }
            };


            var images = new List<Image>()
            {
            //Catégorie : Hockey
            new Image
            { NomImage = "bat_hockeyjpg.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Hockey/bat_hockeyjpg.jpg"),
              ContentType = "image/jpeg"
            },
            new Image
            { NomImage = "casque_hockey.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Hockey/casque_hockey.jpg"),
              ContentType = "image/jpeg"
            },
            new Image
            { NomImage = "gant_hockey.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Hockey/gant_hockey.jpg"),
              ContentType = "image/jpeg"
            },

            //Catégorie : Baseball
            new Image
            { NomImage = "batte1.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Baseball/batte1.jpg"),
              ContentType = "image/jpeg"
            },
            new Image
            { NomImage = "gants_frappeur.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Baseball/gants_frappeur.jpg"),
              ContentType = "image/jpeg"
            },
            new Image
            { NomImage = "balle_baseball.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Baseball/balle_baseball.jpg"),
              ContentType = "image/jpeg"
            },

            //Catégorie : Soccer
            new Image
            { NomImage = "baalon_soccer.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Soccer/baalon_soccer.jpg"),
              ContentType = "image/jpeg"
            },
            new Image
            { NomImage = "chaussures.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Soccer/chaussures.jpg"),
              ContentType = "image/jpeg"
            },
            new Image
            { NomImage = "protege_tybia.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Soccer/protege_tybia.jpg"),
              ContentType = "image/jpeg"
            },

            //Catégorie : Velo
            new Image
            { NomImage = "velo.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Velo/velo.jpg"),
              ContentType = "image/jpeg"
            },
            new Image
            { NomImage = "casque_velo.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Velo/casque_velo.jpg"),
              ContentType = "image/jpeg"
            },
            new Image
            { NomImage = "cuissard.jpg",
              ImageData = GetPhoto("./wwwroot/images/produits/Velo/cuissard.jpg"),
              ContentType = "image/jpeg"
            }
            };

            foreach (Image img in images)
            {
                context.Images.Add(img);
            }

            foreach (Produit prod in produits)
            {
                context.Produits.Add(prod);
            }

            context.SaveChanges();
        }

        public static byte[] GetPhoto(string filePath)
        {
            using (FileStream stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        public static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ProduitContext>();
                    Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
