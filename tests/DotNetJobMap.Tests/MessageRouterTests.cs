using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class MessageRouterTests
    {
        [Fact]
        public void When_route_called_with_address_that_exists_then_return_job()
        {
            var address = Substitute.For<IJobAddress>();
            var job = Substitute.For<IJob>();
            var manager = Substitute.For<IJobManager>();
            manager.GetJob(address).Returns(job);

            var router = new MessageRouter(manager);

            var result = router.Route(address);

            result.Should().Be(job);
        }

        [Fact]
        public void When_route_called_with_address_that_does_not_exist_then_throw_exception()
        {
            var address = Substitute.For<IJobAddress>();
            var job = Substitute.For<IJob>();
            var manager = Substitute.For<IJobManager>();
            manager.GetJob(address).Returns(i => throw new ArgumentException());

            var router = new MessageRouter(manager);

            Action act = () => router.Route(address);

            act.Should().Throw<ArgumentException>();
        }
    }
}
