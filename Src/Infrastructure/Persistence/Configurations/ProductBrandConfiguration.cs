using Domain.Entities.ProductEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(100);
        builder.HasKey(x => x.Id);
        builder.HasMany(x=>x.Products).WithOne(x => x.ProductBrand).HasForeignKey(x => x.ProductBrandId);
    }
}