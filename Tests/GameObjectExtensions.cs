using System.IO;
using Duck.HieriarchyBehaviour.Tests.Behaviours;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Duck.HieriarchyBehaviour.Tests
{
	[TestFixture]
	internal class GameObjectExtensions
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
			root = new GameObject("UnitTest (HierarchyBehaviour)").AddComponent<TestHierarchyBehaviour>();

			didResourcesExist = Directory.Exists(Application.dataPath + RESOURCE_PATH);
			if (!didResourcesExist)
			{
				Directory.CreateDirectory(Application.dataPath + RESOURCE_PATH);
			}

			prefabBehaviour = new GameObject(PREFAB_WITHOUT_ARGS_RESOURCE_PATH).AddComponent<TestHierarchyBehaviour>();
			PrefabUtility.CreatePrefab("Assets/Resources/" + PREFAB_WITHOUT_ARGS_RESOURCE_PATH + ".prefab", prefabBehaviour.gameObject);
			Object.DestroyImmediate(prefabBehaviour.gameObject);

			prefabBehaviourWithArgs = new GameObject(PREFAB_WITH_ARGS_RESOURCE_PATH).AddComponent<TestHierarchyBehaviourWithArgs>();
			PrefabUtility.CreatePrefab("Assets/Resources/" + PREFAB_WITH_ARGS_RESOURCE_PATH + ".prefab", prefabBehaviourWithArgs.gameObject);
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

		[Test]
		public void Expect_CreateChild_GameObject_With_Name_AsChild()
		{
			const string GAME_OBJECT_NAME = "Test Name";
			var gameObject = root.gameObject.CreateChild(GAME_OBJECT_NAME);
			Assert.IsNotNull(gameObject);
			Assert.AreEqual(GAME_OBJECT_NAME, gameObject.name);
			Assert.AreEqual(root.transform, gameObject.transform.parent);
		}

		[Test]
		public void Expect_CreateChild_GameObject_AsChild()
		{
			var toClone = new GameObject("GameObject To Clone");
			toClone.transform.SetParent(root.transform);
			var gameObject = root.gameObject.CreateChild(toClone);
			Assert.AreEqual(root.transform, gameObject.transform.parent);
		}

		[Test]
		public void Expect_CreateChild_GameObject_FromResources_AsChild()
		{
			var gameObject = root.gameObject.CreateChild(path: PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.AreEqual(root.transform, gameObject.transform.parent);
		}

		[Test]
		public void Expect_CreateChild_New_ToInitialize()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_New_AsChild()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_CreateChild_New_WithArgs_ToInitialize()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviourWithArgs, string>(TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_New_WithArgs_AsChild()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviourWithArgs, string>(TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_CreateChild_New_WithArgs_ToInitialize_WithArgs()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviourWithArgs, string>(TEST_ARGS);
			Assert.AreEqual(TEST_ARGS, behaviour.Args);
		}

		[Test]
		public void Expect_CreateChild_FromResources_ToInitialize()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_FromResources_AsChild()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_CreateChild_FromResources_WithArgs_ToInitialize()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviourWithArgs, string>(PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_FromResources_WithArgs_AsChild()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviourWithArgs, string>(PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_CreateChild_FromResources_WithArgs_ToInitialize_WithArgs()
		{
			var behaviour = root.gameObject.CreateChild<TestHierarchyBehaviourWithArgs, string>(PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.AreEqual(TEST_ARGS, behaviour.Args);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_ToInitialize()
		{
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_AsChild()
		{
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_WithArgs_ToInitialize()
		{
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_WithArgs_AsChild()
		{
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_CreateChild_FromLoaded_WithArgs_ToInitialize_WithArgs()
		{
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour, TEST_ARGS);
			Assert.AreEqual(TEST_ARGS, behaviour.Args);
		}

		[Test]
		public void Expect_ReplaceChild_New_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			root.gameObject.ReplaceChild<TestHierarchyBehaviour>(toReplace);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_New_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_ReplaceChild_New_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviour>(toReplace);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_New_WithArgs_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_New_WithArgs_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			root.gameObject.ReplaceChild<TestHierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_New_WithArgs_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_ReplaceChild_New_WithArgs_ToInitialize_WithArgs()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			Assert.AreEqual(TEST_ARGS, behaviour.Args);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			root.gameObject.ReplaceChild<TestHierarchyBehaviour>(toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviour>(toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviour>(toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_WithArgs_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviourWithArgs, string>(toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_WithArgs_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			root.gameObject.ReplaceChild<TestHierarchyBehaviourWithArgs, string>(toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_WithArgs_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviourWithArgs, string>(toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_ReplaceChild_FromResources_WithArgs_ToInitialize_WithArgs()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<TestHierarchyBehaviourWithArgs, string>(toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.AreEqual(TEST_ARGS, behaviour.Args);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			root.gameObject.ReplaceChild(toReplace, loadedBehaviour);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_WithArgs_ToInitialize()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_WithArgs_ToDestroy()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(toReplace == null);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_WithArgs_ToCreateAsChild()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_ReplaceChild_FromLoaded_WithArgs_ToInitialize_WithArgs()
		{
			var toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			Assert.AreEqual(TEST_ARGS, behaviour.Args);
		}
	}
}
