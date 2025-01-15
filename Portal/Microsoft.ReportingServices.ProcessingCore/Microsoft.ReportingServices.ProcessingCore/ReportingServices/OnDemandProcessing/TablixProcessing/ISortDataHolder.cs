using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x02000902 RID: 2306
	internal interface ISortDataHolder : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007EEC RID: 32492
		void NextRow();

		// Token: 0x06007EED RID: 32493
		void Traverse(ProcessingStages operation, ITraversalContext traversalContext);
	}
}
