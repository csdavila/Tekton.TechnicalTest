using FluentAssertions;
using Tekton.TechnicalTest.Domain.Entities;

namespace Tekton.TechnicalTest.UnitTests.Domain
{
    public class StatusTest
    {
        [Fact]
        public void StatusValid()
        {
            Status Status = new()
            {
                StatusId = 1,
                StatusName = "Active",
                StatusKey = 1,
            };

            var expected = new Status()
            {
                StatusId = 1,
                StatusName = "Active",
                StatusKey = 1,
            };

            var currentStatus = new Status()
            {
                StatusId = Status.StatusId,
                StatusName = Status.StatusName,
                StatusKey = Status.StatusKey
            };

            currentStatus.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void StatusInvalid()
        {
            Status Status = new()
            {
                StatusId = 1,
                StatusName = "Active",
                StatusKey = 1,
            };

            var expected = new Status()
            {
                StatusId = 2,
                StatusName = "Inactive",
                StatusKey = 0,
            };

            var currentStatus = new Status()
            {
                StatusId = Status.StatusId,
                StatusName = Status.StatusName,
                StatusKey = Status.StatusKey
            };
            currentStatus.Should().NotBe(expected);
        }
    }
}
