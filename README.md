# unity-hierarchy-behaviour

# What is it?
Its a collection of GameObject extension methods to allow for runtime instantiation of MonoBehaviours as a child GameObject that include an Initialize method that takes type-safe arguments.

## What are the Core Features?
 * Control the flow of data
 * Divide responsibility of components
 * Visualize that responsibility
 * Control lifecycles

## Why does it exist?
The ability to easily add child game objects with behaviours, initialize them and pass in type-safe args.

## How to use it.
HierarchyBehaviour is entirely run via `GameObject` Extension methods for creation and interfaces for implementation.

```c#
public class MyClass : MonoBehaviour, IHierarchyBehaviour
{
    public void Initialize()
    {
        Debug.Log("Initialized");
    }
}
```
```c#
public class MyClassWithArgs : MonoBehaviour, IHierarchyBehaviour<CustomArgs>
{
    public void Initialize(CustomArgs args)
    {
        Debug.Log("Initialized with " + args);
    }
}
```

The Initialize methods are automatically called by the GameObjectExtention methods used to create the new instance.
- CreateChild (New, Resources, Loaded or Instantiated)
- ReplaceChild (New, Resources, Loaded or Instantiated)

However you can just add your class that implements `IHierarchyBehaviour` and choose to call Initialize when you prefer to.

### CreateChild
With Arguements
```C#
var myClassWithArgs = gameObject.CreateChild<MyClassWithArgs, CustomArgs>(new CustomArgs("HelloWorld"));
```
Without Arguements
```C#
var myClass = gameObject.CreateChild<MyClass>();
```

This will create a child new GameObject and apply the template class `TBehaviour`.  
The template class must extend MonoBehaviour and implement `IHierarchyBehaviour` or `IHierarchyBehaviour<TArgs>`.
This will return the new instance of `TBehaviour`.

### CreateChild from resources
With Arguements
```C#
var myClassWithArgs = gameObject.CreateChild<MyClassWithArgs, CustomArgs>("MyResourcePath", new CustomArgs("HelloWorld"));
```
Without Arguements
```C#
var myClass = gameObject.CreateChild<MyClass>("MyResourcePath");
```

This will load and instantiate a asset of type `TBehaviour` from Unity's `Resources`.
The template class must extend MonoBehaviour and implement `IHierarchyBehaviour` or `IHierarchyBehaviour<TArgs>`. 
This will return the new instance of `TBehaviour`.

### CreateChild from loaded
With Arguements
```C#
var myClassWithArgs = gameObject.CreateChild<MyClassWithArgs>(prefab, new CustomArgs("HelloWorld"));
```
Without Arguements
```C#
var myClass = gameObject.CreateChild<MyClass>(prefab);
```

This will take a pre-existing (loaded or instantiated) `IHierarchyBehaviour` and clone it. The template class must extend MonoBehaviour and implement `IHierarchyBehaviour` or `IHierarchyBehaviour<TArgs>`. 
This will return the new instance of `TBehaviour`.


### ReplaceChild
With Arguements
```C#
var myClassWithArgs = gameObject.ReplaceChild<MyClassWithArgs, CustomArgs>(toReplace, new CustomArgs("HelloWorld"));
```
Without Arguements
```C#
var myClass = gameObject.ReplaceChild<MyClass>(toReplace);
```

You can also use ReplaceChild with resourced assets and loaded assets.
```C#
var myClassWithArgs = gameObject.ReplaceChild<MyClassWithArgs>(toReplace, "MyResourcePath");
```
```C#
var myClass = gameObject.ReplaceChild<MyClass>(toReplace, prefab);
```

ReplaceChild requires a `TBehaviour` to create or instantiate and an existing MonoBehaviour to Destroy.
The template class must extend MonoBehaviour and implement `IHierarchyBehaviour` or `IHierarchyBehaviour<TArgs>`. 
This will return the new instance of `TBehaviour`.  
