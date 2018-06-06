using UnityEngine;

namespace DUCK.HieriarchyBehaviour
{
	public static class GameObjectExtensions
	{
		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(parent);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject parent, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CreateGameObjectWithBehaviour<TBehaviour>(parent);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent, string path, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, parent, worldPositionStay);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject parent, string path, TArgs args, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.InstantiateResource<TBehaviour>(path, parent, worldPositionStay);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent, TBehaviour behaviourToClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CloneBehaviour(behaviourToClone, parent);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject parent, TBehaviour behaviourToClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CloneBehaviour(behaviourToClone, parent);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Utils.Destroy(toDestroy.gameObject);
			return parent.CreateChild<TBehaviour>();
		}

		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Utils.Destroy(toDestroy.gameObject);
			return parent.CreateChild<TBehaviour, TArgs>(args);
		}

		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy, string path)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Utils.Destroy(toDestroy.gameObject);
			return parent.CreateChild<TBehaviour>(path);
		}

		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, string path, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Utils.Destroy(toDestroy.gameObject);
			return parent.CreateChild<TBehaviour, TArgs>(path, args);
		}

		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy, TBehaviour toClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Utils.Destroy(toDestroy.gameObject);
			return parent.CreateChild(toClone);
		}

		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, TBehaviour toClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Utils.Destroy(toDestroy.gameObject);
			return parent.CreateChild(toClone, args);
		}
	}
}
