using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200081E RID: 2078
	internal interface IInternalProcessingContext
	{
		// Token: 0x170026FE RID: 9982
		// (get) Token: 0x0600736F RID: 29551
		ErrorContext ErrorContext { get; }

		// Token: 0x170026FF RID: 9983
		// (get) Token: 0x06007370 RID: 29552
		// (set) Token: 0x06007371 RID: 29553
		bool SnapshotProcessing { get; set; }

		// Token: 0x17002700 RID: 9984
		// (get) Token: 0x06007372 RID: 29554
		DateTime ExecutionTime { get; }

		// Token: 0x17002701 RID: 9985
		// (get) Token: 0x06007373 RID: 29555
		bool EnableDataBackedParameters { get; }
	}
}
