using AutoMapper;
using FluentValidation;
using MediatR;
using Tekton.TechnicalTest.Domain.Entities;
using Tekton.TechnicalTest.Shared.Abstractions;
using Tekton.TechnicalTest.Shared.Common.Attributes;

namespace Tekton.TechnicalTest.Application.Products.Commands
{
    [AuditLog]
    public class CreateProductCommand : IRequest
    {
        public string Name { get; set; } = default!;
        public int Status { get; set; } = default!;
        public int Stock { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }


    public class CreateProductHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(request);
            await _productRepository.AddProduct(newProduct);

            return Unit.Value;
        }
    };

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(r => r.Name).NotNull().NotEmpty().MinimumLength(4).MaximumLength(100);
            RuleFor(r => r.Status).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1);
            RuleFor(r => r.Stock).GreaterThanOrEqualTo(0);
            RuleFor(r => r.Description).MaximumLength(250);
            RuleFor(r => r.Price).GreaterThan(0).PrecisionScale(9, 2, false);
        }
    }

    public class CreateProductCommandMapper : Profile
    {
        public CreateProductCommandMapper() =>
            CreateMap<CreateProductCommand, Product>();
    }
}
