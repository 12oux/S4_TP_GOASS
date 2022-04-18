using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class ProduitContext : DbContext
    {
        public ProduitContext(DbContextOptions<ProduitContext> options) : base(options) 
        { }
        public DbSet<Produit> Produit { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Panier> Panier { get; set; }
        public DbSet<ItemPanier> ItemPanier { get; set; }
        public DbSet<ItemCommande> ItemCommande { get; set; }
        public DbSet<Commande> Commande { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produit>().ToTable("Produits");
            modelBuilder.Entity<Image>(entity => entity.Property(x => x.ImageData).HasColumnType("varbinary(max)"));
        }
    }
}
