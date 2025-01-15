using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200011A RID: 282
	internal class RenderFromSnapshot : RenderStrategyBase
	{
		// Token: 0x06000B5F RID: 2911 RVA: 0x0002A381 File Offset: 0x00028581
		public RenderFromSnapshot(ReportExecutionBase executionContext)
			: base(executionContext)
		{
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00005C88 File Offset: 0x00003E88
		public override RuntimeDataSourceInfoCollection RuntimeDataSources
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00005C88 File Offset: 0x00003E88
		public override RuntimeDataSetInfoCollection RuntimeSharedDataSets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002A38A File Offset: 0x0002858A
		public override ExecutionLogExecType ExecutionType
		{
			get
			{
				if (base.ExecutionContext.FoundInCache)
				{
					return ExecutionLogExecType.Cache;
				}
				return ExecutionLogExecType.Snapshot;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0002A39C File Offset: 0x0002859C
		public override bool WasExecutedFromCachedSnapshot
		{
			get
			{
				return base.ExecutionContext.FoundInCache && !this.NeedsReprocessing;
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002A3B8 File Offset: 0x000285B8
		protected override void PrepareForExecution()
		{
			RSTrace.CatalogTrace.Assert(this.OriginalSnapshot == null);
			this.SetSourceSnapshot();
			RSTrace.CatalogTrace.Assert(this.OriginalSnapshot != null, "OriginalSnapshot must be set after SetSourceSnapshot()");
			this.m_needsReprocessing = this.CheckNeedsReprocessing();
			if (this.NeedsReprocessing)
			{
				base.SnapshotManager.OriginalSnapshot = base.AllocateNewSnapshot(false, base.ExecutionContext.ExecutionDateTime, this.OriginalSnapshot.ProcessingFlags);
				base.PrepareForExecution(this.OriginalSnapshot, base.SnapshotManager.OriginalSnapshot);
				base.ExecutionContext.RequestInfo.Session.SessionReport.EventInfo = null;
				return;
			}
			base.SnapshotManager.OriginalSnapshot = this.OriginalSnapshot;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002A478 File Offset: 0x00028678
		protected override void PerformExceptionCleanup()
		{
			ReportSnapshot chunkTargetSnapshot = base.SnapshotManager.ChunkTargetSnapshot;
			if (this.m_didIncreaseRefcount)
			{
				RSTrace.CatalogTrace.Assert(this.OriginalSnapshot != null, "OriginalSnapshot");
				base.SnapshotManager.DecrementTransientRefCount(this.OriginalSnapshot);
			}
			if (this.OriginalSnapshot != chunkTargetSnapshot && chunkTargetSnapshot != null)
			{
				chunkTargetSnapshot.DeleteSnapshotAndChunks();
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002A4D4 File Offset: 0x000286D4
		protected override void PerformNormalCleanup()
		{
			bool foundInCache = base.ExecutionContext.FoundInCache;
			if (this.m_didIncreaseRefcount)
			{
				RSTrace.CatalogTrace.Assert(this.OriginalSnapshot != null, "OriginalSnapshot");
				RSTrace.CatalogTrace.Assert(base.SnapshotManager.OriginalSnapshot != null, "SnapshotManager.OriginalSnapshot");
				if (this.OriginalSnapshot != base.SnapshotManager.OriginalSnapshot)
				{
					base.SnapshotManager.DecrementTransientRefCount(this.OriginalSnapshot);
				}
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002A550 File Offset: 0x00028750
		protected override void CallProcessingAndRendering(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc, RenderingContext rc, out OnDemandProcessingResult result)
		{
			if (this.NeedsReprocessing)
			{
				using (ISnapshotTransaction snapshotTransaction = this.OriginalSnapshot.EnterTransactionContext())
				{
					result = this.DoProcessingAndRendering(pc, rc);
					snapshotTransaction.Commit();
					goto IL_003A;
				}
			}
			result = this.DoRendering(pc, rc);
			IL_003A:
			this.m_processingResult = result;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002A5B0 File Offset: 0x000287B0
		protected override void UpdateSnapshotMetadata(OnDemandProcessingResult processingResult)
		{
			if (this.NeedsReprocessing)
			{
				base.ExecutionContext.DataProvider.Storage.PromoteSnapshotInfo(base.SnapshotManager.ChunkTargetSnapshot, processingResult.NumberOfPages, processingResult.HasDocumentMap, processingResult.UpdatedPaginationMode, processingResult.UpdatedReportProcessingFlags);
				return;
			}
			base.ApplyUserDependency();
			bool flag = this.PaginationMode != PaginationMode.TotalPages && processingResult.NumberOfPages != 0;
			if (!flag)
			{
				RSTrace.CatalogTrace.Assert(processingResult.NumberOfPages == 0 || processingResult.NumberOfPages == this.PageCount, "page count for total pagination did not match previous total page count");
			}
			int num = (flag ? processingResult.NumberOfPages : this.PageCount);
			PaginationMode paginationMode = (flag ? processingResult.UpdatedPaginationMode : this.PaginationMode);
			if (num != this.PageCount || paginationMode != this.PaginationMode || processingResult.UpdatedReportProcessingFlags != base.SnapshotManager.ChunkTargetSnapshot.ProcessingFlags)
			{
				base.SnapshotManager.VersionSnapshot();
				base.ExecutionContext.DataProvider.Storage.PromoteSnapshotInfo(base.SnapshotManager.ChunkTargetSnapshot, num, processingResult.HasDocumentMap, paginationMode, processingResult.UpdatedReportProcessingFlags);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0002A6CA File Offset: 0x000288CA
		protected override RenderStrategyBase.ProcessOrRender ProcessRenderRequirements
		{
			get
			{
				return RenderStrategyBase.ProcessOrRender.Render | RenderStrategyBase.ProcessOrRender.Process;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002A6CD File Offset: 0x000288CD
		public override SessionReportItem.SaveAction SessionSaveFlags
		{
			get
			{
				return this.InternalSessionSaveFlags();
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00005BEF File Offset: 0x00003DEF
		public override bool TryCacheProcessingOutput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002A6D8 File Offset: 0x000288D8
		protected override ReportRenderingResult TryHitLocalCache()
		{
			ReportSnapshot chunkTargetSnapshot = base.SnapshotManager.ChunkTargetSnapshot;
			ReportRenderingResult cachedResult = RSLocalCacheManager.Current.GetCachedResult(base.ExecutionContext.RequestInfo.ReportContext, null, chunkTargetSnapshot);
			if (cachedResult != null)
			{
				ExecTrace.TraceVerbose("Using cached result");
				StreamManager streamManager = base.ExecutionContext.DataProvider.StreamManager;
				streamManager.SetPrimaryStream(cachedResult.Stream);
				streamManager.SetSecondaryStreamNames(cachedResult.SecondaryStreamNames);
				foreach (string text in cachedResult.SecondaryCacheableStreamNames)
				{
					ReportRenderingResult cachedResult2 = RSLocalCacheManager.Current.GetCachedResult(base.ExecutionContext.RequestInfo.ReportContext, text, chunkTargetSnapshot);
					if (cachedResult2 != null)
					{
						streamManager.AddSecondaryStream(cachedResult2.Stream);
					}
				}
				base.ExecutionContext.EffectiveParameters = cachedResult.EffectiveParameters;
			}
			else
			{
				ExecTrace.TraceVerbose("Could not find cached result");
			}
			return cachedResult;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override UserProfileState ProcessingInitialUserProfileState
		{
			get
			{
				return UserProfileState.None;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002A7B4 File Offset: 0x000289B4
		protected override bool CanLocalCacheRenderedOutput
		{
			get
			{
				return this.IsSharedSnapshot;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0002A7BC File Offset: 0x000289BC
		protected override bool IsSharedSnapshot
		{
			get
			{
				return !this.NeedsReprocessing;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002A7B4 File Offset: 0x000289B4
		protected override bool UpdateSnapshotOnChange
		{
			get
			{
				return this.IsSharedSnapshot;
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002A7C7 File Offset: 0x000289C7
		protected virtual bool CheckNeedsReprocessing()
		{
			return !this.CheckParameters() || this.OriginalSnapshot.DependsOnUser;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002A7DE File Offset: 0x000289DE
		protected virtual void SetSourceSnapshot()
		{
			this.OriginalSnapshot = base.ExecutionContext.ExecutionSnapshot;
			this.OriginalSnapshot.IncreaseTransientRefcount();
			this.m_didIncreaseRefcount = true;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002A803 File Offset: 0x00028A03
		protected ReportProcessing ProcessingEngine
		{
			get
			{
				return base.ExecutionContext.ProcessingEngine;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002A810 File Offset: 0x00028A10
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0002A818 File Offset: 0x00028A18
		protected ReportSnapshot OriginalSnapshot
		{
			get
			{
				return this.m_originalSnapshot;
			}
			set
			{
				this.m_originalSnapshot = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002A821 File Offset: 0x00028A21
		protected bool NeedsReprocessing
		{
			get
			{
				return this.m_needsReprocessing;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002A82C File Offset: 0x00028A2C
		private OnDemandProcessingResult DoProcessingAndRendering(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc, RenderingContext rc)
		{
			RSTrace.CatalogTrace.Assert(pc != null, "pc");
			RSTrace.CatalogTrace.Assert(rc != null, "rc");
			ReportSnapshot chunkTargetSnapshot = base.SnapshotManager.ChunkTargetSnapshot;
			RSTrace.CatalogTrace.Assert(this.OriginalSnapshot != chunkTargetSnapshot, "OriginalSnapshot != targetSnapshot");
			IChunkFactory chunkFactory = ReadOnlyChunkFactory.FromSnapshot(this.OriginalSnapshot);
			bool flag = false;
			pc.ChunkFactory = chunkFactory;
			base.ExecutionContext.ProcessingEngine.ProcessReportParameters(base.ExecutionContext.ExecutionDateTime, pc, true, out flag);
			RSTrace.CatalogTrace.Assert(pc.Parameters == base.ExecutionContext.EffectiveParameters, "pc.Parameters == ExecutionContext.EffectiveParameters");
			base.ExecutionContext.EffectiveParameters.ThrowIfNotValid();
			RSTrace.CatalogTrace.Assert(!base.SnapshotManager.SnapshotVersioningEnabled, "versioning is enabled during reprocessing");
			pc.ChunkFactory = base.SnapshotManager;
			return base.ExecutionContext.ProcessingEngine.ProcessAndRenderSnapshot(pc, rc, this.OriginalSnapshot);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002A934 File Offset: 0x00028B34
		private OnDemandProcessingResult DoRendering(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc, RenderingContext rc)
		{
			RSTrace.CatalogTrace.Assert(pc != null, "pc");
			RSTrace.CatalogTrace.Assert(rc != null, "rc");
			RSTrace.CatalogTrace.Assert(pc.ChunkFactory == base.SnapshotManager, "pc.ChunkFactory == SnapshotManager");
			return base.ExecutionContext.ProcessingEngine.RenderSnapshot(rc, pc);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002A998 File Offset: 0x00028B98
		private SessionReportItem.SaveAction InternalSessionSaveFlags()
		{
			SessionReportItem.SaveAction saveAction = SessionReportItem.SaveAction.SaveSession;
			if (this.m_processingResult != null && this.m_processingResult.SnapshotChanged)
			{
				saveAction = SessionReportItem.SaveAction.SaveSnapshot;
			}
			return saveAction;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002A9C0 File Offset: 0x00028BC0
		private bool CheckParameters()
		{
			ParameterInfoCollection snapshotParameters;
			using (ISnapshotTransaction snapshotTransaction = this.OriginalSnapshot.EnterTransactionContext())
			{
				snapshotParameters = this.ProcessingEngine.GetSnapshotParameters(this.OriginalSnapshot);
				snapshotTransaction.Commit();
			}
			ParameterInfoCollection effectiveParams = base.ExecutionContext.RequestInfo.Session.SessionReport.Report.EffectiveParams;
			NameValueCollection reportParameters = base.ExecutionContext.RequestInfo.ReportContext.RSRequestParameters.ReportParameters;
			bool flag2;
			if (effectiveParams != null)
			{
				bool flag;
				snapshotParameters.SameParameters(effectiveParams, out flag, out flag2);
				if (!flag2)
				{
					base.ExecutionContext.EffectiveParameters = ParameterInfoCollection.Combine(snapshotParameters, effectiveParams, false, true, false, false, Localization.ReportParameterCulture);
				}
				else
				{
					base.ExecutionContext.EffectiveParameters = snapshotParameters;
				}
			}
			else
			{
				flag2 = snapshotParameters.SameSnapshotParameters(reportParameters);
				if (!flag2)
				{
					ParameterInfoCollection parameterInfoCollection = ParameterInfoCollection.DecodeFromNameValueCollectionAndUserCulture(reportParameters);
					base.ExecutionContext.EffectiveParameters = ParameterInfoCollection.Combine(snapshotParameters, parameterInfoCollection, true, true, false, false, Localization.ReportParameterCulture);
				}
				else
				{
					base.ExecutionContext.EffectiveParameters = snapshotParameters;
				}
			}
			RSTrace.CatalogTrace.Assert(base.ExecutionContext.EffectiveParameters != null, "EffectiveParameters");
			return flag2;
		}

		// Token: 0x040004B6 RID: 1206
		private bool m_needsReprocessing;

		// Token: 0x040004B7 RID: 1207
		protected bool m_didIncreaseRefcount;

		// Token: 0x040004B8 RID: 1208
		private ReportSnapshot m_originalSnapshot;

		// Token: 0x040004B9 RID: 1209
		protected OnDemandProcessingResult m_processingResult;
	}
}
