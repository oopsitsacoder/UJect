// Copyright (c) 2026 OopsItsACoder

namespace UJect.Factories
{
    public interface IInstanceFactory<out TImpl>
    {
        TImpl CreateInstance();
    }
}
