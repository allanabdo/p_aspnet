using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.EntityFramework.Repositories
{
    internal class PedidoRepository : GenericRepository<PedidoEntity>
    {
        public PedidoRepository(AppContextProvaASPNET context) : base(context)
        {

        }


        public int ClienteTotalPedidos(Guid clienteId)
        {
            var result = AsQueryable.Where(x=> x.ClienteId == clienteId);
            return result.Count();
        }

        public int ProdutoTotalPedidos(Guid produtoId)
        {
            var result = AsQueryable.Where(x => x.Produtos.Any(y => y.Id == produtoId));
            return result.Count();
        }

        public bool CodigoExsite(string codigo)
        {
            var first = AsQueryable.FirstOrDefault(x => x.Codigo == codigo);
            return first != null;
        }


        public List<PedidoEntity> Lista(int limite, int offset, string codigo)
        {

            var query = AsQueryable;


            if (!string.IsNullOrEmpty(codigo))
            {
                query = query.Where(x => x.Codigo.Contains(codigo));
            }

            query = query.OrderByDescending(x => x.DataPedido).Skip(offset).Take(limite);

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
