using System;

namespace Domain.Entities
{
    public class ClienteEntity
    {
        public Guid Id { get; protected set; }
        public string Codigo { get; protected set; }
        public string Nome { get; protected set; }
        public string Cpf { get; protected set; }
        public DateTime DataNascimento { get; protected set; }
        public DateTime DataCadastro { get; protected set; }

        public ClienteEntity()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        public ClienteEntity(string codigo, string nome, string cpf, DateTime dataNascimento) : this()
        {
            Codigo = codigo;
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
    }
}
