using System;
using System.Runtime.Serialization;

namespace DotNetJobMap
{
    [Serializable]
    public class JobMapException : Exception
    {
        public JobMapException()
        {
        }

        public JobMapException(string message)
            : base(message)
        {
        }

        public JobMapException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected JobMapException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
