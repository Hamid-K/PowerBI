using System;
using System.Globalization;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001C5 RID: 453
	internal static class DateTimeHelpers
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x00044A58 File Offset: 0x00042C58
		public static DateTimeOffset UnixTimestampToDateTime(double unixTimestamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimestamp).ToUniversalTime();
			return dateTime;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00044A90 File Offset: 0x00042C90
		public static DateTimeOffset? UnixTimestampToDateTimeOrNull(double unixTimestamp)
		{
			if (unixTimestamp == 0.0)
			{
				return null;
			}
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimestamp).ToUniversalTime();
			return new DateTimeOffset?(dateTime);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00044AE4 File Offset: 0x00042CE4
		public static string DateTimeToUnixTimestamp(DateTimeOffset dateTimeOffset)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return ((long)dateTimeOffset.Subtract(dateTime).TotalSeconds).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00044B28 File Offset: 0x00042D28
		public static long CurrDateTimeInUnixTimestamp()
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return (long)DateTime.UtcNow.Subtract(dateTime).TotalSeconds;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00044B60 File Offset: 0x00042D60
		public static long GetDurationFromWindowsTimestamp(string windowsTimestampInFuture, ILoggerAdapter logger)
		{
			if (string.IsNullOrEmpty(windowsTimestampInFuture))
			{
				return 0L;
			}
			ulong num;
			if (!ulong.TryParse(windowsTimestampInFuture, out num) || num <= 11644473600UL || num == 18446744073709551615UL)
			{
				logger.Warning("Invalid Universal time " + windowsTimestampInFuture);
				return 0L;
			}
			return (long)(num - 11644473600UL - (ulong)DateTimeHelpers.CurrDateTimeInUnixTimestamp());
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00044BB8 File Offset: 0x00042DB8
		public static long GetDurationFromNowInSeconds(string unixTimestampInFuture)
		{
			if (string.IsNullOrEmpty(unixTimestampInFuture))
			{
				return 0L;
			}
			return long.Parse(unixTimestampInFuture, CultureInfo.InvariantCulture) - DateTimeHelpers.CurrDateTimeInUnixTimestamp();
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00044BD8 File Offset: 0x00042DD8
		public static DateTimeOffset? DateTimeOffsetFromDuration(long? duration)
		{
			if (duration != null)
			{
				return new DateTimeOffset?(DateTimeHelpers.DateTimeOffsetFromDuration(duration.Value));
			}
			return null;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00044C09 File Offset: 0x00042E09
		public static DateTimeOffset DateTimeOffsetFromDuration(long duration)
		{
			return DateTime.UtcNow + TimeSpan.FromSeconds((double)duration);
		}
	}
}
