using UnityEngine;

namespace Duck.HierarchyBehaviour
{
	public static partial class GameObjectExtensions
	{
		/// <summary>
		/// Destroys the given GameObject if it is not null, then creates a child GameObject with the given TComponent component and assigns it to the reference.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toOverwrite">Can be null, if it is not then it will be destroyed.</param>
		/// <typeparam name="TComponent"></typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent OverwriteChild<TComponent>(this GameObject parent, ref Component toOverwrite)
			where TComponent : Component
		{
			var component = toOverwrite ? parent.ReplaceChild<TComponent>(toOverwrite) : parent.CreateChild<TComponent>();
			toOverwrite = component;
			return component;
		}

		/// <summary>
		/// Destroys the given GameObject if it is not null, then creates a child GameObject with the given TComponent component and assigns it to the reference.
		/// TComponent will be initialized with the given arguments.
		/// </summary>
		/// <param name="toOverwrite">Can be null, if it is not then it will be destroyed.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TComponent"></typeparam>
		/// <typeparam name="TArgs">The type of arguments to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent OverwriteChild<TComponent, TArgs>(this GameObject parent, ref Component toOverwrite, TArgs args)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			var component = toOverwrite ? parent.ReplaceChild<TComponent, TArgs>(toOverwrite, args) : parent.CreateChild<TComponent, TArgs>(args);
			toOverwrite = component;
			return component;
		}

		/// <summary>
		/// Destroys the given GameObject if it is not null, then creates a child GameObject from resources and assigns it to the reference.
		/// </summary>
		/// <param name="toOverwrite">Can be null, if it is not then it will be destroyed.</param>
		/// <param name="path">The path to the resourced asset.</param>
		/// <typeparam name="TComponent"></typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent OverwriteChild<TComponent>(this GameObject parent, ref Component toOverwrite, string path)
			where TComponent : Component
		{
			var component = toOverwrite ? parent.ReplaceChild<TComponent>(toOverwrite, path) : parent.CreateChild<TComponent>(path);
			toOverwrite = component;
			return component;
		}

		/// <summary>
		/// Destroys the given GameObject if it is not null, then creates a child GameObject from resources and assigns it to the reference.
		/// TComponent will be initialized with the given arguments.
		/// </summary>
		/// <param name="toOverwrite">Can be null, if it is not then it will be destroyed.</param>
		/// <param name="path">The path to the resourced asset.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TComponent"></typeparam>
		/// <typeparam name="TArgs">The type of arguments to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent OverwriteChild<TComponent, TArgs>(this GameObject parent, ref Component toOverwrite, string path, TArgs args)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			var component = toOverwrite ? parent.ReplaceChild<TComponent, TArgs>(toOverwrite, path, args) : parent.CreateChild<TComponent, TArgs>(path, args);
			toOverwrite = component;
			return component;
		}

		/// <summary>
		/// Destroys the given GameObject if it is not null, then creates a clone of the given TComponent and assigns it to the reference.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toOverwrite">Can be null, if it is not then it will be destroyed.</param>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <typeparam name="TComponent"></typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent OverwriteChild<TComponent>(this GameObject parent, ref Component toOverwrite, TComponent toClone)
			where TComponent : Component
		{
			var component = toOverwrite ? parent.ReplaceChild(toOverwrite, toClone) : parent.CreateChild(toClone);
			toOverwrite = component;
			return component;
		}

		/// <summary>
		/// Destroys the given GameObject if it is not null, then creates a clone of the given TComponent and assigns it to the reference.
		/// IHierarchyBehaviour's will be initialized.
		/// </summary>
		/// <param name="toOverwrite">Can be null, if it is not then it will be destroyed.</param>
		/// <param name="toClone">The GameObject to clone.</param>
		/// <param name="args">The TArgs object to be passed in on initialization.</param>
		/// <typeparam name="TComponent"></typeparam>
		/// <typeparam name="TArgs">The type of arguments to be given on initialization.</typeparam>
		/// <returns>The new TComponent</returns>
		public static TComponent OverwriteChild<TComponent, TArgs>(this GameObject parent, ref Component toOverwrite, TComponent toClone, TArgs args)
			where TComponent : Component, IHierarchyBehaviour<TArgs>
		{
			var component = toOverwrite ? parent.ReplaceChild(toOverwrite, toClone, args) : parent.CreateChild(toClone, args);
			toOverwrite = component;
			return component;
		}
	}
}
