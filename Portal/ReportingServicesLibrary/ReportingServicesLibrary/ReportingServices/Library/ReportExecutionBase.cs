using System;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000113 RID: 275
	internal abstract class ReportExecutionBase : CancelableLibraryStep, IReportExecution
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x00028C73 File Offset: 0x00026E73
		protected ReportExecutionBase(IExecutionDataProvider provider, ExecutionParameters execInfo)
			: base(UrlFriendlyUIDGenerator.Create(), execInfo.ReportContext.ItemPath, JobActionEnum.Render, execInfo.JobType, provider.UserContext)
		{
			this.m_provider = provider;
			this.m_execInfo = execInfo;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00028CA6 File Offset: 0x00026EA6
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00028CAE File Offset: 0x00026EAE
		protected ExecutionResult ExecutionResult
		{
			get
			{
				return this.m_executionResult;
			}
			set
			{
				this.m_executionResult = value;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00028CB7 File Offset: 0x00026EB7
		public ExecutionResult ExecuteReport()
		{
			base.ExecuteWrapper();
			return this.ExecutionResult;
		}

		// Token: 0x06000AD2 RID: 2770
		protected abstract IStorageAccess EnterStorageContext();

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0000289C File Offset: 0x00000A9C
		public virtual void SetCacheTargetSnapshot(ReportSnapshot snapshot)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00028CC8 File Offset: 0x00026EC8
		protected override void Execute()
		{
			RSTrace.CatalogTrace.Assert(this.DataProvider != null, "DataProvider");
			RSTrace.CatalogTrace.Assert(this.RequestInfo != null, "m_execInfo");
			RSTrace.CatalogTrace.Assert(this.RequestInfo.Session != null, "m_execInfo.Session");
			RSTrace.CatalogTrace.Assert(!this.RequestInfo.Session.RedirectRequired, "redirect required");
			RSTrace.CatalogTrace.Assert(this.RequestInfo.ReportContext != null, "RequestInfo.ReportContext");
			RunningJobContext jobContext = Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext;
			RSTrace.CatalogTrace.Assert(jobContext != null, "jobContext");
			jobContext.ExecutionInfo.ExecutionId = this.RequestInfo.Session.SessionID;
			string formatParamValue = this.RequestInfo.ReportContext.RSRequestParameters.FormatParamValue;
			if (string.IsNullOrEmpty(formatParamValue))
			{
				throw new MissingParameterException("format");
			}
			jobContext.ExecutionInfo.Format = formatParamValue;
			try
			{
				this.ExecutionResult = this.InternalExecuteReport();
				if (this.DataProvider.StreamManager != null && this.DataProvider.StreamManager.PrimaryStream != null && !this.DataProvider.StreamManager.PrimaryStream.IsClosed)
				{
					jobContext.ExecutionInfo.ByteCount = this.DataProvider.StreamManager.PrimaryStream.Length;
				}
			}
			finally
			{
				this.WriteParametersToJobContext(jobContext);
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00028E48 File Offset: 0x00027048
		protected virtual void StoreSessionData(RenderStrategyBase strategy, OnDemandProcessingResult processingResult)
		{
			SessionReportItem.SaveAction sessionSaveFlags = strategy.SessionSaveFlags;
			SessionReportItem sessionReport = this.RequestInfo.Session.SessionReport;
			ReportItem report = sessionReport.Report;
			sessionReport.ProcessingResult = processingResult;
			if (strategy.RuntimeDataSources != null)
			{
				sessionReport.Datasources = strategy.RuntimeDataSources;
				sessionReport.DataSets = strategy.RuntimeSharedDataSets;
			}
			report.UserParams = this.RequestInfo.ReportContext.RSRequestParameters.ReportParametersXml;
			sessionReport.IsNewExecution = strategy is RenderLive;
			sessionReport.FoundInCache = strategy.WasExecutedFromCachedSnapshot;
			sessionReport.AwaitingFirstExecution = false;
			sessionReport.Timeout = Global.SessionTimeoutSeconds;
			if (processingResult != null)
			{
				sessionReport.AutoRefreshSeconds = processingResult.AutoRefresh;
				this.RequestInfo.Session.DontCache = processingResult.HasInteractivity;
				if (processingResult.EventInfoChanged)
				{
					sessionReport.EventInfo = processingResult.NewEventInfo;
				}
			}
			if (this.DataProvider.StreamManager != null && this.DataProvider.StreamManager.PersistedStreamManager != null)
			{
				this.DataProvider.StreamManager.PersistedStreamManager.AllStreamsFinished();
			}
			bool flag = this.WillAttemptDbCache(strategy) || this.RequestInfo.JobType.Type == JobTypeEnum.System;
			sessionReport.SnapshotTransactionFactory = strategy.SnapshotManager;
			sessionReport.Save(sessionSaveFlags, flag);
		}

		// Token: 0x06000AD6 RID: 2774
		protected abstract RenderStrategyBase GetExecutionStrategy();

		// Token: 0x06000AD7 RID: 2775
		protected abstract void TryDbCacheSnapshot(RenderStrategyBase strategy);

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000AD8 RID: 2776
		protected abstract bool ClearShowHideStateBeforeExecution { get; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00028F84 File Offset: 0x00027184
		public IExecutionDataProvider DataProvider
		{
			get
			{
				return this.m_provider;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00028F8C File Offset: 0x0002718C
		public ExecutionParameters RequestInfo
		{
			get
			{
				return this.m_execInfo;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00028F94 File Offset: 0x00027194
		public ReportProcessing ProcessingEngine
		{
			get
			{
				if (this.m_processingEngine == null)
				{
					this.m_processingEngine = this.DataProvider.CreateProcessingEngine();
				}
				return this.m_processingEngine;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00028FB5 File Offset: 0x000271B5
		public bool IsHistoryExecution
		{
			get
			{
				return this.RequestInfo.ReportContext.RSRequestParameters.SnapshotParamValue != null;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000ADD RID: 2781
		// (set) Token: 0x06000ADE RID: 2782
		public abstract ParameterInfoCollection EffectiveParameters { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000ADF RID: 2783
		public abstract ReportSnapshot IntermediateSnapshot { get; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000AE0 RID: 2784
		public abstract ItemProperties ReportProperties { get; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000AE1 RID: 2785
		public abstract bool ExecuteExistingSnapshot { get; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000AE2 RID: 2786
		public abstract string Description { get; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000AE3 RID: 2787
		public abstract DataSourceInfoCollection DataSources { get; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000AE4 RID: 2788
		public abstract DataSetInfoCollection DataSets { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000AE5 RID: 2789
		public abstract Guid ReportId { get; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000AE6 RID: 2790
		public abstract ReportSnapshot ExecutionSnapshot { get; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000AE7 RID: 2791
		// (set) Token: 0x06000AE8 RID: 2792
		public abstract DateTime ExecutionDateTime { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000AE9 RID: 2793
		public abstract bool CachingRequested { get; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000AEA RID: 2794
		// (set) Token: 0x06000AEB RID: 2795
		public abstract DateTime ExpirationDateTime { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000AEC RID: 2796
		public abstract bool FoundInCache { get; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000AED RID: 2797
		public abstract int PageCount { get; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000AEE RID: 2798
		public abstract PaginationMode PaginationMode { get; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000AEF RID: 2799
		public abstract UserProfileState UserDependencies { get; }

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00028FCF File Offset: 0x000271CF
		private bool WillAttemptDbCache(RenderStrategyBase strategy)
		{
			return strategy.TryCacheProcessingOutput && this.CachingRequested && !this.ExecuteExistingSnapshot;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00028FEC File Offset: 0x000271EC
		private void WriteParametersToJobContext(RunningJobContext jobContext)
		{
			try
			{
				if (this.EffectiveParameters != null)
				{
					if (this.EffectiveParameters.Count > 0)
					{
						jobContext.ExecutionInfo.Parameters = this.EffectiveParameters.ToUrl(false);
					}
				}
				else if (this.RequestInfo.ReportContext.RSRequestParameters.ReportParameters != null && this.RequestInfo.ReportContext.RSRequestParameters.ReportParameters.Count > 0)
				{
					jobContext.ExecutionInfo.Parameters = ParameterInfoCollection.ToUrl(this.RequestInfo.ReportContext.RSRequestParameters.ReportParameters);
				}
			}
			catch (InvalidParameterException ex)
			{
				jobContext.ExecutionInfo.Parameters = ex.Message;
				RSTrace.CatalogTrace.TraceException(TraceLevel.Warning, ex.ToString());
			}
			catch (Exception ex2)
			{
				RSTrace.CatalogTrace.TraceException(TraceLevel.Warning, ex2.ToString());
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000290D8 File Offset: 0x000272D8
		private ExecutionResult InternalExecuteReport()
		{
			ExecutionResult executionResult2;
			using (IStorageAccess storageAccess = this.EnterStorageContext())
			{
				RenderStrategyBase executionStrategy;
				ExecutionResult executionResult;
				try
				{
					executionStrategy = this.GetExecutionStrategy();
					RunningJobContext jobContext = Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext;
					jobContext.ExecutionInfo.Source = executionStrategy.ExecutionType;
					int reportTimeout = this.DataProvider.GetReportTimeout(this.ReportProperties);
					jobContext.SetTimeout(reportTimeout);
					OnDemandProcessingResult onDemandProcessingResult;
					executionResult = executionStrategy.ExecuteStrategy(out onDemandProcessingResult);
					this.StoreSessionData(executionStrategy, onDemandProcessingResult);
				}
				finally
				{
					this.RequestInfo.Session.SessionReport.ThreadNoLongerUseThisSession();
				}
				if (this.WillAttemptDbCache(executionStrategy) && !Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.ExecutionInfo.Format.Equals("SHAREDDATASETJSON", StringComparison.OrdinalIgnoreCase))
				{
					this.TryDbCacheSnapshot(executionStrategy);
				}
				if (storageAccess != null)
				{
					storageAccess.Commit();
				}
				executionResult2 = executionResult;
			}
			return executionResult2;
		}

		// Token: 0x040004A9 RID: 1193
		private readonly IExecutionDataProvider m_provider;

		// Token: 0x040004AA RID: 1194
		private readonly ExecutionParameters m_execInfo;

		// Token: 0x040004AB RID: 1195
		private ExecutionResult m_executionResult;

		// Token: 0x040004AC RID: 1196
		private ReportProcessing m_processingEngine;
	}
}
