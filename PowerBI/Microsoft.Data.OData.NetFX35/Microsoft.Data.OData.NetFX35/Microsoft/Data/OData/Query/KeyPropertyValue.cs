using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000CB RID: 203
	public sealed class KeyPropertyValue
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00011541 File Offset: 0x0000F741
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00011549 File Offset: 0x0000F749
		public IEdmProperty KeyProperty { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00011552 File Offset: 0x0000F752
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0001155A File Offset: 0x0000F75A
		public SingleValueNode KeyValue { get; set; }
	}
}
