using Application.Dtos.Products;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetAll;
using Application.Features.Products.Queries.GetByBrandId;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers;

public class ProductsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetProductQuery(id), cancellationToken));
    }


    [HttpGet("brand/{id:int}")]
    public async Task<ActionResult<ProductHeroSlider>> GetProductsByBrandId([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetProductsByBrandIdQuery(id), cancellationToken));
    }
}