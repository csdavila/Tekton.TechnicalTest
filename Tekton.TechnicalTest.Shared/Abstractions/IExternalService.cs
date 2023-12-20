using Tekton.TechnicalTest.Domain;

namespace Tekton.TechnicalTest.Shared.Abstractions
{
    public interface IExternalService
    {
        Task<ExternalServiceDto?> SearchProductAmountPercent(int productId);
    }
}
