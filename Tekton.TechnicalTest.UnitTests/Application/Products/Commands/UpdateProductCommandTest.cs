using AutoMapper;
using FluentAssertions;
using Moq;
using Tekton.TechnicalTest.Application.Products.Commands;
using Tekton.TechnicalTest.Domain.Entities;
using Tekton.TechnicalTest.Shared.Abstractions;

namespace Tekton.TechnicalTest.UnitTests.Application.Products.Commands
{
    public class UpdateProductCommandTest
    {
        private readonly UpdateProductHandler _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public UpdateProductCommandTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _sut = new UpdateProductHandler(_productRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Update_Product_ShouldCallCommandHandlerAndMapper()
        {
            UpdateProductCommand command = new UpdateProductCommand()
            {
                ProductId = 1,
                Name = "name",
                Status = 1,
                Stock = 1,
                Description = "description",
                Price = 1
            };

            var product = new Product()
            {
                ProductId = 1,
                Name = "name",
                Status = 1,
                Stock = 1,
                Description = "description",
                Price = 1
            };

            var repositoryGetResult = Task.FromResult<Product?>(product);
            _productRepositoryMock.Setup(x => x.GetProductById(command.ProductId)).Returns(repositoryGetResult);

            var repositoryUpdateResult = Task.FromResult<Product?>(product);
            _productRepositoryMock.Setup(x => x.UpdateProduct(product)).Returns(repositoryUpdateResult);

            var expected = MediatR.Unit.Value;
            var current = await _sut.Handle(command, new CancellationToken());

            current.Should().Be(expected);
        }
    }
}
