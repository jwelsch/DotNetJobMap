using System.Threading.Tasks;

namespace DotNetJobMap
{
    public interface IJob
    {
        IJobAddress Address { get; }

        Task<IMessageEnvelope> Execute(IMessagePayload payload);
    }

    public abstract class Job : IJob
    {
        private readonly IMessageEnvelopeFactory _envelopeFactory;
        private readonly IMessagePayloadFactory _payloadFactory;

        public IJobAddress Address { get; }

        public Job(IJobAddress address, IMessageEnvelopeFactory envelopeFactory, IMessagePayloadFactory payloadFactory)
        {
            Address = address;
            _envelopeFactory = envelopeFactory;
            _payloadFactory = payloadFactory;
        }

        public async Task<IMessageEnvelope> Execute(IMessagePayload payload)
        {
            return await Execute(payload, _envelopeFactory, _payloadFactory);
        }

        protected abstract Task<IMessageEnvelope> Execute(IMessagePayload payload, IMessageEnvelopeFactory envelopeFactory, IMessagePayloadFactory payloadFactory);
    }
}
