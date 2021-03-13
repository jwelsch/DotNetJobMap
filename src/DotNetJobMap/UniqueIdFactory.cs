namespace DotNetJobMap
{
    public interface IUniqueIdFactory
    {
        int Create();
    }

    public class UniqueIdFactory : IUniqueIdFactory
    {
        private readonly object _sync = new object();

        public int _tracker;

        public int Create()
        {
            lock(_sync)
            {
                return ++_tracker;
            }
        }
    }
}
