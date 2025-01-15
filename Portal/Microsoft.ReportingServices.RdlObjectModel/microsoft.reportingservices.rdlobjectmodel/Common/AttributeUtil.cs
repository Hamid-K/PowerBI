using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200005C RID: 92
	internal static class AttributeUtil
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x00015841 File Offset: 0x00013A41
		public static T GetCustomAttribute<T>(MemberInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00015858 File Offset: 0x00013A58
		public static T GetCustomAttribute<T>(MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00015870 File Offset: 0x00013A70
		public static T GetCustomAttribute<T>(ParameterInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00015887 File Offset: 0x00013A87
		public static T GetCustomAttribute<T>(ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001589F File Offset: 0x00013A9F
		public static T GetCustomAttribute<T>(Assembly element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000158B6 File Offset: 0x00013AB6
		public static T GetCustomAttribute<T>(Module element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}
	}
}
