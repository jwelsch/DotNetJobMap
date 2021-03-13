namespace DotNetJobMap
{
    public interface IMessagePayloadFactory
    {
        IMessagePayload Create();
    }

    public class MessagePayloadFactory : IMessagePayloadFactory
    {
        private readonly IPayloadItemFactory _itemFactory;

        public MessagePayloadFactory(IPayloadItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public IMessagePayload Create()
        {
            return new MessagePayload(_itemFactory);
        }
    }
}
