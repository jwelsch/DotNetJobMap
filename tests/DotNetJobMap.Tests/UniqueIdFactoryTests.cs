using FluentAssertions;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class UniqueIdFactoryTests
    {
        [Fact]
        public void When_create_called_then_returns_first_id()
        {
            var factory = new UniqueIdFactory();

            var specimen = factory.Create();

            specimen.Should().Be(1);
        }

        [Fact]
        public void When_create_called_multiple_times_then_returns_sequential_ids()
        {
            var factory = new UniqueIdFactory();

            var iterations = 10;

            var specimen = new int[iterations];
            var expected = new int[iterations];

            for (var i = 0; i < specimen.Length; i++)
            {
                specimen[i] = factory.Create();

                expected[i] = i + 1;
            }

            specimen.Should().ContainInOrder(expected);
        }
    }
}
