using FluentAssertions;
using Tekton.TechnicalTest.Domain.Entities;

namespace Tekton.TechnicalTest.UnitTests.Domain
{
    public class BaseEntityTest
    {
        [Fact]
        public void BaseEntityValid()
        {
            BaseEntity BaseEntity = new() { CreatedBy = "", CreatedAt = new DateTime(2023, 12, 19), LastModifiedBy = "", LastModifiedByAt = new DateTime(2023, 12, 19) };

            var expected = new BaseEntity() { CreatedBy = "", CreatedAt = new DateTime(2023, 12, 19), LastModifiedBy = "", LastModifiedByAt = new DateTime(2023, 12, 19) };

            var currentBaseEntity = new BaseEntity()
            {
                CreatedBy = BaseEntity.CreatedBy,
                CreatedAt = BaseEntity.CreatedAt,
                LastModifiedBy = BaseEntity.LastModifiedBy,
                LastModifiedByAt = BaseEntity.LastModifiedByAt
            };

            currentBaseEntity.Should().BeEquivalentTo(expected);
        }
    }
}
