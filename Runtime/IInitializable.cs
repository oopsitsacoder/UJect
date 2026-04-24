// Copyright (c) 2026 OopsItsACoder
using UJect.Utilities;

namespace UJect
{
    public interface IInitializable
    {
        [LibraryEntryPoint]
        void Initialize(DiContainer diContainer);
    }
}
