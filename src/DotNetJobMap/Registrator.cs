using Autofac;
using DotNetReflector;

namespace DotNetJobMap
{
    internal static class Registrator
    {
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();

            // Reflection
            builder.RegisterType<TypeReflectorFactory>().As<ITypeReflectorFactory>();

            // Job Map
            builder.RegisterType<JobMethodInfoProvider>().As<IJobMethodInfoProvider>();
            builder.RegisterType<JobIndexer>().As<IJobIndexer>();
            builder.RegisterType<JobContainer>().As<IJobContainer>();
            builder.RegisterType<MessageRouter>().As<IMessageRouter>();

            return builder.Build();
        }
    }
}
