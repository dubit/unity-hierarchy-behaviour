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
		public static TBehaviour CreateChild<TBehaviour>(this GameObject gameObject)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(gameObject.transform);
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
		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject gameObject, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(gameObject.transform);
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
		public static TBehaviour CreateChildFromResources<TBehaviour>(this GameObject gameObject, string path, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, gameObject.transform, worldPositionStay);
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
		public static TBehaviour CreateChildFromResources<TBehaviour, TArgs>(this GameObject gameObject, string path, TArgs args, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, gameObject.transform, worldPositionStay);
			behaviour.Initialize(args);
			return behaviour;
		}

		/// <summary>
		/// Create a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized automatically and then returned.
		/// </summary>
		/// <param name="behaviourToClone">The GameObject to clone.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour that is being cloned.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChildFromLoaded<TBehaviour>(this GameObject gameObject, TBehaviour behaviourToClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CloneBehaviour(behaviourToClone, gameObject.transform);
			behaviour.Initialize();
			return behaviour;
		}

		/// <summary>
		/// Create a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized with the given arguements automatically and then returned.
		/// </summary>
		/// <param name="behaviourToClone">The GameObject to clone.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour that is being cloned.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChildFromLoaded<TBehaviour, TArgs>(this GameObject gameObject, TBehaviour behaviourToClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CloneBehaviour(behaviourToClone, gameObject.transform);
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
		public static TBehaviour Replace<TBehaviour>(this GameObject gameObject, MonoBehaviour toDestroy)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChild<TBehaviour>();
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
		public static TBehaviour Replace<TBehaviour, TArgs>(this GameObject gameObject, MonoBehaviour toDestroy, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChild<TBehaviour, TArgs>(args);
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
		public static TBehaviour Replace<TBehaviour>(this GameObject gameObject, MonoBehaviour toDestroy, string path, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChildFromResources<TBehaviour>(path, worldPositionStay);
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
		public static TBehaviour Replace<TBehaviour, TArgs>(this GameObject gameObject, MonoBehaviour toDestroy, string path, TArgs args, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChildFromResources<TBehaviour, TArgs>(path, args, worldPositionStay);
		}

		/// <summary>
		/// Destroy a MonoBehaviour and reate a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized automatically and then returned.
		/// </summary>
		/// <param name="toDestroy">The MonoBehaviour to destroy.</param>
		/// <param name="behaviourToClone">The GameObject to clone.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour Replace<TBehaviour>(this GameObject gameObject, MonoBehaviour toDestroy, TBehaviour behaviourToClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChildFromLoaded(behaviourToClone);
		}

		/// <summary>
		/// Destroy a MonoBehaviour and reate a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized with the given arguements automatically and then returned.
		/// </summary>
		/// <param name="toDestroy">The MonoBehaviour to destroy.</param>
		/// <param name="behaviourToClone">The GameObject to clone.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour Replace<TBehaviour, TArgs>(this GameObject gameObject, MonoBehaviour toDestroy, TBehaviour behaviourToClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChildFromLoaded(behaviourToClone, args);
		}
	}
}
