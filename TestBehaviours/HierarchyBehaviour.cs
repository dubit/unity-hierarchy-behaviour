using UnityEngine;

namespace DUCK.HieriarchyBehaviour.TestBehaviours
{
	[AddComponentMenu("")]
	public class HierarchyBehaviour : MonoBehaviour, IHierarchyBehaviour
	{
		public bool DidInitialize { get; private set; }

		public void Initialize()
		{
			DidInitialize = true;
		}
	}
}
