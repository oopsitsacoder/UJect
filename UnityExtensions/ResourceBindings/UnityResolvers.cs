// Copyright (c) 2026 OopsItsACoder
using System;
using UJect.Factories;
using UJect.Injection;
using UJect.Resolvers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UJect.UnityExtensions.Resolvers
{
    internal class ResourceInstanceResolver<TImpl> : ResolverBase<TImpl> where TImpl : Object
    {
        private readonly string resourcePath;

        public ResourceInstanceResolver(string resourcePath)
        {
            this.resourcePath = resourcePath;
        }

        public override IResolvedInstance<TImpl> ResolveTypedInstance()
        {
            var loadedResource = Resources.Load<TImpl>(resourcePath);
            return new UnityObjectResolvedInstance<TImpl>(loadedResource);
        }
    }
    
    /// <summary>
    /// Simplest resolver. Always returns the same provided instance of TImpl
    /// </summary>
    /// <typeparam name="TImpl"></typeparam>
    internal class InstanceResolver<TImpl> : ResolverBase<TImpl> where TImpl : UnityEngine.Object
    {
        private readonly IResolvedInstance<TImpl> resolvedInstance;

        public InstanceResolver(TImpl instance)
        {
            this.resolvedInstance = new UnityObjectResolvedInstance<TImpl>(instance);
        }

        public override IResolvedInstance<TImpl> ResolveTypedInstance() => resolvedInstance;
    }

    /// <summary>
    /// Resolves by getting a new instance if one has already been created, or creating a new one.
    /// Useful for resolving the same instance for multiple interface bindings
    /// </summary>
    /// <typeparam name="TImpl"></typeparam>
    internal class NewPrefabInstanceResolver<TImpl> : ResolverBase<TImpl> where TImpl : UnityEngine.Component
    {
        private readonly DiContainer diContainer;
        private readonly TImpl prefab;

        public NewPrefabInstanceResolver(DiContainer diContainer, TImpl prefab)
        {
            if (prefab == null) throw new ArgumentNullException(nameof(prefab), "Cannot specify null prefab");
            this.diContainer = diContainer;
            this.prefab = prefab;
        }

        public override IResolvedInstance<TImpl> ResolveTypedInstance()
        {
            var instanceObject = UnityEngine.Object.Instantiate(prefab);
            diContainer.InjectInto(instanceObject);
            return new UnityObjectResolvedInstance<TImpl>(instanceObject);
        }
    }

    /// <summary>
    /// Resolve an instance via a Func
    /// </summary>
    /// <typeparam name="TImpl"></typeparam>
    internal class FunctionInstanceResolver<TImpl> : ResolverBase<TImpl> where TImpl : UnityEngine.Object
    {
        private readonly Func<TImpl> resolve;

        public FunctionInstanceResolver(Func<TImpl> resolve)
        {
            this.resolve = resolve;
        }

        public override IResolvedInstance<TImpl> ResolveTypedInstance()
        {
            var newInstance = resolve.Invoke();
            return new UnityObjectResolvedInstance<TImpl>(newInstance);
        }
    }

    internal class ExternalFactoryResolver<TImpl> : ResolverBase<TImpl> where TImpl : UnityEngine.Object
    {
        private readonly DiContainer             diContainer;
        private readonly IInstanceFactory<TImpl> factory;

        public ExternalFactoryResolver(IInstanceFactory<TImpl> factory, DiContainer diContainer)
        {
            this.factory     = factory;
            this.diContainer = diContainer;
        }

        public override IResolvedInstance<TImpl> ResolveTypedInstance()
        {
            diContainer.InjectInto(factory);
            var newInstance = factory.CreateInstance();
            return new UnityObjectResolvedInstance<TImpl>(newInstance);
        }
    }
}
