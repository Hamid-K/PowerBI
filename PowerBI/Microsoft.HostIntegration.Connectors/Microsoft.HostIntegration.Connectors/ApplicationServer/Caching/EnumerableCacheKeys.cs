using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000096 RID: 150
	internal class EnumerableCacheKeys : EnumerableCacheObjectsBase, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06000358 RID: 856 RVA: 0x00011317 File Offset: 0x0000F517
		public EnumerableCacheKeys(DataCache cache, string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener)
			: base(cache, region, tags, op, listener)
		{
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000114C8 File Offset: 0x0000F6C8
		public IEnumerator<string> GetEnumerator()
		{
			return new CacheKeyEnumerator(this._cache, this._regionName, this._tags, this._op, this._listener);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000114ED File Offset: 0x0000F6ED
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
