using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000111 RID: 273
	public class RemovedSubtreeEntry
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x0007E431 File Offset: 0x0007C631
		internal RemovedSubtreeEntry()
		{
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0007E439 File Offset: 0x0007C639
		// (set) Token: 0x060011CF RID: 4559 RVA: 0x0007E441 File Offset: 0x0007C641
		public MetadataObject RemovedObject { get; internal set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0007E44A File Offset: 0x0007C64A
		// (set) Token: 0x060011D1 RID: 4561 RVA: 0x0007E452 File Offset: 0x0007C652
		public MetadataObject LastParent { get; internal set; }
	}
}
