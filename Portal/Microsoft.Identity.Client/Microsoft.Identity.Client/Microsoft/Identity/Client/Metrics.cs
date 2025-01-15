using System;
using System.Threading;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200015D RID: 349
	public class Metrics
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x0003B8CA File Offset: 0x00039ACA
		private Metrics()
		{
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0003B8D2 File Offset: 0x00039AD2
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x0003B8D9 File Offset: 0x00039AD9
		public static long TotalAccessTokensFromIdP
		{
			get
			{
				return Metrics._totalAccessTokensFromIdP;
			}
			internal set
			{
				Metrics._totalAccessTokensFromIdP = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0003B8E1 File Offset: 0x00039AE1
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x0003B8E8 File Offset: 0x00039AE8
		public static long TotalAccessTokensFromCache
		{
			get
			{
				return Metrics._totalAccessTokensFromCache;
			}
			internal set
			{
				Metrics._totalAccessTokensFromCache = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0003B8F0 File Offset: 0x00039AF0
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x0003B8F7 File Offset: 0x00039AF7
		public static long TotalAccessTokensFromBroker
		{
			get
			{
				return Metrics._totalAccessTokensFromBroker;
			}
			internal set
			{
				Metrics._totalAccessTokensFromBroker = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0003B8FF File Offset: 0x00039AFF
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x0003B906 File Offset: 0x00039B06
		public static long TotalDurationInMs
		{
			get
			{
				return Metrics._totalDurationInMs;
			}
			internal set
			{
				Metrics._totalDurationInMs = value;
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0003B90E File Offset: 0x00039B0E
		internal static void IncrementTotalAccessTokensFromIdP()
		{
			Interlocked.Increment(ref Metrics._totalAccessTokensFromIdP);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0003B91B File Offset: 0x00039B1B
		internal static void IncrementTotalAccessTokensFromCache()
		{
			Interlocked.Increment(ref Metrics._totalAccessTokensFromCache);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0003B928 File Offset: 0x00039B28
		internal static void IncrementTotalAccessTokensFromBroker()
		{
			Interlocked.Increment(ref Metrics._totalAccessTokensFromBroker);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0003B935 File Offset: 0x00039B35
		internal static void IncrementTotalDurationInMs(long requestDurationInMs)
		{
			Interlocked.Add(ref Metrics._totalDurationInMs, requestDurationInMs);
		}

		// Token: 0x04000532 RID: 1330
		private static long _totalAccessTokensFromIdP;

		// Token: 0x04000533 RID: 1331
		private static long _totalAccessTokensFromCache;

		// Token: 0x04000534 RID: 1332
		private static long _totalAccessTokensFromBroker;

		// Token: 0x04000535 RID: 1333
		private static long _totalDurationInMs;
	}
}
