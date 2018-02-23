using Domain.Entities;

namespace Domain.EntityFramework.Repositories
{
    internal class ProdutoRepository : GenericRepository<ProdutoEntity>
    {
        public ProdutoRepository(AppContextProvaASPNET context) : base(context)
        {

        }
    }
}
