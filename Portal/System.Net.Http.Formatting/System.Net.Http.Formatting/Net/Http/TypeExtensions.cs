using System;
using System.Linq;

namespace System.Net.Http
{
	// Token: 0x0200000A RID: 10
	internal static class TypeExtensions
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000027A4 File Offset: 0x000009A4
		public static Type ExtractGenericInterface(this Type queryType, Type interfaceType)
		{
			Func<Type, bool> func = (Type t) => t.IsGenericType() && t.GetGenericTypeDefinition() == interfaceType;
			if (!func(queryType))
			{
				return queryType.GetInterfaces().FirstOrDefault(func);
			}
			return queryType;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027E0 File Offset: 0x000009E0
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027E8 File Offset: 0x000009E8
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027F0 File Offset: 0x000009F0
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}
	}
}
