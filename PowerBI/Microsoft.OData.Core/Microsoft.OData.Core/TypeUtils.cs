using System;

namespace Microsoft.OData
{
	// Token: 0x020000D2 RID: 210
	internal static class TypeUtils
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x00018E0C File Offset: 0x0001700C
		internal static bool IsNullableType(Type type)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00018E2A File Offset: 0x0001702A
		internal static Type GetNonNullableType(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00018E37 File Offset: 0x00017037
		internal static Type GetNullableType(Type type)
		{
			if (!TypeUtils.TypeAllowsNull(type))
			{
				type = typeof(Nullable<>).MakeGenericType(new Type[] { type });
			}
			return type;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00018E5D File Offset: 0x0001705D
		internal static bool TypeAllowsNull(Type type)
		{
			return !type.IsValueType() || TypeUtils.IsNullableType(type);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00018E6F File Offset: 0x0001706F
		internal static bool AreTypesEquivalent(Type typeA, Type typeB)
		{
			return typeA != null && typeB != null && typeA == typeB;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00018E80 File Offset: 0x00017080
		internal static void ParseQualifiedTypeName(string qualifiedTypeName, out string namespaceName, out string typeName, out bool isCollection)
		{
			isCollection = qualifiedTypeName.StartsWith("Collection(", StringComparison.Ordinal);
			if (isCollection)
			{
				qualifiedTypeName = qualifiedTypeName.Substring("Collection".Length + 1).TrimEnd(new char[] { ')' });
			}
			int num = qualifiedTypeName.LastIndexOf(".", StringComparison.Ordinal);
			namespaceName = qualifiedTypeName.Substring(0, num);
			typeName = qualifiedTypeName.Substring((num == 0) ? 0 : (num + 1));
		}
	}
}
