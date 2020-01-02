using UnityEngine;

namespace Duck.HierarchyBehaviour
{
	public static partial class GameObjectExtensions
	{
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
		/// TComponent will be initialized with the given arguments.
		/// </summary>
		/// <param name="toDestroy">The child Component to destroy.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TComponent">The type of Component to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguments to be given on initialization.</typeparam>
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
		/// TComponent will be initialized with the given arguments.
		/// </summary>
		/// <param name="toDestroy">The child Component to destroy.</param>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TComponent">The type of Component to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguments to be given on initialization.</typeparam>
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
		/// TComponent will be initialized with the given arguments.
		/// </summary>
		/// <param name="toDestroy">The child Component to destroy.</param>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TComponent">The type of Component to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguments to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent ReplaceChild<TComponent, TArgs>(this GameObject parent, Component toDestroy, TComponent toClone, TArgs args)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild(toClone, args);
		}
	}
}
