using DotNetJobMap;
using System;
using System.Collections.Generic;
using System.Text;

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
