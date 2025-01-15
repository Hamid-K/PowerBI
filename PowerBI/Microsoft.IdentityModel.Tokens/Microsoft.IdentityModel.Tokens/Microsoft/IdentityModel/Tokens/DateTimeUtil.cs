using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200012A RID: 298
	public static class DateTimeUtil
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003A990 File Offset: 0x00038B90
		public static DateTime Add(DateTime time, TimeSpan timespan)
		{
			if (timespan == TimeSpan.Zero)
			{
				return time;
			}
			if (timespan > TimeSpan.Zero && DateTime.MaxValue - time <= timespan)
			{
				return DateTimeUtil.GetMaxValue(time.Kind);
			}
			if (timespan < TimeSpan.Zero && DateTime.MinValue - time >= timespan)
			{
				return DateTimeUtil.GetMinValue(time.Kind);
			}
			return time + timespan;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003AA10 File Offset: 0x00038C10
		public static DateTime GetMaxValue(DateTimeKind kind)
		{
			if (kind == DateTimeKind.Unspecified)
			{
				return new DateTime(DateTime.MaxValue.Ticks, DateTimeKind.Utc);
			}
			return new DateTime(DateTime.MaxValue.Ticks, kind);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003AA48 File Offset: 0x00038C48
		public static DateTime GetMinValue(DateTimeKind kind)
		{
			if (kind == DateTimeKind.Unspecified)
			{
				return new DateTime(DateTime.MinValue.Ticks, DateTimeKind.Utc);
			}
			return new DateTime(DateTime.MinValue.Ticks, kind);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003AA80 File Offset: 0x00038C80
		public static DateTime? ToUniversalTime(DateTime? value)
		{
			if (value == null || value.Value.Kind == DateTimeKind.Utc)
			{
				return value;
			}
			return new DateTime?(DateTimeUtil.ToUniversalTime(value.Value));
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003AABB File Offset: 0x00038CBB
		public static DateTime ToUniversalTime(DateTime value)
		{
			if (value.Kind == DateTimeKind.Utc)
			{
				return value;
			}
			return value.ToUniversalTime();
		}
	}
}
