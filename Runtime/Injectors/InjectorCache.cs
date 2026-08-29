// Copyright (c) 2026 OopsItsACoder
using System;
using System.Collections.Generic;

namespace UJect.Injection
{
    /// <summary>
    /// Static singleton containing cached injectors for various types.
    /// Generally this should stay alive for the duration of your program to speed up subsequent calls,
    /// but you can clear it via <see cref="ClearCache"/>
    /// </summary>
    internal static class InjectorCache
    {
        private static readonly Dictionary<Type, Injector> injectorCache = new();

        internal static int CachedInjectorCount => injectorCache.Count;
        
        /// <summary>
        /// Get or create an injector for the given type. Stored in a static dictionary for further access.
        /// </summary>
        /// <param name="t">The type to retrieve an <see cref="Injector"/> for</param>
        public static Injector GetOrCreateInjector(Type t)
        {
            if (!injectorCache.TryGetValue(t, out var existingInjector))
            {
                existingInjector = new Injector(t);
                injectorCache.Add(t, existingInjector);
            }

            return existingInjector;
        }

        /// <summary>
        /// Try to get a cached injector for the provided type
        /// </summary>
        /// <param name="t">The type to retrieve an <see cref="Injector"/> for</param>
        /// <param name="injector">The injector, if one is found</param>
        /// <returns>True if an injector exists for the provided type, false otherwise</returns>
        internal static bool TryGetInjector(Type t, out Injector injector) => injectorCache.TryGetValue(t, out injector);

        /// <summary>
        /// Clear the injector cache.
        /// </summary>
        public static void ClearCache()
        {
            injectorCache.Clear();
        }
    }
}
