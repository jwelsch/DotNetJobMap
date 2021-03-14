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

            var controller = new Controller();

            controller.AddJobs(jobs);

            var result = controller.DoNext(new Message1());

            while (result != null)
            {
                result = controller.DoNext();
            }
        }
    }
}
