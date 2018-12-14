using UnityEngine;

namespace Duck.HieriarchyBehaviour.Tests.Behaviours
{
	[AddComponentMenu("")]
	public class TestHierarchyBehaviourWithArgs : MonoBehaviour, IHierarchyBehaviour<string>
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
