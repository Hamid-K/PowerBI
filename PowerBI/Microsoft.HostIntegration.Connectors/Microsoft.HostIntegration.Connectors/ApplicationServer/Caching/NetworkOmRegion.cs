using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000277 RID: 631
	[DataContract]
	internal sealed class NetworkOmRegion : DMCacheItem
	{
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0004127F File Offset: 0x0003F47F
		public string CacheName
		{
			get
			{
				return this._namedCache.CacheName;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0004128C File Offset: 0x0003F48C
		public string RegionName
		{
			get
			{
				return this._regionName;
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00041294 File Offset: 0x0003F494
		internal NetworkOmRegion(string regionName, string namedCache)
		{
			this._namedCache = new OMNamedCache(namedCache);
			this._regionName = regionName;
		}

		// Token: 0x04000C6B RID: 3179
		[DataMember]
		private string _regionName;

		// Token: 0x04000C6C RID: 3180
		[DataMember]
		private OMNamedCache _namedCache;
	}
}
