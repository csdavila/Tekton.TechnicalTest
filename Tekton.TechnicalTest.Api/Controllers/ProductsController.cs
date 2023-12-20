using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tekton.TechnicalTest.Application.Products.Commands;
using Tekton.TechnicalTest.Application.Products.Queries;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tekton.TechnicalTest.Api.Controllers
{
    ///<Summary>
    /// ProductsController
    ///</Summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET api/<ProductsController>/5
        /// <summary>
        /// Consulta un producto por su ID
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Success</response>
        /// <response code="400">Product has missing/invalid values</response>
        /// <response code="401">Access Denied Exception</response>
        /// <response code="403">Forbidden Exception</response>
        /// <response code="404">Not Found Exception</response>
        /// <response code="500">Oops! Can't find your product right now</response>
        /// <returns></returns>
        [HttpGet("{ProductId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<GetProductQueryResponse> Get([FromRoute] GetProductQuery query) => _mediator.Send(query);


        // POST api/<ProductsController>
        /// <summary>
        /// Crea un producto nuevo
        /// </summary>
        /// <param name="command"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /products
        ///     {
        ///        "name" : "Producto Swagger",
        ///        "status" : 1,
        ///        "stock" : 1,
        ///        "description" : "Producto Demo",
        ///        "price" : 100
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="201">Product created</response>
        /// <response code="400">Product has missing/invalid values</response>
        /// <response code="401">Access Denied Exception</response>
        /// <response code="403">Forbidden Exception</response>
        /// <response code="500">Oops! Can't create your product right now</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            var entity = await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created, entity);
        }

        // PUT api/<ProductsController>/5
        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="command"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /products
        ///     {
        ///        "productId": "1",
        ///        "name" : "Producto Swagger",
        ///        "status" : 1,
        ///        "stock" : 1,
        ///        "description" : "Producto Demo",
        ///        "price" : 100
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="201">Product updated</response>
        /// <response code="400">Product has missing/invalid values</response>
        /// <response code="401">Access Denied Exception</response>
        /// <response code="403">Forbidden Exception</response>
        /// <response code="500">Oops! Can't update your product right now</response>
        /// <returns></returns>
        [HttpPut("{ProductId}")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }


    }
}
