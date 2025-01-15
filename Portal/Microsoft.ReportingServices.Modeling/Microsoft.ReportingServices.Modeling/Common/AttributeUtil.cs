using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000005 RID: 5
	internal static class AttributeUtil
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000221A File Offset: 0x0000041A
		public static T GetCustomAttribute<T>(MemberInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002231 File Offset: 0x00000431
		public static T GetCustomAttribute<T>(MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002249 File Offset: 0x00000449
		public static T GetCustomAttribute<T>(ParameterInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002260 File Offset: 0x00000460
		public static T GetCustomAttribute<T>(ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002278 File Offset: 0x00000478
		public static T GetCustomAttribute<T>(Assembly element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000228F File Offset: 0x0000048F
		public static T GetCustomAttribute<T>(Module element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}
	}
}
