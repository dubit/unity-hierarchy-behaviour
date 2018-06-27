using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DUCK.HieriarchyBehaviour
{
	internal static class Utils
	{
		public static GameObject CloneGameObject(GameObject gameObjectToClone, GameObject parent)
		{
			var gameObject = Object.Instantiate(gameObjectToClone, parent.transform);
			gameObject.name = gameObjectToClone.name;
			gameObject.transform.localPosition = Vector3.zero;
			return gameObject;
		}

		public static TBehaviour CloneBehaviour<TBehaviour>(TBehaviour behaviourToClone, GameObject parent)
			where TBehaviour : MonoBehaviour
		{
			var behaviour = Object.Instantiate(behaviourToClone, parent.transform);
			behaviour.name = behaviourToClone.name;
			behaviour.transform.localPosition = Vector3.zero;
			return behaviour;
		}

		public static TBehaviour CreateGameObjectWithBehaviour<TBehaviour>(GameObject parent)
			where TBehaviour : MonoBehaviour
		{
			var behaviour = new GameObject(typeof(TBehaviour).Name).AddComponent<TBehaviour>();
			behaviour.transform.SetParent(parent.transform);
			behaviour.transform.localPosition = Vector3.zero;
			return behaviour;
		}

		public static TObject InstantiateResource<TObject>(string path, GameObject parent, bool worldPositionStays = true) 
			where TObject : Object
		{
			var loadedBehaviour = Resources.Load<TObject>(path);
			var behaviour = Object.Instantiate(loadedBehaviour, parent.transform, worldPositionStays);
			behaviour.name = loadedBehaviour.name;
			return behaviour;
		}

		public static void DestroyChild(GameObject parent, MonoBehaviour child)
		{
			if (child.transform.parent != parent.transform)
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
