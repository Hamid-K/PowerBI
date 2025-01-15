using System;

namespace Microsoft.ReportingServices.Portal.Services.Util
{
	// Token: 0x02000027 RID: 39
	public static class EnumUtils
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		public static T Parse<T>(object objectToParse)
		{
			return (T)((object)Enum.Parse(typeof(T), objectToParse.ToString(), true));
		}
	}
}
