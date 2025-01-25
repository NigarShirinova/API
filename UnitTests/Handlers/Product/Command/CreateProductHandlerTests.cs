using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Features.Product.Commands.CreateProduct;
using Data.Repositories.Product;
using Data.UnitOfWork;
using Moq;
using Common.Exceptions;
using UnitTests.MockData.Product.CreateProductHandler;
using Business.Wrappers;

namespace UnitTests.Handlers.Product.Command
{
    public class CreateProductHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProductReadRepository> _productReadRepository;
        private readonly Mock<IProductWriteRepository> _productWriteRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly CreateProductHandler _handler;
        public CreateProductHandlerTests()
        {
            _productWriteRepository = new Mock<IProductWriteRepository>();
            _productReadRepository = new Mock<IProductReadRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();

            _handler = new CreateProductHandler(_unitOfWork.Object,_productWriteRepository.Object, _productReadRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task Handle_WhenValidatorIsFailed_ShouldThrowValidationException()
        {
            //Arrange
            var request = CreateProductHandlerMockData.CreateProductCommandV1;


            //Act
            Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            var exception = await Assert.ThrowsAsync<Common.Exceptions.ValidationException>(func);
            Assert.Contains("Sekil daxil edilmelidir", exception.Errors);
        }

        [Fact]
        public async Task Handle_WhenProductAlreadyExist_ShouldThrowValidationException()
        {
            // Arrange
            var request = CreateProductHandlerMockData.CreateProductCommandV2;

            _productReadRepository.Setup(x => x.GetByNameAsync(request.Name))
                .ReturnsAsync(new Common.Entities.Product()); 

            // Act
            Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            var exception = await Assert.ThrowsAsync<Common.Exceptions.ValidationException>(func);
            Assert.Contains("Bu adda product var", exception.Errors);
        }


        [Fact]
        public async Task Handle_WhenFlowIsSucceeded_ShouldReturnResponseModel()
        {
            //Arrange
            var request = CreateProductHandlerMockData.CreateProductCommandV2;

            _productReadRepository.Setup(x => x.GetByNameAsync(request.Name))
                .ReturnsAsync(value : null);

            _mapper.Setup(x => x.Map<Common.Entities.Product>(request))
                .Returns(new Common.Entities.Product());

            //Act
            var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            Assert.IsType<Response>(response);
            Assert.Equal("Product yaradildi", response.Message);
        }
    }
}


