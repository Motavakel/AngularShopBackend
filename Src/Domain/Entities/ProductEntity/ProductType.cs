using Domain.Entities.Base;

namespace Domain.Entities.ProductEntity;

public class ProductType : BaseAuditableEntity, ICommands
{
    public string Title { get; set; }
    public bool IsActive { get; set; }
}


