using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000009 RID: 9
	internal static class EnumUtil
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000259A File Offset: 0x0000079A
		public static bool IsDefined<T>(T value)
		{
			return Enum.IsDefined(typeof(T), value);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025B4 File Offset: 0x000007B4
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
