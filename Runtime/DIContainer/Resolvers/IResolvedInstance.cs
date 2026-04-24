// Copyright (c) 2026 OopsItsACoder

namespace UJect
{
    public interface IResolvedInstance : IInitializable
    {
        object InstanceObject { get; }
        bool   IsDestroyed    { get; }
    }
    
    public interface IResolvedInstance<out TImpl> : IResolvedInstance
    {
        TImpl InstanceObjectTyped { get; }
    }
}
