using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Domain.EntityFramework.Maps
{
    internal class ProdutoMap : EntityTypeConfiguration<ProdutoEntity>
    {
        public ProdutoMap()
        {
            ToTable("tbl_produto");
            Property(x => x.Id).
                HasColumnName("id_produto").
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).
                IsRequired();

            Property(x => x.Codigo).HasColumnName("codigo").HasMaxLength(30).IsRequired()
            .HasColumnAnnotation(
            "Index",
            new IndexAnnotation(new IndexAttribute("IX_codigoProd") { IsUnique = true }));

            Property(x => x.CodigoBarra).HasColumnName("codigo_barra").HasMaxLength(15).IsRequired()
           .HasColumnAnnotation(
           "Index",
           new IndexAnnotation(new IndexAttribute("IX_codigoBarraProd") { IsUnique = true }));

            Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(255).IsRequired();
            Property(x => x.ValorVenda).HasColumnName("valor_venda").IsRequired();
            Property(x => x.DataCadastro).HasColumnName("data_cadastro").IsRequired();
        }
    }
}
