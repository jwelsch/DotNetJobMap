using DotNetJobMap;

namespace ConsoleTest
{
    public class JobA : Job<Message1>
    {
        protected override IMessage Do(Message1 message)
        {
            return new Message2();
        }
    }
}
