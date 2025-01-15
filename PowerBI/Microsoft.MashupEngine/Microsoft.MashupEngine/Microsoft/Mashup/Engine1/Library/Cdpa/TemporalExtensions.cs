using System;
using System.Xml;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E54 RID: 3668
	internal static class TemporalExtensions
	{
		// Token: 0x0600629D RID: 25245 RVA: 0x001525C8 File Offset: 0x001507C8
		public static DateTime? Max(DateTime? dt1, DateTime? dt2)
		{
			if (dt1 == null)
			{
				return dt2;
			}
			if (dt2 == null)
			{
				return dt1;
			}
			if (!(dt1 >= dt2))
			{
				return dt2;
			}
			return dt1;
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x0015261C File Offset: 0x0015081C
		public static DateTime? Min(DateTime? dt1, DateTime? dt2)
		{
			if (dt1 == null)
			{
				return dt2;
			}
			if (dt2 == null)
			{
				return dt1;
			}
			if (!(dt1 <= dt2))
			{
				return dt2;
			}
			return dt1;
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x0015266F File Offset: 0x0015086F
		public static TimeSpan Min(TimeSpan timeSpan1, TimeSpan timeSpan2)
		{
			if (!(timeSpan1 <= timeSpan2))
			{
				return timeSpan2;
			}
			return timeSpan1;
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x0015267D File Offset: 0x0015087D
		public static string ToIso8601(this TimeSpan duration)
		{
			duration = TimeSpan.FromSeconds(duration.TotalSeconds + (duration.Milliseconds >= 500));
			return XmlConvert.ToString(duration);
		}
	}
}
