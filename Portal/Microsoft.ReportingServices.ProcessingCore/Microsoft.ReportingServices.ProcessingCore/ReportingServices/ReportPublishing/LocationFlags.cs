using System;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000385 RID: 901
	[Flags]
	internal enum LocationFlags
	{
		// Token: 0x04001126 RID: 4390
		None = 1,
		// Token: 0x04001127 RID: 4391
		InDataSet = 2,
		// Token: 0x04001128 RID: 4392
		InDataRegion = 4,
		// Token: 0x04001129 RID: 4393
		InGrouping = 8,
		// Token: 0x0400112A RID: 4394
		InDetail = 16,
		// Token: 0x0400112B RID: 4395
		InDynamicTablixCell = 32,
		// Token: 0x0400112C RID: 4396
		InPageSection = 64,
		// Token: 0x0400112D RID: 4397
		InTablixSubtotal = 128,
		// Token: 0x0400112E RID: 4398
		InDataRegionCellTopLevelItem = 256,
		// Token: 0x0400112F RID: 4399
		InTablix = 512,
		// Token: 0x04001130 RID: 4400
		InDataRegionGroupHeader = 1024,
		// Token: 0x04001131 RID: 4401
		InNonToggleableHiddenStaticTablixMember = 4096,
		// Token: 0x04001132 RID: 4402
		InParameter = 8192,
		// Token: 0x04001133 RID: 4403
		InTablixCell = 16384,
		// Token: 0x04001134 RID: 4404
		InTablixRowHierarchy = 32768,
		// Token: 0x04001135 RID: 4405
		InTablixColumnHierarchy = 65536
	}
}
