namespace DotNetJobMap
{
    public interface IPayloadItemFactory
    {
        IPayloadItem Create(object value);
    }

    public class PayloadItemFactory : IPayloadItemFactory
    {
        public IPayloadItem Create(object value)
        {
            return new PayloadItem(value);
        }
    }
}
