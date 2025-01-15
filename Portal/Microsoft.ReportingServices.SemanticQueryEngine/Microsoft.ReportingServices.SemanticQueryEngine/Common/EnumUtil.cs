using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000071 RID: 113
	internal static class EnumUtil
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x000154DE File Offset: 0x000136DE
		public static bool IsDefined<T>(T value)
		{
			return Enum.IsDefined(typeof(T), value);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000154F8 File Offset: 0x000136F8
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
