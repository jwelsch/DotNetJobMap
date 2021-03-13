using System.Collections.Concurrent;

namespace DotNetJobMap
{
    public interface IMessageQueue
    {
        int Count { get; }

        void Enqueue(IMessageEnvelope message);

        bool TryDequeue(out IMessageEnvelope message);
    }

    public class MessageQueue : IMessageQueue
    {
        private readonly ConcurrentQueue<IMessageEnvelope> _queue = new ConcurrentQueue<IMessageEnvelope>();

        public int Count => _queue.Count;

        public void Enqueue(IMessageEnvelope message)
        {
            _queue.Enqueue(message);
        }

        public bool TryDequeue(out IMessageEnvelope message)
        {
            return _queue.TryDequeue(out message);
        }
    }
}
