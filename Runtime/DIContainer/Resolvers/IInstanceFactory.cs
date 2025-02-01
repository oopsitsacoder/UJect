// Copyright (c) 2024 OopsItsACoder

namespace UJect.Factories
{
    public interface IInstanceFactory<out TImpl>
    {
        TImpl CreateInstance();
    }
}
