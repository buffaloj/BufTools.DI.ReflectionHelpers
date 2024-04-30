using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.Reflection;

namespace BufTools.Extensions.DependencyInjection
{
    /// <summary>
    /// A set of convenience methods to register multiple classes of a type or with an attribute for dependency injection
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Registers scoped classes for dependency injection that have a baseclass or interface of type T
        /// </summary>
        /// <typeparam name="T">The type of base class or interface to register classes of</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static void AddScopedClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetConcreteClasses<T>();

            foreach (var type in types)
                services.AddScoped(type);
        }

        /// <summary>
        /// Registers singleton classes for dependency injection that have a baseclass or interface of type T
        /// </summary>
        /// <typeparam name="T">The type of base class or interface to register classes of</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static void AddSingletonClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetConcreteClasses<T>();

            foreach (var type in types)
                services.AddSingleton(type);
        }

        /// <summary>
        /// Registers transient classes for dependency injection that have a baseclass or interface of type T
        /// </summary>
        /// <typeparam name="T">The type of base class or interface to register classes of</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static void AddTransientClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetConcreteClasses<T>();

            foreach (var type in types)
                services.AddTransient(type);
        }

        private static Type[] GetConcreteClasses<T>(this Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToArray();
        }

        /// <summary>
        /// Registers scoped classes for dependency injection that have a class attribute of type TAttribute
        /// </summary>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/> that class to register are marked with</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static void AddScopedClassesWithAttribute<TAttribute>(this IServiceCollection services, Assembly assembly)
            where TAttribute : Attribute
        {
            var types = assembly.GetConcreteTypesWithAttribute<TAttribute>();

            foreach (var type in types)
                services.AddScoped(type);
        }

        /// <summary>
        /// Registers singleton classes for dependency injection that have a class attribute of type TAttribute
        /// </summary>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/> that class to register are marked with</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static void AddSingletonClassesWithAttribute<TAttribute>(this IServiceCollection services, Assembly assembly)
            where TAttribute : Attribute
        {
            var types = assembly.GetConcreteTypesWithAttribute<TAttribute>();

            foreach (var type in types)
                services.AddSingleton(type);
        }

        /// <summary>
        /// Registers transient classes for dependency injection that have a class attribute of type TAttribute
        /// </summary>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/> that class to register are marked with</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static void AddTransientClassesWithAttribute<TAttribute>(this IServiceCollection services, Assembly assembly)
            where TAttribute : Attribute
        {
            var types = assembly.GetConcreteTypesWithAttribute<TAttribute>();

            foreach (var type in types)
                services.AddTransient(type);
        }

        private static Type[] GetConcreteTypesWithAttribute<TAttribute>(this Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(TAttribute), true).Any() &&
                            !t.IsInterface && 
                            !t.IsAbstract)
                .ToArray();
        }

    }
}
