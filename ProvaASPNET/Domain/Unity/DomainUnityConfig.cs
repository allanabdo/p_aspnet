using Domain.EntityFramework;
using Domain.Interfaces.Services;
using Domain.Services;
using Unity;
using Unity.Lifetime;

namespace Domain.Unity
{
    public class DomainUnityConfig
    {
        public static UnityContainer Configure(UnityContainer container, PerThreadLifetimeManager lifetime)
        {
            container.RegisterType<AppContextProvaASPNET>(lifetime);
            
            container.RegisterType<IClienteService, ClienteService>();
            container.RegisterType<IProdutoService, ProdutoService>();

            return container;
        }
    }
}
