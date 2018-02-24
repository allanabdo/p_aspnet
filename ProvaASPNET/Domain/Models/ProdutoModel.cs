using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProdutoModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Código é obrigatório")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Código de barras é obrigatório")]
        public string CodigoBarra { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Valor de venda é obrigatório")]
        public double ValorVenda { get; set; }

        public static ProdutoModel FromEntity(ProdutoEntity entity)
        {
            return new ProdutoModel
            {
                Id = entity.Id,
                Codigo = entity.Codigo,
                CodigoBarra = entity.CodigoBarra,
                Descricao = entity.Descricao,
                ValorVenda = entity.ValorVenda
            };
        }
    }
}
