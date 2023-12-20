using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Tekton.TechnicalTest.Api.Controllers;
using Tekton.TechnicalTest.Application.Products.Commands;
using Tekton.TechnicalTest.Application.Products.Queries;

namespace Tekton.TechnicalTest.UnitTests.Controllers
{
    public class ProductsControllerTest
    {
        private readonly ProductsController _sut;
        private readonly Mock<IMediator> _mediator;

        public ProductsControllerTest()
        {
            this._mediator = new Mock<IMediator>();
            this._sut = new ProductsController(this._mediator.Object);
        }

        [Fact]
        public async Task CreateProduct()
        {
            CreateProductCommand command = new CreateProductCommand();
            var expected = Unit.Value;
            _mediator.Setup(x => x.Send(It.IsAny<IRequest<Unit>>(), default(System.Threading.CancellationToken)))
                .Returns(Task.FromResult(expected));

            var current = await _sut.Post(command);

            ((ObjectResult)current).StatusCode.Should().Be((int)HttpStatusCode.Created);
            ((ObjectResult)current).Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateProduct()
        {
            UpdateProductCommand command = new UpdateProductCommand();
            var expected = Unit.Value;
            _mediator.Setup(x => x.Send(It.IsAny<IRequest<Unit>>(), default(System.Threading.CancellationToken)))
                .Returns(Task.FromResult(expected));

            var current = await _sut.Put(command);

            ((OkResult)current).StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById()
        {
            GetProductQuery query = new GetProductQuery() { ProductId = 1 };
            var expected = new GetProductQueryResponse() { ProductId = 1 };

            _mediator.Setup(x => x.Send(It.IsAny<IRequest<GetProductQueryResponse>>(), default(System.Threading.CancellationToken)))
                 .Returns(Task.FromResult(expected));

            var actual = await _sut.Get(query);

            actual.Should().NotBeNull();
            actual.ProductId.Should().Be(expected.ProductId);
        }
    }
}
