using DotNetReflector;
using System.Linq;

namespace DotNetJobMap
{
    public interface IJobIndexer
    {
        ITypeReflector GetIndex(IJob job);
    }

    public class JobIndexer : IJobIndexer
    {
        private readonly IJobMethodInfoProvider _infoProvider;
        private readonly ITypeReflectorFactory _typeReflectorFactory;

        public JobIndexer(IJobMethodInfoProvider infoProvider, ITypeReflectorFactory typeReflectorFactory)
        {
            _infoProvider = infoProvider;
            _typeReflectorFactory = typeReflectorFactory;
        }

        public ITypeReflector GetIndex(IJob job)
        {
            var reflector = _typeReflectorFactory.Create(job.GetType());

            var methodInfos = reflector.GetMethods(_infoProvider.BindingFlags)
                                       .Where(i => i.Name == _infoProvider.MethodName)
                                       .ToArray();

            if (methodInfos == null || methodInfos.Length == 0)
            {
                throw new JobMapException($"The expected {_infoProvider.JobTypeName} method '{_infoProvider.MethodName}' was not found.");
            }

            if (methodInfos.Length > 1)
            {
                throw new JobMapException($"More than one {_infoProvider.JobTypeName} method '{_infoProvider.MethodName}' was found.");
            }

            var methodInfo = methodInfos[0];

            var parameterInfos = methodInfo.GetParameters();

            if (parameterInfos == null || parameterInfos.Length == 0)
            {
                throw new JobMapException($"The method {_infoProvider.JobTypeName}.{_infoProvider.MethodName} does not take any arguments.");
            }

            if (parameterInfos.Length > 1)
            {
                throw new JobMapException($"The method {_infoProvider.JobTypeName}.{_infoProvider.MethodName} takes more than one argument.");
            }

            var parameterInfo = parameterInfos[0];

            var messageTypeReflector = new TypeReflector(typeof(IMessage));

            if (!messageTypeReflector.IsAssignableFrom(parameterInfo.ParameterType))
            {
                throw new JobMapException($"The method {_infoProvider.JobTypeName}.{_infoProvider.MethodName} takes an argument that is not derived from '{nameof(IMessage)}'.");
            }

            return parameterInfo.ParameterType;
        }
    }
}
