using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Dtos.Product;
using Business.Features.Product.Queries.GetProduct;
using Business.Wrappers;
using Common.Exceptions;
using Data.Repositories.Product;
using Moq;

namespace UnitTests.Handlers.Product.Query
{
    public class GetProductHandlerTests
    {
        private readonly Mock<IProductReadRepository> _productReadRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly GetProductHandler _handler;
        public GetProductHandlerTests()
        {
            _productReadRepository = new Mock<IProductReadRepository>();
            _mapper = new Mock<IMapper>();

            _handler = new GetProductHandler(_productReadRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task Handle_WhenProductNotFound_ShouldThrowNotFoundException()
        {
            //Arrange

            var request = new GetProductQuery { Id = It.IsAny<int>() };

            _productReadRepository.Setup(x => x.GetAsync(request.Id))
                .ReturnsAsync(value: null);

            //Act
            Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(func);
            Assert.Contains("Product tapilmadi", exception.Errors);

        }

        [Fact]  
        public async Task Handle_WhenFlowIsSucceeded_ReturnResponseModel()
        {
            //Arrange
            var request = new GetProductQuery { Id = It.IsAny<int>() };
            var product = new Common.Entities.Product();
            var productDto = new ProductDto();
            //Act
            var response = await _handler.Handle(request, It.IsAny<CancellationToken>());


            //Assert
            Assert.IsType<Response<ProductDto>>(response);
            Assert.Equal(productDto, response.Data);
        }
    }
}