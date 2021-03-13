using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class MessageEnvelopeTests
    {
        private readonly static Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_messageenvelope_created_then_id_returns_correct_value()
        {
            var id = AutoFixture.Create<int>();
            var address = Substitute.For<IJobAddress>();
            var payload = Substitute.For<IMessagePayload>();

            var message = new MessageEnvelope(id, address, payload);

            message.Id.Should().Be(id);
        }

        [Fact]
        public void When_messageenvelope_created_then_address_returns_correct_value()
        {
            var id = AutoFixture.Create<int>();
            var address = new JobAddress(AutoFixture.Create<int>());
            var payload = Substitute.For<IMessagePayload>();

            var message = new MessageEnvelope(id, address, payload);

            message.Address.Should().Be(address);
        }

        [Fact]
        public void When_messageenvelope_created_then_payload_returns_correct_value()
        {
            var id = AutoFixture.Create<int>();
            var address = Substitute.For<IJobAddress>();
            var payload = Substitute.For<IMessagePayload>();

            var message = new MessageEnvelope(id, address, payload);

            message.Payload.Should().Be(payload);
        }
    }
}
