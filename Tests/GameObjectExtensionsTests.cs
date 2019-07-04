using System.IO;
using Duck.HierarchyBehaviour.Tests.Behaviours;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Duck.HierarchyBehaviour.Tests
{
	[TestFixture]
	internal partial class GameObjectExtensionsTests
	{
		private const string TEST_ARGS = "test-args";
		private const string PREFAB_WITHOUT_ARGS_RESOURCE_PATH = "PrefabWithoutArgs";
		private const string PREFAB_WITH_ARGS_RESOURCE_PATH = "PrefabWithArgs";
		private const string RESOURCE_PATH = "/Resources/";

		private TestHierarchyBehaviour root;
		private TestHierarchyBehaviour prefabBehaviour;
		private TestHierarchyBehaviourWithArgs prefabBehaviourWithArgs;
		private bool didResourcesExist;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			root = new GameObject(nameof(TestHierarchyBehaviour)).AddComponent<TestHierarchyBehaviour>();

			didResourcesExist = Directory.Exists(Application.dataPath + RESOURCE_PATH);
			if (!didResourcesExist)
			{
				Directory.CreateDirectory(Application.dataPath + RESOURCE_PATH);
			}

			prefabBehaviour = new GameObject(PREFAB_WITHOUT_ARGS_RESOURCE_PATH).AddComponent<TestHierarchyBehaviour>();
			PrefabUtility.SaveAsPrefabAsset(prefabBehaviour.gameObject, $"Assets/Resources/{PREFAB_WITHOUT_ARGS_RESOURCE_PATH}.prefab");
			Object.DestroyImmediate(prefabBehaviour.gameObject);

			prefabBehaviourWithArgs = new GameObject(PREFAB_WITH_ARGS_RESOURCE_PATH).AddComponent<TestHierarchyBehaviourWithArgs>();
			PrefabUtility.SaveAsPrefabAsset(prefabBehaviourWithArgs.gameObject, $"Assets/Resources/{PREFAB_WITH_ARGS_RESOURCE_PATH}.prefab");
			Object.DestroyImmediate(prefabBehaviourWithArgs.gameObject);

			AssetDatabase.Refresh();
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			Object.DestroyImmediate(root.gameObject);

			var prefabWithoutArgsPath = Application.dataPath + RESOURCE_PATH + PREFAB_WITHOUT_ARGS_RESOURCE_PATH + ".prefab";
			File.Delete(prefabWithoutArgsPath);
			File.Delete(prefabWithoutArgsPath + ".meta");
			var prefabWithArgsPath = Application.dataPath + RESOURCE_PATH + PREFAB_WITH_ARGS_RESOURCE_PATH + ".prefab";
			File.Delete(prefabWithArgsPath);
			File.Delete(prefabWithArgsPath + ".meta");
			AssetDatabase.Refresh();

			if (!didResourcesExist)
			{
				Directory.Delete(Application.dataPath + RESOURCE_PATH);
				File.Delete(Application.dataPath + "/Resources.meta");
			}

			AssetDatabase.Refresh();
		}
	}
}
