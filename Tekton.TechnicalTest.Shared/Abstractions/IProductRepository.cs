using Tekton.TechnicalTest.Domain.Entities;

namespace Tekton.TechnicalTest.Shared.Abstractions
{
    public interface IProductRepository
    {
        Task<Product?> GetProductById(int productId);

        Task<Product> AddProduct(Product product);

        Task<Product?> UpdateProduct(Product product);

    }
}
