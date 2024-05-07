# Reflection Extensions
This solution provides basic convenience methods:
 - find classes by type and with attribute

```cs
var types = assembly.GetClasses<IValidator>();
```

```cs
var types = assembly.GetClasses(typeof(IValidator));
```

```cs
var types = assembly.GetConcreteTypesWithAttribute<MyCustomAttribute>();
```

# Getting Started

Install the nuget package and consume the methods.