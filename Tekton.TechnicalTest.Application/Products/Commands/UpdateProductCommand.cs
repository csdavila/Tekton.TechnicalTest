using AutoMapper;
using FluentValidation;
using MediatR;
using Tekton.TechnicalTest.Domain.Entities;
using Tekton.TechnicalTest.Shared.Abstractions;
using Tekton.TechnicalTest.Shared.Common.Attributes;
using Tekton.TechnicalTest.Shared.Common.Exceptions;

namespace Tekton.TechnicalTest.Application.Products.Commands
{
    [AuditLog]
    public class UpdateProductCommand : IRequest
    {
        public int ProductId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Status { get; set; } = default!;
        public int Stock { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }
    public class UpdateProductHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _ = await _productRepository.GetProductById(request.ProductId) ?? throw new NotFoundException();

            await _productRepository.UpdateProduct(_mapper.Map<Product>(request));

            return Unit.Value;
        }
    };

    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(r => r.ProductId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(r => r.Name).NotNull().NotEmpty().MinimumLength(4).MaximumLength(100);
            RuleFor(r => r.Status).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1);
            RuleFor(r => r.Stock).GreaterThanOrEqualTo(0);
            RuleFor(r => r.Description).MaximumLength(250);
            RuleFor(r => r.Price).GreaterThan(0).PrecisionScale(9, 2, false);
        }
    }

    public class UpdateProductCommandMapper : Profile
    {
        public UpdateProductCommandMapper() =>
            CreateMap<UpdateProductCommand, Product>();
    }
}
