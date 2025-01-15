using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200030D RID: 781
	[DataContract]
	internal class DetailedCacheNodeStats
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0005727A File Offset: 0x0005547A
		// (set) Token: 0x06001CA4 RID: 7332 RVA: 0x00057282 File Offset: 0x00055482
		public HostCacheStats CacheNodeStatsSummary
		{
			get
			{
				return this._cacheNodeStatsSummary;
			}
			set
			{
				this._cacheNodeStatsSummary = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x0005728B File Offset: 0x0005548B
		// (set) Token: 0x06001CA6 RID: 7334 RVA: 0x00057293 File Offset: 0x00055493
		public DetailedNamedCacheStats[] NamedCacheStats
		{
			get
			{
				return this._namedCacheStats;
			}
			set
			{
				this._namedCacheStats = value;
			}
		}

		// Token: 0x04000FC1 RID: 4033
		[DataMember]
		private HostCacheStats _cacheNodeStatsSummary;

		// Token: 0x04000FC2 RID: 4034
		[DataMember]
		private DetailedNamedCacheStats[] _namedCacheStats;
	}
}
