using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000AA RID: 170
	public class GetDatasetTablesResult
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00010DBE File Offset: 0x0000EFBE
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00010DC6 File Offset: 0x0000EFC6
		public GetDatasetTablesResult.Table[] Tables { get; set; }

		// Token: 0x02000164 RID: 356
		public class Table
		{
			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06001235 RID: 4661 RVA: 0x00049D5E File Offset: 0x00047F5E
			// (set) Token: 0x06001236 RID: 4662 RVA: 0x00049D66 File Offset: 0x00047F66
			public string Name { get; set; }
		}
	}
}
