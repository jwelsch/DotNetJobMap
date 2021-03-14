using Autofac;
using System;

namespace DotNetJobMap
{
    public interface IController
    {
        void AddJobs(params IJob[] jobs);

        void RemoveJobs(params IJob[] jobs);

        IMessage DoNext(IMessage message = null);
    }

    public class Controller : IController
    {
        private static IContainer Container { get; } = Registrator.Register();

        private readonly IMessageRouter _router;

        private IMessage _nextMessage;

        public Controller()
            : this((IMessageRouter)null, null, null)
        {
        }

        public Controller(IMessage firstMessage, params IJob[] jobs)
            : this(null, firstMessage, jobs)
        {
        }

        public Controller(IMessageRouter router)
        {
            _router = router ?? Container.Resolve<IMessageRouter>();
        }

        public Controller(IMessageRouter router, IMessage firstMessage, params IJob[] jobs)
            : this(router)
        {
            _nextMessage = firstMessage;
            AddJobs(jobs);
        }

        public void AddJobs(params IJob[] jobs)
        {
            _router.AddJobs(jobs);
        }

        public void RemoveJobs(params IJob[] jobs)
        {
            _router.RemoveJobs(jobs);
        }

        public IMessage DoNext(IMessage message = null)
        {
            var messageToRoute = message ?? _nextMessage;

            if (messageToRoute == null)
            {
                throw new JobMapException($"The message to route was null.");
            }

            var job = _router.Route(messageToRoute);

            if (job == null)
            {
                throw new JobMapException($"A job that takes a message with type '{message.GetType().FullName}' was not found.");
            }

            _nextMessage = job.Do(messageToRoute);

            return _nextMessage;
        }
    }
}
