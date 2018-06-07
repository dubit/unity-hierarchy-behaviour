namespace DUCK.HieriarchyBehaviour
{
	public interface IHierarchyBehaviour<TArgs>
	{
		void Initialize(TArgs args);
	}

	public interface IHierarchyBehaviour
	{
		void Initialize();
	}
}
