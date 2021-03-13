using AutoFixture;
using System.Threading.Tasks;

namespace DotNetJobMap.Tests.Samples
{
    public class SampleJob : Job
    {
        private readonly static Fixture AutoFixture = new Fixture();

        public SampleJob(IJobAddress address, IMessageEnvelopeFactory envelopeFactory, IMessagePayloadFactory payloadFactory)
            : base(address, envelopeFactory, payloadFactory)
        {
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected override async Task<IMessageEnvelope> Execute(IMessagePayload payload, IMessageEnvelopeFactory envelopeFactory, IMessagePayloadFactory payloadFactory)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return envelopeFactory.Create(new JobAddress(AutoFixture.Create<int>()), payload);
        }
    }
}
