using AutoMapper;
using FluentAssertions;
using Moq;
using Tekton.TechnicalTest.Application.Products.Commands;
using Tekton.TechnicalTest.Domain.Entities;
using Tekton.TechnicalTest.Shared.Abstractions;

namespace Tekton.TechnicalTest.UnitTests.Application.Products.Commands
{
    public class CreateProductCommandTest
    {
        private readonly CreateProductHandler _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateProductCommandTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _sut = new CreateProductHandler(_productRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Create_Product_ShouldCallCommandHandlerAndMapper()
        {
            CreateProductCommand command = new CreateProductCommand()
            {
                Name = "name",
                Status = 1,
                Stock = 1,
                Description = "description",
                Price = 1
            };

            var product = new Product()
            {
                Name = "name",
                Status = 1,
                Stock = 1,
                Description = "description",
                Price = 1
            };
            var repositoryResult = Task.FromResult(product);
            _productRepositoryMock.Setup(x => x.AddProduct(product)).Returns(repositoryResult);

            var expected = MediatR.Unit.Value;
            var current = await _sut.Handle(command, new CancellationToken());

            current.Should().Be(expected);
        }
    }
}
