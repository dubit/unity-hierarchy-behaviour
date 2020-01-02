using UnityEngine;

namespace Duck.HierarchyBehaviour.Tests.Behaviours
{
	[AddComponentMenu("")]
	public class TestHierarchyBehaviour : MonoBehaviour, IHierarchyBehaviour
	{
		public bool DidInitialize { get; private set; }

		public void Initialize()
		{
			DidInitialize = true;
		}
	}
}
