using System;
using System.Reflection;

namespace Microsoft.Reflection
{
	// Token: 0x02000007 RID: 7
	internal static class ReflectionExtensions
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021B7 File Offset: 0x000003B7
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021BF File Offset: 0x000003BF
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C7 File Offset: 0x000003C7
		public static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021CF File Offset: 0x000003CF
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021D7 File Offset: 0x000003D7
		public static Assembly Assembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021DF File Offset: 0x000003DF
		public static TypeCode GetTypeCode(this Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021E7 File Offset: 0x000003E7
		public static bool ReflectionOnly(this Assembly assm)
		{
			return assm.ReflectionOnly;
		}
	}
}
