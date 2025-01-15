using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000087 RID: 135
	internal static class TypeExtensions
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0001313F File Offset: 0x0001133F
		public static bool IsAssignableFrom(this Type type, Type otherType)
		{
			return type.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00013154 File Offset: 0x00011354
		public static PropertyInfo[] GetProperties(this Type type)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			list.AddRange(type.GetTypeInfo().DeclaredProperties);
			Type baseType = type.GetTypeInfo().BaseType;
			if (baseType != null)
			{
				list.AddRange(baseType.GetProperties());
			}
			return list.ToArray();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001319F File Offset: 0x0001139F
		public static Type[] GetGenericArguments(this Type type)
		{
			return type.GetTypeInfo().GenericTypeArguments;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000131AC File Offset: 0x000113AC
		public static Type[] GetInterfaces(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces.ToArray<Type>();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000131BE File Offset: 0x000113BE
		public static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000131CB File Offset: 0x000113CB
		public static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}
	}
}
