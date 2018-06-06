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

		private interface ITestInitialize
		{
			bool DidInitialize { get; }
		}

		private interface ITestArgs<out TArgs>
		{
			TArgs Args { get; }
		}

		private interface ITestHierarchyBehaviour : IHierarchyBehaviour, ITestInitialize
		{
		}

		private interface ITestHierarchyBehaviour<TArgs> : IHierarchyBehaviour<TArgs>, ITestInitialize, ITestArgs<TArgs>
		{
		}

		private class HierarchyBehaviourWithArgs : MonoBehaviour, ITestHierarchyBehaviour<string>
		{
			public string Args { get; private set; }
			public bool DidInitialize { get; private set; }

			public void Initialize(string args)
			{
				DidInitialize = true;
				Args = args;
			}
		}

		private class HierarchyBehaviour : MonoBehaviour, ITestHierarchyBehaviour
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
		public void Expect_CreateChild()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviour>();
			TestBehaviourWithoutArgs(behaviour);
		}

		[Test]
		public void Expect_CreateChildWithArgs()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviourWithArgs, string>(TEST_ARGS);
			TestBehaviourWithArgs(behaviour, TEST_ARGS);
		}

		[Test]
		public void Expect_CreateChildFromResources()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			TestBehaviourWithoutArgs(behaviour);
		}

		[Test]
		public void Expect_CreateChildFromResourcesWithArgs()
		{
			var behaviour = root.gameObject.CreateChild<HierarchyBehaviourWithArgs, string>(PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			TestBehaviourWithArgs(behaviour, TEST_ARGS);
		}

		[Test]
		public void Expect_CreateChildFromLoaded()
		{
			var loadedBehaviour = Resources.Load<HierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour);
			TestBehaviourWithoutArgs(behaviour);
		}

		[Test]
		public void Expect_CreateChildFromLoadedWithArgs()
		{
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.CreateChild(loadedBehaviour, TEST_ARGS);
			TestBehaviourWithArgs(behaviour, TEST_ARGS);
		}

		[Test]
		public void Expect_ReplaceChild()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviour>(toReplace);
			TestBehaviourReplacedWithoutArgs(toReplace, behaviour);
		}

		[Test]
		public void Expect_ReplaceChildWithArgs()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviourWithArgs, string>(TEST_ARGS);
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, TEST_ARGS);
			TestBehaviourReplacedWithArgs(toReplace, behaviour, TEST_ARGS);
		}

		[Test]
		public void Expect_ReplaceChildFromResources()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviour>(toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			TestBehaviourReplacedWithoutArgs(toReplace, behaviour);
		}

		[Test]
		public void Expect_ReplaceChildFromResourcesWithArgs()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var behaviour = root.gameObject.ReplaceChild<HierarchyBehaviourWithArgs, string>(toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			TestBehaviourReplacedWithArgs(toReplace, behaviour, TEST_ARGS);
		}

		[Test]
		public void Expect_ReplaceChildFromLoaded()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<HierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour);
			TestBehaviourReplacedWithoutArgs(toReplace, behaviour);
		}

		[Test]
		public void Expect_ReplaceChildFromLoadedWithArgs()
		{
			var toReplace = root.gameObject.CreateChild<HierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<HierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.ReplaceChild(toReplace, loadedBehaviour, TEST_ARGS);
			TestBehaviourReplacedWithArgs(toReplace, behaviour, TEST_ARGS);
		}

		private void TestBehaviourReplacedWithoutArgs<TBehaviour>(MonoBehaviour toDestroy, TBehaviour behaviour)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour, ITestInitialize
		{
			Assert.IsTrue(toDestroy == null);
			TestBehaviourWithoutArgs(behaviour);
		}

		private void TestBehaviourReplacedWithArgs<TBehaviour, TArgs>(MonoBehaviour toDestroy, TBehaviour behaviour, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>, ITestInitialize, ITestArgs<TArgs>
		{
			Assert.IsTrue(toDestroy == null);
			TestBehaviourWithArgs(behaviour, args);
		}

		private void TestBehaviourWithoutArgs<TBehaviour>(TBehaviour behaviour)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour, ITestInitialize
		{
			IsInitialized(behaviour);
			IsChildOf(behaviour.transform, root.transform);
		}

		private void TestBehaviourWithArgs<TBehaviour, TArgs>(TBehaviour behaviour, TArgs args)
			where TBehaviour : MonoBehaviour, IHierarchyBehaviour<TArgs>, ITestInitialize, ITestArgs<TArgs>
		{
			IsInitialized(behaviour);
			IsChildOf(behaviour.transform, root.transform);
			IsInitializedWithArgs(behaviour, args);
		}

		private static void IsChildOf(Transform child, Transform parent)
		{
			Assert.True(child.parent == parent);
		}

		private static void IsInitialized<TBehaviour>(TBehaviour behaviour)
			where TBehaviour : ITestInitialize
		{
			Assert.IsTrue(behaviour.DidInitialize);
		}

		private static void IsInitializedWithArgs<TBehaviour, TArgs>(TBehaviour behaviour, TArgs args)
			where TBehaviour : ITestArgs<TArgs>
		{
			Assert.AreEqual(behaviour.Args, args);
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
