using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class JobAddressTests
    {
        private readonly static Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_equals_is_given_null_then_returns_false()
        {
            var address = new JobAddress(AutoFixture.Create<int>());

            var result = address.Equals(null);

            result.Should().BeFalse();
        }

        [Fact]
        public void When_two_jobaddress_have_the_same_jobid_then_equals_returns_true()
        {
            var id = AutoFixture.Create<int>();

            var address1 = new JobAddress(id);
            var address2 = new JobAddress(id);

            var result = address1.Equals(address2);

            result.Should().BeTrue();
        }

        [Fact]
        public void When_two_jobaddress_have_different_jobids_then_equals_returns_false()
        {
            var id1 = AutoFixture.Create<int>();
            var id2 = AutoFixture.Create<int>();

            var address1 = new JobAddress(id1);
            var address2 = new JobAddress(id2);

            var result = address1.Equals(address2);

            result.Should().BeFalse();
        }

        [Fact]
        public void When_two_jobaddress_are_different_types_and_given_as_ijobaddress_then_equals_returns_false()
        {
            var address1 = new JobAddress(AutoFixture.Create<int>());
            var address2 = Substitute.For<IJobAddress>();

            var result = address1.Equals(address2);

            result.Should().BeFalse();
        }

        [Fact]
        public void When_two_jobaddress_are_different_types_and_given_as_object_then_equals_returns_false()
        {
            var address1 = new JobAddress(AutoFixture.Create<int>());
            var address2 = Substitute.For<IJobAddress>();

            var result = address1.Equals((object)address2);

            result.Should().BeFalse();
        }

        [Fact]
        public void When_tostring_is_called_returns_formatted_string()
        {
            var id = AutoFixture.Create<int>();
            var address = new JobAddress(id);

            var text = address.ToString();

            text.Should().Be($"{{ Address: {id} }}");
        }
    }
}
