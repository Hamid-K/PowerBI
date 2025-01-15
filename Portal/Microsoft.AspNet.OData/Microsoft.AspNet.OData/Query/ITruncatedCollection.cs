using System;
using System.Collections;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000C3 RID: 195
	public interface ITruncatedCollection : IEnumerable
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000684 RID: 1668
		int PageSize { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000685 RID: 1669
		bool IsTruncated { get; }
	}
}
