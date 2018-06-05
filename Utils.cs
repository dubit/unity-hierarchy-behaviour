using UnityEngine;

namespace DUCK.HieriarchyBehaviour
{
	internal static class Utils
	{
		internal static TBehaviour CloneBehaviour<TBehaviour>(TBehaviour behaviourToClone, Transform parent)
			where TBehaviour : MonoBehaviour
		{
			var behaviour = Object.Instantiate(behaviourToClone, parent);
			behaviour.name = behaviourToClone.name;
			behaviour.transform.localPosition = Vector3.zero;
			return behaviour;
		}

		internal static TBehaviour CreateGameObjectWithBehaviour<TBehaviour>(Transform parent)
			where TBehaviour : MonoBehaviour
		{
			var behaviour = new GameObject(typeof(TBehaviour).Name).AddComponent<TBehaviour>();
			behaviour.transform.SetParent(parent.transform);
			behaviour.transform.localPosition = Vector3.zero;
			return behaviour;
		}

		internal static TBehaviour InstantiateResource<TBehaviour>(string path, Transform parent, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour
		{
			var loadedBehaviour = Resources.Load<TBehaviour>(path);
			var behaviour = Object.Instantiate(loadedBehaviour, parent, worldPositionStay);
			behaviour.name = loadedBehaviour.name;
			return behaviour;
		}
	}
}
