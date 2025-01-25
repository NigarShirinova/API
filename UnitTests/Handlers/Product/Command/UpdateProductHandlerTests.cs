using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.Features.Product.Commands.UpdateProduct;
using Business.Wrappers;
using Common.Exceptions;
using Data.Repositories.Product;
using Data.UnitOfWork;
using Moq;
using Xunit;

namespace UnitTests.Handlers.Product.Command
{
    public class UpdateProductHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProductWriteRepository> _productWriteRepository;
        private readonly Mock<IProductReadRepository> _productReadRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly UpdateProductHandler _handler;

        public UpdateProductHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _productWriteRepository = new Mock<IProductWriteRepository>();
            _productReadRepository = new Mock<IProductReadRepository>();
            _mapper = new Mock<IMapper>();

            _handler = new UpdateProductHandler(
                _unitOfWork.Object,
                _productWriteRepository.Object,
                _productReadRepository.Object,
                _mapper.Object);
        }

        [Fact]
        public async Task Handle_WhenProductNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateProductCommand { Id = It.IsAny<int>(), Name = "Yeni ad" };

            _productReadRepository.Setup(x => x.GetAsync(request.Id))
                .ReturnsAsync(value: null);

            // Act
            Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            var exception = await Assert.ThrowsAsync<Common.Exceptions.NotFoundException>(func);
            Assert.Contains("Product tapilmaid", exception.Errors);
        }

        [Fact]
        public async Task Handle_WhenValidationFails_ShouldThrowValidationException()
        {
            // Arrange
            var request = new UpdateProductCommand { Id = 1, Name = "" };
            var product = new Common.Entities.Product { Id = 1, Name = "Kohne ad" }; 

            _productReadRepository.Setup(x => x.GetAsync(It.Is<int>(id => id == request.Id)))
                .ReturnsAsync(product);

            // Act
            Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            var exception = await Assert.ThrowsAsync<Common.Exceptions.ValidationException>(func);
            Assert.Contains("Ad vacibdir", exception.Errors); 
        }

        [Fact]
        public async Task Handle_WhenFlowIsSucceeded_ShouldReturnResponseModel()
        {
            // Arrange
            var request = new UpdateProductCommand
            {
                Id = 1,
                Name = "Yeni ad",
                Price = 120,
                Description = "DescriptionDescriptionDescriptionDescription",
                Quantity = 100,
                Type = Common.Constants.ProductType.New,
                Photo = "salam"
            };

            var product = new Common.Entities.Product
            {
                Id = request.Id,
                Name = "Kohne ad",
                Price = 100,
                Description = "DescriptionDescriptionDescriptionDescription",
                CreatedDate = DateTime.Now,
                Photo = "salam"
            };

            
            _productReadRepository.Setup(x => x.GetAsync(request.Id)).ReturnsAsync(product);
            _mapper.Setup(x => x.Map(request, product));
            _productWriteRepository.Setup(x => x.Update(product));
            _unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

            // Act
            var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            Assert.IsType<Response>(response);
            Assert.Equal("Product deyisildi", response.Message);
        }

    }
}
