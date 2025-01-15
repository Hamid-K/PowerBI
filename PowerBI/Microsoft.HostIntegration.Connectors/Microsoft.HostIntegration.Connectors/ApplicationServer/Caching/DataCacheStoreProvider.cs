using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E6 RID: 486
	public abstract class DataCacheStoreProvider : IDisposable
	{
		// Token: 0x06000FBD RID: 4029
		public abstract void Write(DataCacheItem item);

		// Token: 0x06000FBE RID: 4030
		public abstract void Write(IDictionary<DataCacheItemKey, DataCacheItem> items);

		// Token: 0x06000FBF RID: 4031
		public abstract DataCacheItem Read(DataCacheItemKey key);

		// Token: 0x06000FC0 RID: 4032
		public abstract void Read(ReadOnlyCollection<DataCacheItemKey> keys, IDictionary<DataCacheItemKey, DataCacheItem> items);

		// Token: 0x06000FC1 RID: 4033
		public abstract void Delete(DataCacheItemKey key);

		// Token: 0x06000FC2 RID: 4034
		public abstract void Delete(Collection<DataCacheItemKey> keys);

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00035F22 File Offset: 0x00034122
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000FC4 RID: 4036
		protected abstract void Dispose(bool disposing);
	}
}
