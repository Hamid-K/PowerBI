using System;
using System.Collections;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000B7 RID: 183
	internal interface ICountOptionCollection : IEnumerable
	{
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000633 RID: 1587
		long? TotalCount { get; }
	}
}
