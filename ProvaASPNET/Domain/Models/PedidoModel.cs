using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Entities;

namespace Domain.Models
{
    public class PedidoModel
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public Guid ClienteId { get; set; }
        public virtual ClienteModel Cliente { get; set; }

        [Required(ErrorMessage = "Produtos são obrigatórios")]
        public ICollection<Guid> ProdutosId { get; set; }

        public ICollection<ProdutoModel> Produtos { get; set; }

        [Required(ErrorMessage = "Valor é obrigatório")]
        public double ValorTotal { get; set; }

        public DateTime DataPedido { get; set; }


        public static PedidoModel FromEntity(PedidoEntity entity)
        {
            var produtos = new List<ProdutoModel>();
            produtos.AddRange(entity.Produtos.Select(ProdutoModel.FromEntity));

            return new PedidoModel
            {
                ClienteId = entity.ClienteId,
                Cliente =  ClienteModel.FromEntity(entity.Cliente),
                Codigo = entity.Codigo,
                DataPedido = entity.DataPedido,
                Id = entity.Id,
                Produtos = produtos,
                ValorTotal = entity.ValorTotal
            };
        }
    }
}
