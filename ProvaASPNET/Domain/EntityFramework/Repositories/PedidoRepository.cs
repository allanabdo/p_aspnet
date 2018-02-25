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


        public List<PedidoEntity> Lista(int limite, int offset, string codigo, string cliente, string dataInicial, string dataFinal)
        {

            var query = AsQueryable;


            if (!string.IsNullOrEmpty(codigo))
            {
                query = query.Where(x => x.Codigo.Contains(codigo));
            }

            if (!string.IsNullOrEmpty(cliente))
            {
                query = query.Where(x => x.Cliente.Nome.Contains(cliente));
            }

            if (!string.IsNullOrEmpty(dataInicial) && !string.IsNullOrEmpty(dataFinal))
            {
                if (DateTime.TryParse(dataInicial, out var dataI) && DateTime.TryParse(dataFinal, out var dataF))
                {
                    query = query.Where(x => x.DataPedido >= dataI && x.DataPedido <= dataF);
                }
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
