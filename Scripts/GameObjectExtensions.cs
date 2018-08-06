using UnityEngine;

namespace DUCK.HieriarchyBehaviour
{
	public static class GameObjectExtensions
	{
		/// <summary>
		/// Creates a clone of the given GameObject as a child transform.
		/// </summary>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <returns>The new GameObject</returns>
		public static GameObject CreateChild(this GameObject parent, GameObject toClone)
		{
			return Utils.CloneGameObject(toClone, parent);
		}

		/// <summary>
		/// Creates a child GameObject from resources.
		/// </summary>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <returns>The new GameObject</returns>
		public static GameObject CreateChild(this GameObject parent, string path, bool worldPositionStays = true)
		{
			return Utils.InstantiateResource<GameObject>(path, parent, worldPositionStays);
		}

		/// <summary>
		/// Creates a child GameObject with the given TComponent component.
		/// IHierarchyBehaviours's will be initialized.
		/// </summary>
		/// <typeparam name="TComponent">The type of Component to be added to the new child GameObject.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent CreateChild<TComponent>(this GameObject parent)
			where TComponent : Component
		{
			var behaviour = Utils.CreateGameObjectWithComponent<TComponent>(parent);
			var hierarchyBehaviour = behaviour as IHierarchyBehaviour;
			if (hierarchyBehaviour != null)
			{
				hierarchyBehaviour.Initialize();
			}

			return behaviour;
		}

		/// <summary>
		/// Creates a child GameObject with the given TComponent component.
		/// TComponent will be initialized with the given arguements.
		/// </summary>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TComponent">The type of Component to be added to the new child GameObject.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent CreateChild<TComponent, TArgs>(this GameObject parent, TArgs args)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CreateGameObjectWithComponent<TComponent>(parent);
			behaviour.Initialize(args);
			return behaviour;
		}

		/// <summary>
		/// Creates a child GameObject from resources.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TComponent">The type of Component to be added to the new GameObject.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent CreateChild<TComponent>(this GameObject parent, string path, bool worldPositionStays = true)
			where TComponent : Component
		{
			var behaviour = Utils.InstantiateResource<TComponent>(path, parent, worldPositionStays);
			var hierarchyBehaviour = behaviour as IHierarchyBehaviour;
			if (hierarchyBehaviour != null)
			{
				hierarchyBehaviour.Initialize();
			}

			return behaviour;
		}

		/// <summary>
		/// Creates a child GameObject from resources.
		/// TComponent will be initialized with the given arguements.
		/// </summary>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TComponent">The type of Component to be added to the new GameObject.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent CreateChild<TComponent, TArgs>(this GameObject parent, string path, TArgs args, bool worldPositionStays = true)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.InstantiateResource<TComponent>(path, parent, worldPositionStays);
			behaviour.Initialize(args);
			return behaviour;
		}

		/// <summary>
		/// Creates a clone of the given TComponent as a child transform.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <typeparam name="TComponent">The type of Component that is being cloned.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent CreateChild<TComponent>(this GameObject parent, TComponent toClone)
			where TComponent : Component
		{
			var behaviour = Utils.CloneComponent(toClone, parent);
			var hierarchyBehaviour = behaviour as IHierarchyBehaviour;
			if (hierarchyBehaviour != null)
			{
				hierarchyBehaviour.Initialize();
			}

			return behaviour;
		}

		/// <summary>
		/// Creates a clone of the given TComponent as a child transform.
		/// TComponent will be initialized with the given arguements.
		/// </summary>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TComponent">The type of Component that is being cloned.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent CreateChild<TComponent, TArgs>(this GameObject parent, TComponent toClone, TArgs args)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CloneComponent(toClone, parent);
			behaviour.Initialize(args);
			return behaviour;
		}

		/// <summary>
		/// Destroys the child GameObject and creates a new child GameObject with the given TComponent component.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toDestroy">The child Component to destroy.</param>
		/// <typeparam name="TComponent">The type of Component to be created.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent ReplaceChild<TComponent>(this GameObject parent, Component toDestroy)
			where TComponent : Component
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TComponent>();
		}

		/// <summary>
		/// Destroys the child GameObject and creates a new child GameObject with the given TComponent component.
		/// TComponent will be initialized with the given arguements.
		/// </summary>
		/// <param name="toDestroy">The child Component to destroy.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TComponent">The type of Component to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent ReplaceChild<TComponent, TArgs>(this GameObject parent, Component toDestroy, TArgs args)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TComponent, TArgs>(args);
		}

		/// <summary>
		/// Destroys the child GameObject and creates a new child GameObject, by loading from resources and instantiating.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toDestroy">The child Component to destroy.</param>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TComponent">The type of Component to be created.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent ReplaceChild<TComponent>(this GameObject parent, Component toDestroy, string path, bool worldPositionStays = true)
			where TComponent : Component
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TComponent>(path, worldPositionStays);
		}

		/// <summary>
		/// Destroys the child GameObject and creates a new child GameObject, by loading from resources and instantiating.
		/// TComponent will be initialized with the given arguements.
		/// </summary>
		/// <param name="toDestroy">The child Component to destroy.</param>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TComponent">The type of Component to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent ReplaceChild<TComponent, TArgs>(this GameObject parent, Component toDestroy, string path, TArgs args, bool worldPositionStays = true)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TComponent, TArgs>(path, args, worldPositionStays);
		}

		/// <summary>
		/// Destroys the child GameObject and creates a clone of the given TComponent as a child transform.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toDestroy">The child Component to destroy.</param>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <typeparam name="TComponent">The type of Component to be created.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent ReplaceChild<TComponent>(this GameObject parent, Component toDestroy, TComponent toClone)
			where TComponent : Component
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild(toClone);
		}

		/// <summary>
		/// Destroys the child GameObject and creates a clone of the given TComponent as a child transform.
		/// TComponent will be initialized with the given arguements.
		/// </summary>
		/// <param name="toDestroy">The child Component to destroy.</param>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TComponent">The type of Component to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent ReplaceChild<TComponent, TArgs>(this GameObject parent, Component toDestroy, TComponent toClone, TArgs args)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild(toClone, args);
		}
	}
}
