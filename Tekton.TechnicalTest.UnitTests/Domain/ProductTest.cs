using FluentAssertions;
using Tekton.TechnicalTest.Domain.Entities;

namespace Tekton.TechnicalTest.UnitTests.Domain
{
    public class ProductTest
    {
        [Fact]
        public void ProductValid()
        {
            Product Product = new()
            {
                ProductId = 1,
                Name = "TestProduct",
                Status = 1,
                Stock = 1,
                Description = "",
                Price = 1,
            };

            var expected = new Product()
            {
                ProductId = 1,
                Name = "TestProduct",
                Status = 1,
                Stock = 1,
                Description = "",
                Price = 1,
            };

            var currentProduct = new Product()
            {
                ProductId = Product.ProductId,
                Name = Product.Name,
                Status = Product.Status,
                Stock = Product.Stock,
                Description = Product.Description,
                Price = Product.Price,
            };

            currentProduct.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ProductInvalid()
        {
            Product Product = new()
            {
                ProductId = 1,
                Name = "",
                Status = 1,
                Stock = 1,
                Description = "",
                Price = 1,
                CreatedBy = "",
                CreatedAt = new DateTime(2019, 10, 10),
                LastModifiedBy = "",
                LastModifiedByAt = new DateTime(2019, 10, 10)
            };

            var expected = new Product()
            {
                ProductId = 1,
                Name = "",
                Status = 1,
                Stock = 1,
                Description = "",
                Price = 1,
                CreatedBy = "",
                CreatedAt = new DateTime(2019, 10, 10),
                LastModifiedBy = "",
                LastModifiedByAt = new DateTime(2019, 10, 10)
            };

            var currentProduct = new Product()
            {
                ProductId = Product.ProductId,
                Name = Product.Name,
                Status = Product.Status,
                Stock = Product.Stock,
                Description = Product.Description,
                Price = Product.Price,
                CreatedBy = Product.CreatedBy,
                CreatedAt = Product.CreatedAt,
                LastModifiedBy = Product.LastModifiedBy,
                LastModifiedByAt = Product.LastModifiedByAt
            };

            currentProduct.Should().NotBe(expected);
        }
    }
}
