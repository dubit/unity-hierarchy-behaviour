using UnityEngine;

namespace DUCK.HieriarchyBehaviour
{
	public static class GameObjectExtensions
	{
		public static TBehaviour CreateChild<TBehaviour>(this GameObject gameObject)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(gameObject);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject gameObject, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(gameObject);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour>(this GameObject gameObject, string path, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, gameObject, worldPositionStay);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject gameObject, string path, TArgs args, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, gameObject, worldPositionStay);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour>(this GameObject gameObject, TBehaviour behaviourToClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CloneBehaviour(behaviourToClone, gameObject);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject gameObject, TBehaviour behaviourToClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CloneBehaviour(behaviourToClone, gameObject);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject gameObject, MonoBehaviour toDestroy)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChild<TBehaviour>();
		}

		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject gameObject, MonoBehaviour toDestroy, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChild<TBehaviour, TArgs>(args);
		}

		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject gameObject, MonoBehaviour toDestroy, string path)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChild<TBehaviour>(path);
		}

		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject gameObject, MonoBehaviour toDestroy, string path, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChild<TBehaviour, TArgs>(path, args);
		}

		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject gameObject, MonoBehaviour toDestroy, TBehaviour toClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChild(toClone);
		}

		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject gameObject, MonoBehaviour toDestroy, TBehaviour toClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			return gameObject.CreateChild(toClone, args);
		}
	}
}
