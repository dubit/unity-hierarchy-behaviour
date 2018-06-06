using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DUCK.HieriarchyBehaviour
{
	internal static class Utils
	{
		internal static TBehaviour CloneBehaviour<TBehaviour>(TBehaviour behaviourToClone, GameObject parent)
			where TBehaviour : MonoBehaviour
		{
			var behaviour = Object.Instantiate(behaviourToClone, parent.transform);
			behaviour.name = behaviourToClone.name;
			behaviour.transform.localPosition = Vector3.zero;
			return behaviour;
		}

		internal static TBehaviour CreateGameObjectWithBehaviour<TBehaviour>(GameObject parent)
			where TBehaviour : MonoBehaviour
		{
			var behaviour = new GameObject(typeof(TBehaviour).Name).AddComponent<TBehaviour>();
			behaviour.transform.SetParent(parent.transform);
			behaviour.transform.localPosition = Vector3.zero;
			return behaviour;
		}

		internal static TBehaviour InstantiateResource<TBehaviour>(string path, GameObject parent, bool worldPositionStay = true)
			where TBehaviour : MonoBehaviour
		{
			var loadedBehaviour = Resources.Load<TBehaviour>(path);
			var behaviour = Object.Instantiate(loadedBehaviour, parent.transform, worldPositionStay);
			behaviour.name = loadedBehaviour.name;
			return behaviour;
		}

		internal static void DestroyChild(GameObject parent, MonoBehaviour child)
		{
			if (child.transform.parent != parent)
			{
				throw new ArgumentException(string.Format("{0} is not a child transform of {1}", child.name, parent.name));
			}

			if (Application.isEditor)
			{
				Object.DestroyImmediate(child.gameObject);
			}
			else
			{
				Object.Destroy(child.gameObject);
			}
		}
	}
}
