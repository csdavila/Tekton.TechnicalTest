namespace Tekton.TechnicalTest.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = default!;
        public int Status { get; set; } = default!;
        public int Stock { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }
}