using AutoFixture;
using DotNetReflector;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class JobContainerTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_one_job_is_added_then_it_is_put_in_internal_storage()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var jobReflector = Substitute.For<ITypeReflector>();
            jobReflector.Type.Returns(jobType);

            var indexer = Substitute.For<IJobIndexer>();
            indexer.GetIndex(job).Returns(jobReflector);

            var storage = Substitute.For<IDictionary<Type, IJob>>();

            var specimen = new JobContainer(indexer, storage);

            specimen.Add(job);

            indexer.Received(1).GetIndex(job);
            storage.Received(1).Add(jobReflector.Type, job);
        }

        [Fact]
        public void When_multiple_jobs_are_added_then_they_are_put_in_internal_storage()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var jobReflector = Substitute.For<ITypeReflector>();
            jobReflector.Type.Returns(jobType);

            var indexer = Substitute.For<IJobIndexer>();
            indexer.GetIndex(job).Returns(jobReflector);

            var storage = Substitute.For<IDictionary<Type, IJob>>();

            var specimen = new JobContainer(indexer, storage);

            specimen.Add(job, job, job);

            indexer.Received(3).GetIndex(job);
            storage.Received(3).Add(jobReflector.Type, job);
        }

        [Fact]
        public void When_no_job_is_added_then_nothing_is_put_in_internal_storage()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var jobReflector = Substitute.For<ITypeReflector>();
            jobReflector.Type.Returns(jobType);

            var indexer = Substitute.For<IJobIndexer>();
            indexer.GetIndex(job).Returns(jobReflector);

            var storage = Substitute.For<IDictionary<Type, IJob>>();

            var specimen = new JobContainer(indexer, storage);

            specimen.Add(new IJob[0]);

            indexer.DidNotReceive().GetIndex(Arg.Any<IJob>());
            storage.DidNotReceive().Add(Arg.Any<Type>(), Arg.Any<IJob>());
        }

        [Fact]
        public void When_add_called_with_null_then_nothing_is_put_in_internal_storage()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var jobReflector = Substitute.For<ITypeReflector>();
            jobReflector.Type.Returns(jobType);

            var indexer = Substitute.For<IJobIndexer>();
            indexer.GetIndex(job).Returns(jobReflector);

            var storage = Substitute.For<IDictionary<Type, IJob>>();

            var specimen = new JobContainer(indexer, storage);

            specimen.Add(null);

            indexer.DidNotReceive().GetIndex(Arg.Any<IJob>());
            storage.DidNotReceive().Add(Arg.Any<Type>(), Arg.Any<IJob>());
        }

        [Fact]
        public void When_one_job_is_removed_then_it_is_removed_from_internal_storage()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var jobReflector = Substitute.For<ITypeReflector>();
            jobReflector.Type.Returns(jobType);

            var indexer = Substitute.For<IJobIndexer>();
            indexer.GetIndex(job).Returns(jobReflector);

            var storage = Substitute.For<IDictionary<Type, IJob>>();

            var specimen = new JobContainer(indexer, storage);

            specimen.Remove(job);

            indexer.Received(1).GetIndex(job);
            storage.Received(1).Remove(jobReflector.Type);
        }

        [Fact]
        public void When_multiple_jobs_are_removed_then_they_are_removed_from_internal_storage()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var jobReflector = Substitute.For<ITypeReflector>();
            jobReflector.Type.Returns(jobType);

            var indexer = Substitute.For<IJobIndexer>();
            indexer.GetIndex(job).Returns(jobReflector);

            var storage = Substitute.For<IDictionary<Type, IJob>>();

            var specimen = new JobContainer(indexer, storage);

            specimen.Remove(job, job, job);

            indexer.Received(3).GetIndex(job);
            storage.Received(3).Remove(jobReflector.Type);
        }

        [Fact]
        public void When_no_job_is_removed_then_nothing_is_removed_from_internal_storage()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var jobReflector = Substitute.For<ITypeReflector>();
            jobReflector.Type.Returns(jobType);

            var indexer = Substitute.For<IJobIndexer>();
            indexer.GetIndex(job).Returns(jobReflector);

            var storage = Substitute.For<IDictionary<Type, IJob>>();

            var specimen = new JobContainer(indexer, storage);

            specimen.Remove(new IJob[0]);

            indexer.DidNotReceive().GetIndex(Arg.Any<IJob>());
            storage.DidNotReceive().Remove(Arg.Any<Type>());
        }

        [Fact]
        public void When_remove_called_with_null_then_nothing_is_removed_from_internal_storage()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var jobReflector = Substitute.For<ITypeReflector>();
            jobReflector.Type.Returns(jobType);

            var indexer = Substitute.For<IJobIndexer>();
            indexer.GetIndex(job).Returns(jobReflector);

            var storage = Substitute.For<IDictionary<Type, IJob>>();

            var specimen = new JobContainer(indexer, storage);

            specimen.Add(null);

            indexer.DidNotReceive().GetIndex(Arg.Any<IJob>());
            storage.DidNotReceive().Remove(Arg.Any<Type>());
        }

        [Fact]
        public void When_job_is_in_internal_storage_then_tryget_return_true_and_job()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var indexer = Substitute.For<IJobIndexer>();

            var storage = Substitute.For<IDictionary<Type, IJob>>();
            storage.TryGetValue(jobType, out job).Returns(true);

            var specimen = new JobContainer(indexer, storage);

            var result = specimen.TryGet(jobType, out job);

            storage.Received(1).TryGetValue(jobType, out job);
            result.Should().BeTrue();
            job.Should().NotBeNull();
        }

        [Fact]
        public void When_job_is_not_in_internal_storage_then_tryget_return_false()
        {
            var jobType = AutoFixture.Create<Type>();
            var job = Substitute.For<IJob>();
            var indexer = Substitute.For<IJobIndexer>();

            var storage = Substitute.For<IDictionary<Type, IJob>>();
            storage.TryGetValue(jobType, out job).Returns(false);

            var specimen = new JobContainer(indexer, storage);

            var result = specimen.TryGet(jobType, out job);

            storage.Received(1).TryGetValue(jobType, out job);
            result.Should().BeFalse();
        }
    }
}
