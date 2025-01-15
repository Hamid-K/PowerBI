using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000118 RID: 280
	internal sealed class RenderForStream : RenderForExistingSession
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x00029908 File Offset: 0x00027B08
		public RenderForStream(IExecutionDataProvider provider, ExecutionParameters execInfo)
			: base(provider, execInfo)
		{
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00029914 File Offset: 0x00027B14
		protected override void Execute()
		{
			try
			{
				RenderStream renderStream = this.GetExecutionStrategy() as RenderStream;
				RSTrace.CatalogTrace.Assert(renderStream != null, "strategy");
				OnDemandProcessingResult onDemandProcessingResult;
				ExecutionResult executionResult = renderStream.ExecuteStrategy(out onDemandProcessingResult);
				base.ExecutionResult = executionResult;
				if (renderStream.SessionSnapshotUpdated)
				{
					this.StoreSessionData(renderStream, onDemandProcessingResult);
				}
			}
			finally
			{
				base.RequestInfo.Session.SessionReport.ThreadNoLongerUseThisSession();
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00029988 File Offset: 0x00027B88
		protected override RenderStrategyBase GetExecutionStrategy()
		{
			return new RenderStream(this);
		}
	}
}
