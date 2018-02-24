using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Domain.EntityFramework.Repositories
{
    internal class ClienteRepository : GenericRepository<ClienteEntity>
    {
        public ClienteRepository(AppContextProvaASPNET context) : base(context)
        {

        }


        public bool CpfExiste(string cpf)
        {
            var entity = AsQueryable.FirstOrDefault(x => x.Cpf == cpf);
            return entity != null;
        }


        public bool CodigoExiste(string codigo)
        {
            var entity = AsQueryable.FirstOrDefault(x => x.Codigo == codigo);
            return entity != null;
        }

        public List<ClienteEntity> Lista(int limite, int offset, ClientePesquisaModel pesquisa)
        {

            var query = AsQueryable;
            if (pesquisa != null)
            {
                if (!string.IsNullOrEmpty(pesquisa.Nome))
                {
                    query = query.Where(x => x.Nome.Contains(pesquisa.Nome));
                }

                if (!string.IsNullOrEmpty(pesquisa.Codigo))
                {
                    query = query.Where(x => x.Codigo.Contains(pesquisa.Codigo));
                }

                if (!string.IsNullOrEmpty(pesquisa.Cpf))
                {
                    query = query.Where(x => x.Cpf.Contains(pesquisa.Cpf));
                }
            }
            query = query.OrderByDescending(x => x.DataCadastro).Skip(offset).Take(limite);

            return query.ToList();
        }

        public int TotalRegistros(ClientePesquisaModel pesquisa)
        {
            var query = AsQueryable;

            if (pesquisa != null)
            {
                if (!string.IsNullOrEmpty(pesquisa.Nome))
                {
                    query = query.Where(x => x.Nome.Contains(pesquisa.Nome));
                }

                if (!string.IsNullOrEmpty(pesquisa.Codigo))
                {
                    query = query.Where(x => x.Codigo.Contains(pesquisa.Codigo));
                }

                if (!string.IsNullOrEmpty(pesquisa.Cpf))
                {
                    query = query.Where(x => x.Cpf.Contains(pesquisa.Cpf));
                }
            }

            return query.Count();
        }
    }
}
