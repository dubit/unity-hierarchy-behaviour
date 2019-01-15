# unity-hierarchy-behaviour

## What is it?
Its a collection of GameObject extension methods to allow for runtime instantiation a MonoBehaviour that include an Initialize method that takes type-safe arguments.

## What are the Core Features?
The ability to easily add child game objects with a specified `IHierarchyBehaviour` that can be initialize with type-safe args.

## What are the benifits?
 * Control the flow of data
 * Divide responsibility of components
 * Visualize that responsibility
 * Control lifecycles

## What are the requirements?
 * Unity 2018.x

## How to use it
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

In addition you can also implement multiple `IHierarchyBehaviour`'s, for example:
```c#
public class LightEstimation : MonoBehaviour, IHierarchyBehaviour, IHierarchyBehaviour<Light[]>
{
    private Light[] lights;

    public void Initialize()
    {
        lights = FindObjectsOfType<Light>();
    }
    
    public void Initialize(params Light[] lights)
    {
        this.lights = lights;
    }
}
```
In this case the class `LightEstimation` has the option to be initialized via  
`gameObject.CreateChild<LightEstimation>();` in which it will call `Initalize()` with no args.  

or we can do  
```
gameObject.CreateChild<LightEstimation, Light[]>(new[]
{
    directionalLight
});
```
If you already have reference to it you can simply do  
```c#
lightEstimation.Initialize();
```
or  
```c#
lightEstimation.Initalize(directionalLight, pointLight);
```

### CreateChild

With Name
```C#
var myGameObject = gameObject.CreateChild("HelloWorld");
```
Without Name
```C#
var myGameObject = gameObject.CreateChild();
```

With Arguements
```C#
var myClassWithArgs = gameObject.CreateChild<MyClassWithArgs, CustomArgs>(new CustomArgs("HelloWorld"));
```
Without Arguements
```C#
var myClass = gameObject.CreateChild<MyClass>();
```

This will create a child new GameObject and adds the component specified by the `TBehaviour` type parameter.
The type parameter must extend MonoBehaviour and implement `IHierarchyBehaviour` or `IHierarchyBehaviour<TArgs>`.
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
GameObject
```C#
var myGameObject = gameObject.CreateChild(path: "MyResourcePath");
```

This will create a child new GameObject and adds the component specified by the `TBehaviour` type parameter.
The type parameter must extend MonoBehaviour and implement `IHierarchyBehaviour` or `IHierarchyBehaviour<TArgs>`.
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
GameObject
```C#
var myGameObject = gameObject.CreateChild(prefab.gameObject);
```

This will take a pre-existing (loaded or instantiated) `IHierarchyBehaviour` and clone it.  
The type parameter must extend MonoBehaviour and implement `IHierarchyBehaviour` or `IHierarchyBehaviour<TArgs>`.  
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

This will destroy the given child MonoBehaviour and create a child new GameObject and adds the component specified by the `TBehaviour` type parameter.  
The type parameter must extend MonoBehaviour and implement `IHierarchyBehaviour` or `IHierarchyBehaviour<TArgs>`.  
This will return the new instance of `TBehaviour`.  
