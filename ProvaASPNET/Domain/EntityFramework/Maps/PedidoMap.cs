using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Domain.EntityFramework.Maps
{
    internal class PedidoMap : EntityTypeConfiguration<PedidoEntity>
    {
        public PedidoMap()
        {
            ToTable("tbl_pedido");
            Property(x => x.Id).
                HasColumnName("id_pedido").
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).
                IsRequired();

            Property(x => x.Codigo).HasColumnName("codigo").HasMaxLength(30).IsRequired()
            .HasColumnAnnotation(
            "Index",
            new IndexAnnotation(new IndexAttribute("IX_codigoPedi") { IsUnique = true }));

            Property(x => x.ClienteId).HasColumnName("id_cliente").IsRequired();
            HasRequired(x => x.Cliente).WithMany().HasForeignKey(x => x.ClienteId);

            HasMany(x => x.Produtos).WithMany().Map(cs =>
            {
                cs.MapLeftKey("id_pedido");
                cs.MapRightKey("id_produto");
                cs.ToTable("tbl_pedido_produto");
            });

            Property(x => x.ValorTotal).HasColumnName("valor_total").IsRequired();
            Property(x => x.DataPedido).HasColumnName("data_pedido").IsRequired();

        }
    }
}
