using FluentAssertions;
using System.Reflection;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class JobMethodInfoProviderTests
    {
        [Fact]
        public void When_message_type_name_called_then_return_correct_value()
        {
            var specimen = new JobMethodInfoProvider();

            specimen.MessageTypeName.Should().Be(nameof(IMessage));
        }

        [Fact]
        public void When_job_type_name_called_then_return_correct_value()
        {
            var specimen = new JobMethodInfoProvider();

            specimen.JobTypeName.Should().Be(nameof(Job<IMessage>));
        }

        [Fact]
        public void When_method_name_called_then_return_correct_value()
        {
            var specimen = new JobMethodInfoProvider();

            specimen.MethodName.Should().Be(nameof(IJob.Do));
        }

        [Fact]
        public void When_binding_flags_called_then_return_correct_value()
        {
            var specimen = new JobMethodInfoProvider();

            specimen.BindingFlags.Should().Be(BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
