using UnityEngine;

namespace DUCK.HieriarchyBehaviour
{
	public static class GameObjectExtensions
	{
		public static TBehaviour CreateChild<TBehaviour>(this GameObject gameObject)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(gameObject.transform);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject gameObject, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(gameObject.transform);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour CreateChildFromResources<TBehaviour>(this GameObject gameObject, string path, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, gameObject.transform, worldPositionStay);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChildFromResources<TBehaviour, TArgs>(this GameObject gameObject, string path, TArgs args, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, gameObject.transform, worldPositionStay);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour CreateChildFromLoaded<TBehaviour>(this GameObject gameObject, TBehaviour behaviourToClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CloneBehaviour(behaviourToClone, gameObject.transform);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChildFromLoaded<TBehaviour, TArgs>(this GameObject gameObject, TBehaviour behaviourToClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CloneBehaviour(behaviourToClone, gameObject.transform);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour Replace<TBehaviour>(this GameObject gameObject, MonoBehaviour toDestroy)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			var behaviour = gameObject.CreateChild<TBehaviour>();
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour Replace<TBehaviour, TArgs>(this GameObject gameObject, MonoBehaviour toDestroy, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			var behaviour = gameObject.CreateChild<TBehaviour, TArgs>(args);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour Replace<TBehaviour>(this GameObject gameObject, MonoBehaviour toDestroy, string path)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			var behaviour = gameObject.CreateChildFromResources<TBehaviour>(path);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour Replace<TBehaviour, TArgs>(this GameObject gameObject, MonoBehaviour toDestroy, string path, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			var behaviour = gameObject.CreateChildFromResources<TBehaviour, TArgs>(path, args);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour Replace<TBehaviour>(this GameObject gameObject, MonoBehaviour toDestroy, TBehaviour toClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Object.Destroy(toDestroy.gameObject);
			var behaviour = Utils.CloneBehaviour(toClone, gameObject.transform);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour Replace<TBehaviour, TArgs>(this GameObject gameObject, MonoBehaviour toDestroy, TBehaviour toClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Object.Destroy(toDestroy.gameObject);
			var behaviour = Utils.CloneBehaviour(toClone, gameObject.transform);
			behaviour.Initialize(args);
			return behaviour;
		}
	}
}
