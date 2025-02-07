using AutoMapper;
using Business.Features.Product.Commands.CreateProduct;
using Business.Services;
using Business.Services.Abstract;
using Business.Validators.Product;
using Business.Wrappers;
using Common.Exceptions;
using Data.Repositories.Product;
using Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Product.Commands.UpdateProduct
{

		public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Response>
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IMapper _mapper;
		private readonly IProductReadRepository _productReadRepository;
        private readonly IProducerService _producerService;

        public UpdateProductHandler(IUnitOfWork unitOfWork,
			IProductWriteRepository productWriteRepository,
			IProductReadRepository productReadRepository,
			IMapper mapper
			IProducerService producerService)
		{
			_unitOfWork = unitOfWork;
			_productWriteRepository = productWriteRepository;
			_mapper = mapper;
			_productReadRepository = productReadRepository;
			_producerService = producerService;
		}
		public async Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			var product = await _productReadRepository.GetAsync(request.Id);
			if (product is null)
				throw new NotFoundException("Product tapilmaid");

			var result = await new UpdateProductCommandValidator().ValidateAsync(request);
			if (!result.IsValid)
				throw new ValidationException(result.Errors);

			_mapper.Map(request, product);

			_productWriteRepository.Update(product);
			await _unitOfWork.CommitAsync();

            await _producerService.ProduceAsync("Update", product);

            return new Response
			{
				Message = "Product deyisildi"
			};
		}
	}
}
