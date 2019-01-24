using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Duck.HieriarchyBehaviour
{
	internal static class Utils
	{
		public static GameObject CreateChildGameObject(GameObject parent, string name)
		{
			var gameObject = new GameObject(name);
			gameObject.transform.SetParent(parent.transform);
			return gameObject;
		}

		public static GameObject CloneGameObject(GameObject gameObjectToClone, GameObject parent)
		{
			var gameObject = Object.Instantiate(gameObjectToClone, parent.transform);
			gameObject.name = gameObjectToClone.name;
			gameObject.transform.localPosition = Vector3.zero;
			return gameObject;
		}

		public static TComponent CloneComponent<TComponent>(TComponent componentToClone, GameObject parent)
			where TComponent : Component
		{
			var component = Object.Instantiate(componentToClone, parent.transform);
			component.name = componentToClone.name;
			component.transform.localPosition = Vector3.zero;
			return component;
		}

		public static TComponent CreateGameObjectWithComponent<TComponent>(GameObject parent)
			where TComponent : Component
		{
			var component = new GameObject(typeof(TComponent).Name).AddComponent<TComponent>();
			component.transform.SetParent(parent.transform);
			component.transform.localPosition = Vector3.zero;
			return component;
		}

		public static TObject InstantiateResource<TObject>(string path, GameObject parent, bool worldPositionStays = true)
			where TObject : Object
		{
			var loadedBehaviour = Resources.Load<TObject>(path);
			var behaviour = Object.Instantiate(loadedBehaviour, parent.transform, worldPositionStays);
			behaviour.name = loadedBehaviour.name;
			return behaviour;
		}

		public static void DestroyChild(GameObject parent, Component child)
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
