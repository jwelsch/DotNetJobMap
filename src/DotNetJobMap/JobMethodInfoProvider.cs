using System.Reflection;

namespace DotNetJobMap
{
    public interface IJobMethodInfoProvider
    {
        string MessageTypeName { get; }

        string JobTypeName { get; }

        string MethodName { get; }

        BindingFlags BindingFlags { get; }
    }

    public class JobMethodInfoProvider : IJobMethodInfoProvider
    {
        public string MessageTypeName => nameof(IMessage);

        public string JobTypeName => nameof(Job<IMessage>);

        public string MethodName => nameof(Job<IMessage>.Do);

        public BindingFlags BindingFlags => BindingFlags.Instance | BindingFlags.NonPublic;
    }
}
