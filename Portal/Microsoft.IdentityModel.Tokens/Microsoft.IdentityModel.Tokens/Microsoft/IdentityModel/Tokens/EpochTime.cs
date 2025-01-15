using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000138 RID: 312
	public static class EpochTime
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x0003CE58 File Offset: 0x0003B058
		public static long GetIntDate(DateTime datetime)
		{
			DateTime dateTime = datetime;
			if (datetime.Kind != DateTimeKind.Utc)
			{
				dateTime = datetime.ToUniversalTime();
			}
			if (dateTime.ToUniversalTime() <= EpochTime.UnixEpoch)
			{
				return 0L;
			}
			return (long)(dateTime - EpochTime.UnixEpoch).TotalSeconds;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003CEA4 File Offset: 0x0003B0A4
		public static DateTime DateTime(long secondsSinceUnixEpoch)
		{
			if (secondsSinceUnixEpoch <= 0L)
			{
				return EpochTime.UnixEpoch;
			}
			if ((double)secondsSinceUnixEpoch > TimeSpan.MaxValue.TotalSeconds)
			{
				return DateTimeUtil.Add(EpochTime.UnixEpoch, TimeSpan.MaxValue).ToUniversalTime();
			}
			return DateTimeUtil.Add(EpochTime.UnixEpoch, TimeSpan.FromSeconds((double)secondsSinceUnixEpoch)).ToUniversalTime();
		}

		// Token: 0x040004E0 RID: 1248
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
	}
}
