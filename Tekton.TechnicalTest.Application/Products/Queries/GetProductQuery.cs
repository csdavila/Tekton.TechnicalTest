using MediatR;
using Tekton.TechnicalTest.Domain.Entities;
using Tekton.TechnicalTest.Shared.Abstractions;
using Tekton.TechnicalTest.Shared.Common.Exceptions;

namespace Tekton.TechnicalTest.Application.Products.Queries
{
    public class GetProductQuery : IRequest<GetProductQueryResponse>
    {
        public int ProductId { get; set; } = default!;
    }

    public class GetProductQueryHandler(IProductRepository productRepository, IStatusRepository statusRepository, IExternalService externalService) : IRequestHandler<GetProductQuery, GetProductQueryResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IStatusRepository _statusRepository = statusRepository;
        private readonly IExternalService _externalService = externalService;

        public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.ProductId);
            return product is null
                ? throw new NotFoundException(nameof(Product), request.ProductId)
                : await BuildResponse(product);
        }

        private async Task<GetProductQueryResponse> BuildResponse(Product product)
        {
            int discount = await GetExternalProductDiscount(product.ProductId);
            return new GetProductQueryResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                StatusName = GetCacheStatusName(product.Status),
                Stock = product.Stock,
                Description = product.Description,
                Price = product.Price,
                Discount = discount,
                FinalPrice = CalculateFinalPrice(product.Price, discount),
                CreatedAt = product.CreatedAt,
                LastModifiedByAt = product.LastModifiedByAt,
            };

        }
        private string GetCacheStatusName(int status)
        {
            var cacheStatus = _statusRepository.GetCacheStatus(status);
            return cacheStatus?.StatusName ?? string.Empty;
        }

        private async Task<int> GetExternalProductDiscount(int productId)
        {
            var externalData = await _externalService.SearchProductAmountPercent(productId);
            return externalData?.DiscountPercent ?? 0;
        }

        private static decimal CalculateFinalPrice(decimal Price, int Discount)
        {
            return Price * (100 - Discount) / 100;
        }
    }

    public class GetProductQueryResponse
    {
        public int ProductId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string StatusName { get; set; } = default!;
        public int Stock { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal Discount { get; set; } = default!;
        public decimal FinalPrice { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedByAt { get; set; }
    }
}
