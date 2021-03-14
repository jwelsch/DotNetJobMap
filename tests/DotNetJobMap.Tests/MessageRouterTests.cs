using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class MessageRouterTests
    {
        [Fact]
        public void When_add_is_called_then_jobs_are_added_to_container()
        {
            var job = Substitute.For<IJob>();
            var container = Substitute.For<IJobContainer>();

            var specimen = new MessageRouter(container);

            specimen.AddJobs(job);

            container.Received(1).Add(job);
        }

        [Fact]
        public void When_remove_is_called_then_jobs_are_removed_from_container()
        {
            var job = Substitute.For<IJob>();
            var container = Substitute.For<IJobContainer>();

            var specimen = new MessageRouter(container);

            specimen.RemoveJobs(job);

            container.Received(1).Remove(job);
        }

        [Fact]
        public void When_message_with_job_is_routed_then_job_is_returned()
        {
            var message = Substitute.For<IMessage>();
            var messageType = message.GetType();

            var job = Substitute.For<IJob>();

            var container = Substitute.For<IJobContainer>();
            container.TryGet(messageType, out Arg.Any<IJob>())
                     .Returns(i =>
                     {
                         i[1] = job;
                         return true;
                     });
 
            var specimen = new MessageRouter(container);

            var result = specimen.Route(message);

            container.Received(1).TryGet(messageType, out Arg.Any<IJob>());
            result.Should().Be(job);
        }

        [Fact]
        public void When_message_with_no_job_is_routed_then_throw()
        {
            var message = Substitute.For<IMessage>();
            var messageType = message.GetType();

            var job = Substitute.For<IJob>();

            var container = Substitute.For<IJobContainer>();
            container.TryGet(messageType, out Arg.Any<IJob>())
                     .Returns(false);

            var specimen = new MessageRouter(container);

            Action action = () => specimen.Route(message);

            action.Should().Throw<JobMapException>();
            container.Received(1).TryGet(messageType, out Arg.Any<IJob>());
        }
    }
}
