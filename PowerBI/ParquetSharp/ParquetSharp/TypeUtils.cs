using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000091 RID: 145
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TypeUtils
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000EB10 File Offset: 0x0000CD10
		public static bool IsNullable(Type type, out Type inner)
		{
			if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>))
			{
				inner = null;
				return false;
			}
			inner = type.GetGenericArguments().Single<Type>();
			return true;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		public static bool IsNested(Type type, out Type inner)
		{
			if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nested<>))
			{
				inner = null;
				return false;
			}
			inner = type.GetGenericArguments().Single<Type>();
			return true;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000EB88 File Offset: 0x0000CD88
		public static bool IsNullableNested(Type type, out Type inner)
		{
			Type type2;
			Type type3;
			if (TypeUtils.IsNullable(type, out type2) && TypeUtils.IsNested(type2, out type3))
			{
				inner = type3;
				return true;
			}
			inner = null;
			return false;
		}
	}
}
