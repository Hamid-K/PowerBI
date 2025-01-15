using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x02000631 RID: 1585
	public interface ICloneableCache<TKey, TValue, out TCache> : ICloneableCache, ICachefulObject, ICachefulObject<TCache> where TCache : ICloneableCache<TKey, TValue, TCache>
	{
		// Token: 0x06002268 RID: 8808
		void Add(TKey key, TValue value);

		// Token: 0x06002269 RID: 8809
		bool Replace(TKey key, TValue value, out TValue oldValue);

		// Token: 0x0600226A RID: 8810
		bool Remove(TKey key, out TValue removedValue);

		// Token: 0x0600226B RID: 8811
		bool Lookup(TKey key, out TValue value);

		// Token: 0x0600226C RID: 8812
		TValue GetOrAdd(TKey key, Func<TKey, TValue> insertValueFunc);

		// Token: 0x0600226D RID: 8813
		TValue AddOrUpdate(TKey key, Func<TKey, TValue> insertValueFunc, Func<TKey, TValue, TValue> updateValueFunc);

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600226E RID: 8814
		IEnumerable<TKey> Keys { get; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600226F RID: 8815
		IEnumerable<TValue> Values { get; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002270 RID: 8816
		IEnumerable<KeyValuePair<TKey, TValue>> Mappings { get; }

		// Token: 0x06002271 RID: 8817
		TCache ShallowClone();

		// Token: 0x06002272 RID: 8818
		TCache DeepClone();

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002273 RID: 8819
		Func<TKey, TKey> KeyCloner { get; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002274 RID: 8820
		Func<TValue, TValue> ValueCloner { get; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002275 RID: 8821
		IEqualityComparer<TKey> EqualityComparer { get; }
	}
}
