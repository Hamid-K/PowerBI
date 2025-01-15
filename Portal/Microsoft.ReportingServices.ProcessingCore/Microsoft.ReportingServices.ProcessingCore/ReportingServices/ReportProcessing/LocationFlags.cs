using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200076B RID: 1899
	[Flags]
	internal enum LocationFlags
	{
		// Token: 0x040033E2 RID: 13282
		None = 1,
		// Token: 0x040033E3 RID: 13283
		InDataSet = 2,
		// Token: 0x040033E4 RID: 13284
		InDataRegion = 4,
		// Token: 0x040033E5 RID: 13285
		InGrouping = 8,
		// Token: 0x040033E6 RID: 13286
		InDetail = 16,
		// Token: 0x040033E7 RID: 13287
		InMatrixCell = 32,
		// Token: 0x040033E8 RID: 13288
		InPageSection = 64,
		// Token: 0x040033E9 RID: 13289
		InMatrixSubtotal = 128,
		// Token: 0x040033EA RID: 13290
		InMatrixCellTopLevelItem = 256,
		// Token: 0x040033EB RID: 13291
		InMatrixOrTable = 512,
		// Token: 0x040033EC RID: 13292
		InMatrixGroupHeader = 1024
	}
}
