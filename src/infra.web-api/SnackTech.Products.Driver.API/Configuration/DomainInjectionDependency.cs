using System.Diagnostics.CodeAnalysis;
using SnackTech.Products.Core.Interfaces;
using SnackTech.Products.Core.Controllers;

namespace SnackTech.Products.Driver.API.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class DomainInjectionDependency
    {
        public static IServiceCollection AddDomainControllers(this IServiceCollection services)
        {
            services.AddTransient<IProdutoController, ProdutoController>();

            return services;
        }
    }
}