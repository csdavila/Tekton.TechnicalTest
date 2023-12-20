using FluentAssertions;
using Moq;
using Tekton.TechnicalTest.Application.Products.Queries;
using Tekton.TechnicalTest.Domain.Entities;
using Tekton.TechnicalTest.Shared.Abstractions;

namespace Tekton.TechnicalTest.UnitTests.Application.Products.Queries
{
    public class GetProductQueryTest
    {
        private readonly GetProductQueryHandler _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IStatusRepository> _statusRepositoryMock;
        private readonly Mock<IExternalService> _externalServiceMock;

        public GetProductQueryTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _statusRepositoryMock = new Mock<IStatusRepository>();
            _externalServiceMock = new Mock<IExternalService>();

            _sut = new GetProductQueryHandler(_productRepositoryMock.Object, _statusRepositoryMock.Object, _externalServiceMock.Object);
        }

        [Fact]
        public async Task GetProductByIdTest()
        {
            var query = new GetProductQuery() { ProductId = 1 };

            var product = new Product() { ProductId = 1 };
            var repositoryResult = Task.FromResult<Product?>(product);
            _productRepositoryMock.Setup(x => x.GetProductById(query.ProductId)).Returns(repositoryResult);

            var result = await _sut.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetProductQueryResponse>();
            result.ProductId.Should().Be(query.ProductId);
        }
    }
}
