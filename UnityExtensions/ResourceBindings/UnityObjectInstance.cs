// Copyright (c) 2024 OopsItsACoder

namespace UJect.UnityExtensions
{
    internal class UnityObjectResolvedInstance<TImpl> : ResolvedInstanceBase<TImpl> where TImpl : UnityEngine.Object
    {
        public override TImpl InstanceObjectTyped { get; }
        public override bool IsDestroyed => InstanceObjectTyped == null;
        
        public UnityObjectResolvedInstance(TImpl instanceObjectTyped)
        {
            InstanceObjectTyped = instanceObjectTyped;
        }
    }
}
