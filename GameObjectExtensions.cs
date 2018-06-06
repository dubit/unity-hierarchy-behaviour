using UnityEngine;

namespace DUCK.HieriarchyBehaviour
{
	public static class GameObjectExtensions
	{
		/// <summary>
		/// Create a new GameObject as a child transform that has the given TBehaviour component.
		/// TBehaviour will be initialized automatically and then returned.
		/// </summary>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be added to the new GameObject.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(parent);
			behaviour.Initialize();
			return behaviour;
		}

		/// <summary>
		/// Create a new GameObject as a child transform that has the given TBehaviour component.
		/// TBehaviour will be initialized with the given arguements automatically and then returned.
		/// </summary>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be added to the new GameObject.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject parent, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(parent);
			behaviour.Initialize(args);
			return behaviour;
		}

		/// <summary>
		/// Using the given path to load and create the asset as a child transform that has the given TBehaviour component.
		/// TBehaviour will be initialized automatically and then returned.
		/// </summary>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="worldPositionStay">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be added to the new GameObject.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent, string path, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, parent, worldPositionStay);
			behaviour.Initialize();
			return behaviour;
		}

		/// <summary>
		/// Using the given path to load and create the asset as a child transform that has the given TBehaviour component.
		/// TBehaviour will be initialized with the given arguements automatically and then returned.
		/// </summary>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <param name="worldPositionStay">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be added to the new GameObject.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject parent, string path, TArgs args, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, parent, worldPositionStay);
			behaviour.Initialize(args);
			return behaviour;
		}

		/// <summary>
		/// Create a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized automatically and then returned.
		/// </summary>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour that is being cloned.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent, TBehaviour toClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CloneBehaviour(toClone, parent);
			behaviour.Initialize();
			return behaviour;
		}

		/// <summary>
		/// Create a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized with the given arguements automatically and then returned.
		/// </summary>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour that is being cloned.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject parent, TBehaviour toClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CloneBehaviour(toClone, parent);
			behaviour.Initialize(args);
			return behaviour;
		}

		/// <summary>
		/// Destroy a MonoBehaviour and create a new GameObject as a child transform that has the given TBehaviour component.
		/// TBehaviour will be initialized automatically and then returned.
		/// </summary>
		/// <param name="toDestroy">The MonoBehaviour to destroy.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			return parent.CreateChild<TBehaviour>();
		}

		/// <summary>
		/// Destroy a MonoBehaviour and create a new GameObject as a child transform that has the given TBehaviour component.
		/// TBehaviour will be initialized with the given arguements automatically and then returned.
		/// </summary>
		/// <param name="toDestroy">The MonoBehaviour to destroy.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			return parent.CreateChild<TBehaviour, TArgs>(args);
		}

		/// <summary>
		/// Destroy a MonoBehaviour, then using the given path to load and create the asset as a child transform that has the given TBehaviour component.
		/// TBehaviour will be initialized automatically and then returned.
		/// </summary>
		/// <param name="toDestroy">The MonoBehaviour to destroy.</param>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="worldPositionStay">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy, string path, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			return parent.CreateChild<TBehaviour>(path, worldPositionStay);
		}

		/// <summary>
		/// Destroy a MonoBehaviour, then using the given path to load and create the asset as a child transform that has the given TBehaviour component.
		/// TBehaviour will be initialized with the given arguements automatically and then returned.
		/// </summary>
		/// <param name="toDestroy">The MonoBehaviour to destroy.</param>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <param name="worldPositionStay">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, string path, TArgs args, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			return parent.CreateChild<TBehaviour, TArgs>(path, args, worldPositionStay);
		}

		/// <summary>
		/// Destroy a MonoBehaviour and reate a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized automatically and then returned.
		/// </summary>
		/// <param name="toDestroy">The MonoBehaviour to destroy.</param>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy, TBehaviour toClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			return parent.CreateChild(toClone);
		}

		/// <summary>
		/// Destroy a MonoBehaviour and reate a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized with the given arguements automatically and then returned.
		/// </summary>
		/// <param name="toDestroy">The MonoBehaviour to destroy.</param>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, TBehaviour toClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			return parent.CreateChild(toClone, args);
		}
	}
}
