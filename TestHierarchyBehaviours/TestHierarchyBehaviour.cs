using UnityEngine;

namespace DUCK.HieriarchyBehaviour.TestHierarchyBehaviours
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
