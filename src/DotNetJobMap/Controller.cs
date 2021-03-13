using System;

namespace DotNetJobMap
{
    public interface IController
    {
        void DoNext();
    }

    public class Controller : IController
    {
        private readonly IJobManager _jobManager;
        private readonly IMessageRouter _messageRouter;
        private readonly IMessageQueue _messageQueue;

        public Controller(IJobManager jobManager, IMessageRouter messageRouter, IMessageQueue messageQueue)
        {
            _jobManager = jobManager;
            _messageRouter = messageRouter;
            _messageQueue = messageQueue;
        }

        public void DoNext()
        {
            // _messageQueue.TryDequeue(out IMessageEnvelope envelope);
            // var job = _messageRouter.Route(envelope.Address);
            // job.Execute();
        }
    }
}
