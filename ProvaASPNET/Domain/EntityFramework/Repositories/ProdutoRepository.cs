using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Domain.EntityFramework.Repositories
{
    internal class ProdutoRepository : GenericRepository<ProdutoEntity>
    {
        public ProdutoRepository(AppContextProvaASPNET context) : base(context)
        {

        }


        public bool CodigoExiste(string codigo)
        {
            var query = AsQueryable.FirstOrDefault(x => x.Codigo == codigo);
            return query != null;
        }

        public bool CodigoBarraExiste(string codigoBarra)
        {
            var query = AsQueryable.FirstOrDefault(x => x.CodigoBarra == codigoBarra);
            return query != null;
        }

        public List<ProdutoEntity> Lista(int limite, int offset, string codigo)
        {

            var query = AsQueryable;


            if (!string.IsNullOrEmpty(codigo))
            {
                query = query.Where(x => x.Codigo.Contains(codigo));
            }
            
            query = query.OrderByDescending(x => x.DataCadastro).Skip(offset).Take(limite);

            return query.ToList();
        }

        public int TotalRegistros(string codigo)
        {
            var query = AsQueryable;

            if (!string.IsNullOrEmpty(codigo))
            {
                query = query.Where(x => x.Codigo.Contains(codigo));
            }


            return query.Count();
        }
    }
}
