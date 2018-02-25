using Domain.Entities;
using Domain.Models.CustomValidations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ClienteModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Código é obrigatório")]
        [MaxLength(30, ErrorMessage = "Tamanho máximo de 30 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(30, ErrorMessage = "Tamanho máximo de 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [MaxLength(14, ErrorMessage = "Tamanho máximo de 14 caracteres")]
        [ValidCpf]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        public string DataNascimento { get; set; }


        public static ClienteModel FromEntity(ClienteEntity entity)
        {
            return new ClienteModel
            {
                Id = entity.Id,
                Codigo = entity.Codigo,
                Nome = entity.Nome,
                Cpf = entity.Cpf,
                DataNascimento = entity.DataNascimento.ToString("dd/MM/yyyy")
            };
        }
    }
}
