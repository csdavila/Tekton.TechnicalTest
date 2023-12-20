using Microsoft.EntityFrameworkCore;
using Tekton.TechnicalTest.Domain.Entities;
using Tekton.TechnicalTest.Infrastructure.Persistence.Contexts;
using Tekton.TechnicalTest.Shared.Abstractions;

namespace Tekton.TechnicalTest.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Product?> GetProductById(int productId) => await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateProduct(Product updateProduct)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == updateProduct.ProductId);
            if (product is not null)
            {
                product.Name = updateProduct.Name;
                product.Status = updateProduct.Status;
                product.Stock = updateProduct.Stock;
                product.Description = updateProduct.Description;
                product.Price = updateProduct.Price;
            }

            await _context.SaveChangesAsync();

            return product;
        }
    };
}
