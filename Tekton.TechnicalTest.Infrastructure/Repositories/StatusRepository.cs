using LazyCache;
using Tekton.TechnicalTest.Domain.Entities;
using Tekton.TechnicalTest.Infrastructure.Persistence.Contexts;
using Tekton.TechnicalTest.Shared.Abstractions;

namespace Tekton.TechnicalTest.Infrastructure.Repositories
{
    public class StatusRepository(AppDbContext context, IAppCache cache) : IStatusRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IAppCache _cache = cache;

        public Status? GetCacheStatus(int status)
        {
            var statusWithCaching = _cache.GetOrAdd("get-status", () => _context.Status.ToList(), TimeSpan.FromMinutes(5));
            var cacheStatus = statusWithCaching.FirstOrDefault(x => x.StatusKey == status);
            return cacheStatus;
        }
    };
}
