using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000044 RID: 68
	public sealed class KeyPropertyValue
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000096C0 File Offset: 0x000078C0
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000096C8 File Offset: 0x000078C8
		public IEdmProperty KeyProperty { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000096D1 File Offset: 0x000078D1
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000096D9 File Offset: 0x000078D9
		public SingleValueQueryNode KeyValue { get; set; }
	}
}
