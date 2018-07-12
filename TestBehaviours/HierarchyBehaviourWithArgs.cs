using UnityEngine;

namespace DUCK.HieriarchyBehaviour.TestBehaviours
{
	[AddComponentMenu("")]
	public class HierarchyBehaviourWithArgs : MonoBehaviour, IHierarchyBehaviour<string>
	{
		public string Args { get; private set; }
		public bool DidInitialize { get; private set; }

		public void Initialize(string args)
		{
			DidInitialize = true;
			Args = args;
		}
	}
}
