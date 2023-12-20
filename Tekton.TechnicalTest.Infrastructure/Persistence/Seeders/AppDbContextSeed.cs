using Tekton.TechnicalTest.Domain.Entities;
using Tekton.TechnicalTest.Infrastructure.Persistence.Contexts;

namespace Tekton.TechnicalTest.Infrastructure.Persistence.Seeders
{
    public class AppDbContextSeed
    {
        public static async Task SeedDataAsync(AppDbContext context)
        {
            #region Seed Status
            if (!context.Status.Any())
            {
                context.Status.AddRange(new List<Status>
                {
                    new() {
                        StatusKey= 1,
                        StatusName = "Active",
                    },
                    new() {
                        StatusKey = 0,
                        StatusName = "Inactive",
                    },

                });

                await context.SaveChangesAsync();
            }
            #endregion
        }
    }
}
