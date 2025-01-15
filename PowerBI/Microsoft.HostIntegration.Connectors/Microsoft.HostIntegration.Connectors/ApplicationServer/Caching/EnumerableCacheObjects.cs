using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000094 RID: 148
	internal class EnumerableCacheObjects : EnumerableCacheObjectsBase, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x0600034E RID: 846 RVA: 0x00011317 File Offset: 0x0000F517
		public EnumerableCacheObjects(DataCache cache, string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener)
			: base(cache, region, tags, op, listener)
		{
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00011326 File Offset: 0x0000F526
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return new CacheEnumerator(this._cache, this._regionName, this._tags, this._op, this._listener);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001134B File Offset: 0x0000F54B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
