using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000278 RID: 632
	internal static class TypeUtils
	{
		// Token: 0x060013B4 RID: 5044 RVA: 0x00049BE0 File Offset: 0x00047DE0
		internal static bool IsNullableType(Type type)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00049BFE File Offset: 0x00047DFE
		internal static Type GetNonNullableType(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00049C0C File Offset: 0x00047E0C
		internal static Type GetNullableType(Type type)
		{
			if (!TypeUtils.TypeAllowsNull(type))
			{
				type = typeof(Nullable).MakeGenericType(new Type[] { type });
			}
			return type;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00049C3F File Offset: 0x00047E3F
		internal static bool TypeAllowsNull(Type type)
		{
			return !type.IsValueType() || TypeUtils.IsNullableType(type);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00049C51 File Offset: 0x00047E51
		internal static bool AreTypesEquivalent(Type typeA, Type typeB)
		{
			return typeA != null && typeB != null && typeA == typeB;
		}
	}
}
