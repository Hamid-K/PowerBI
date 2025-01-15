using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000119 RID: 281
	internal abstract class RenderStrategyBase
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x00029990 File Offset: 0x00027B90
		public virtual ExecutionResult ExecuteStrategy(out OnDemandProcessingResult processingResult)
		{
			Microsoft.ReportingServices.ReportProcessing.ProcessingContext processingContext = null;
			RenderingContext renderingContext = null;
			processingResult = null;
			List<Warning> list = new List<Warning>();
			try
			{
				this.PrepareForExecution();
				RSTrace.CatalogTrace.Assert(this.ExecutionContext.RequestInfo.Session.SessionReport.Report != null);
				this.ExecutionContext.RequestInfo.Session.SessionReport.Report.Description = this.ExecutionContext.Description;
				this.SnapshotManager.SnapshotVersioningEnabled = this.UpdateSnapshotOnChange;
				if (this.SnapshotManager.SnapshotVersioningEnabled)
				{
					this.SnapshotManager.EnrollInPersistedEvent(this.ExecutionContext.RequestInfo.Session.SessionReport);
				}
				this.LockSession();
				ImpersonationContext impersonationContext = null;
				ISubreportRetrieval subreportRetrieval = null;
				try
				{
					if ((this.ProcessRenderRequirements & RenderStrategyBase.ProcessOrRender.Process) == RenderStrategyBase.ProcessOrRender.Process)
					{
						impersonationContext = this.BeginProcessingContext(out processingContext, out subreportRetrieval);
					}
					if ((this.ProcessRenderRequirements & RenderStrategyBase.ProcessOrRender.Render) == RenderStrategyBase.ProcessOrRender.Render)
					{
						renderingContext = this.GenerateRenderingContext();
					}
					ReportRenderingResult reportRenderingResult = this.TryHitLocalCache();
					if (reportRenderingResult == null)
					{
						if (this.ExecutionContext.DataProvider.StreamManager != null)
						{
							this.ExecutionContext.DataProvider.StreamManager.NeedCacheableStreams = true;
						}
						using (ISnapshotTransaction snapshotTransaction = this.SnapshotManager.EnterTransactionContext())
						{
							this.CallProcessingAndRendering(processingContext, renderingContext, out processingResult);
							snapshotTransaction.Commit();
							RSTrace.CatalogTrace.Assert(processingResult != null, "processingResult");
						}
						this.UsedUserProfile = processingResult.UsedUserProfileState;
						if (processingContext != null)
						{
							ReportProcessingContext reportProcessingContext = processingContext as ReportProcessingContext;
							ISharedDataSet sharedDataSet = ((reportProcessingContext != null) ? reportProcessingContext.DataSetExecute : null);
							if (sharedDataSet != null && sharedDataSet.HasUserDependencies)
							{
								this.UsedUserProfile |= UserProfileState.InQuery;
							}
						}
						this.UpdateSnapshotMetadata(processingResult);
						SafeMethods.AddRange<Warning>(list, Warning.ProcessingMessagesToWarningArray(processingResult.Warnings));
					}
					else
					{
						RSTrace.CatalogTrace.Assert(processingResult == null);
						SafeMethods.AddRange<Warning>(list, reportRenderingResult.Warnings);
					}
					this.SetExecutionInfoInSession();
					if (reportRenderingResult == null)
					{
						bool flag = false;
						if (this.CanLocalCacheRenderedOutput)
						{
							flag = this.TryCacheRenderedOuput(processingResult);
						}
						if (!flag)
						{
							this.TryCacheSecondaryStreams(processingResult);
						}
					}
					this.ExecutionContext.DataProvider.Storage.Commit();
				}
				finally
				{
					if (impersonationContext != null)
					{
						impersonationContext.Dispose();
					}
					if (subreportRetrieval != null)
					{
						subreportRetrieval.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				RSTrace.CatalogTrace.TraceException(TraceLevel.Warning, ex.ToString());
				try
				{
					this.PerformExceptionCleanup();
				}
				catch (Exception ex2)
				{
					RSTrace.CatalogTrace.TraceException(TraceLevel.Error, "Error performing exception cleanup: " + ex2.ToString());
				}
				throw;
			}
			this.PerformNormalCleanup();
			ExecutionResult executionResult = new ExecutionResult();
			if (list.Count > 0)
			{
				executionResult.Warnings = list.ToArray();
			}
			else
			{
				executionResult.Warnings = null;
			}
			executionResult.EffectiveParameters = this.ExecutionContext.EffectiveParameters;
			if (this.ExecutionContext.DataProvider.StreamManager != null)
			{
				executionResult.OutputStream = this.ExecutionContext.DataProvider.StreamManager.PrimaryStream;
				executionResult.StreamIds = this.ExecutionContext.DataProvider.StreamManager.SecondaryStreamNames;
			}
			return executionResult;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00029CE8 File Offset: 0x00027EE8
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x00029CF0 File Offset: 0x00027EF0
		public UserProfileState UsedUserProfile
		{
			get
			{
				return this.m_usedUserProfile;
			}
			set
			{
				this.m_usedUserProfile = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00029CF9 File Offset: 0x00027EF9
		public SnapshotManager SnapshotManager
		{
			get
			{
				return this.m_snapshotManager;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00029D01 File Offset: 0x00027F01
		public virtual int PageCount
		{
			get
			{
				return this.ExecutionContext.PageCount;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00029D0E File Offset: 0x00027F0E
		public virtual PaginationMode PaginationMode
		{
			get
			{
				return this.ExecutionContext.PaginationMode;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00029D1B File Offset: 0x00027F1B
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x00029D23 File Offset: 0x00027F23
		public bool SnapshotWasCached
		{
			get
			{
				return this.m_snapshotWasCached;
			}
			set
			{
				this.m_snapshotWasCached = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000B3D RID: 2877
		public abstract SessionReportItem.SaveAction SessionSaveFlags { get; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000B3E RID: 2878
		public abstract RuntimeDataSourceInfoCollection RuntimeDataSources { get; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000B3F RID: 2879
		public abstract RuntimeDataSetInfoCollection RuntimeSharedDataSets { get; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000B40 RID: 2880
		public abstract bool TryCacheProcessingOutput { get; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000B41 RID: 2881
		public abstract ExecutionLogExecType ExecutionType { get; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000B42 RID: 2882
		public abstract bool WasExecutedFromCachedSnapshot { get; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000B43 RID: 2883
		protected abstract UserProfileState ProcessingInitialUserProfileState { get; }

		// Token: 0x06000B44 RID: 2884
		protected abstract void PrepareForExecution();

		// Token: 0x06000B45 RID: 2885
		protected abstract ReportRenderingResult TryHitLocalCache();

		// Token: 0x06000B46 RID: 2886
		protected abstract void CallProcessingAndRendering(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc, RenderingContext rc, out OnDemandProcessingResult result);

		// Token: 0x06000B47 RID: 2887
		protected abstract void UpdateSnapshotMetadata(OnDemandProcessingResult processingResult);

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000B48 RID: 2888
		protected abstract RenderStrategyBase.ProcessOrRender ProcessRenderRequirements { get; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000B49 RID: 2889
		protected abstract bool CanLocalCacheRenderedOutput { get; }

		// Token: 0x06000B4A RID: 2890
		protected abstract void PerformExceptionCleanup();

		// Token: 0x06000B4B RID: 2891
		protected abstract void PerformNormalCleanup();

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000B4C RID: 2892
		protected abstract bool UpdateSnapshotOnChange { get; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000B4D RID: 2893
		protected abstract bool IsSharedSnapshot { get; }

		// Token: 0x06000B4E RID: 2894 RVA: 0x00029D2C File Offset: 0x00027F2C
		protected void ApplyUserDependency()
		{
			if ((this.UsedUserProfile & UserProfileState.InQuery) == UserProfileState.InQuery || (this.UsedUserProfile & UserProfileState.InReport) == UserProfileState.InReport)
			{
				if (this.SnapshotManager.ChunkTargetSnapshot.DependsOnUser)
				{
					return;
				}
				if (this.IsSharedSnapshot)
				{
					this.SnapshotManager.VersionSnapshot();
					this.SnapshotManager.ChunkTargetSnapshot.MarkAsDependentOnUser();
				}
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00029D88 File Offset: 0x00027F88
		protected RenderingContext GenerateRenderingContext()
		{
			RSTrace.CatalogTrace.Assert(this.SnapshotManager.OriginalSnapshot != null, "OriginalSnapshot");
			ExecutionParameters requestInfo = this.ExecutionContext.RequestInfo;
			IExecutionDataProvider dataProvider = this.ExecutionContext.DataProvider;
			SessionReportItem sessionReport = requestInfo.Session.SessionReport;
			RSTrace.CatalogTrace.Assert(sessionReport != null, "SessionReport");
			RSTrace.CatalogTrace.Assert(sessionReport.Report != null, "SessionReport.Report");
			int num = this.PageCount;
			RSTrace.CatalogTrace.Assert(num >= 0, "pageCount >= 0");
			if (this.PaginationMode != PaginationMode.TotalPages)
			{
				num = -num;
			}
			ServerParameterStore serverParameterStore = dataProvider.CreateParameterStore();
			return new RenderingContext(requestInfo.ReportContext, requestInfo.Session.SessionReport.Report.Description, requestInfo.Session.SessionReport.EventInfo, ReportRuntimeSetup.GetDefault(), new ReportProcessing.StoreServerParameters(serverParameterStore.Store), this.GetUserProfileState(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.JobType), this.RequestedPaginationMode, num);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00029E88 File Offset: 0x00028088
		protected ImpersonationContext BeginProcessingContext(out Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc, out ISubreportRetrieval subreportRetrieval)
		{
			ImpersonationContext impersonationContext = null;
			try
			{
				ReportProcessing.ExecutionType executionType;
				impersonationContext = SurrogateContextFactory.CreateContext(out executionType);
				pc = this.GenerateProcessingContext(executionType, out subreportRetrieval);
			}
			catch (Exception)
			{
				if (impersonationContext != null)
				{
					impersonationContext.Dispose();
				}
				throw;
			}
			return impersonationContext;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00029EC8 File Offset: 0x000280C8
		protected RenderStrategyBase(ReportExecutionBase executionContext)
		{
			this.m_executionContext = executionContext;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00029EE2 File Offset: 0x000280E2
		protected ReportExecutionBase ExecutionContext
		{
			get
			{
				return this.m_executionContext;
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00029EEA File Offset: 0x000280EA
		protected ReportSnapshot AllocateNewSnapshot(bool isPermanent, DateTime createdDate, ReportProcessingFlags processingFlags)
		{
			ReportSnapshot reportSnapshot = ReportSnapshot.Create(isPermanent, processingFlags);
			reportSnapshot.WriteNewSnapshotToDB(this.ExecutionContext.EffectiveParameters, createdDate, this.ExecutionContext.Description);
			return reportSnapshot;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00029F10 File Offset: 0x00028110
		protected void PrepareForExecution(ReportSnapshot source, ReportSnapshot target)
		{
			source.PrepareExecutionSnapshot(target, null);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00029F1A File Offset: 0x0002811A
		protected void LockSession()
		{
			this.ExecutionContext.RequestInfo.Session.SessionReport.WriteLockSession();
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00029F38 File Offset: 0x00028138
		protected void SetExecutionInfoInSession()
		{
			SessionReportItem sri = this.ExecutionContext.RequestInfo.Session.SessionReport;
			sri.Report.SnapshotData = this.SnapshotManager.ChunkTargetSnapshot;
			if (this.SnapshotManager.SnapshotVersioningEnabled)
			{
				this.SnapshotManager.SnapshotUpdated += delegate(object sender, SnapshotUpdatedEventArgs e)
				{
					sri.Report.SnapshotData = e.NewSnapshot;
				};
			}
			sri.Report.EffectiveParams = this.ExecutionContext.EffectiveParameters;
			sri.ExecutionDateTime = this.ExecutionContext.ExecutionDateTime;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00029FD6 File Offset: 0x000281D6
		protected virtual PaginationMode RequestedPaginationMode
		{
			get
			{
				return RenderStrategyBase.GetPaginationModeFromContext(this.ExecutionContext.RequestInfo.ReportContext);
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00029FF0 File Offset: 0x000281F0
		private bool TryCacheRenderedOuput(OnDemandProcessingResult renderResult)
		{
			ReportSnapshot chunkTargetSnapshot = this.SnapshotManager.ChunkTargetSnapshot;
			RSTrace.CatalogTrace.Assert(chunkTargetSnapshot != null, "targetSnapshot");
			if (chunkTargetSnapshot.DependsOnUser)
			{
				ExecTrace.TraceVerbose("Target Snapshot depends on user... will not add to local cache.");
			}
			else if (renderResult.HasInteractivity)
			{
				ExecTrace.TraceVerbose("Rendered result has interactive elements... will not add to local cache.");
			}
			else
			{
				if ((this.UsedUserProfile & UserProfileState.OnDemandExpressions) != UserProfileState.OnDemandExpressions)
				{
					ExecTrace.TraceVerbose("Adding rendering result to local cache...");
					ReportRenderingResult reportRenderingResult = new ReportRenderingResult(renderResult, this.ExecutionContext.EffectiveParameters, this.ExecutionContext.RequestInfo.Session.SessionReport.Report, this.ExecutionContext.ExecutionDateTime, this.ExecutionContext.DataProvider.StreamManager);
					RSLocalCacheManager.Current.CacheRenderedResult(this.ExecutionContext.RequestInfo.ReportContext, this.ExecutionContext.DataProvider.StreamManager, reportRenderingResult, this.ReportExpirationDateTime, chunkTargetSnapshot);
					return true;
				}
				ExecTrace.TraceVerbose("Rendered result had OnDemandExpression user profile dependency... will not add to local cache.");
			}
			return false;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002A0EC File Offset: 0x000282EC
		private void TryCacheSecondaryStreams(OnDemandProcessingResult processingResult)
		{
			ExecTrace.TraceVerbose("Caching secondary streams...");
			ReportRenderingResult reportRenderingResult = new ReportRenderingResult(processingResult, this.ExecutionContext.EffectiveParameters, this.ExecutionContext.RequestInfo.Session.SessionReport.Report, this.ExecutionContext.ExecutionDateTime, this.ExecutionContext.DataProvider.StreamManager);
			RSLocalCacheManager.Current.CacheOnlySecondaryStreams(this.ExecutionContext.RequestInfo.ReportContext, this.ExecutionContext.DataProvider.StreamManager, reportRenderingResult, this.ReportExpirationDateTime, this.SnapshotManager.ChunkTargetSnapshot);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002A188 File Offset: 0x00028388
		private Microsoft.ReportingServices.ReportProcessing.ProcessingContext GenerateProcessingContext(ReportProcessing.ExecutionType execType, out ISubreportRetrieval subreportRetrieval)
		{
			RSTrace.CatalogTrace.Assert(this.SnapshotManager != null, "SnapshotManager");
			subreportRetrieval = this.ExecutionContext.DataProvider.CreateSubreportRetrievalContext(this.SnapshotManager);
			return new ReportProcessingContext(this.ExecutionContext.RequestInfo.ReportContext, this.ExecutionContext.DataProvider.UserContext.UserName, this.ExecutionContext.EffectiveParameters, this.RuntimeDataSources, this.RuntimeSharedDataSets, new ReportProcessing.OnDemandSubReportCallback(subreportRetrieval.GetSubreport), this.ExecutionContext.DataProvider.ResourceCallback, this.SnapshotManager, execType, Localization.ClientPrimaryCulture, this.GetUserProfileState(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.JobType), this.ProcessingInitialUserProfileState, new ServerDataExtensionConnection(this.ExecutionContext.DataProvider.DataExtensionCallback, this.ExecutionContext.DataProvider.UserContext, execType, this.ExecutionContext.DataProvider.GetAdditionalTokenInterface(this.ExecutionContext.RequestInfo.ReportContext)), ReportRuntimeSetup.GetDefault(), new CreateAndRegisterStream(this.ExecutionContext.DataProvider.StreamManager.GetNewStream), false, ServerJobContext.ConstructJobContext(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext), new ServerExtensionFactory(), DataProtection.Instance, new SharedDataSetExecution(this.ExecutionContext.DataProvider, this.SnapshotManager));
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002A2D8 File Offset: 0x000284D8
		private UserProfileState GetUserProfileState(JobType type)
		{
			UserProfileState userProfileState = UserProfileState.None;
			if (Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.Type != JobTypeEnum.System || Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.SubType == JobSubTypeEnum.Subscription)
			{
				userProfileState = UserProfileState.Both;
			}
			return userProfileState;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0002A304 File Offset: 0x00028504
		private DateTime ReportExpirationDateTime
		{
			get
			{
				DateTime expirationDateTime = this.ExecutionContext.ExpirationDateTime;
				if (!(expirationDateTime == DateTime.MinValue))
				{
					return expirationDateTime;
				}
				return DateTime.Now.AddSeconds((double)Global.SessionTimeoutSeconds);
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002A33F File Offset: 0x0002853F
		private static void ExtractPromotedSnapshotInfo(OnDemandProcessingResult processingResult, out PaginationMode paginationMode, out int numberOfPages)
		{
			paginationMode = PaginationMode.TotalPages;
			numberOfPages = 0;
			if (processingResult != null)
			{
				paginationMode = processingResult.UpdatedPaginationMode;
				numberOfPages = processingResult.NumberOfPages;
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002A35C File Offset: 0x0002855C
		protected static PaginationMode GetPaginationModeFromContext(CatalogItemContext context)
		{
			return new PageCountModeValue(context.RSRequestParameters.PaginationModeValue).ToProcessingPaginationMode();
		}

		// Token: 0x040004B2 RID: 1202
		private UserProfileState m_usedUserProfile;

		// Token: 0x040004B3 RID: 1203
		private ReportExecutionBase m_executionContext;

		// Token: 0x040004B4 RID: 1204
		private readonly SnapshotManager m_snapshotManager = new SnapshotManager();

		// Token: 0x040004B5 RID: 1205
		private bool m_snapshotWasCached;

		// Token: 0x02000469 RID: 1129
		[Flags]
		protected enum ProcessOrRender
		{
			// Token: 0x04000FC3 RID: 4035
			Render = 1,
			// Token: 0x04000FC4 RID: 4036
			Process = 2
		}
	}
}
