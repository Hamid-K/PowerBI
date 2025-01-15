using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200011F RID: 287
	internal class RenderLiveCachedData : RenderFromSnapshot
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0002AAE4 File Offset: 0x00028CE4
		public RenderLiveCachedData(ReportExecutionBase executionContext)
			: base(executionContext)
		{
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override bool IsSharedSnapshot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override bool UpdateSnapshotOnChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002B124 File Offset: 0x00029324
		protected override void PrepareForExecution()
		{
			RSTrace.CatalogTrace.Assert(base.OriginalSnapshot == null);
			base.ExecutionContext.ExecutionDateTime = DateTime.Now;
			base.SnapshotManager.OriginalSnapshot = base.AllocateNewSnapshot(false, base.ExecutionContext.ExecutionDateTime, base.ExecutionContext.IntermediateSnapshot.ProcessingFlags);
			base.PrepareForExecution(base.ExecutionContext.IntermediateSnapshot, base.SnapshotManager.OriginalSnapshot);
			base.ExecutionContext.ExecutionSnapshot.CopyDataChunksTo(base.SnapshotManager.OriginalSnapshot, null);
			base.ExecutionContext.DataProvider.Storage.Commit();
			base.ExecutionContext.DataProvider.Storage.SetCacheLastUsed(base.ExecutionContext.ExecutionSnapshot);
			base.ExecutionContext.DataProvider.Storage.Commit();
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002B204 File Offset: 0x00029404
		protected override void CallProcessingAndRendering(ProcessingContext pc, RenderingContext rc, out OnDemandProcessingResult result)
		{
			try
			{
				using (base.ExecutionContext.ExecutionSnapshot.EnterTransactionContext())
				{
					ReadOnlyChunkFactory readOnlyChunkFactory = ReadOnlyChunkFactory.FromSnapshot(base.ExecutionContext.ExecutionSnapshot);
					result = base.ExecutionContext.ProcessingEngine.RenderReportWithCachedData(base.ExecutionContext.ExecutionDateTime, pc, rc, readOnlyChunkFactory);
					this.m_processingResult = result;
				}
			}
			catch (ReportProcessing.DataCacheUnavailableException ex)
			{
				ExecTrace.TraceWarning("Failed to use cached data set for edit session {0}.", new object[] { base.ExecutionContext.RequestInfo.ReportContext.ItemPath.FullEditSessionIdentifier });
				base.ExecutionContext.DataProvider.FlushCache(base.ExecutionContext.ReportId, false);
				throw new DataCacheInvalidException(ex);
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002B2DC File Offset: 0x000294DC
		protected override void UpdateSnapshotMetadata(OnDemandProcessingResult processingResult)
		{
			base.ExecutionContext.DataProvider.Storage.PromoteSnapshotInfo(base.SnapshotManager.ChunkTargetSnapshot, processingResult.NumberOfPages, processingResult.HasDocumentMap, processingResult.UpdatedPaginationMode, processingResult.UpdatedReportProcessingFlags);
		}
	}
}
