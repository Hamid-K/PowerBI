using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000079 RID: 121
	internal static class XmlConvertEx
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x00015CC4 File Offset: 0x00013EC4
		public static string ToString(Enum value)
		{
			return value.ToString();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00015CCC File Offset: 0x00013ECC
		public static T ToEnum<T>(string value)
		{
			return (T)((object)Enum.Parse(typeof(T), value));
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00015CE3 File Offset: 0x00013EE3
		public static string ToString(CultureInfo value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value.Name;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00015CF9 File Offset: 0x00013EF9
		public static CultureInfo ToCultureInfo(string value)
		{
			return CultureInfo.GetCultureInfo(value);
		}
	}
}
