using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000A1 RID: 161
	public class DataCacheNotificationProperties
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x00012E08 File Offset: 0x00011008
		public DataCacheNotificationProperties(long maxQueueLength, TimeSpan pollInterval)
		{
			if (maxQueueLength <= 0L)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "MaxQueueLengthZero"), "maxQueueLength");
			}
			if (pollInterval.CompareTo(TimeSpan.Zero) <= 0)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "pollInterval");
			}
			this._pollInterval = pollInterval;
			this._maxQueueLength = maxQueueLength;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00012E71 File Offset: 0x00011071
		public TimeSpan PollInterval
		{
			get
			{
				return this._pollInterval;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00012E79 File Offset: 0x00011079
		public long MaxQueueLength
		{
			get
			{
				return this._maxQueueLength;
			}
		}

		// Token: 0x040002E3 RID: 739
		private TimeSpan _pollInterval;

		// Token: 0x040002E4 RID: 740
		private long _maxQueueLength;
	}
}
