# DiContainer
### What is it?
`DiContainer` is a scoped container holding on to a bunch of bound implementations.
You can use it as either a Service Locator, or use more advanced injection techniques.

## Dependency Resolution
UJect expects the following lifecycle (but does not enforce it):
1. Bind implementations and resources
2. Construct dependency tree
2. Resolve all dependencies in dependency-tree-order

It achieves this internally by tracking a `DiPhase`, with two states: `Bind` and `Resolved`.

By default, `DiContainer` is in the `Bind` phase when it's constructed, and remains there until `TryResolveAll()` is called.

At the end of `TryResolveAll()`, the container moves to the `Resolved` state.

**Note:** `Get<T>()` calls `TryResolveAll()` under the hood if the DiContainer is in the `Bind` state. You do not need to call TryResolveAll manually.


### Performance Note: Bind/Resolve duplication
The `Bind` methods will cause a `DiContainer` to move back into the `Bind` state if it's not already there (as the dependency tree is changing). This can cause performance issues if it happens too often.

e.g.
```
container.Bind<IInterface1>().ToNewInstance<Impl1>(); // Moves to Bind state
container.Get<IInterface1>(); // Moves to Resolved state
container.Bind<IInterface2>().ToNewInstance<Impl2>(); // Moves BACK to Bind state
container.Get<IInterface2>(); // Resolves entire dependency tree again
```

It is best practice to do all Binding *before* querying things from the DiContainer to avoid the relatively heavyweight `TryResolveAll()` call as much as possible.

## Scoping
DiContainers can be nested to control specific implementation scopes. For example:
```
var rootContainer = new DiContainer("Root");
var childContainer = rootContainer.CreateChildContainer("Child");
```

Container scoping follows these rules:
1. A child container will always have access to implementation bound in its parent container(s). 
2. A parent container *does not* have access to implementations in its children.

e.g. This is valid:

```
var rootContainer = new DiContainer("Root");
var childContainer = rootContainer.CreateChildContainer("Child");

rootContainer.Bind<IInterface>().ToNewInstance<Impl>();
childContainr.Get<IInterface>(); // This is valid!
```

This is not:

```
var rootContainer = new DiContainer("Root");
var childContainer = rootContainer.CreateChildContainer("Child");

childContainr.Bind<IInterface>().ToNewInstance<Impl>();
rootContainer.Get<IInterface>(); // This is NOT valid!
```