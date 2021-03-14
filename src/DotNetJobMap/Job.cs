namespace DotNetJobMap
{
    public interface IJob
    {
        IMessage Do(IMessage message);
    }

    public abstract class Job<T> : IJob where T : IMessage
    {
        protected abstract IMessage Do(T message);

        public IMessage Do(IMessage message)
        {
            return Do((T)message);
        }
    }
}
