using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BufTools.Extensions.Reflection
{
    /// <summary>
    /// A set of convenience methods for performing common reflection operations
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets all types of type T
        /// </summary>
        /// <typeparam name="T">The class, abstract class or interface to search for</typeparam>
        /// <param name="assembly">The assembly to reflect on</param>
        /// <returns>All types that are of TYPE T or inherit from TYPE T</returns>
        /// <remarks>If you ask for all types for an interface, you will the interface itself as well as types that inherit from it</remarks>
        /// <remarks>If you ask for all types for an abstract class, you will the abstract class itself as well as types that inherit from it</remarks>
        public static Type[] GetTypes<T>(this Assembly assembly)
            where T : class
        {
            return assembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t)).ToArray();
        }

        /// <summary>
        /// Gets all concrete class types that are of type T or inherit from type T
        /// </summary>
        /// <typeparam name="T">The type to search for</typeparam>
        /// <param name="assembly">The assembly to reflect on</param>
        /// <returns>All concrete classes that are of TYPE T inherit from TYPE T </returns>
        /// <remarks>If you ask for all types of an interface, you will get the concrete classes that implement it but not the interface Type itself</remarks>
        /// <remarks>If you ask for all types of an abstract class, you will get the concrete classes that implement it but not the abstract class Type itself</remarks>
        public static Type[] GetClasses<T>(this Assembly assembly)
            where T : class
        {
            return assembly.GetTypes().Where(t => typeof(T).IsAssignableFrom(t) &&
                                                  !t.IsInterface &&
                                                  !t.IsAbstract)
                           .ToArray();
        }

        /// <summary>
        /// Gets all concrete class types that are of type T or inherit from type T
        /// </summary>
        /// <param name="assembly">The assembly to reflect on</param>
        /// <param name="type">The type to search for</param>
        /// <returns>All concrete classes that are of TYPE T inherit from TYPE T </returns>
        /// <remarks>If you ask for all types of an interface, you will get the concrete classes that implement it but not the interface Type itself</remarks>
        /// <remarks>If you ask for all types of an abstract class, you will get the concrete classes that implement it but not the abstract class Type itself</remarks>
        public static Type[] GetClasses(this Assembly assembly, Type type)
        {
            return assembly.GetTypes().Where(t => type.IsAssignableFrom(t) &&
                                                  !t.IsInterface &&
                                                  !t.IsAbstract)
                           .ToArray();
        }

        /// <summary>
        /// Gets all class types in an assembly
        /// </summary>
        /// <param name="assembly">The assembly to fetch classes from</param>
        /// <returns></returns>
        public static Type[] GetClasses(this Assembly assembly)
        {
            return assembly.GetTypes().Where(t => !t.IsInterface &&
                                                  !t.IsAbstract)
                           .ToArray();
        }

        /// <summary>
        /// Gets the path to the location of the assembly on disk
        /// </summary>
        /// <param name="assembly">The assembly to get a path to</param>
        /// <returns>A string containing the path to the assembly</returns>
        public static string GetDirectoryPath(this Assembly assembly)
        {
            string codeBase = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        public static Type[] GetConcreteTypesWithAttribute<TAttribute>(this Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(TAttribute), true).Any() &&
                            !t.IsInterface &&
                            !t.IsAbstract)
                .ToArray();
        }
    }
}
