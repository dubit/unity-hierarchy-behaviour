using Duck.HierarchyBehaviour.Tests.Behaviours;
using NUnit.Framework;
using UnityEngine;

namespace Duck.HierarchyBehaviour.Tests
{
	[TestFixture]
	internal partial class GameObjectExtensionsTests
	{
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
