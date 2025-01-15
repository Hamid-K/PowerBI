using System;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200011F RID: 287
	internal class OperationTitleAnnotation
	{
		// Token: 0x060009D9 RID: 2521 RVA: 0x00028994 File Offset: 0x00026B94
		public OperationTitleAnnotation(string title)
		{
			this.Title = title;
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x000289A3 File Offset: 0x00026BA3
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x000289AB File Offset: 0x00026BAB
		public string Title { get; private set; }
	}
}
