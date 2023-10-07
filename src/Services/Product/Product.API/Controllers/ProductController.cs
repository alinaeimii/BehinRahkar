using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Commands;
using Product.Application.DTOs;
using Product.Application.Queries;

namespace Product.API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost("product")]
        public async Task<IActionResult> AddProduct(IMediator mediator, [FromBody] ProductsDTO productDto)
        {
            return Ok(await mediator.Send(new ProductCommand.AddProductCommand(productDto)));
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProduct(IMediator mediator)
        {
            return Ok(await mediator.Send(new ProductQuery.ReadAllProductQuery()));
        }
    }
}
