using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class MessageEnvelopeFactoryTests
    {
        private readonly static Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_create_called_then_envelope_has_expected_values()
        {
            var id = AutoFixture.Create<int>();
            var idFactory = Substitute.For<IUniqueIdFactory>();
            idFactory.Create().Returns(id);

            var address = Substitute.For<IJobAddress>();
            var payload = Substitute.For<IMessagePayload>();

            var factory = new MessageEnvelopeFactory(idFactory);

            var specimen = factory.Create(address, payload);

            specimen.Id.Should().Be(id);
            specimen.Address.Should().Be(address);
            specimen.Payload.Should().Be(payload);
        }
    }
}
