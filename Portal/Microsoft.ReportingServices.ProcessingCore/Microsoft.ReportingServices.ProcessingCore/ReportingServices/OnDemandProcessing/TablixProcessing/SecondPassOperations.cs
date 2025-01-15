using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008FF RID: 2303
	[Flags]
	internal enum SecondPassOperations
	{
		// Token: 0x04003E65 RID: 15973
		None = 0,
		// Token: 0x04003E66 RID: 15974
		Variables = 1,
		// Token: 0x04003E67 RID: 15975
		Sorting = 2,
		// Token: 0x04003E68 RID: 15976
		FilteringOrAggregatesOrDomainScope = 4
	}
}
