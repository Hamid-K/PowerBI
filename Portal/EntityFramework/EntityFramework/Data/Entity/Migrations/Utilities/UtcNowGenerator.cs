using System;
using System.Globalization;
using System.Threading;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x020000A7 RID: 167
	internal static class UtcNowGenerator
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x0001F960 File Offset: 0x0001DB60
		public static DateTime UtcNow()
		{
			DateTime dateTime = DateTime.UtcNow;
			DateTime value = UtcNowGenerator._lastNow.Value;
			if (dateTime <= value || dateTime.ToString("yyyyMMddHHmmssf", CultureInfo.InvariantCulture).Equals(value.ToString("yyyyMMddHHmmssf", CultureInfo.InvariantCulture), StringComparison.Ordinal))
			{
				dateTime = value.AddMilliseconds(100.0);
			}
			UtcNowGenerator._lastNow.Value = dateTime;
			return dateTime;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		public static string UtcNowAsMigrationIdTimestamp()
		{
			return UtcNowGenerator.UtcNow().ToString("yyyyMMddHHmmssf", CultureInfo.InvariantCulture);
		}

		// Token: 0x0400083C RID: 2108
		public const string MigrationIdFormat = "yyyyMMddHHmmssf";

		// Token: 0x0400083D RID: 2109
		private static readonly ThreadLocal<DateTime> _lastNow = new ThreadLocal<DateTime>(() => DateTime.UtcNow);
	}
}
