using DotNetJobMap;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] _)
        {
            var jobs = new IJob[]
            {
                new JobA(),
                new JobB()
            };

            var controller = new Controller(new Message1(), jobs);

            IMessage result;

            do
            {
                result = controller.DoNext();
            }
            while (result != null);
        }
    }
}
