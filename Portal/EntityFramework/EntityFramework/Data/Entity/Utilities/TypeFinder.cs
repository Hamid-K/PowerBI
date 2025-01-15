using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000083 RID: 131
	internal class TypeFinder
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x000101F1 File Offset: 0x0000E3F1
		public TypeFinder(Assembly assembly)
		{
			this._assembly = assembly;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00010200 File Offset: 0x0000E400
		public Type FindType(Type baseType, string typeName, Func<IEnumerable<Type>, IEnumerable<Type>> filter, Func<string, Exception> noType = null, Func<string, IEnumerable<Type>, Exception> multipleTypes = null, Func<string, string, Exception> noTypeWithName = null, Func<string, string, Exception> multipleTypesWithName = null)
		{
			bool flag = !string.IsNullOrWhiteSpace(typeName);
			Type type = null;
			if (flag)
			{
				type = this._assembly.GetType(typeName);
			}
			if (type == null)
			{
				string name = this._assembly.GetName().Name;
				IEnumerable<Type> enumerable = from t in this._assembly.GetAccessibleTypes()
					where baseType.IsAssignableFrom(t)
					select t;
				if (flag)
				{
					enumerable = enumerable.Where((Type t) => string.Equals(t.Name, typeName, StringComparison.OrdinalIgnoreCase)).ToList<Type>();
					if (enumerable.Count<Type>() > 1)
					{
						enumerable = enumerable.Where((Type t) => string.Equals(t.Name, typeName, StringComparison.Ordinal)).ToList<Type>();
					}
					if (!enumerable.Any<Type>())
					{
						if (noTypeWithName != null)
						{
							throw noTypeWithName(typeName, name);
						}
						return null;
					}
					else if (enumerable.Count<Type>() > 1)
					{
						if (multipleTypesWithName != null)
						{
							throw multipleTypesWithName(typeName, name);
						}
						return null;
					}
				}
				else
				{
					enumerable = filter(enumerable);
					if (!enumerable.Any<Type>())
					{
						if (noType != null)
						{
							throw noType(name);
						}
						return null;
					}
					else if (enumerable.Count<Type>() > 1)
					{
						if (multipleTypes != null)
						{
							throw multipleTypes(name, enumerable);
						}
						return null;
					}
				}
				type = enumerable.Single<Type>();
			}
			return type;
		}

		// Token: 0x04000113 RID: 275
		private readonly Assembly _assembly;
	}
}
