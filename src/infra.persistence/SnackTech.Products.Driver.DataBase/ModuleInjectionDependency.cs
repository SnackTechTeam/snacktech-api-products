using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SnackTech.Products.Common.Interfaces.DataSources;
using SnackTech.Products.Driver.DataBase.DataSources;

namespace SnackTech.Products.Driver.DataBase;

[ExcludeFromCodeCoverage]
public static class ModuleInjectionDependency
{
    public static IServiceCollection AddAdapterDatabaseRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProdutoDataSource, ProdutoDataSource>();

        return services;
    }
}