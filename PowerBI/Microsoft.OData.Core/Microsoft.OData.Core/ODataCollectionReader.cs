using System;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000061 RID: 97
	public abstract class ODataCollectionReader
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000367 RID: 871
		public abstract ODataCollectionReaderState State { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000368 RID: 872
		public abstract object Item { get; }

		// Token: 0x06000369 RID: 873
		public abstract bool Read();

		// Token: 0x0600036A RID: 874
		public abstract Task<bool> ReadAsync();
	}
}
