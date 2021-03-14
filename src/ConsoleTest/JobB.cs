using DotNetJobMap;
using System;
using System.Collections.Generic;
using System.Text;

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
