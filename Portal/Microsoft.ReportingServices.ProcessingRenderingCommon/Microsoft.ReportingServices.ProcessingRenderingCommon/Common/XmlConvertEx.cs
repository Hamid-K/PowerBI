using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200000B RID: 11
	internal static class XmlConvertEx
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000044AB File Offset: 0x000026AB
		public static string ToString(Enum value)
		{
			return value.ToString();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000044B3 File Offset: 0x000026B3
		public static T ToEnum<T>(string value)
		{
			return (T)((object)Enum.Parse(typeof(T), value));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000044CA File Offset: 0x000026CA
		public static string ToString(CultureInfo value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value.Name;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000044E0 File Offset: 0x000026E0
		public static CultureInfo ToCultureInfo(string value)
		{
			return CultureInfo.GetCultureInfo(value);
		}
	}
}
