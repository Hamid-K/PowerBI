using System;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000055 RID: 85
	internal class TimeZoneInfoHelper
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A90E File Offset: 0x00008B0E
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000A922 File Offset: 0x00008B22
		public static TimeZoneInfo TimeZone
		{
			get
			{
				if (TimeZoneInfoHelper._defaultTimeZoneInfo == null)
				{
					return TimeZoneInfo.Local;
				}
				return TimeZoneInfoHelper._defaultTimeZoneInfo;
			}
			set
			{
				TimeZoneInfoHelper._defaultTimeZoneInfo = value;
			}
		}

		// Token: 0x040000B9 RID: 185
		private static TimeZoneInfo _defaultTimeZoneInfo;
	}
}
