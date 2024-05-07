# XmlComment Extensions
This solution provides basic convenience methods:
 - read XML comments for classes, methods, and properties

# Getting Started

1. Load the XML docs for an assembly
```cs
var xmlDocs = assembly.LoadXmlDocumentation();
```

2. Fetch XML docs on a class
```
var classdocs = xmlDocs.GetDocumentation(typeof(MyClass));
```

3. Fetch XML docs for a property within a class
```
foreach (var property in typeof(MyClass).GetProperties())
{
	var propertyDocs = xmlDocs.GetDocumentation(property);
}
```

4. Fetch XML docs for a method within a class 
```
var methods = typeof(MyClass).GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
foreach (var method in methods)
{
	var methodDocs = xmlDocs.GetDocumentation(method);
}
```