using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005D5 RID: 1493
	internal static class EnumUtil
	{
		// Token: 0x060053C6 RID: 21446 RVA: 0x00160F92 File Offset: 0x0015F192
		public static bool IsDefined<T>(T value)
		{
			return Enum.IsDefined(typeof(T), value);
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x00160FAC File Offset: 0x0015F1AC
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
