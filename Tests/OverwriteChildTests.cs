using Duck.HieriarchyBehaviour.Tests.Behaviours;
using NUnit.Framework;
using UnityEngine;

namespace Duck.HieriarchyBehaviour.Tests
{
	[TestFixture]
	internal partial class GameObjectExtensionsTests
	{
		[Test]
		public void Expect_OverwriteChild_New_ToOverwrite()
		{
			Component original;
			var toReplace = original = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviour>(ref toReplace);
			Assert.IsTrue(toReplace != original);
			Assert.IsTrue(toReplace == behaviour);
			Assert.IsTrue(original == null);
		}

		[Test]
		public void Expect_OverwriteChild_New_ToCreateAsChild()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviourWithArgs, string>(ref toReplace, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_OverwriteChild_New_ToInitialize()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviour>(ref toReplace);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_OverwriteChild_New_WithArgs_ToInitialize()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviourWithArgs, string>(ref toReplace, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_OverwriteChild_New_WithArgs_ToOverwrite()
		{
			Component original;
			var toReplace = original = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviourWithArgs, string>(ref toReplace, TEST_ARGS);
			Assert.IsTrue(toReplace != original);
			Assert.IsTrue(toReplace == behaviour);
			Assert.IsTrue(original == null);
		}

		[Test]
		public void Expect_OverwriteChild_New_WithArgs_ToCreateAsChild()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviourWithArgs, string>(ref toReplace, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_OverwriteChild_New_WithArgs_ToInitialize_WithArgs()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviourWithArgs, string>(ref toReplace, TEST_ARGS);
			Assert.AreEqual(TEST_ARGS, behaviour.Args);
		}

		[Test]
		public void Expect_OverwriteChild_FromResources_ToDestroy()
		{
			Component original;
			var toReplace = original = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviour>(ref toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.IsTrue(toReplace != original);
			Assert.IsTrue(toReplace == behaviour);
			Assert.IsTrue(original == null);
		}

		[Test]
		public void Expect_OverwriteChild_FromResources_ToCreateAsChild()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviour>(ref toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_OverwriteChild_FromResources_ToInitialize()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviour>(ref toReplace, PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_OverwriteChild_FromResources_WithArgs_ToInitialize()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviourWithArgs, string>(ref toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_OverwriteChild_FromResources_WithArgs_ToOverwrite()
		{
			Component original;
			var toReplace = original = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviourWithArgs, string>(ref toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.IsTrue(toReplace != original);
			Assert.IsTrue(toReplace == behaviour);
			Assert.IsTrue(original == null);
		}

		[Test]
		public void Expect_OverwriteChild_FromResources_WithArgs_ToCreateAsChild()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviourWithArgs, string>(ref toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_OverwriteChild_FromResources_WithArgs_ToInitialize_WithArgs()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var behaviour = root.gameObject.OverwriteChild<TestHierarchyBehaviourWithArgs, string>(ref toReplace, PREFAB_WITH_ARGS_RESOURCE_PATH, TEST_ARGS);
			Assert.AreEqual(TEST_ARGS, behaviour.Args);
		}

		[Test]
		public void Expect_OverwriteChild_FromLoaded_ToOverwrite()
		{
			Component original;
			var toReplace = original = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.OverwriteChild(ref toReplace, loadedBehaviour);
			Assert.IsTrue(toReplace != original);
			Assert.IsTrue(toReplace == behaviour);
			Assert.IsTrue(original == null);
		}

		[Test]
		public void Expect_OverwriteChild_FromLoaded_ToCreateAsChild()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviour>(PREFAB_WITHOUT_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.OverwriteChild(ref toReplace, loadedBehaviour);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_OverwriteChild_FromLoaded_ToInitialize()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.OverwriteChild(ref toReplace, loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_OverwriteChild_FromLoaded_WithArgs_ToInitialize()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.OverwriteChild(ref toReplace, loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(behaviour.DidInitialize);
		}

		[Test]
		public void Expect_OverwriteChild_FromLoaded_WithArgs_ToOverwrite()
		{
			Component original;
			var toReplace = original = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.OverwriteChild(ref toReplace, loadedBehaviour, TEST_ARGS);
			Assert.IsTrue(toReplace != original);
			Assert.IsTrue(toReplace == behaviour);
			Assert.IsTrue(original == null);
		}

		[Test]
		public void Expect_OverwriteChild_FromLoaded_WithArgs_ToCreateAsChild()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.OverwriteChild(ref toReplace, loadedBehaviour, TEST_ARGS);
			Assert.AreEqual(root.transform, behaviour.transform.parent);
		}

		[Test]
		public void Expect_OverwriteChild_FromLoaded_WithArgs_ToInitialize_WithArgs()
		{
			Component toReplace = root.gameObject.CreateChild<TestHierarchyBehaviour>();
			var loadedBehaviour = Resources.Load<TestHierarchyBehaviourWithArgs>(PREFAB_WITH_ARGS_RESOURCE_PATH);
			var behaviour = root.gameObject.OverwriteChild(ref toReplace, loadedBehaviour, TEST_ARGS);
			Assert.AreEqual(TEST_ARGS, behaviour.Args);
		}
	}
}
