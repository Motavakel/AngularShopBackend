using Application.Dtos.ProductTypeDto;
using Application.Features.ProductTypes.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers;

public class ProductTypeController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductTypeDto>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetAllProductTypeQuery(), cancellationToken));
    }
}