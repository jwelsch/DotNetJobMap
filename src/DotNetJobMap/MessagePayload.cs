using System.Collections.Generic;
using System.Linq;

namespace DotNetJobMap
{
    public interface IMessagePayload
    {
        IPayloadItem GetItem(string key);

        void SetItem(string key, object value);

        void Clean();
    }

    public class MessagePayload : IMessagePayload
    {
        private readonly IDictionary<string, IPayloadItem> _catalogue = new Dictionary<string, IPayloadItem>();
        private readonly IPayloadItemFactory _itemFactory;

        public MessagePayload(IPayloadItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public IPayloadItem GetItem(string key)
        {
            if (_catalogue.TryGetValue(key, out IPayloadItem item))
            {
                return item;
            }

            return null;
        }

        public void SetItem(string key, object value)
        {
            if (_catalogue.TryGetValue(key, out IPayloadItem _))
            {
                _catalogue[key] = _itemFactory.Create(value);
            }
            else
            {
                var item = _itemFactory.Create(value);

                _catalogue.Add(key, item);
            }
        }

        public void Clean()
        {
            var deletedKeys = _catalogue.Where(i => i.Value.IsDeleted)
                                        .Select(i => i.Key)
                                        .ToList();

            foreach (var key in deletedKeys)
            {
                _catalogue.Remove(key);
            }
        }
    }
}
