using System;

namespace Microsoft.OData
{
	// Token: 0x020000B1 RID: 177
	internal static class TypeUtils
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x000137AF File Offset: 0x000119AF
		internal static bool IsNullableType(Type type)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000137CD File Offset: 0x000119CD
		internal static Type GetNonNullableType(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000137DA File Offset: 0x000119DA
		internal static Type GetNullableType(Type type)
		{
			if (!TypeUtils.TypeAllowsNull(type))
			{
				type = typeof(Nullable).MakeGenericType(new Type[] { type });
			}
			return type;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00013800 File Offset: 0x00011A00
		internal static bool TypeAllowsNull(Type type)
		{
			return !type.IsValueType() || TypeUtils.IsNullableType(type);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00013812 File Offset: 0x00011A12
		internal static bool AreTypesEquivalent(Type typeA, Type typeB)
		{
			return typeA != null && typeB != null && typeA == typeB;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00013820 File Offset: 0x00011A20
		internal static void ParseQualifiedTypeName(string qualifiedTypeName, out string namespaceName, out string typeName, out bool isCollection)
		{
			isCollection = qualifiedTypeName.StartsWith("Collection(", 4);
			if (isCollection)
			{
				qualifiedTypeName = qualifiedTypeName.Substring("Collection".Length + 1).TrimEnd(new char[] { ')' });
			}
			int num = qualifiedTypeName.LastIndexOf(".", 4);
			namespaceName = qualifiedTypeName.Substring(0, num);
			typeName = qualifiedTypeName.Substring((num == 0) ? 0 : (num + 1));
		}
	}
}
