using System;

namespace DotNetJobMap
{
    public interface IController
    {
        void DoNext();
    }

    public class Controller : IController
    {
        public Controller()
        {
        }

        public void DoNext()
        {
        }
    }
}
