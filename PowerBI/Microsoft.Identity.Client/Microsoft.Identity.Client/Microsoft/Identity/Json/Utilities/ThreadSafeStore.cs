using System;
using System.Collections.Concurrent;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x0200006F RID: 111
	internal class ThreadSafeStore<TKey, TValue>
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x000193BB File Offset: 0x000175BB
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			this._creator = creator;
			this._concurrentStore = new ConcurrentDictionary<TKey, TValue>();
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000193E0 File Offset: 0x000175E0
		public TValue Get(TKey key)
		{
			return this._concurrentStore.GetOrAdd(key, this._creator);
		}

		// Token: 0x0400020E RID: 526
		private readonly ConcurrentDictionary<TKey, TValue> _concurrentStore;

		// Token: 0x0400020F RID: 527
		private readonly Func<TKey, TValue> _creator;
	}
}
