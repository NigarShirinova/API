using Business.Dtos.Product;
using Business.Features.Product.Commands.CreateProduct;
using Business.Features.Product.Commands.DeleteProduct;
using Business.Features.Product.Commands.UpdateProduct;
using Business.Features.Product.Queries.GetAllProducts;
using Business.Features.Product.Queries.GetProduct;
using Business.Services.Abstract;
using Business.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_task.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductController(IMediator mediator)
		{
			_mediator = mediator;
		}

		#region Documentation
		/// <summary>
		/// Products List 
		/// </summary>
		[ProducesResponseType(typeof(Response<List<ProductDto>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
		#endregion
		[HttpGet]
		public async Task<Response<List<ProductDto>>> GetAllProductsAsync()
		=> await _mediator.Send(new GetAllProductsQuery());

		#region Documentation
		/// <summary>
		/// Get product by Id 
		/// </summary>
		/// <param name="id"></param>
		[ProducesResponseType(typeof(Response<ProductDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
		#endregion
		[HttpGet("{id}")]
		public async Task<Response<ProductDto>> GetProductAsync(int id)
		=> await _mediator.Send(new GetProductQuery { Id = id});

		#region Documentation
		/// <summary>
		/// For product creating
		/// </summary>
		/// <remarks>
		/// <ul>
		/// <li><b>Type:</b><p>0 - New, 1 - Sold</p></li>>
		/// </ul>  
		/// </remarks>
		/// <param name="model"></param>
		[ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
		#endregion
		[HttpPost]
		public async Task<Response> CreateProductAsync(CreateProductCommand request)
		=> await _mediator.Send(request);

		#region Documentation
		/// <summary>
		/// Product Updating
		/// </summary>
		/// <param name="id"></param>
		/// <param name="model"></param>
		[ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
		#endregion
		[HttpPut("{id}")]
		public async Task<Response> UpdateProductAsync(int id, UpdateProductCommand request)
		=> await _mediator.Send(request);

		#region Documentation
		/// <summary>
		/// Product deleting
		/// </summary>
		/// <param name="id"></param>
		[ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
		#endregion
		[HttpDelete]
		public async Task<Response> DeleteProductAsync(int id)
		=> await _mediator.Send(new DeleteProductCommand { Id=id});
	}
}
