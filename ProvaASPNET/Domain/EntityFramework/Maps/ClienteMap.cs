using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Domain.EntityFramework.Maps
{
    internal class ClienteMap : EntityTypeConfiguration<ClienteEntity>
    {

        public ClienteMap()
        {
            ToTable("tbl_cliente");
            Property(x => x.Id).
                HasColumnName("id_cliente").
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).
                IsRequired();

            Property(x => x.Codigo).HasColumnName("codigo").HasMaxLength(30).IsRequired()
            .HasColumnAnnotation(
            "Index",
            new IndexAnnotation(new IndexAttribute("IX_codigoCli") { IsUnique = true })); 

            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(200).IsRequired();
            Property(x => x.Cpf).HasColumnName("cpf").HasMaxLength(14).IsRequired()
            .HasColumnAnnotation(
            "Index",
            new IndexAnnotation(new IndexAttribute("IX_cpfCli") { IsUnique = true }));

            Property(x => x.DataNascimento).HasColumnName("data_nascimento").IsRequired();
            Property(x => x.DataCadastro).HasColumnName("data_cadastro").IsRequired();

        }
    }
}
