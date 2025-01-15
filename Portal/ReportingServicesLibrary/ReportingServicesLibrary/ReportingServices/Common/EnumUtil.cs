using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000366 RID: 870
	internal static class EnumUtil
	{
		// Token: 0x06001CA1 RID: 7329 RVA: 0x000737F2 File Offset: 0x000719F2
		public static bool IsDefined<T>(T value)
		{
			return Enum.IsDefined(typeof(T), value);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0007380C File Offset: 0x00071A0C
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
