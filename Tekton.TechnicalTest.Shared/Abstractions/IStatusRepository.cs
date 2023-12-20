using Tekton.TechnicalTest.Domain.Entities;

namespace Tekton.TechnicalTest.Shared.Abstractions
{
    public interface IStatusRepository
    {
        Status? GetCacheStatus(int status);

    }
}
