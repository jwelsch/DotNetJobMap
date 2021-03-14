using DotNetJobMap;

namespace ConsoleTest
{
    public class JobB : Job<Message2>
    {
        protected override IMessage Do(Message2 message)
        {
            return null;
        }
    }
}
