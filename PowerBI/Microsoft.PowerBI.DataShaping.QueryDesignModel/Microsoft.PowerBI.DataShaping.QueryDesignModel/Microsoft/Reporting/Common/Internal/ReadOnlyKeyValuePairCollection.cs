using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x02000292 RID: 658
	internal sealed class ReadOnlyKeyValuePairCollection<TKey, TValue> : ReadOnlyKeyedCollection<TKey, KeyValuePair<TKey, TValue>> where TKey : class
	{
		// Token: 0x06001BFD RID: 7165 RVA: 0x0004DFD8 File Offset: 0x0004C1D8
		internal ReadOnlyKeyValuePairCollection(IEnumerable<KeyValuePair<TKey, TValue>> items)
			: base(items)
		{
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0004DFE1 File Offset: 0x0004C1E1
		protected override TKey GetKeyForItem(KeyValuePair<TKey, TValue> item)
		{
			return item.Key;
		}
	}
}
