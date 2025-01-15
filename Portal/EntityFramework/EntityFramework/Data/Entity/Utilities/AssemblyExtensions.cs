using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000087 RID: 135
	internal static class AssemblyExtensions
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x000104B8 File Offset: 0x0000E6B8
		public static string GetInformationalVersion(this Assembly assembly)
		{
			return assembly.GetCustomAttributes<AssemblyInformationalVersionAttribute>().Single<AssemblyInformationalVersionAttribute>().InformationalVersion;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000104CC File Offset: 0x0000E6CC
		public static IEnumerable<Type> GetAccessibleTypes(this Assembly assembly)
		{
			IEnumerable<Type> enumerable;
			try
			{
				enumerable = assembly.DefinedTypes.Select((TypeInfo t) => t.AsType());
			}
			catch (ReflectionTypeLoadException ex)
			{
				enumerable = ex.Types.Where((Type t) => t != null);
			}
			return enumerable;
		}
	}
}
