using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200005E RID: 94
	public sealed class IndexLookupKey
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00008D6F File Offset: 0x00006F6F
		public IndexLookupKey(DataIndexCacheKey primary, IEnumerable<DataIndexCacheKey> alternates)
		{
			this.Primary = primary;
			this.Alternates = ((alternates == null) ? ImmutableList<DataIndexCacheKey>.Empty : alternates.ToImmutableList<DataIndexCacheKey>());
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00008D94 File Offset: 0x00006F94
		public DataIndexCacheKey Primary { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00008D9C File Offset: 0x00006F9C
		public ImmutableList<DataIndexCacheKey> Alternates { get; }
	}
}
