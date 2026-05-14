# DiContainer
### What is it?
`DiContainer` is a scoped container holding on to a bunch of bound implementations.
You can use it as either a Service Locator, or use more advanced injection techniques.

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
