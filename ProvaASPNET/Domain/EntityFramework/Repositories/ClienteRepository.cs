using Domain.Entities;

namespace Domain.EntityFramework.Repositories
{
    internal class ClienteRepository : GenericRepository<ClienteEntity>
    {
        public ClienteRepository(AppContextProvaASPNET context) : base(context)
        {

        }
    }
}
