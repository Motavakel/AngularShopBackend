using Domain.Entities.Base;

namespace Domain.Entities.ProductEntity;

public class ProductBrand : BaseAuditableEntity, ICommands
{
    public string Title { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Product> Products { get; set; }
}