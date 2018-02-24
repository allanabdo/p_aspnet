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
    }
}
