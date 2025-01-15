using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200011E RID: 286
	internal class RenderLive : RenderStrategyBase
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x0002A381 File Offset: 0x00028581
		public RenderLive(ReportExecutionBase executionContext)
			: base(executionContext)
		{
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002AD4D File Offset: 0x00028F4D
		public override RuntimeDataSourceInfoCollection RuntimeDataSources
		{
			get
			{
				return this.m_runtimeDataSources;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0002AD55 File Offset: 0x00028F55
		public override RuntimeDataSetInfoCollection RuntimeSharedDataSets
		{
			get
			{
				return this.m_runtimeSharedDataSets;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002AD60 File Offset: 0x00028F60
		public override SessionReportItem.SaveAction SessionSaveFlags
		{
			get
			{
				if (base.ExecutionContext.RequestInfo.JobType.Type == JobTypeEnum.System && !base.ExecutionContext.CachingRequested && !base.ExecutionContext.DataProvider.StreamManager.HasSecondaryStreams)
				{
					return SessionReportItem.SaveAction.SaveSession;
				}
				return SessionReportItem.SaveAction.SaveSnapshot;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool TryCacheProcessingOutput
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0002ADAC File Offset: 0x00028FAC
		public override ExecutionLogExecType ExecutionType
		{
			get
			{
				if (!base.ExecutionContext.RequestInfo.Session.SessionReport.IsAdhocReport)
				{
					return ExecutionLogExecType.Live;
				}
				return ExecutionLogExecType.AdHoc;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002ADCD File Offset: 0x00028FCD
		public override bool WasExecutedFromCachedSnapshot
		{
			get
			{
				return base.SnapshotWasCached;
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002ADD8 File Offset: 0x00028FD8
		protected override void CallProcessingAndRendering(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc, RenderingContext rc, out OnDemandProcessingResult result)
		{
			result = null;
			RSTrace.CatalogTrace.Assert(pc.ChunkFactory == base.SnapshotManager, "pc.ChunkFactory == SnapshotManager");
			RSTrace.CatalogTrace.Assert(base.ExecutionContext.IntermediateSnapshot != null, "IntermediateSnapshot");
			using (base.ExecutionContext.IntermediateSnapshot.EnterTransactionContext())
			{
				ReadOnlyChunkFactory readOnlyChunkFactory = ReadOnlyChunkFactory.FromSnapshot(base.ExecutionContext.IntermediateSnapshot);
				result = base.ExecutionContext.ProcessingEngine.RenderReport(base.ExecutionContext.ExecutionDateTime, pc, rc, readOnlyChunkFactory);
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002AE88 File Offset: 0x00029088
		protected override void UpdateSnapshotMetadata(OnDemandProcessingResult processingResult)
		{
			RSTrace.CatalogTrace.Assert(processingResult != null, "processingResult");
			base.ExecutionContext.DataProvider.Storage.PromoteSnapshotInfo(base.SnapshotManager.ChunkTargetSnapshot, processingResult.NumberOfPages, processingResult.HasDocumentMap, processingResult.UpdatedPaginationMode, processingResult.UpdatedReportProcessingFlags);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002AEE0 File Offset: 0x000290E0
		protected virtual void PrepareExecutionSnapshot()
		{
			base.SnapshotManager.OriginalSnapshot = base.AllocateNewSnapshot(false, base.ExecutionContext.ExecutionDateTime, base.ExecutionContext.IntermediateSnapshot.ProcessingFlags);
			base.PrepareForExecution(base.ExecutionContext.IntermediateSnapshot, base.SnapshotManager.OriginalSnapshot);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002AF38 File Offset: 0x00029138
		protected override void PrepareForExecution()
		{
			RSTrace.CatalogTrace.Assert(base.ExecutionContext.IntermediateSnapshot != null);
			RSTrace.CatalogTrace.Assert(base.ExecutionContext.RequestInfo.Session.SessionReport != null);
			base.ExecutionContext.ExecutionDateTime = DateTime.Now;
			this.GetRuntimeDataSourcesAndSharedDataSets(out this.m_runtimeDataSources, out this.m_runtimeSharedDataSets);
			this.PrepareExecutionSnapshot();
			bool flag = (base.ExecutionContext.UserDependencies & UserProfileState.InQuery) == UserProfileState.InQuery;
			bool flag2 = (base.ExecutionContext.UserDependencies & UserProfileState.InReport) == UserProfileState.InReport;
			RunningJobContext jobContext = Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext;
			if (jobContext.Type == JobTypeEnum.System)
			{
				if (jobContext.SubType != JobSubTypeEnum.Subscription && (flag || flag2))
				{
					throw new HasUserProfileDependenciesException(base.ExecutionContext.RequestInfo.ReportContext.OriginalItemPath.Value);
				}
				if (!DataSourceCatalogItem.GoodForUnattendedExecution(this.m_runtimeDataSources))
				{
					throw new InvalidDataSourceCredentialSettingException();
				}
			}
			this.m_runtimeDataSources.SetCredentials(base.ExecutionContext.RequestInfo.ReportContext.RSRequestParameters.DatasourcesCred, DataProtection.Instance);
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0002A6CA File Offset: 0x000288CA
		protected override RenderStrategyBase.ProcessOrRender ProcessRenderRequirements
		{
			get
			{
				return RenderStrategyBase.ProcessOrRender.Render | RenderStrategyBase.ProcessOrRender.Process;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00005C88 File Offset: 0x00003E88
		protected override ReportRenderingResult TryHitLocalCache()
		{
			return null;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002B043 File Offset: 0x00029243
		protected override bool CanLocalCacheRenderedOutput
		{
			get
			{
				return base.ExecutionContext.CachingRequested;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002B050 File Offset: 0x00029250
		protected override void PerformExceptionCleanup()
		{
			RSTrace.CatalogTrace.Assert(base.SnapshotManager.UpdatedSnapshot == null, "SnapshotManager.UpdatedSnapshot != null in render live");
			if (base.SnapshotManager.OriginalSnapshot != null)
			{
				base.SnapshotManager.OriginalSnapshot.DeleteSnapshotAndChunks();
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002B08C File Offset: 0x0002928C
		protected override void PerformNormalCleanup()
		{
			RSTrace.CatalogTrace.Assert(base.SnapshotManager.UpdatedSnapshot == null, "SnapshotManager.UpdatedSnapshot != null in render live");
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0002B0AB File Offset: 0x000292AB
		protected override UserProfileState ProcessingInitialUserProfileState
		{
			get
			{
				return base.ExecutionContext.UserDependencies;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002B043 File Offset: 0x00029243
		protected override bool IsSharedSnapshot
		{
			get
			{
				return base.ExecutionContext.CachingRequested;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override bool UpdateSnapshotOnChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002B0B8 File Offset: 0x000292B8
		private void GetRuntimeDataSourcesAndSharedDataSets(out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets)
		{
			base.ExecutionContext.DataProvider.GetAllRuntimeDataSourcesAndDataSets(base.ExecutionContext.RequestInfo.Session, base.ExecutionContext.ProcessingEngine, base.ExecutionContext.RequestInfo.ReportContext, base.ExecutionContext.IntermediateSnapshot, base.ExecutionContext.DataSources, base.ExecutionContext.DataSets, out runtimeDataSources, out runtimeDataSets);
		}

		// Token: 0x040004BB RID: 1211
		private RuntimeDataSourceInfoCollection m_runtimeDataSources;

		// Token: 0x040004BC RID: 1212
		private RuntimeDataSetInfoCollection m_runtimeSharedDataSets;
	}
}
