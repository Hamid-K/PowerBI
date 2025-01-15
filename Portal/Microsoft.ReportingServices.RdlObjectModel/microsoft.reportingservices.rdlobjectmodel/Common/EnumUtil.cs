using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000060 RID: 96
	internal static class EnumUtil
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00015BC2 File Offset: 0x00013DC2
		public static bool IsDefined<T>(T value)
		{
			return Enum.IsDefined(typeof(T), value);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00015BDC File Offset: 0x00013DDC
		public static bool TryParse<T>(string value, out T enumValue)
		{
			bool flag;
			try
			{
				enumValue = (T)((object)Enum.Parse(typeof(T), value));
				flag = true;
			}
			catch (ArgumentException)
			{
				enumValue = default(T);
				flag = false;
			}
			return flag;
		}
	}
}
