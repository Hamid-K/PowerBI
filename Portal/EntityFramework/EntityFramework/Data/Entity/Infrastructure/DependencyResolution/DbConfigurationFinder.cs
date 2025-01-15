using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002AC RID: 684
	internal class DbConfigurationFinder
	{
		// Token: 0x060021A4 RID: 8612 RVA: 0x0005E616 File Offset: 0x0005C816
		public virtual Type TryFindConfigurationType(Type contextType, IEnumerable<Type> typesToSearch = null)
		{
			return this.TryFindConfigurationType(contextType.Assembly(), contextType, typesToSearch);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0005E628 File Offset: 0x0005C828
		public virtual Type TryFindConfigurationType(Assembly assemblyHint, Type contextTypeHint, IEnumerable<Type> typesToSearch = null)
		{
			if (contextTypeHint != null)
			{
				Type type = (from a in contextTypeHint.GetCustomAttributes(true)
					select a.ConfigurationType).FirstOrDefault<Type>();
				if (type != null)
				{
					if (!typeof(DbConfiguration).IsAssignableFrom(type))
					{
						throw new InvalidOperationException(Strings.CreateInstance_BadDbConfigurationType(type.ToString(), typeof(DbConfiguration).ToString()));
					}
					return type;
				}
			}
			List<Type> list = (typesToSearch ?? assemblyHint.GetAccessibleTypes()).Where((Type t) => t.IsSubclassOf(typeof(DbConfiguration)) && !t.IsAbstract() && !t.IsGenericType()).ToList<Type>();
			if (list.Count > 1)
			{
				throw new InvalidOperationException(Strings.MultipleConfigsInAssembly(list.First<Type>().Assembly(), typeof(DbConfiguration).Name));
			}
			return list.FirstOrDefault<Type>();
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0005E718 File Offset: 0x0005C918
		public virtual Type TryFindContextType(Assembly assemblyHint, Type contextTypeHint, IEnumerable<Type> typesToSearch = null)
		{
			if (contextTypeHint != null)
			{
				return contextTypeHint;
			}
			List<Type> list = (typesToSearch ?? assemblyHint.GetAccessibleTypes()).Where((Type t) => t.IsSubclassOf(typeof(DbContext)) && !t.IsAbstract() && !t.IsGenericType() && t.GetCustomAttributes(true).Any<DbConfigurationTypeAttribute>()).ToList<Type>();
			if (list.Count != 1)
			{
				return null;
			}
			return list[0];
		}
	}
}
