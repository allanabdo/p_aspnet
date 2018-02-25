using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProdutoModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Código é obrigatório")]
        [MaxLength(30, ErrorMessage = "Tamanho máximo de 30 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Código de barras é obrigatório")]
        [MaxLength(30, ErrorMessage = "Tamanho máximo de 15 caracteres")]
        public string CodigoBarra { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório")]
        [MaxLength(30, ErrorMessage = "Tamanho máximo de 255 caracteres")]
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
