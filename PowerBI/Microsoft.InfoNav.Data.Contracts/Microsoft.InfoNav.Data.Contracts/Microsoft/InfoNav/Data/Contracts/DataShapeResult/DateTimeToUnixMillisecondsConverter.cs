using System;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000110 RID: 272
	public static class DateTimeToUnixMillisecondsConverter
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x0000F088 File Offset: 0x0000D288
		public static bool TryConvert(DateTime dateTime, bool ignoreSubMilliseconds, out long unixTimestamp)
		{
			long num = dateTime.Ticks - DateTimeToUnixMillisecondsConverter.StartOfUnixEpoch.Ticks;
			if (num < 0L || (ignoreSubMilliseconds && num % 10000L != 0L))
			{
				unixTimestamp = -1L;
				return false;
			}
			unixTimestamp = num / 10000L;
			return true;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		public static bool TryParse(long unixTimestamp, out DateTime dateTime)
		{
			if (unixTimestamp < 0L || unixTimestamp > DateTimeToUnixMillisecondsConverter.MaxDateTimeAsUnixTimestamp)
			{
				dateTime = default(DateTime);
				return false;
			}
			long num = unixTimestamp * 10000L + DateTimeToUnixMillisecondsConverter.StartOfUnixEpoch.Ticks;
			dateTime = new DateTime(num, DateTimeKind.Unspecified);
			return true;
		}

		// Token: 0x04000324 RID: 804
		private static readonly DateTime StartOfUnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000325 RID: 805
		private static readonly long MaxDateTimeAsUnixTimestamp = (DateTime.MaxValue.Ticks - DateTimeToUnixMillisecondsConverter.StartOfUnixEpoch.Ticks) / 10000L;
	}
}
