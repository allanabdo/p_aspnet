using Domain.Unity;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Site
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container = DomainUnityConfig.Configure(container, new Unity.Lifetime.PerThreadLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
