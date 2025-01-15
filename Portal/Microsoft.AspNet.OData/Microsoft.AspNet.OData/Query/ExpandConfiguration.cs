using System;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000AD RID: 173
	public class ExpandConfiguration
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x000153C5 File Offset: 0x000135C5
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x000153CD File Offset: 0x000135CD
		public SelectExpandType ExpandType { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x000153D6 File Offset: 0x000135D6
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x000153DE File Offset: 0x000135DE
		public int MaxDepth { get; set; }
	}
}
