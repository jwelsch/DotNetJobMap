namespace DotNetJobMap
{
    public interface IMessageEnvelopeFactory
    {
        IMessageEnvelope Create(IJobAddress address, IMessagePayload payload);
    }

    public class MessageEnvelopeFactory
    {
        private readonly IUniqueIdFactory _idFactory;

        public MessageEnvelopeFactory(IUniqueIdFactory idFactory)
        {
            _idFactory = idFactory;
        }

        public IMessageEnvelope Create(IJobAddress address, IMessagePayload payload)
        {
            return new MessageEnvelope(_idFactory.Create(), address, payload);
        }
    }
}
