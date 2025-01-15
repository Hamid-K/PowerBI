using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE1 RID: 7137
	public static class DateTimeExtensions
	{
		// Token: 0x0600B221 RID: 45601 RVA: 0x00245260 File Offset: 0x00243460
		public static DateTime SafeAdd(this DateTime dateTime, TimeSpan timeSpan)
		{
			long num = dateTime.Ticks + timeSpan.Ticks;
			if (num >= DateTime.MinValue.Ticks && num <= DateTime.MaxValue.Ticks)
			{
				return new DateTime(num);
			}
			if (Math.Sign(timeSpan.Ticks) <= 0)
			{
				return DateTime.MinValue;
			}
			return DateTime.MaxValue;
		}
	}
}
