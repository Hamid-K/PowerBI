using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005D1 RID: 1489
	internal static class AttributeUtil
	{
		// Token: 0x060053B6 RID: 21430 RVA: 0x00160C13 File Offset: 0x0015EE13
		public static T GetCustomAttribute<T>(MemberInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x00160C2A File Offset: 0x0015EE2A
		public static T GetCustomAttribute<T>(MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x00160C42 File Offset: 0x0015EE42
		public static T GetCustomAttribute<T>(ParameterInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x00160C59 File Offset: 0x0015EE59
		public static T GetCustomAttribute<T>(ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x00160C71 File Offset: 0x0015EE71
		public static T GetCustomAttribute<T>(Assembly element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x00160C88 File Offset: 0x0015EE88
		public static T GetCustomAttribute<T>(Module element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}
	}
}
