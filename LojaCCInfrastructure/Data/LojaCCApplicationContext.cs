using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LojaCCDomain.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaCCInfrastructure.Data
{
    public class LojaCCApplicationContext : DbContext
    {
        public LojaCCApplicationContext(DbContextOptions<LojaCCApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Pedidos)
                .WithOne()
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pedido>()
                .HasMany(a => a.ItensPedido)
                .WithOne()
                .HasForeignKey(x => x.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasMany(i => i.ItensPedido)
                .WithOne()
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<User>(x => x.ClienteId);

            modelBuilder.Entity<Pedido>()
                .Property(a => a.PedidoId);

            modelBuilder.Entity<ItemPedido>()
                .Property(b => b.ItemPedidoId);

            modelBuilder.Entity<Item>()
                .Property(i => i.ItemId);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.ClienteId);
        }

        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<User> User { get; set; }


    }
}
