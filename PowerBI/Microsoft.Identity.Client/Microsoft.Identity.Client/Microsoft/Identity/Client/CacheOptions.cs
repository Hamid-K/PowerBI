using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000136 RID: 310
	public class CacheOptions
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00039F13 File Offset: 0x00038113
		public static CacheOptions EnableSharedCacheOptions
		{
			get
			{
				return new CacheOptions(true);
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00039F1B File Offset: 0x0003811B
		public CacheOptions()
		{
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00039F23 File Offset: 0x00038123
		public CacheOptions(bool useSharedCache)
		{
			this.UseSharedCache = useSharedCache;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00039F32 File Offset: 0x00038132
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00039F3A File Offset: 0x0003813A
		public bool UseSharedCache { get; set; }
	}
}
