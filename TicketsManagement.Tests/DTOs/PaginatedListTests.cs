using FluentAssertions;
using TicketsManagement.Application.DTOs;
using Xunit;

namespace TicketsManagement.Tests.DTOs
{
    public class PaginatedListTests
    {
        [Fact]
        public void Properties_AssignedCorrectly()
        {
            var list = new PaginatedList<int>
            {
                Items = new[] { 1, 2, 3 },
                TotalCount = 10,
                PageNumber = 2,
                PageSize = 3
            };
            list.Items.Count().Should().Be(3);
            list.TotalCount.Should().Be(10);
            list.PageNumber.Should().Be(2);
            list.PageSize.Should().Be(3);
        }
    }
}
