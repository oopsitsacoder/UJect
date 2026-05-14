
# Binding

## Basic Bindings
UJect provides a simple binding interface for a few different use cases:

1. Bind to existing instance
```
//Bind IInterface to an existing instance of impl
var impl = new Impl();
container.Bind<IInterface>().ToInstance(impl);
```

2. Bind to a new instance
```
//Bind IInterface to a new instance of impl
container.Bind<IInterface>().ToNewInstance<Impl>();
```

3. Bind to factory
```
//Bind IInterface to a new instance, which will be created by the factory at resolution time
IInstanceFactory<Impl> myFactory = ...;
container.Bind<IInterface>().ToFactory(myFactory);
```

4. Bind to factory method
```
//Bind IInterface to a new instance, which will be created by the factory method at resolution time
Func<Impl> myFactoryFunc = ...;
container.Bind<IInterface>().ToFactoryMethod(myFactoryFunc);
```

## Unity Object Bindings
There are some special Unity bindings provided via `UnityExtensions`

1. Bind to a file in `Resources`
```diContainer.UnityBind<IInterface1>().ToResource<MyScriptableObject>("ScriptableObjectPath");```

2. Bind to a new instance of a prefab:
```
var prefab = Resources.Load<MyMonoBehaviour>("MonoBehaviourPrefab");
diContainer.UnityBind<IInterface1>().ToNewPrefabInstance(prefab);
```

## Multi-Bindings
More than one interface can be bound to the same instance:
```
class Impl : IInterface1, IInterface2 { ... }

//Bind IInterface1 and IInterface2 to the same new instance of Impl
var impl = new Impl();
container.Bind<IInterface1, IInterface2>().ToNewInstance<Impl>();

var interface1 = container.Get<IInterface1>();
var interface2 = container.Get<IInterface2>();
var isSameInstance = object.ReferenceEquals(interface1, interface2); // Is true
```

## Unbinding
You can unbind by calling
```
container.Unbind<IInterface>();
```

This is generally NOT advised as it muddies the lifecycle of dependencies (better to dispose/create a new container), 
but is provided for completeness.

### Custom IDs
Sometimes, you want to bind multiple instances of the same interface. In that case, you can give bind each with a custom `string` id:
```
var impl1 = new Impl();
var impl2 = new Impl();
container.Bind<IInterface>().WithId("InstanceA").ToInstance(impl1);
container.Bind<IInterface>().WithId("InstanceB").ToInstance(impl2);
```
They can be retrieved in a similar manner:
```
var dependency1 = container.Get<IInterface>("InstanceA");
var dependency2 = container.Get<IInterface>("InstanceB");
```
