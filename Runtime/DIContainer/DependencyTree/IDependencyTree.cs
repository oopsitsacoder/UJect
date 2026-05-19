using System.Collections.Generic;

namespace UJect
{
    public interface IDependencyTree
    {
        IEnumerable<InjectionKey> RootKeys { get; }
        IEnumerable<InjectionKey> Sorted();
        IEnumerable<InjectionKey> DependsOn(InjectionKey key);
    }
}