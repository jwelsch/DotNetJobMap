namespace DotNetJobMap
{
    public interface IMessageRouter
    {
        IJob Route(IJobAddress address);
    }

    public class MessageRouter : IMessageRouter
    {
        private readonly IJobManager _jobManager;

        public MessageRouter(IJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public IJob Route(IJobAddress address)
        {
            return _jobManager.GetJob(address);
        }
    }
}
