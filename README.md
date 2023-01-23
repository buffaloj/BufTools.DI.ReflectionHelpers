# DI Reflection Helpers
This solution provides basic convenience methods to register dependencies for injection for all classes of base type T, and all classes with a specified [Attribute]

```cs
serviceCollection.AddSingletonClasses<IValidator>(_assembly);
```

```cs
serviceCollection.AddTransientClassesWithAttribute<MyCustomAttribute>(_assembly);
```

# Getting Started

All registrations are extension methods for a service collection.  

1. Choose the type of registration you want
 - Transient
 - Scoped
 - Singleton
 
 2. Call the appropriate method

```cs
serviceCollection.AddSingletonClasses<IValidator>(_assembly);
```
