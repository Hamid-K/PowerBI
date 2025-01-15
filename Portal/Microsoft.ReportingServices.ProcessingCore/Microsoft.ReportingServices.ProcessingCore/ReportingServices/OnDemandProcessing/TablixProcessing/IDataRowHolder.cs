using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x02000904 RID: 2308
	public interface IDataRowHolder
	{
		// Token: 0x06007EF3 RID: 32499
		void ReadRows(DataActions action, ITraversalContext context);

		// Token: 0x06007EF4 RID: 32500
		void UpdateAggregates(AggregateUpdateContext context);

		// Token: 0x06007EF5 RID: 32501
		void SetupEnvironment();
	}
}
