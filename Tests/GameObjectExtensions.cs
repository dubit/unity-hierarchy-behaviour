using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace DUCK.HieriarchyBehaviour
{
	[TestFixture]
	internal class GameObjectExtensions
	{
		private const string TEST_ARGS = "test-args";
		private const string PREFAB_WITHOUT_ARGS_RESOURCE_PATH = "PrefabWithoutArgs";
		private const string PREFAB_WITH_ARGS_RESOURCE_PATH = "PrefabWithArgs";
		private const string RESOURCE_PATH = "/Resources/";

		private class HierarchyBehaviourWithArgs : MonoBehaviour, IHierarchyBehaviour<string>
		{
			public string Args { get; private set; }
			public bool DidInitialize { get; private set; }

			public void Initialize(string args)
			{
				DidInitialize = true;
				Args = args;
			}
		}

		private class HierarchyBehaviour : MonoBehaviour, IHierarchyBehaviour
		{
			public bool DidInitialize { get; private set; }

			public void Initialize()
			{
				DidInitialize = true;
			}
		}

		private HierarchyBehaviour root;
		private HierarchyBehaviour prefabBehaviour;
		private HierarchyBehaviourWithArgs prefabBehaviourWithArgs;
		private bool didResourcesExist;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			root = new GameObject("UnitTest (HierarchyBehaviour)").AddComponent<HierarchyBehaviour>();

			didResourcesExist = Directory.Exists(Application.dataPath + RESOURCE_PATH);
			if (!didResourcesExist)
			{
				Directory.CreateDirectory(Application.dataPath + RESOURCE_PATH);
			}

			prefabBehaviour = new GameObject(PREFAB_WITHOUT_ARGS_RESOURCE_PATH).AddComponent<HierarchyBehaviour>();
			PrefabUtility.CreatePrefab("Assets/Resources/" + PREFAB_WITHOUT_ARGS_RESOURCE_PATH + ".prefab", prefabBehaviour.gameObject);
			Object.DestroyImmediate(prefabBehaviour.gameObject);

			prefabBehaviourWithArgs = new GameObject(PREFAB_WITH_ARGS_RESOURCE_PATH).AddComponent<HierarchyBehaviourWithArgs>();
			PrefabUtility.CreatePrefab("Assets/Resources/" + PREFAB_WITH_ARGS_RESOURCE_PATH + ".prefab", prefabBehaviourWithArgs.gameObject);
			Object.DestroyImmediate(prefabBehaviourWithArgs.gameObject);

			AssetDatabase.Refresh();
		}

		[Test]
		public void Expect_CreateChild_ToInitialize()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviour>();
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_AsChild()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviour>();
			Assert.IsTrue(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_CreateChild_WithArgs_ToInitialize()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviourWithArgs, string>(TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_WithArgs_AsChild()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviourWithArgs, string>(TEST_ARGS);
			Assert.IsTrue(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_CreateChild_WithArgs_ToInitialize_WithArgs()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviourWithArgs, string>(TEST_ARGS);
			Assert.AreEqual(behaviour.Args, TEST_ARGS);
		}

		[Test]
		public void Expect_CreateChild_FromResources_ToInitialize()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_FromResources_AsChild()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.IsTrue(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_CreateChild_FromResources_WithArgs_ToInitialize()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviourWithArgs, string>(PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_FromResources_WithArgs_AsChild()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviourWithArgs, string>(PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.IsTrue(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_CreateChild_FromResources_WithArgs_ToInitialize_WithArgs()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviourWithArgs, string>(PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.AreEqual(behaviour.Args, TEST_ARGS);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_ToInitialize()
		{
			var loadedBehaviour = Resources.Load<HierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_AsChild()
		{
			var loadedBehaviour = Resources.Load<HierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour);
			Assert.IsTrue(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_WithArgs_ToInitialize()
		{
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_WithArgs_AsChild()
		{
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_WithArgs_ToInitialize_WithArgs()
		{
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour, TEST_ARGS);
			Assert.AreEqual(behaviour.Args, TEST_ARGS);
		}

		[Test]
		public void Expect_ReplaceChild_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			root.gameObject.ReplaceChild<HierarchyBehaviour>(toReplace);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.True(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_ReplaceChild_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviour>(toReplace);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_WithArgs_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_WithArgs_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_WithArgs_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.True(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_ReplaceChild_WithArgs_ToInitialize_WithArgs()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.AreEqual(behaviour.Args, TEST_ARGS);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			root.gameObject.ReplaceChild<HierarchyBehaviour>(toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviour>(toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.True(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviour>(toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_WithArgs_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_WithArgs_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_WithArgs_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.True(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_WithArgs_ToInitialize_WithArgs()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.AreEqual(behaviour.Args, TEST_ARGS);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<HierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			root.gameObject.ReplaceChild(toReplace, loadedBehaviour);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<HierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour);
			Assert.True(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_WithArgs_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_WithArgs_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_WithArgs_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.True(behaviour.transform.parent == root.transform);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_WithArgs_ToInitialize_WithArgs()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.AreEqual(behaviour.Args, TEST_ARGS);
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			Object.DestroyImmediate(root.gameObject);

			var prefabWithoutArgsPath = Application.dataPath + RESOURCE_PATH + PREFAB_WITHOUT_ARGS_RESOURCE_PATH + ".prefab";
			File.Delete(prefabWithoutArgsPath);
			File.Delete(prefabWithoutArgsPath + ".prefab.meta");
			var prefabWithArgsPath = Application.dataPath + RESOURCE_PATH + PREFAB_WITH_ARGS_RESOURCE_PATH + ".prefab";
			File.Delete(prefabWithArgsPath);
			File.Delete(prefabWithArgsPath + ".prefab.meta");
			AssetDatabase.Refresh();

			if (!didResourcesExist)
			{
				Directory.Delete(Application.dataPath + RESOURCE_PATH);
				File.Delete(Application.dataPath + "Resources.meta");
			}

			AssetDatabase.Refresh();
		}
	}
}
