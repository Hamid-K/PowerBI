using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000051 RID: 81
	internal enum CancelationTrigger
	{
		// Token: 0x0400010C RID: 268
		None,
		// Token: 0x0400010D RID: 269
		AfterDsqParsing,
		// Token: 0x0400010E RID: 270
		AfterDataSourceResolution,
		// Token: 0x0400010F RID: 271
		DsqtAfterValidation,
		// Token: 0x04000110 RID: 272
		DsqtAfterQueryGeneration,
		// Token: 0x04000111 RID: 273
		DsqtAfterDsdGeneration,
		// Token: 0x04000112 RID: 274
		ReportProcessing
	}
}
