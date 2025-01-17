﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TP_test.Database;

#nullable disable

namespace TP_test.Migrations
{
    [DbContext(typeof(ProduitContext))]
    partial class ProduitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TP_test.Models.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageID"));

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("NomImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TP_test.Models.Panier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("StatutAchat")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("TimeAchat")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Panier");
                });

            modelBuilder.Entity("TP_test.Models.Produit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Categorie")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ImageID")
                        .HasColumnType("int");

                    b.Property<string>("Marque")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.Property<string>("Taille")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImageID");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("TP_test.Models.ProduitPanier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("PanierID")
                        .HasColumnType("int");

                    b.Property<int>("ProduitID")
                        .HasColumnType("int");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PanierID");

                    b.HasIndex("ProduitID");

                    b.ToTable("ProduitPanier");
                });

            modelBuilder.Entity("TP_test.Models.Produit", b =>
                {
                    b.HasOne("TP_test.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("TP_test.Models.ProduitPanier", b =>
                {
                    b.HasOne("TP_test.Models.Panier", "Panier")
                        .WithMany("ProduitPanier")
                        .HasForeignKey("PanierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TP_test.Models.Produit", "Produit")
                        .WithMany("ProduitPanier")
                        .HasForeignKey("ProduitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Panier");

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("TP_test.Models.Panier", b =>
                {
                    b.Navigation("ProduitPanier");
                });

            modelBuilder.Entity("TP_test.Models.Produit", b =>
                {
                    b.Navigation("ProduitPanier");
                });
#pragma warning restore 612, 618
        }
    }
}
