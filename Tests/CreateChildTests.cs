using Duck.HieriarchyBehaviour.Tests.Behaviours;
using NUnit.Framework;
using UnityEngine;

namespace Duck.HieriarchyBehaviour.Tests
{
	[TestFixture]
	internal partial class GameObjectExtensionsTests
	{
		[Test]
		public void Expect_CreateChild_GameObject_AsChild()
		{
			var gameObject = root.gameObject.CreateChild();
			Assert.IsNotNull(gameObject);
			Assert.AreEqual(root.transform, gameObject.transform.parent);
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
		public void Expect_CreateChild_Cloned_GameObject_AsChild()
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
	}
}
