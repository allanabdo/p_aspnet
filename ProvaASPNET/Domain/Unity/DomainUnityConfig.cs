using Domain.EntityFramework;
using Unity;
using Unity.Lifetime;

namespace Domain.Unity
{
    public class DomainUnityConfig
    {
        public static void Configure(IUnityContainer container, PerThreadLifetimeManager lifetime)
        {
            container.RegisterType<AppContextProvaASPNET>(lifetime);
  
        }
    }
}
