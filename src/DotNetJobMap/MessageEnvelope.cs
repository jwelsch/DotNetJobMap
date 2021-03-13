namespace DotNetJobMap
{
    public interface IMessageEnvelope
    {
        int Id { get; }

        IJobAddress Address { get; }

        IMessagePayload Payload { get; }
    }

    public class MessageEnvelope : IMessageEnvelope
    {
        public int Id { get; }

        public IJobAddress Address { get; }

        public IMessagePayload Payload { get; }

        public MessageEnvelope(int id, IJobAddress address, IMessagePayload payload)
        {
            Id = id;
            Address = address;
            Payload = payload;
        }
    }
}
