using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200005D RID: 93
	public interface IDataIndexStore
	{
		// Token: 0x060002A6 RID: 678
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Index", "IndexKey", "MatchedKey" })]
		Task<global::System.ValueTuple<DataIndex, DataIndexCacheKey, DataIndexCacheKey>> GetIndexAsync(IndexLookupKey key, IDatabaseContext databaseContext, CancellationToken token);

		// Token: 0x060002A7 RID: 679
		Task<bool> StoreIndexAsync(IndexLookupKey key, DataIndex index, CancellationToken token);

		// Token: 0x060002A8 RID: 680
		Task<bool> DeleteIndexAsync(DataIndexCacheKey key, CancellationToken token);

		// Token: 0x060002A9 RID: 681
		void StoreAlternateKey(DataIndexCacheKey primaryKey, DataIndexCacheKey alternateKey, CancellationToken token);
	}
}
