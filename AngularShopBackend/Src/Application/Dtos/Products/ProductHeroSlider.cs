using Application.Common.Mapping;
using Application.Common.Mapping.Resolvers;
using AutoMapper;
using Domain.Entities.ProductEntity;

namespace Application.Dtos.Products;

public class ProductHeroSlider : IMapFrom<Product>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductHeroSlider>()
            .ForMember(x => x.PictureUrl,
                c => c.MapFrom<ProductImageUrlResolver<ProductHeroSlider>>());
    }
}