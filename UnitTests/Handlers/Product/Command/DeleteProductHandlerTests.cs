using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Features.Product.Commands.DeleteProduct;
using Business.Wrappers;
using Common.Exceptions;
using Data.Repositories.Product;
using Data.UnitOfWork;
using Moq;
using Xunit;

namespace UnitTests.Handlers.Product.Command
{
    public class DeleteProductHandlerTests
    {
        private readonly Mock<IProductReadRepository> _productReadRepository;
        private readonly Mock<IProductWriteRepository> _productWriteRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _productReadRepository = new Mock<IProductReadRepository>();
            _productWriteRepository = new Mock<IProductWriteRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _handler = new DeleteProductHandler(_productReadRepository.Object, _productWriteRepository.Object, _unitOfWork.Object);
        }

        [Fact]
        public async Task Handle_WhenProductNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteProductCommand { Id = It.IsAny<int>() };

            _productReadRepository.Setup(x => x.GetAsync(request.Id))
                .ReturnsAsync(value: null);

            // Act
            Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(func);
            Assert.Equal("Product tapilmadi", exception.Message);
        }

        [Fact]
        public async Task Handle_WhenFlowIsSucceeded_ShouldReturnResponseModel()
        {
            // Arrange
            var request = new DeleteProductCommand { Id = It.IsAny<int>() };

            var product = new Common.Entities.Product { Id = request.Id, Name = "Product" };
            _productReadRepository.Setup(x => x.GetAsync(request.Id))
                .ReturnsAsync(product);

            // Act
            var response = await _handler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            Assert.IsType<Response>(response);
            Assert.Equal("Product ugurla silindi", response.Message);

        }
    }
}
