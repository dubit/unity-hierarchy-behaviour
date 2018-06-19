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
		/// Creates a child GameObject with the given TBehaviour component.
		/// IHierarchyBehaviours's will be initialized.
		/// </summary>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be added to the new child GameObject.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent)
			where TBehaviour : MonoBehaviour
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(parent);
			var hierarchyBehaviour = behaviour as IHierarchyBehaviour;
			if (hierarchyBehaviour != null)
			{
				hierarchyBehaviour.Initialize();
			}

			return behaviour;
		}

		/// <summary>
		/// Creates a child GameObject with the given TBehaviour component.
		/// TBehaviour will be initialized with the given arguements.
		/// </summary>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be added to the new child GameObject.</typeparam>
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
		/// Creates a child GameObject from resources.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be added to the new GameObject.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent, string path, bool worldPositionStays = true)
			where TBehaviour : MonoBehaviour
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, parent, worldPositionStays);
			var hierarchyBehaviour = behaviour as IHierarchyBehaviour;
			if (hierarchyBehaviour != null)
			{
				hierarchyBehaviour.Initialize();
			}

			return behaviour;
		}

		/// <summary>
		/// Creates a child GameObject from resources.
		/// TBehaviour will be initialized with the given arguements.
		/// </summary>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be added to the new GameObject.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject parent, string path, TArgs args, bool worldPositionStays = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, parent, worldPositionStays);
			behaviour.Initialize(args);
			return behaviour;
		}

		/// <summary>
		/// Creates a clone of the given TBehaviour as a child transform.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour that is being cloned.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent, TBehaviour toClone)
			where TBehaviour : MonoBehaviour
		{
			var behaviour = Utils.CloneBehaviour(toClone, parent);
			var hierarchyBehaviour = behaviour as IHierarchyBehaviour;
			if (hierarchyBehaviour != null)
			{
				hierarchyBehaviour.Initialize();
			}

			return behaviour;
		}

		/// <summary>
		/// Creates a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized with the given arguements.
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
		/// Destroys the child MonoBehaviour and creates a child GameObject with the given TBehaviour component.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toDestroy">The child MonoBehaviour to destroy.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy)
			where TBehaviour : MonoBehaviour
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TBehaviour>();
		}

		/// <summary>
		/// Destroys the child MonoBehaviour and creates a child GameObject with the given TBehaviour component.
		/// TBehaviour will be initialized with the given arguements.
		/// </summary>
		/// <param name="toDestroy">The child MonoBehaviour to destroy.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TBehaviour, TArgs>(args);
		}

		/// <summary>
		/// Destroys the child MonoBehaviour and creates a child GameObject, by loading from resources and instantiating.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toDestroy">The child MonoBehaviour to destroy.</param>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy, string path, bool worldPositionStays = true)
			where TBehaviour : MonoBehaviour
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TBehaviour>(path, worldPositionStays);
		}

		/// <summary>
		/// Destroys the child MonoBehaviour and creates a child GameObject, by loading from resources and instantiating.
		/// TBehaviour will be initialized with the given arguements.
		/// </summary>
		/// <param name="toDestroy">The child MonoBehaviour to destroy.</param>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <param name="worldPositionStays">Will the instantiated GameObject stay in its world position or be set to local origin.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, string path, TArgs args, bool worldPositionStays = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TBehaviour, TArgs>(path, args, worldPositionStays);
		}

		/// <summary>
		/// Destroys the child MonoBehaviour and creates a clone of the given TBehaviour as a child transform.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toDestroy">The child MonoBehaviour to destroy.</param>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy, TBehaviour toClone)
			where TBehaviour : MonoBehaviour
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild(toClone);
		}

		/// <summary>
		/// Destroys the child MonoBehaviour and creates a clone of the given TBehaviour as a child transform.
		/// TBehaviour will be initialized with the given arguements.
		/// </summary>
		/// <param name="toDestroy">The child MonoBehaviour to destroy.</param>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TBehaviour">The type of MonoBehaviour to be created.</typeparam>
		/// <typeparam name="TArgs">The type of arguements to be given on initialization.</typeparam>
		/// <returns>The new TBehaviour</returns>
		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, TBehaviour toClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild(toClone, args);
		}
	}
}
