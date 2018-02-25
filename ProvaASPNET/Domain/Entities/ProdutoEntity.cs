using System;

namespace Domain.Entities
{
    public class ProdutoEntity
    {
        public Guid Id { get; protected set; }
        public string Codigo { get; protected set; }
        public string CodigoBarra { get; protected set; }
        public string Descricao { get; protected set; }
        public double ValorVenda { get; protected set; }
        public DateTime DataCadastro { get; protected set; }

        public ProdutoEntity()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        public ProdutoEntity(string codigo, string codigoBarra, string descricao, double valorVenda) : this()
        {
            Codigo = codigo;
            CodigoBarra = codigoBarra;
            Descricao = descricao;
            ValorVenda = valorVenda;
        }

        public void Update(string descricao, double valorVenda)
        {
            Descricao = descricao;
            ValorVenda = valorVenda;
        }

    }
}
