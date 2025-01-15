using System;
using System.Reflection;

namespace Dapper
{
	// Token: 0x02000011 RID: 17
	internal static class TypeExtensions
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000097A4 File Offset: 0x000079A4
		public static string Name(this Type type)
		{
			return type.Name;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000097AC File Offset: 0x000079AC
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000097B4 File Offset: 0x000079B4
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000097BC File Offset: 0x000079BC
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000097C4 File Offset: 0x000079C4
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000097CC File Offset: 0x000079CC
		public static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000097D4 File Offset: 0x000079D4
		public static MethodInfo GetPublicInstanceMethod(this Type type, string name, Type[] types)
		{
			return type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public, null, types, null);
		}
	}
}
