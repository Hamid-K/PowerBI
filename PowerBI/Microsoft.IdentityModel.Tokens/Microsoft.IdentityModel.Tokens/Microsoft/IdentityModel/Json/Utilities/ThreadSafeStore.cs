using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Utilities
{
	// Token: 0x02000070 RID: 112
	internal class ThreadSafeStore<[Nullable(1)] TKey, TValue>
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0001998F File Offset: 0x00017B8F
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			this._creator = creator;
			this._concurrentStore = new ConcurrentDictionary<TKey, TValue>();
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000199B4 File Offset: 0x00017BB4
		public TValue Get(TKey key)
		{
			return this._concurrentStore.GetOrAdd(key, this._creator);
		}

		// Token: 0x04000229 RID: 553
		private readonly ConcurrentDictionary<TKey, TValue> _concurrentStore;

		// Token: 0x0400022A RID: 554
		private readonly Func<TKey, TValue> _creator;
	}
}
