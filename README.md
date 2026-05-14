# UJect

Uject is an attempt at a simplified dependency injection framework for Unity3d Games.

## Installation
Open the Unity package manager, choose "Install package from git URL", and input `https://github.com/oopsitsacoder/UJect.git`

## How to use

At its simplest, you can use a `DiContainer` as a dictionary of interfaces to concrete instances:

```
public class SampleClass
{
    //Create a new container
    private DiContainer container = new DiContainer("MyContainer");
    
    public SampleClass()
    {
        //Bind IInterface to an existing instance of impl
        var impl = new Impl();
        container.Bind<IInterface>().ToInstance(impl);

        //Retrieve from the container
        var dependency = container.Get<IInterface>();
    }

    private interface IInterface {}
    private class Impl : IInterface {}
}

```

## [DiContainer](~docs/DiContainer.md)
`DiContainer` is a scoped container holding on to a bunch of bound implementations.
You can use it as either a Service Locator, or use more advanced injection techniques.

## [Binding](~docs/Binding.md)
Binding is how you define dependencies and how resources are resolved.

### Quick Start
```
//Bind IInterface to an existing instance of impl
var impl = new Impl();
container.Bind<IInterface>().ToInstance(impl);
```

## Injection
Because calling `Get<TInterface>()` everywhere is annoying, UJect comes with an `[Inject]` attribute, which can be used to automatically inject dependencies into other classes via reflection.

### Field injection
You can mark fields to be injected automatically.
```
public class SampleClass
{
    //Create a new container
    private DiContainer container = new DiContainer("MyContainer");
    
    public SampleClass()
    {
        container.Bind<IInterface>().ToNewInstance<Impl>();
        container.Bind<IInterface2>().ToNewInstance<Impl2>();

        //Retrieve from the container
        var dependency = container.Get<IInterface>();
    }

    private interface IInterface {}
    private class Impl : IInterface 
    {
        [Inject] private readonly IInterface2 impl2;
    }
    
    private interface IInterface2 {}
    private class Impl2 : IInterface2 {}
}
```
In this example, `impl2` will be automatically injected into the `Impl` instance when `IInterface` is resolved.

### Constructor injection
You can also mark constructor parameters as injectable, and UJect will attempt to fill them in (or throw an exception if it cannot).
```
    private class Impl : IInterface 
    {
        private readonly IInterface2 impl2;

        // Constructor parameter will be automatically filled in
        public Impl([Inject] IInterface2 impl2) => this.impl2 = impl2;
    }
```

### Note:
If Unity's code stripping feature is turned on, it's possible that injected fields and constructors will be stripped, as they're only referenced via reflection. If you have code stripping turned on, you'll have to mark your injected memebers with `[Preserve]`
