using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class JobManagerTests
    {
        [Fact]
        public void When_no_jobs_managed_then_remove_returns_false()
        {
            var address = Substitute.For<IJobAddress>();
            var manager = new JobManager();

            var result = manager.RemoveJob(address);

            result.Should().BeFalse();
        }

        [Fact]
        public void When_one_job_managed_then_remove_with_correct_address_returns_true()
        {
            var address = Substitute.For<IJobAddress>();
            address.Equals(Arg.Any<IJobAddress>()).Returns(true);

            var job = Substitute.For<IJob>();
            job.Address.Returns(address);

            var manager = new JobManager();

            manager.AddJob(job);

            var result = manager.RemoveJob(address);

            result.Should().BeTrue();
        }

        [Fact]
        public void When_one_job_managed_then_remove_with_incorrect_address_returns_false()
        {
            var address = Substitute.For<IJobAddress>();
            address.Equals(Arg.Any<IJobAddress>()).Returns(false);

            var job = Substitute.For<IJob>();
            job.Address.Returns(address);

            var manager = new JobManager();

            manager.AddJob(job);

            var result = manager.RemoveJob(address);

            result.Should().BeFalse();
        }

        [Fact]
        public void When_no_jobs_managed_then_jobcount_returns_zero()
        {
            var manager = new JobManager();

            manager.JobCount.Should().Be(0);
        }

        [Fact]
        public void When_one_job_added_then_jobcount_returns_one()
        {
            var job = Substitute.For<IJob>();

            var manager = new JobManager();

            manager.AddJob(job);

            manager.JobCount.Should().Be(1);
        }

        [Fact]
        public void When_one_job_added_and_then_removed_then_jobcount_returns_zero()
        {
            var address = Substitute.For<IJobAddress>();
            address.Equals(Arg.Any<IJobAddress>()).Returns(true);

            var job = Substitute.For<IJob>();
            job.Address.Returns(address);

            var manager = new JobManager();

            manager.AddJob(job);
            manager.RemoveJob(job.Address);

            manager.JobCount.Should().Be(0);
        }

        [Fact]
        public void When_a_job_with_the_same_address_is_added_more_than_once_then_throw_exception()
        {
            var address = Substitute.For<IJobAddress>();
            address.Equals(Arg.Any<IJobAddress>()).Returns(true);

            var job = Substitute.For<IJob>();
            job.Address.Returns(address);

            var manager = new JobManager();

            manager.AddJob(job);

            Action act = () => manager.AddJob(job);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_getjob_called_with_address_that_exists_then_return_job()
        {
            var address = Substitute.For<IJobAddress>();
            address.Equals(Arg.Any<IJobAddress>()).Returns(true);

            var job = Substitute.For<IJob>();
            job.Address.Returns(address);

            var manager = new JobManager();

            manager.AddJob(job);

            var result = manager.GetJob(job.Address);

            result.Address.Should().Be(job.Address);
        }

        [Fact]
        public void When_getjob_called_with_address_that_does_not_exist_then_throw_exception()
        {
            var address = Substitute.For<IJobAddress>();
            address.Equals(Arg.Any<IJobAddress>()).Returns(false);

            var job = Substitute.For<IJob>();
            job.Address.Returns(address);

            var manager = new JobManager();

            Action act = () => manager.GetJob(job.Address);

            act.Should().Throw<ArgumentException>();
        }
    }
}
