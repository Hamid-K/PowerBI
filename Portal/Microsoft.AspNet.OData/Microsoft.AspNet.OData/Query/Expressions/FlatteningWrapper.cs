using System;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000E7 RID: 231
	internal class FlatteningWrapper<T> : GroupByWrapper
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001D12D File Offset: 0x0001B32D
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0001D135 File Offset: 0x0001B335
		public T Source { get; set; }
	}
}
