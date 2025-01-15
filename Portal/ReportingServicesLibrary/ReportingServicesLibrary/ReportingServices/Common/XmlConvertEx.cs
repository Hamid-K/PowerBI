using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200036E RID: 878
	internal static class XmlConvertEx
	{
		// Token: 0x06001CCB RID: 7371 RVA: 0x00073FD8 File Offset: 0x000721D8
		public static string ToString(Enum value)
		{
			return value.ToString();
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00073FE0 File Offset: 0x000721E0
		public static T ToEnum<T>(string value)
		{
			return (T)((object)Enum.Parse(typeof(T), value));
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00073FF7 File Offset: 0x000721F7
		public static string ToString(CultureInfo value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value.Name;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0007400D File Offset: 0x0007220D
		public static CultureInfo ToCultureInfo(string value)
		{
			return CultureInfo.GetCultureInfo(value);
		}
	}
}
