﻿using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class ControllerTests
    {
        [Fact]
        public void When_addjobs_is_called_then_jobs_are_added_to_router()
        {
            var job = Substitute.For<IJob>();
            var router = Substitute.For<IMessageRouter>();

            var specimen = new Controller(router);

            specimen.AddJobs(job);

            router.Received(1).AddJobs(job);
        }

        [Fact]
        public void When_removejobs_is_called_then_jobs_are_removed_from_router()
        {
            var job = Substitute.For<IJob>();
            var router = Substitute.For<IMessageRouter>();

            var specimen = new Controller(router);

            specimen.RemoveJobs(job);

            router.Received(1).RemoveJobs(job);
        }

        [Fact]
        public void When_donext_is_called_with_null_and_no_next_message_then_throw()
        {
            var router = Substitute.For<IMessageRouter>();

            var specimen = new Controller(router);

            Action action = () => specimen.DoNext();

            action.Should().Throw<JobMapException>();
        }

        [Fact]
        public void When_donext_is_called_with_message_that_has_a_job_then_job_is_done()
        {
            var message = Substitute.For<IMessage>();
            var nextMessage = Substitute.For<IMessage>();

            var job = Substitute.For<IJob>();
            job.Do(message).Returns(nextMessage);

            var router = Substitute.For<IMessageRouter>();
            router.Route(message).Returns(job);

            var specimen = new Controller(router);

            var result = specimen.DoNext(message);

            router.Received(1).Route(message);
            job.Received(1).Do(message);
            result.Should().Be(nextMessage);
        }

        [Fact]
        public void When_donext_is_called_with_message_that_has_no_job_then_throw()
        {
            var message = Substitute.For<IMessage>();
            var nextMessage = Substitute.For<IMessage>();

            var job = Substitute.For<IJob>();
            job.Do(message).Returns(nextMessage);

            var router = Substitute.For<IMessageRouter>();
            router.Route(message).Returns((IJob)null);

            var specimen = new Controller(router);

            Action action = () => specimen.DoNext(message);

            action.Should().Throw<JobMapException>();
        }
    }
}
