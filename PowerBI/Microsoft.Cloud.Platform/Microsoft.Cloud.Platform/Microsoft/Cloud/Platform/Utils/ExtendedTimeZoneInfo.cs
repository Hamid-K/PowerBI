using System;
using System.Linq;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002C9 RID: 713
	public static class ExtendedTimeZoneInfo
	{
		// Token: 0x06001322 RID: 4898 RVA: 0x00042420 File Offset: 0x00040620
		public static TimeSpan DaylightSavingTimeOffset(this TimeZoneInfo tz, DateTime datetime)
		{
			if (tz.SupportsDaylightSavingTime && tz.IsDaylightSavingTime(datetime))
			{
				return tz.GetAdjustmentRules().Single((TimeZoneInfo.AdjustmentRule r) => datetime >= r.DateStart && datetime <= r.DateEnd).DaylightDelta;
			}
			return TimeSpan.Zero;
		}
	}
}
