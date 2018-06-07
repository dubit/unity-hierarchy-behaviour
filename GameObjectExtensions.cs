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

		public static TBehaviour CreateChild<TBehaviour>(this GameObject parent, TBehaviour toClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			var behaviour = Utils.CloneBehaviour(toClone, parent);
			behaviour.Initialize();
			return behaviour;
		}

		public static TBehaviour CreateChild<TBehaviour, TArgs>(this GameObject parent, TBehaviour toClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			var behaviour = Utils.CloneBehaviour(toClone, parent);
			behaviour.Initialize(args);
			return behaviour;
		}

		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TBehaviour>();
		}

		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TBehaviour, TArgs>(args);
		}

		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy, string path, bool worldPositionStays = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TBehaviour>(path, worldPositionStays);
		}

		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, string path, TArgs args, bool worldPositionStays = true)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild<TBehaviour, TArgs>(path, args, worldPositionStays);
		}

		public static TBehaviour ReplaceChild<TBehaviour>(this GameObject parent, MonoBehaviour toDestroy, TBehaviour toClone)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild(toClone);
		}

		public static TBehaviour ReplaceChild<TBehaviour, TArgs>(this GameObject parent, MonoBehaviour toDestroy, TBehaviour toClone, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>
		{
			Utils.DestroyChild(parent, toDestroy);
			return parent.CreateChild(toClone, args);
		}
	}
}
