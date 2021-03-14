namespace DotNetJobMap
{
    public interface IMessageRouter
    {
        void AddJobs(params IJob[] jobs);

        void RemoveJobs(params IJob[] jobs);

        IJob Route(IMessage message);
    }

    public class MessageRouter : IMessageRouter
    {
        private readonly IJobContainer _jobContainer;

        public MessageRouter(IJobContainer jobContainer)
        {
            _jobContainer = jobContainer;
        }

        public void AddJobs(params IJob[] jobs)
        {
            _jobContainer.Add(jobs);
        }

        public void RemoveJobs(params IJob[] jobs)
        {
            _jobContainer.Remove(jobs);
        }

        public IJob Route(IMessage message)
        {
            var messageType = message.GetType();

            if (!_jobContainer.TryGet(messageType, out IJob job))
            {
                throw new JobMapException($"A job that takes a message with type '{messageType.FullName}' was not found.");
            }

            return job;
        }
    }
}
