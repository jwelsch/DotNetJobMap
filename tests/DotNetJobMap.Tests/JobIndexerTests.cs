using AutoFixture;
using DotNetReflector;
using FluentAssertions;
using NSubstitute;
using System;
using System.Reflection;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class JobIndexerTests
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_job_has_expected_method_then_return_method_parameter_type()
        {
            var parameterTypeFullName = AutoFixture.Create<string>();

            var parameterTypeReflector = Substitute.For<ITypeReflector>();
            parameterTypeReflector.FullName.Returns(parameterTypeFullName);
            parameterTypeReflector.Type.Returns(typeof(IMessage));

            var parameterReflector = Substitute.For<IParameterReflector>();
            parameterReflector.ParameterType.Returns(parameterTypeReflector);

            var methodName = AutoFixture.Create<string>();

            var methodReflector = Substitute.For<IMethodReflector>();
            methodReflector.GetParameters().Returns(new[] { parameterReflector });
            methodReflector.Name.Returns(methodName);

            var jobTypeReflector = Substitute.For<ITypeReflector>();
            jobTypeReflector.GetMethods(Arg.Any<BindingFlags>()).Returns(new[] { methodReflector });

            var infoProvider = Substitute.For<IJobMethodInfoProvider>();
            infoProvider.MethodName.Returns(methodName);

            var reflectorFactory = Substitute.For<ITypeReflectorFactory>();
            reflectorFactory.Create(Arg.Any<Type>()).Returns(jobTypeReflector);

            var job = Substitute.For<IJob>();

            var specimen = new JobIndexer(infoProvider, reflectorFactory);

            var result = specimen.GetIndex(job);

            result.FullName.Should().Be(parameterTypeFullName);
        }

        [Fact]
        public void When_job_does_not_have_expected_method_then_throw()
        {
            var parameterTypeFullName = AutoFixture.Create<string>();

            var parameterTypeReflector = Substitute.For<ITypeReflector>();
            parameterTypeReflector.FullName.Returns(parameterTypeFullName);
            parameterTypeReflector.Type.Returns(typeof(IMessage));

            var parameterReflector = Substitute.For<IParameterReflector>();
            parameterReflector.ParameterType.Returns(parameterTypeReflector);

            var wrongMethodName = AutoFixture.Create<string>();
            var methodName = AutoFixture.Create<string>();

            var methodReflector = Substitute.For<IMethodReflector>();
            methodReflector.GetParameters().Returns(new[] { parameterReflector });
            methodReflector.Name.Returns(wrongMethodName);

            var jobTypeReflector = Substitute.For<ITypeReflector>();
            jobTypeReflector.GetMethods(Arg.Any<BindingFlags>()).Returns(new[] { methodReflector });

            var infoProvider = Substitute.For<IJobMethodInfoProvider>();
            infoProvider.MethodName.Returns(methodName);

            var reflectorFactory = Substitute.For<ITypeReflectorFactory>();
            reflectorFactory.Create(Arg.Any<Type>()).Returns(jobTypeReflector);

            var job = Substitute.For<IJob>();

            var specimen = new JobIndexer(infoProvider, reflectorFactory);

            Action action = () => specimen.GetIndex(job);

            action.Should().Throw<JobMapException>();
        }

        [Fact]
        public void When_job_has_more_than_one_expected_method_then_throw()
        {
            var parameterTypeFullName = AutoFixture.Create<string>();

            var parameterTypeReflector = Substitute.For<ITypeReflector>();
            parameterTypeReflector.FullName.Returns(parameterTypeFullName);
            parameterTypeReflector.Type.Returns(typeof(IMessage));

            var parameterReflector = Substitute.For<IParameterReflector>();
            parameterReflector.ParameterType.Returns(parameterTypeReflector);

            var methodName = AutoFixture.Create<string>();

            var methodReflector = Substitute.For<IMethodReflector>();
            methodReflector.GetParameters().Returns(new[] { parameterReflector });
            methodReflector.Name.Returns(methodName);

            var jobTypeReflector = Substitute.For<ITypeReflector>();
            jobTypeReflector.GetMethods(Arg.Any<BindingFlags>()).Returns(new[] { methodReflector, methodReflector });

            var infoProvider = Substitute.For<IJobMethodInfoProvider>();
            infoProvider.MethodName.Returns(methodName);

            var reflectorFactory = Substitute.For<ITypeReflectorFactory>();
            reflectorFactory.Create(Arg.Any<Type>()).Returns(jobTypeReflector);

            var job = Substitute.For<IJob>();

            var specimen = new JobIndexer(infoProvider, reflectorFactory);

            Action action = () => specimen.GetIndex(job);

            action.Should().Throw<JobMapException>();
        }

        [Fact]
        public void When_job_method_has_no_parameters_then_throw()
        {
            var parameterTypeFullName = AutoFixture.Create<string>();

            var parameterTypeReflector = Substitute.For<ITypeReflector>();
            parameterTypeReflector.FullName.Returns(parameterTypeFullName);
            parameterTypeReflector.Type.Returns(typeof(IMessage));

            var parameterReflector = Substitute.For<IParameterReflector>();
            parameterReflector.ParameterType.Returns(parameterTypeReflector);

            var methodName = AutoFixture.Create<string>();

            var methodReflector = Substitute.For<IMethodReflector>();
            methodReflector.GetParameters().Returns(new IParameterReflector[0]);
            methodReflector.Name.Returns(methodName);

            var jobTypeReflector = Substitute.For<ITypeReflector>();
            jobTypeReflector.GetMethods(Arg.Any<BindingFlags>()).Returns(new[] { methodReflector });

            var infoProvider = Substitute.For<IJobMethodInfoProvider>();
            infoProvider.MethodName.Returns(methodName);

            var reflectorFactory = Substitute.For<ITypeReflectorFactory>();
            reflectorFactory.Create(Arg.Any<Type>()).Returns(jobTypeReflector);

            var job = Substitute.For<IJob>();

            var specimen = new JobIndexer(infoProvider, reflectorFactory);

            Action action = () => specimen.GetIndex(job);

            action.Should().Throw<JobMapException>();
        }

        [Fact]
        public void When_job_method_has_more_than_one_parameter_then_throw()
        {
            var parameterTypeFullName = AutoFixture.Create<string>();

            var parameterTypeReflector = Substitute.For<ITypeReflector>();
            parameterTypeReflector.FullName.Returns(parameterTypeFullName);
            parameterTypeReflector.Type.Returns(typeof(IMessage));

            var parameterReflector = Substitute.For<IParameterReflector>();
            parameterReflector.ParameterType.Returns(parameterTypeReflector);

            var methodName = AutoFixture.Create<string>();

            var methodReflector = Substitute.For<IMethodReflector>();
            methodReflector.GetParameters().Returns(new[] { parameterReflector, parameterReflector });
            methodReflector.Name.Returns(methodName);

            var jobTypeReflector = Substitute.For<ITypeReflector>();
            jobTypeReflector.GetMethods(Arg.Any<BindingFlags>()).Returns(new[] { methodReflector });

            var infoProvider = Substitute.For<IJobMethodInfoProvider>();
            infoProvider.MethodName.Returns(methodName);

            var reflectorFactory = Substitute.For<ITypeReflectorFactory>();
            reflectorFactory.Create(Arg.Any<Type>()).Returns(jobTypeReflector);

            var job = Substitute.For<IJob>();

            var specimen = new JobIndexer(infoProvider, reflectorFactory);

            Action action = () => specimen.GetIndex(job);

            action.Should().Throw<JobMapException>();
        }

        [Fact]
        public void When_job_method_has_parameter_that_does_not_derive_from_imessage_then_throw()
        {
            var parameterTypeFullName = AutoFixture.Create<string>();

            var parameterTypeReflector = Substitute.For<ITypeReflector>();
            parameterTypeReflector.FullName.Returns(parameterTypeFullName);
            parameterTypeReflector.Type.Returns(AutoFixture.Create<Type>());

            var parameterReflector = Substitute.For<IParameterReflector>();
            parameterReflector.ParameterType.Returns(parameterTypeReflector);

            var methodName = AutoFixture.Create<string>();

            var methodReflector = Substitute.For<IMethodReflector>();
            methodReflector.GetParameters().Returns(new[] { parameterReflector });
            methodReflector.Name.Returns(methodName);

            var jobTypeReflector = Substitute.For<ITypeReflector>();
            jobTypeReflector.GetMethods(Arg.Any<BindingFlags>()).Returns(new[] { methodReflector });

            var infoProvider = Substitute.For<IJobMethodInfoProvider>();
            infoProvider.MethodName.Returns(methodName);

            var reflectorFactory = Substitute.For<ITypeReflectorFactory>();
            reflectorFactory.Create(Arg.Any<Type>()).Returns(jobTypeReflector);

            var job = Substitute.For<IJob>();

            var specimen = new JobIndexer(infoProvider, reflectorFactory);

            Action action = () => specimen.GetIndex(job);

            action.Should().Throw<JobMapException>();
        }
    }
}
