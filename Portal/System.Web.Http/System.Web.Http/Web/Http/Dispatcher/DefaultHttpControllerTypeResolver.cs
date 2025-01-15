using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000083 RID: 131
	public class DefaultHttpControllerTypeResolver : IHttpControllerTypeResolver
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00009C08 File Offset: 0x00007E08
		public DefaultHttpControllerTypeResolver()
			: this(new Predicate<Type>(DefaultHttpControllerTypeResolver.IsControllerType))
		{
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00009C1C File Offset: 0x00007E1C
		public DefaultHttpControllerTypeResolver(Predicate<Type> predicate)
		{
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			this._isControllerTypePredicate = predicate;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00009C4B File Offset: 0x00007E4B
		protected internal virtual Predicate<Type> IsControllerTypePredicate
		{
			get
			{
				return this._isControllerTypePredicate;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00009C53 File Offset: 0x00007E53
		internal static bool IsControllerType(Type t)
		{
			return t != null && t.IsClass && t.IsVisible && !t.IsAbstract && typeof(IHttpController).IsAssignableFrom(t) && DefaultHttpControllerTypeResolver.HasValidControllerName(t);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00009C90 File Offset: 0x00007E90
		public virtual ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
		{
			if (assembliesResolver == null)
			{
				throw Error.ArgumentNull("assembliesResolver");
			}
			List<Type> list = new List<Type>();
			foreach (Assembly assembly in assembliesResolver.GetAssemblies())
			{
				Type[] array = null;
				if (!(assembly == null) && !assembly.IsDynamic)
				{
					try
					{
						array = this._getTypesFunc(assembly);
					}
					catch (ReflectionTypeLoadException ex)
					{
						array = ex.Types;
					}
					catch
					{
						continue;
					}
					if (array != null)
					{
						list.AddRange(array.Where((Type x) => DefaultHttpControllerTypeResolver.TypeIsVisible(x) && this.IsControllerTypePredicate(x)));
					}
				}
			}
			return list;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009D4C File Offset: 0x00007F4C
		internal static Type[] GetTypes(Assembly assembly)
		{
			return assembly.GetTypes();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00009D54 File Offset: 0x00007F54
		internal static bool HasValidControllerName(Type controllerType)
		{
			string controllerSuffix = DefaultHttpControllerSelector.ControllerSuffix;
			return controllerType.Name.Length > controllerSuffix.Length && controllerType.Name.EndsWith(controllerSuffix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009D89 File Offset: 0x00007F89
		internal void SetGetTypesFunc(Func<Assembly, Type[]> getTypesFunc)
		{
			this._getTypesFunc = getTypesFunc;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00009D92 File Offset: 0x00007F92
		private static bool TypeIsVisible(Type type)
		{
			return type != null && type.IsVisible;
		}

		// Token: 0x040000B9 RID: 185
		private readonly Predicate<Type> _isControllerTypePredicate;

		// Token: 0x040000BA RID: 186
		private Func<Assembly, Type[]> _getTypesFunc = new Func<Assembly, Type[]>(DefaultHttpControllerTypeResolver.GetTypes);
	}
}
