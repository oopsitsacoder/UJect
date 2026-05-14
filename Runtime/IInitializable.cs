// Copyright (c) 2026 OopsItsACoder
using UJect.Utilities;

namespace UJect
{
    /// <summary>
    /// <para>Interface denoting a bound resource as "Initializable".</para>
    ///
    /// <para><see cref="Initialize"/> will be called after all dependencies are resolved to instances.
    /// It is the first time call where ALL bound resources are available, not just ones specified by the
    /// dependency tree.</para>
    ///
    /// <para>Note: No ordering is guaranteed between IInitializables' <see cref="Initialize"/> calls.</para>
    /// </summary>
    public interface IInitializable
    {
        [LibraryEntryPoint]
        void Initialize(DiContainer diContainer);
    }
}
