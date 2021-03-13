using AutoFixture;
using FluentAssertions;
using NSubstitute;
using System.Linq;
using Xunit;

namespace DotNetJobMap.Tests
{
    public class MessageQueueTests
    {
        private readonly static Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_no_messages_enqueued_then_trydequeued_return_false()
        {
            var messageQueue = new MessageQueue();

            var result = messageQueue.TryDequeue(out IMessageEnvelope message);

            result.Should().BeFalse();
            message.Should().BeNull();
        }

        [Fact]
        public void When_one_message_enqueued_then_can_be_dequeued()
        {
            var id = AutoFixture.Create<int>();

            var message1 = Substitute.For<IMessageEnvelope>();
            message1.Id.Returns(id);

            var messageQueue = new MessageQueue();

            messageQueue.Enqueue(message1);

            var result = messageQueue.TryDequeue(out IMessageEnvelope message2);

            result.Should().BeTrue();
            message2.Should().NotBeNull();
            message2.Id.Should().Be(id);
        }

        [Fact]
        public void When_two_messages_enqueued_then_first_in_is_dequeued()
        {
            var ids = AutoFixture.CreateMany<int>(2).ToArray();

            var message1 = Substitute.For<IMessageEnvelope>();
            message1.Id.Returns(ids[0]);

            var message2 = Substitute.For<IMessageEnvelope>();
            message2.Id.Returns(ids[1]);

            var messageQueue = new MessageQueue();

            messageQueue.Enqueue(message1);
            messageQueue.Enqueue(message2);

            var result = messageQueue.TryDequeue(out IMessageEnvelope message3);

            result.Should().BeTrue();
            message3.Should().NotBeNull();
            message3.Id.Should().Be(ids[0]);
        }

        [Fact]
        public void When_no_messages_enqueued_then_count_is_zero()
        {
            var messageQueue = new MessageQueue();

            messageQueue.Count.Should().Be(0);
        }

        [Fact]
        public void When_one_message_enqueued_then_count_is_one()
        {
            var message1 = Substitute.For<IMessageEnvelope>();

            var messageQueue = new MessageQueue();

            messageQueue.Enqueue(message1);

            messageQueue.Count.Should().Be(1);
        }

        [Fact]
        public void When_one_messages_enqueued_and_then_dequeued_then_count_is_zero()
        {
            var message1 = Substitute.For<IMessageEnvelope>();

            var messageQueue = new MessageQueue();

            messageQueue.Enqueue(message1);
            messageQueue.TryDequeue(out IMessageEnvelope _);

            messageQueue.Count.Should().Be(0);
        }
    }
}
