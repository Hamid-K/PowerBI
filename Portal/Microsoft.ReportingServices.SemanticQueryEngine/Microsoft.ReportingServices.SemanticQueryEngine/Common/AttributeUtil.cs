using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200006D RID: 109
	internal static class AttributeUtil
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x00015160 File Offset: 0x00013360
		public static T GetCustomAttribute<T>(MemberInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00015177 File Offset: 0x00013377
		public static T GetCustomAttribute<T>(MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001518F File Offset: 0x0001338F
		public static T GetCustomAttribute<T>(ParameterInfo element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000151A6 File Offset: 0x000133A6
		public static T GetCustomAttribute<T>(ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T), inherit));
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000151BE File Offset: 0x000133BE
		public static T GetCustomAttribute<T>(Assembly element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000151D5 File Offset: 0x000133D5
		public static T GetCustomAttribute<T>(Module element) where T : Attribute
		{
			return (T)((object)Attribute.GetCustomAttribute(element, typeof(T)));
		}
	}
}
