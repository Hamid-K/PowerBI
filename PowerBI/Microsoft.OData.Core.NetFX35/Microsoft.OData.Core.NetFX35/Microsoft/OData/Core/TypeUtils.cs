using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020001BA RID: 442
	internal static class TypeUtils
	{
		// Token: 0x06001063 RID: 4195 RVA: 0x000394AA File Offset: 0x000376AA
		internal static bool IsNullableType(Type type)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x000394C8 File Offset: 0x000376C8
		internal static Type GetNonNullableType(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000394D8 File Offset: 0x000376D8
		internal static Type GetNullableType(Type type)
		{
			if (!TypeUtils.TypeAllowsNull(type))
			{
				type = typeof(Nullable).MakeGenericType(new Type[] { type });
			}
			return type;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003950B File Offset: 0x0003770B
		internal static bool TypeAllowsNull(Type type)
		{
			return !type.IsValueType() || TypeUtils.IsNullableType(type);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0003951D File Offset: 0x0003771D
		internal static bool AreTypesEquivalent(Type typeA, Type typeB)
		{
			return typeA != null && typeB != null && typeA == typeB;
		}
	}
}
