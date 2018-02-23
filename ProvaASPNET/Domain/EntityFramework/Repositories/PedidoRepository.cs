using Domain.Entities;

namespace Domain.EntityFramework.Repositories
{
    internal class PedidoRepository : GenericRepository<PedidoEntity>
    {
        public PedidoRepository(AppContextProvaASPNET context) : base(context)
        {

        }
    }
}
