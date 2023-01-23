using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.Reflection;

namespace BufTools.DI.ReflectionHelpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddScopedClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetConcreteClasses<T>();

            foreach (var type in types)
                services.AddScoped(type);
        }

        public static void AddSingletonClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetConcreteClasses<T>();

            foreach (var type in types)
                services.AddSingleton(type);
        }

        public static void AddTransientClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetConcreteClasses<T>();

            foreach (var type in types)
                services.AddTransient(type);
        }

        private static Type[] GetConcreteClasses<T>(this Assembly assembly)
        {
            return assembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t) &&
                                                  !t.IsInterface &&
                                                  !t.IsAbstract)
                           .ToArray();
        }
    }
}
