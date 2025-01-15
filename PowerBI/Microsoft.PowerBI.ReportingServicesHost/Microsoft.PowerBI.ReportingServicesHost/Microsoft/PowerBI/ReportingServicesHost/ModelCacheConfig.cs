using System;
using System.Collections.Specialized;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000051 RID: 81
	public sealed class ModelCacheConfig
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x000050EC File Offset: 0x000032EC
		public ModelCacheConfig(TimeSpan pollingInterval, int cacheMemoryLimitMegabytes, TimeSpan transientCacheExpiration)
		{
			this.TransientCacheExpiration = transientCacheExpiration;
			this.MemoryCacheConfig = new NameValueCollection();
			this.MemoryCacheConfig.Add("pollingInterval", pollingInterval.ToString("hh\\:mm\\:ss"));
			this.MemoryCacheConfig.Add("cacheMemoryLimitMegabytes", cacheMemoryLimitMegabytes.ToString());
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00005144 File Offset: 0x00003344
		public TimeSpan TransientCacheExpiration { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000514C File Offset: 0x0000334C
		public NameValueCollection MemoryCacheConfig { get; }
	}
}
