using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using solvexTecnical.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace solvexTecnical.Infraestructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){
        }

        public DbSet<Users> User { get; set; }
        public DbSet<Products> Product { get; set; }
        public DbSet<ProductsBrands> ProductsBrand { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }
        public DbSet<SuperMarket> SuperMarket { get; set; }
        

        protected override void OnModelCreating(ModelBuilder m)
        {

            #region Keys
            m.Entity<Users>().HasKey(x => x.Id);
            m.Entity<Products>().HasKey(x => x.Id);
            m.Entity<ProductsBrands>().HasKey(x => x.Id);
            m.Entity<FinalProducts>().HasKey(x => x.Id);
            m.Entity<ShoppingList>().HasKey(x => x.Id);
            m.Entity<ShoppingListProducts>().HasKey(x => x.Id);
            m.Entity<SuperMarket>().HasKey(x => x.Id);
            #endregion

            #region Relations
            
            // brands - market
            
            m.Entity<ProductsBrands>()
                .HasOne(x => x.SuperMarket)
                .WithMany(x => x.ProductsBrands)
                .HasForeignKey(x => x.SuperMarketId)
                .OnDelete(DeleteBehavior.NoAction);

            // products - market
            m.Entity<Products>()
                .HasOne(x => x.SuperMarket)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.SuperMarketId)
                .OnDelete(DeleteBehavior.NoAction);

            // brands - products (FinalProducts)
            m.Entity<FinalProducts>()
                .HasOne(x => x.Product)
                .WithMany(x => x.FinalProducts)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            m.Entity<FinalProducts>()
                .HasOne(x => x.Brand)
                .WithMany(x => x.FinalProducts)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            // shopping list - user
            m.Entity<ShoppingList>()
                .HasOne(x => x.User)
                .WithMany(x => x.ShoppingLists)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // shopping list - FinalProducts
            m.Entity<ShoppingListProducts>()
                .HasOne(x => x.ShoppingList)
                .WithMany(x => x.ShoppingListProducts)
                .HasForeignKey(x => x.ShoppingListId)
                .OnDelete(DeleteBehavior.NoAction);

            m.Entity<ShoppingListProducts>()
               .HasOne(x => x.FinalProducts)
               .WithMany(x => x.ShoppingListProducts)
               .HasForeignKey(x => x.FinalProductId)
               .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Props
            m.Entity<Users>().Property(x => x.Name).IsRequired();
            m.Entity<FinalProducts>().Property(x => x.Price).HasColumnType("decimal(18,2)");
            m.Entity<ShoppingList>().Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
            #endregion

            base.OnModelCreating(m);
        }
    }
    
}
