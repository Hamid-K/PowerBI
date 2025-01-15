using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000061 RID: 97
	internal enum KpiDataSetLastUpdateStatus
	{
		// Token: 0x040001E9 RID: 489
		Success,
		// Token: 0x040001EA RID: 490
		UnknownError,
		// Token: 0x040001EB RID: 491
		DataSetReportCouldNotBeGenerated,
		// Token: 0x040001EC RID: 492
		DataSetReportCouldNotBeParsed,
		// Token: 0x040001ED RID: 493
		DataSetContainedNoData,
		// Token: 0x040001EE RID: 494
		InvalidKpiSharedDataSetColumn
	}
}
