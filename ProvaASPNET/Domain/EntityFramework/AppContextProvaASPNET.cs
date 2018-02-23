using Domain.Entities;
using Domain.EntityFramework.Maps;
using System.Data.Entity;

namespace Domain.EntityFramework
{
    public class AppContextProvaASPNET : DbContext
    {
        public AppContextProvaASPNET() : base("name=ProvaASPNET")
        {
            
        }

        public DbSet<ClienteEntity> Clientes { get; set; }
        public DbSet<ProdutoEntity> Produtos { get; set; }
        public DbSet<PedidoEntity> Pedidos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new PedidoMap());
        }
    }
}
