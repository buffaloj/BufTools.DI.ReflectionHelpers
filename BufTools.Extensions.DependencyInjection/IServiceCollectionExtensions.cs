using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.Reflection;
using System.Collections.Generic;

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
        /// <returns>The types that were registered</returns>
        /// <typeparam name="T">The type of base class or interface to register classes of</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static IEnumerable<Type> AddScopedClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetConcreteClasses<T>();

            foreach (var type in types)
                services.AddScoped(type);

            return types;
        }

        /// <summary>
        /// Registers scoped classes for dependency injection that have a baseclass or interface of type T
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="T">The type of base class or interface to register classes of</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assemblies">The assemblies to search within for class types</param>
        public static IEnumerable<Type> AddScopedClasses<T>(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(assembly => services.AddScopedClasses<T>(assembly)).ToList();
        }

        /// <summary>
        /// Registers singleton classes for dependency injection that have a baseclass or interface of type T
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="T">The type of base class or interface to register classes of</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static IEnumerable<Type> AddSingletonClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetConcreteClasses<T>();

            foreach (var type in types)
                services.AddSingleton(type);

            return types;
        }

        /// <summary>
        /// Registers singleton classes for dependency injection that have a baseclass or interface of type T
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="T">The type of base class or interface to register classes of</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assemblies">The assemblies to search within for class types</param>
        public static IEnumerable<Type> AddSingletonClasses<T>(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(assembly => services.AddSingletonClasses<T>(assembly)).ToList();
        }

        /// <summary>
        /// Registers transient classes for dependency injection that have a baseclass or interface of type T
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="T">The type of base class or interface to register classes of</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static IEnumerable<Type> AddTransientClasses<T>(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetConcreteClasses<T>();

            foreach (var type in types)
                services.AddTransient(type);

            return types;
        }

        /// <summary>
        /// Registers transient classes for dependency injection that have a baseclass or interface of type T
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="T">The type of base class or interface to register classes of</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assemblies">The assemblies to search within for class types</param>
        public static IEnumerable<Type> AddTransientClasses<T>(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(assembly => services.AddTransientClasses<T>(assembly)).ToList();
        }

        /// <summary>
        /// Returns a collection of classes that are concrete implementations
        /// </summary>
        /// <typeparam name="T">The Type to search for</typeparam>
        /// <param name="assembly">The assembly to search within</param>
        /// <returns>An array of types</returns>
        private static Type[] GetConcreteClasses<T>(this Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToArray();
        }

        /// <summary>
        /// Registers scoped classes for dependency injection that have a class attribute of type TAttribute
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/> that class to register are marked with</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static IEnumerable<Type> AddScopedClassesWithAttribute<TAttribute>(this IServiceCollection services, Assembly assembly)
            where TAttribute : Attribute
        {
            var types = assembly.GetConcreteTypesWithAttribute<TAttribute>();

            foreach (var type in types)
                services.AddScoped(type);

            return types;
        }

        /// <summary>
        /// Registers scoped classes for dependency injection that have a class attribute of type TAttribute
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/> that class to register are marked with</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assemblies">The assemblies to search within for class types</param>
        public static IEnumerable<Type> AddScopedClassesWithAttribute<TAttribute>(this IServiceCollection services, IEnumerable<Assembly> assemblies)
            where TAttribute : Attribute
        {
            return assemblies.SelectMany(assembly => services.AddScopedClassesWithAttribute<TAttribute>(assembly)).ToList();
        }

        /// <summary>
        /// Registers singleton classes for dependency injection that have a class attribute of type TAttribute
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/> that class to register are marked with</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static IEnumerable<Type> AddSingletonClassesWithAttribute<TAttribute>(this IServiceCollection services, Assembly assembly)
            where TAttribute : Attribute
        {
            var types = assembly.GetConcreteTypesWithAttribute<TAttribute>();

            foreach (var type in types)
                services.AddSingleton(type);

            return types;
        }

        /// <summary>
        /// Registers singleton classes for dependency injection that have a class attribute of type TAttribute
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/> that class to register are marked with</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assemblies">The assemblies to search within for class types</param>
        public static IEnumerable<Type> AddSingletonClassesWithAttribute<TAttribute>(this IServiceCollection services, IEnumerable<Assembly> assemblies)
            where TAttribute : Attribute
        {
            return assemblies.SelectMany(assembly => services.AddSingletonClassesWithAttribute<TAttribute>(assembly)).ToList();
        }

        /// <summary>
        /// Registers transient classes for dependency injection that have a class attribute of type TAttribute
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/> that class to register are marked with</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assembly">The assembly to search within for class types</param>
        public static IEnumerable<Type> AddTransientClassesWithAttribute<TAttribute>(this IServiceCollection services, Assembly assembly)
            where TAttribute : Attribute
        {
            var types = assembly.GetConcreteTypesWithAttribute<TAttribute>();

            foreach (var type in types)
                services.AddTransient(type);

            return types;
        }

        /// <summary>
        /// Registers transient classes for dependency injection that have a class attribute of type TAttribute
        /// </summary>
        /// <returns>The types that were registered</returns>
        /// <typeparam name="TAttribute">The type of <see cref="Attribute"/> that class to register are marked with</typeparam>
        /// <param name="services">The service collection to register classes with</param>
        /// <param name="assemblies">The assemblies to search within for class types</param>
        public static IEnumerable<Type> AddTransientClassesWithAttribute<TAttribute>(this IServiceCollection services, IEnumerable<Assembly> assemblies)
            where TAttribute : Attribute
        {
            return assemblies.SelectMany(assembly => services.AddTransientClassesWithAttribute<TAttribute>(assembly)).ToList();
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
