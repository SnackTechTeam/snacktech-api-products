using System.Diagnostics.CodeAnalysis;
using SnackTech.Products.Core.Controllers;
using SnackTech.Products.Core.Interfaces;

namespace SnackTech.Products.Driver.API.Configuration;

[ExcludeFromCodeCoverage]
public static class DomainInjectionDependency
{
    public static IServiceCollection AddDomainControllers(this IServiceCollection services)
    {
        services.AddTransient<IProdutoController, ProdutoController>();

        return services;
    }
}