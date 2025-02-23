using Application.Dtos.Products;
using AutoMapper;
using Domain.Entities.ProductEntity;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Mapping.Resolvers;


public class ProductImageUrlResolver<T> : IValueResolver<Product, T, string>
{
    private readonly IConfiguration _configuration;

    public ProductImageUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(Product source, T destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
            return _configuration["BackendUrl"] + _configuration["LocationImages:ProductsImageLocation"] +
                   source.PictureUrl;
        return null;
    }
}