using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class PedidoEntity
    {
        public Guid Id { get; protected set; }
        public string Codigo { get; protected set; }
        public Guid ClienteId { get; protected set; }
        public virtual ClienteEntity Cliente { get; protected set; }
        public virtual ICollection<ProdutoEntity> Produtos { get; protected set; } = new Collection<ProdutoEntity>();
        public double ValorTotal { get; protected set; }
        public DateTime DataPedido { get; protected set; }


        public PedidoEntity()
        {
            Id = Guid.NewGuid();
            DataPedido = DateTime.Now;
        }

        public PedidoEntity(string codigo, Guid clienteId, double valorTotal) : this()
        {
            Codigo = codigo;
            ClienteId = clienteId;
            ValorTotal = valorTotal;
        }

        public void AddProduto(Guid id)
        {
            var produto = new ProdutoEntity();
            produto.SetId(id);
            Produtos.Add(produto);
        }


    }
}
