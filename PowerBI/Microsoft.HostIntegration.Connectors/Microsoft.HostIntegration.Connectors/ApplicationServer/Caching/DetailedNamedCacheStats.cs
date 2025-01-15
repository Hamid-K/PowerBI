using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200030E RID: 782
	[DataContract]
	internal class DetailedNamedCacheStats
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0005729C File Offset: 0x0005549C
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x000572A4 File Offset: 0x000554A4
		public string CacheName
		{
			get
			{
				return this._cacheName;
			}
			set
			{
				this._cacheName = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x000572AD File Offset: 0x000554AD
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x000572B5 File Offset: 0x000554B5
		public NamedCacheStats NamedCacheLevelStats
		{
			get
			{
				return this._namedCacheLevelStats;
			}
			set
			{
				this._namedCacheLevelStats = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x000572BE File Offset: 0x000554BE
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x000572C6 File Offset: 0x000554C6
		public CacheRegionStats[] RegionStats
		{
			get
			{
				return this._regionStats;
			}
			set
			{
				this._regionStats = value;
			}
		}

		// Token: 0x04000FC3 RID: 4035
		[DataMember]
		private string _cacheName;

		// Token: 0x04000FC4 RID: 4036
		[DataMember]
		private NamedCacheStats _namedCacheLevelStats;

		// Token: 0x04000FC5 RID: 4037
		[DataMember]
		private CacheRegionStats[] _regionStats;
	}
}
