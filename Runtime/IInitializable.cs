// Copyright (c) 2024 OopsItsACoder
using UJect.Utilities;

namespace UJect
{
    public interface IInitializable
    {
        [LibraryEntryPoint]
        void Initialize(DiContainer diContainer);
    }
}
