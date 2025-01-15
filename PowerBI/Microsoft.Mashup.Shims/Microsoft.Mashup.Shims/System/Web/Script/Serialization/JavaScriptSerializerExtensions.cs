using System;

namespace System.Web.Script.Serialization
{
	// Token: 0x02000006 RID: 6
	public static class JavaScriptSerializerExtensions
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000210D File Offset: 0x0000030D
		public static void RegisterConverter<T>(this JavaScriptSerializer javaScriptSerializer, JavaScriptConverter<T> javaScriptConverter)
		{
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[] { javaScriptConverter });
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000211F File Offset: 0x0000031F
		public static string Serialize<T>(this JavaScriptSerializer javaScriptSerializer, T value)
		{
			return javaScriptSerializer.Serialize(value);
		}
	}
}
