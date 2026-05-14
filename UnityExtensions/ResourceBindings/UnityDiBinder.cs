// Copyright (c) 2026 OopsItsACoder

using System;
using UJect.Factories;
using UJect.Resolvers;
using UJect.Utilities;

namespace UJect.UnityExtensions
{

    public class UnityDiBinder<TInterface1> : IUnityDiBinder<TInterface1>
    {
        private readonly DiContainer diContainer;

        private string customId;

        public UnityDiBinder(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        /// <summary>
        /// Bind the type resource with the custom ID provided. This allows disambiguating multiple resources of the same type
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The same binder</returns>
        [LibraryEntryPoint]
        public IUnityDiBinder<TInterface1> WithId(string id)
        {
            customId = id;
            return this;
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation stored in Resources.
        /// </summary>
        /// <typeparam name="TImpl">The implementation concrete type</typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToResource<TImpl>(string resourcePath) where TImpl : UnityEngine.Object, TInterface1
        {
            var resolver = new UnityExtensions.Resolvers.ResourceInstanceResolver<TImpl>(resourcePath);
            return ToCustomResolver<TImpl>(resolver);
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation instance of that type.
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToInstance<TImpl>(TImpl instance) where TImpl : UnityEngine.Object, TInterface1
        {
            var resolver = new UnityExtensions.Resolvers.InstanceResolver<TImpl>(instance);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a new instance of the provided concrete implementation of that type.
        /// </summary>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToNewPrefabInstance<TImpl>(TImpl prefab) where TImpl : UnityEngine.Component, TInterface1
        {
            var resolver = new UnityExtensions.Resolvers.NewPrefabInstanceResolver<TImpl>(diContainer, prefab);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a function that will provide a concrete instance of that type
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactoryMethod<TImpl>(Func<TImpl> factoryMethod) where TImpl : UnityEngine.Object, TInterface1
        {
            var resolver = new UnityExtensions.Resolvers.FunctionInstanceResolver<TImpl>(factoryMethod);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a custom resolver.
        /// </summary>
        /// <param name="customResolver"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToCustomResolver<TImpl>(IResolver customResolver) where TImpl : UnityEngine.Object, TInterface1
        {
            InstallBindings<TImpl>(customResolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a factory implementation that will provide a concrete instance of that type.
        /// Factories can be injected into before resolution, making them useful when you want to use a bunch of injected resources to
        /// create the instance.
        /// </summary>
        /// <param name="factoryImpl"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactory<TImpl>(IInstanceFactory<TImpl> factoryImpl) where TImpl : UnityEngine.Object, TInterface1
        {
                        diContainer.InstallFactoryBinding<TInterface1, TImpl>(customId, factoryImpl);
            return diContainer;
        }

        private void InstallBindings<TImpl>(IResolver resolver) where TImpl :  TInterface1
        {
            diContainer.InstallBinding<TInterface1, TImpl>(customId, resolver);
        }
    }

    public class UnityDiBinder<TInterface1, TInterface2> : IUnityDiBinder<TInterface1, TInterface2>
    {
        private readonly DiContainer diContainer;

        private string customId;

        public UnityDiBinder(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        /// <summary>
        /// Bind the type resource with the custom ID provided. This allows disambiguating multiple resources of the same type
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The same binder</returns>
        [LibraryEntryPoint]
        public IUnityDiBinder<TInterface1, TInterface2> WithId(string id)
        {
            customId = id;
            return this;
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation stored in Resources.
        /// </summary>
        /// <typeparam name="TImpl">The implementation concrete type</typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToResource<TImpl>(string resourcePath) where TImpl : UnityEngine.Object, TInterface1, TInterface2
        {
            var resolver = new UnityExtensions.Resolvers.ResourceInstanceResolver<TImpl>(resourcePath);
            return ToCustomResolver<TImpl>(resolver);
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation instance of that type.
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToInstance<TImpl>(TImpl instance) where TImpl : UnityEngine.Object, TInterface1, TInterface2
        {
            var resolver = new UnityExtensions.Resolvers.InstanceResolver<TImpl>(instance);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a new instance of the provided concrete implementation of that type.
        /// </summary>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToNewPrefabInstance<TImpl>(TImpl prefab) where TImpl : UnityEngine.Component, TInterface1, TInterface2
        {
            var resolver = new UnityExtensions.Resolvers.NewPrefabInstanceResolver<TImpl>(diContainer, prefab);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a function that will provide a concrete instance of that type
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactoryMethod<TImpl>(Func<TImpl> factoryMethod) where TImpl : UnityEngine.Object, TInterface1, TInterface2
        {
            var resolver = new UnityExtensions.Resolvers.FunctionInstanceResolver<TImpl>(factoryMethod);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a custom resolver.
        /// </summary>
        /// <param name="customResolver"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToCustomResolver<TImpl>(IResolver customResolver) where TImpl : UnityEngine.Object, TInterface1, TInterface2
        {
            InstallBindings<TImpl>(customResolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a factory implementation that will provide a concrete instance of that type.
        /// Factories can be injected into before resolution, making them useful when you want to use a bunch of injected resources to
        /// create the instance.
        /// </summary>
        /// <param name="factoryImpl"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactory<TImpl>(IInstanceFactory<TImpl> factoryImpl) where TImpl : UnityEngine.Object, TInterface1, TInterface2
        {
            diContainer.InstallFactoryMultiBind(
                customId,
                factoryImpl,
                InjectionKey.Of<TInterface1>(customId),
                InjectionKey.Of<TInterface2>(customId)
            );
            return diContainer;
        }

        private void InstallBindings<TImpl>(IResolver resolver) where TImpl :  TInterface1, TInterface2
        {
            diContainer.InstallMultiBinding(
                InjectionKey.Of<TImpl>(),
                resolver,
                InjectionKey.Of<TInterface1>(customId),
                InjectionKey.Of<TInterface2>(customId)
            );
        }
    }

    public class UnityDiBinder<TInterface1, TInterface2, TInterface3> : IUnityDiBinder<TInterface1, TInterface2, TInterface3>
    {
        private readonly DiContainer diContainer;

        private string customId;

        public UnityDiBinder(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        /// <summary>
        /// Bind the type resource with the custom ID provided. This allows disambiguating multiple resources of the same type
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The same binder</returns>
        [LibraryEntryPoint]
        public IUnityDiBinder<TInterface1, TInterface2, TInterface3> WithId(string id)
        {
            customId = id;
            return this;
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation stored in Resources.
        /// </summary>
        /// <typeparam name="TImpl">The implementation concrete type</typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToResource<TImpl>(string resourcePath) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3
        {
            var resolver = new UnityExtensions.Resolvers.ResourceInstanceResolver<TImpl>(resourcePath);
            return ToCustomResolver<TImpl>(resolver);
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation instance of that type.
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToInstance<TImpl>(TImpl instance) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3
        {
            var resolver = new UnityExtensions.Resolvers.InstanceResolver<TImpl>(instance);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a new instance of the provided concrete implementation of that type.
        /// </summary>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToNewPrefabInstance<TImpl>(TImpl prefab) where TImpl : UnityEngine.Component, TInterface1, TInterface2, TInterface3
        {
            var resolver = new UnityExtensions.Resolvers.NewPrefabInstanceResolver<TImpl>(diContainer, prefab);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a function that will provide a concrete instance of that type
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactoryMethod<TImpl>(Func<TImpl> factoryMethod) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3
        {
            var resolver = new UnityExtensions.Resolvers.FunctionInstanceResolver<TImpl>(factoryMethod);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a custom resolver.
        /// </summary>
        /// <param name="customResolver"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToCustomResolver<TImpl>(IResolver customResolver) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3
        {
            InstallBindings<TImpl>(customResolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a factory implementation that will provide a concrete instance of that type.
        /// Factories can be injected into before resolution, making them useful when you want to use a bunch of injected resources to
        /// create the instance.
        /// </summary>
        /// <param name="factoryImpl"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactory<TImpl>(IInstanceFactory<TImpl> factoryImpl) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3
        {
            diContainer.InstallFactoryMultiBind(
                customId,
                factoryImpl,
                InjectionKey.Of<TInterface1>(customId),
                InjectionKey.Of<TInterface2>(customId),
                InjectionKey.Of<TInterface3>(customId)
            );
            return diContainer;
        }

        private void InstallBindings<TImpl>(IResolver resolver) where TImpl :  TInterface1, TInterface2, TInterface3
        {
            diContainer.InstallMultiBinding(
                InjectionKey.Of<TImpl>(),
                resolver,
                InjectionKey.Of<TInterface1>(customId),
                InjectionKey.Of<TInterface2>(customId),
                InjectionKey.Of<TInterface3>(customId)
            );
        }
    }

    public class UnityDiBinder<TInterface1, TInterface2, TInterface3, TInterface4> : IUnityDiBinder<TInterface1, TInterface2, TInterface3, TInterface4>
    {
        private readonly DiContainer diContainer;

        private string customId;

        public UnityDiBinder(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        /// <summary>
        /// Bind the type resource with the custom ID provided. This allows disambiguating multiple resources of the same type
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The same binder</returns>
        [LibraryEntryPoint]
        public IUnityDiBinder<TInterface1, TInterface2, TInterface3, TInterface4> WithId(string id)
        {
            customId = id;
            return this;
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation stored in Resources.
        /// </summary>
        /// <typeparam name="TImpl">The implementation concrete type</typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToResource<TImpl>(string resourcePath) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4
        {
            var resolver = new UnityExtensions.Resolvers.ResourceInstanceResolver<TImpl>(resourcePath);
            return ToCustomResolver<TImpl>(resolver);
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation instance of that type.
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToInstance<TImpl>(TImpl instance) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4
        {
            var resolver = new UnityExtensions.Resolvers.InstanceResolver<TImpl>(instance);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a new instance of the provided concrete implementation of that type.
        /// </summary>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToNewPrefabInstance<TImpl>(TImpl prefab) where TImpl : UnityEngine.Component, TInterface1, TInterface2, TInterface3, TInterface4
        {
            var resolver = new UnityExtensions.Resolvers.NewPrefabInstanceResolver<TImpl>(diContainer, prefab);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a function that will provide a concrete instance of that type
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactoryMethod<TImpl>(Func<TImpl> factoryMethod) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4
        {
            var resolver = new UnityExtensions.Resolvers.FunctionInstanceResolver<TImpl>(factoryMethod);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a custom resolver.
        /// </summary>
        /// <param name="customResolver"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToCustomResolver<TImpl>(IResolver customResolver) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4
        {
            InstallBindings<TImpl>(customResolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a factory implementation that will provide a concrete instance of that type.
        /// Factories can be injected into before resolution, making them useful when you want to use a bunch of injected resources to
        /// create the instance.
        /// </summary>
        /// <param name="factoryImpl"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactory<TImpl>(IInstanceFactory<TImpl> factoryImpl) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4
        {
            diContainer.InstallFactoryMultiBind(
                customId,
                factoryImpl,
                InjectionKey.Of<TInterface1>(customId),
                InjectionKey.Of<TInterface2>(customId),
                InjectionKey.Of<TInterface3>(customId),
                InjectionKey.Of<TInterface4>(customId)
            );
            return diContainer;
        }

        private void InstallBindings<TImpl>(IResolver resolver) where TImpl :  TInterface1, TInterface2, TInterface3, TInterface4
        {
            diContainer.InstallMultiBinding(
                InjectionKey.Of<TImpl>(),
                resolver,
                InjectionKey.Of<TInterface1>(customId),
                InjectionKey.Of<TInterface2>(customId),
                InjectionKey.Of<TInterface3>(customId),
                InjectionKey.Of<TInterface4>(customId)
            );
        }
    }

    public class UnityDiBinder<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5> : IUnityDiBinder<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5>
    {
        private readonly DiContainer diContainer;

        private string customId;

        public UnityDiBinder(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        /// <summary>
        /// Bind the type resource with the custom ID provided. This allows disambiguating multiple resources of the same type
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The same binder</returns>
        [LibraryEntryPoint]
        public IUnityDiBinder<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5> WithId(string id)
        {
            customId = id;
            return this;
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation stored in Resources.
        /// </summary>
        /// <typeparam name="TImpl">The implementation concrete type</typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToResource<TImpl>(string resourcePath) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            var resolver = new UnityExtensions.Resolvers.ResourceInstanceResolver<TImpl>(resourcePath);
            return ToCustomResolver<TImpl>(resolver);
        }

        /// <summary>
        /// Bind the given type to a provided concrete implementation instance of that type.
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToInstance<TImpl>(TImpl instance) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            var resolver = new UnityExtensions.Resolvers.InstanceResolver<TImpl>(instance);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a new instance of the provided concrete implementation of that type.
        /// </summary>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToNewPrefabInstance<TImpl>(TImpl prefab) where TImpl : UnityEngine.Component, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            var resolver = new UnityExtensions.Resolvers.NewPrefabInstanceResolver<TImpl>(diContainer, prefab);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a function that will provide a concrete instance of that type
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactoryMethod<TImpl>(Func<TImpl> factoryMethod) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            var resolver = new UnityExtensions.Resolvers.FunctionInstanceResolver<TImpl>(factoryMethod);
            InstallBindings<TImpl>(resolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a custom resolver.
        /// </summary>
        /// <param name="customResolver"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToCustomResolver<TImpl>(IResolver customResolver) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            InstallBindings<TImpl>(customResolver);
            return diContainer;
        }

        /// <summary>
        /// Bind the given type to a factory implementation that will provide a concrete instance of that type.
        /// Factories can be injected into before resolution, making them useful when you want to use a bunch of injected resources to
        /// create the instance.
        /// </summary>
        /// <param name="factoryImpl"></param>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns>The original container</returns>
        [LibraryEntryPoint]
        public DiContainer ToFactory<TImpl>(IInstanceFactory<TImpl> factoryImpl) where TImpl : UnityEngine.Object, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            diContainer.InstallFactoryMultiBind(
                customId,
                factoryImpl,
                InjectionKey.Of<TInterface1>(customId),
                InjectionKey.Of<TInterface2>(customId),
                InjectionKey.Of<TInterface3>(customId),
                InjectionKey.Of<TInterface4>(customId),
                InjectionKey.Of<TInterface5>(customId)
            );
            return diContainer;
        }

        private void InstallBindings<TImpl>(IResolver resolver) where TImpl :  TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            diContainer.InstallMultiBinding(
                InjectionKey.Of<TImpl>(),
                resolver,
                InjectionKey.Of<TInterface1>(customId),
                InjectionKey.Of<TInterface2>(customId),
                InjectionKey.Of<TInterface3>(customId),
                InjectionKey.Of<TInterface4>(customId),
                InjectionKey.Of<TInterface5>(customId)
            );
        }
    }
}