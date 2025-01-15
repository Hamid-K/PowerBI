using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000A3 RID: 163
	internal class EvictionParametrs
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x00013521 File Offset: 0x00011721
		public EvictionParametrs(long count, int pcntCleanup, TimeSpan expiryInterval, TimeSpan evictionInterval)
		{
			this._hwObjectCount = count;
			this._pcntCleanup = pcntCleanup;
			this._evictinInterval = evictionInterval;
			this._expiryInterval = expiryInterval;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00013546 File Offset: 0x00011746
		public EvictionParametrs(long count, int pcntCleanup)
			: this(count, pcntCleanup, ConfigManager.LocalCacheExpiryInterval, ConfigManager.LocalCacheEvictionInterval)
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001355A File Offset: 0x0001175A
		public EvictionParametrs()
			: this(10000L, 20)
		{
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0001356A File Offset: 0x0001176A
		public long HWObjectCount
		{
			get
			{
				return this._hwObjectCount;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00013572 File Offset: 0x00011772
		public int PcntCleanup
		{
			get
			{
				return this._pcntCleanup;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0001357A File Offset: 0x0001177A
		public TimeSpan ExpiryInterval
		{
			get
			{
				return this._expiryInterval;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00013582 File Offset: 0x00011782
		public TimeSpan EvictionInterval
		{
			get
			{
				return this._evictinInterval;
			}
		}

		// Token: 0x040002F2 RID: 754
		private long _hwObjectCount;

		// Token: 0x040002F3 RID: 755
		private int _pcntCleanup;

		// Token: 0x040002F4 RID: 756
		private TimeSpan _expiryInterval;

		// Token: 0x040002F5 RID: 757
		private TimeSpan _evictinInterval;
	}
}
