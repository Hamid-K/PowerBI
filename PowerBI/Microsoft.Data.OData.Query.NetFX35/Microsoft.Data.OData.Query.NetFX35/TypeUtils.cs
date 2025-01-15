using System;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x02000020 RID: 32
	internal static class TypeUtils
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003C74 File Offset: 0x00001E74
		internal static bool IsNullableType(Type type)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003C92 File Offset: 0x00001E92
		internal static Type GetNonNullableType(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003CA0 File Offset: 0x00001EA0
		internal static Type GetNullableType(Type type)
		{
			if (!TypeUtils.TypeAllowsNull(type))
			{
				type = typeof(Nullable).MakeGenericType(new Type[] { type });
			}
			return type;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003CD3 File Offset: 0x00001ED3
		internal static bool TypeAllowsNull(Type type)
		{
			return !type.IsValueType() || TypeUtils.IsNullableType(type);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003CE5 File Offset: 0x00001EE5
		internal static bool AreTypesEquivalent(Type typeA, Type typeB)
		{
			return typeA != null && typeB != null && typeA == typeB;
		}
	}
}
