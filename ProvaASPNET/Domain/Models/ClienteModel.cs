using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ClienteModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Código é obrigatório")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }


        public static ClienteModel FromEntity(ClienteEntity entity)
        {
            return new ClienteModel
            {
                Id = entity.Id,
                Codigo = entity.Codigo,
                Nome = entity.Nome,
                Cpf = entity.Cpf,
                DataNascimento = entity.DataNascimento
            };
        }
    }
}
