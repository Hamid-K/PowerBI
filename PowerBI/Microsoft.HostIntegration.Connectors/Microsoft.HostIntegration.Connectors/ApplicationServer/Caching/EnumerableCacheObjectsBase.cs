using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000093 RID: 147
	internal class EnumerableCacheObjectsBase
	{
		// Token: 0x0600034D RID: 845 RVA: 0x000112EA File Offset: 0x0000F4EA
		public EnumerableCacheObjectsBase(DataCache cache, string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener)
		{
			this._cache = cache;
			this._regionName = region;
			this._tags = tags;
			this._op = op;
			this._listener = listener;
		}

		// Token: 0x040002A5 RID: 677
		protected DataCache _cache;

		// Token: 0x040002A6 RID: 678
		protected string _regionName;

		// Token: 0x040002A7 RID: 679
		protected DataCacheTag[] _tags;

		// Token: 0x040002A8 RID: 680
		protected GetByTagsOperation _op;

		// Token: 0x040002A9 RID: 681
		protected IMonitoringListener _listener;
	}
}
