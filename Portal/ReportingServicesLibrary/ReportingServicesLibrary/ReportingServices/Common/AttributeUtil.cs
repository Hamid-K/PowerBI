using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000362 RID: 866
	internal static class AttributeUtil
	{
		// Token: 0x06001C91 RID: 7313 RVA: 0x00073474 File Offset: 0x00071674
		public static T GetCustomAttribute<T>(MemberInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0007348B File Offset: 0x0007168B
		public static T GetCustomAttribute<T>(MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x000734A3 File Offset: 0x000716A3
		public static T GetCustomAttribute<T>(ParameterInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x000734BA File Offset: 0x000716BA
		public static T GetCustomAttribute<T>(ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000734D2 File Offset: 0x000716D2
		public static T GetCustomAttribute<T>(Assembly element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000734E9 File Offset: 0x000716E9
		public static T GetCustomAttribute<T>(Module element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}
	}
}
