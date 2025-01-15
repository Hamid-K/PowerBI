using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003ED RID: 1005
	public static class AssemblyTypeUtils
	{
		// Token: 0x060016DC RID: 5852 RVA: 0x00045FAE File Offset: 0x000441AE
		public static IEnumerable<TypeInfo> AllTypesInAssembly(this Type type)
		{
			return type.GetTypeInfo().Assembly.DefinedTypes;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00045FC0 File Offset: 0x000441C0
		public static IEnumerable<TypeInfo> AllNonAbstractTypesInAssembly(this Type type)
		{
			return from t in type.AllTypesInAssembly()
				where !t.IsAbstract
				select t;
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00045FEC File Offset: 0x000441EC
		public static IEnumerable<TypeInfo> AllSubTypesInAssembly(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return from t in type.AllTypesInAssembly()
				where typeInfo.IsAssignableFrom(t.AsType())
				select t;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00046022 File Offset: 0x00044222
		public static IEnumerable<TypeInfo> AllNonAbstractSubTypesInAssembly(this Type type)
		{
			return from t in type.AllSubTypesInAssembly()
				where !t.IsAbstract
				select t;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0004604E File Offset: 0x0004424E
		public static IEnumerable<Type> AsType(this IEnumerable<TypeInfo> typeInfos)
		{
			return typeInfos.Select((TypeInfo ti) => ti.AsType());
		}
	}
}
