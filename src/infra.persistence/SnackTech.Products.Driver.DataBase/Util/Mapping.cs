using AutoMapper;
using SnackTech.Products.Common.Dto.DataSource;
using SnackTech.Products.Driver.DataBase.Entities;

namespace SnackTech.Products.Driver.DataBase.Util;

public static class Mapping
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg => {
            // This line ensures that internal properties are also mapped over.
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<MappingProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => Lazy.Value;
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Produto, ProdutoDto>();
        CreateMap<ProdutoDto, Produto>();

        CreateMap<Produto, ProdutoDto>();
        CreateMap<ProdutoDto, Produto>();
    }
}
