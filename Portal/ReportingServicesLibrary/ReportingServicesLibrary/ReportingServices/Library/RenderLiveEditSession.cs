using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000120 RID: 288
	internal class RenderLiveEditSession : RenderLive
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002B316 File Offset: 0x00029516
		public RenderLiveEditSession(ReportExecutionBase executionContext)
			: base(executionContext)
		{
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002B31F File Offset: 0x0002951F
		protected override void CallProcessingAndRendering(ProcessingContext pc, RenderingContext rc, out OnDemandProcessingResult result)
		{
			if (this.RuntimeDataSources.GoodForDataCaching())
			{
				result = this.RenderAndCacheData(pc, rc);
				return;
			}
			ExecTrace.TraceVerbose("Rendering without caching in edit session due to Data Source restrictions");
			result = this.RenderWithoutCaching(pc, rc);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002B350 File Offset: 0x00029550
		private OnDemandProcessingResult RenderWithoutCaching(ProcessingContext pc, RenderingContext rc)
		{
			OnDemandProcessingResult onDemandProcessingResult;
			base.CallProcessingAndRendering(pc, rc, out onDemandProcessingResult);
			return onDemandProcessingResult;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002B368 File Offset: 0x00029568
		private OnDemandProcessingResult RenderAndCacheData(ProcessingContext pc, RenderingContext rc)
		{
			ReportSnapshot reportSnapshot = base.AllocateNewSnapshot(false, base.ExecutionContext.ExecutionDateTime, base.ExecutionContext.IntermediateSnapshot.ProcessingFlags);
			ReadOnlyChunkFactory readOnlyChunkFactory = ReadOnlyChunkFactory.FromSnapshot(base.ExecutionContext.IntermediateSnapshot);
			base.SnapshotManager.ChunkTargetSnapshot.ShareTransactionContext(reportSnapshot);
			OnDemandProcessingResult onDemandProcessingResult = base.ExecutionContext.ProcessingEngine.RenderReportAndCacheData(base.ExecutionContext.ExecutionDateTime, pc, rc, reportSnapshot, readOnlyChunkFactory);
			base.SnapshotManager.ChunkTargetSnapshot.CopyDataChunksTo(reportSnapshot, base.SnapshotManager.ChunkTargetSnapshot.ConnectionManager);
			base.ExecutionContext.SetCacheTargetSnapshot(reportSnapshot);
			return onDemandProcessingResult;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool TryCacheProcessingOutput
		{
			get
			{
				return true;
			}
		}
	}
}
