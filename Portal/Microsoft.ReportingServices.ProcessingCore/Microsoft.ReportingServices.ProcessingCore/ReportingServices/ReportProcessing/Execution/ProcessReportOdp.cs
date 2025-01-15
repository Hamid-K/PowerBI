using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007C7 RID: 1991
	internal abstract class ProcessReportOdp
	{
		// Token: 0x0600708F RID: 28815 RVA: 0x001D525B File Offset: 0x001D345B
		public ProcessReportOdp(IConfiguration configuration, ProcessingContext pc, Report report, ErrorContext errorContext, ReportProcessing.StoreServerParameters storeServerParameters, GlobalIDOwnerCollection globalIDOwnerCollection, ExecutionLogContext executionLogContext)
		{
			this.m_configuration = configuration;
			this.m_publicProcessingContext = pc;
			this.m_reportDefinition = report;
			this.m_errorContext = errorContext;
			this.m_storeServerParameters = storeServerParameters;
			this.m_globalIDOwnerCollection = globalIDOwnerCollection;
			this.m_executionLogContext = executionLogContext;
		}

		// Token: 0x06007090 RID: 28816 RVA: 0x001D5298 File Offset: 0x001D3498
		public ReportSnapshot Execute(out OnDemandProcessingContext odpContext)
		{
			ReportProcessingCompatibilityVersion.TraceCompatibilityVersion(this.m_configuration);
			odpContext = null;
			OnDemandMetadata onDemandMetadata = this.PrepareMetadata();
			onDemandMetadata.GlobalIDOwnerCollection = this.m_globalIDOwnerCollection;
			ReportSnapshot reportSnapshot = onDemandMetadata.ReportSnapshot;
			Global.Tracer.Assert(reportSnapshot != null, "ReportSnapshot object must exist");
			ReportSnapshot reportSnapshot2;
			try
			{
				UserProfileState userProfileState = UserProfileState.None;
				if (this.PublicProcessingContext.Parameters != null)
				{
					userProfileState |= this.PublicProcessingContext.Parameters.UserProfileState;
				}
				odpContext = this.CreateOnDemandContext(onDemandMetadata, reportSnapshot, userProfileState);
				this.CompleteOdpContext(odpContext);
				Merge merge;
				ReportInstance reportInstance = this.CreateReportInstance(odpContext, onDemandMetadata, reportSnapshot, out merge);
				this.PreProcessSnapshot(odpContext, merge, reportInstance, reportSnapshot);
				odpContext.SnapshotProcessing = true;
				odpContext.IsUnrestrictedRenderFormatReferenceMode = true;
				this.ResetEnvironment(odpContext, reportInstance);
				if (odpContext.ThreadCulture != null)
				{
					Thread.CurrentThread.CurrentCulture = odpContext.ThreadCulture;
				}
				this.UpdateUserProfileLocation(odpContext);
				reportSnapshot2 = reportSnapshot;
			}
			finally
			{
				this.CleanupAbortHandler(odpContext);
				if (odpContext != null && odpContext.GlobalDataSourceInfo != null && odpContext.GlobalDataSourceInfo.Values != null)
				{
					foreach (object obj in odpContext.GlobalDataSourceInfo.Values)
					{
						ReportProcessing.DataSourceInfo dataSourceInfo = (ReportProcessing.DataSourceInfo)obj;
						if (dataSourceInfo.TransactionInfo != null)
						{
							if (dataSourceInfo.TransactionInfo.RollbackRequired)
							{
								if (Global.Tracer.TraceInfo)
								{
									Global.Tracer.Trace(TraceLevel.Info, "Data source '{0}': Rolling back transaction.", new object[] { dataSourceInfo.DataSourceName });
								}
								try
								{
									dataSourceInfo.TransactionInfo.Transaction.Rollback();
									goto IL_01F8;
								}
								catch (Exception ex)
								{
									throw new ReportProcessingException(ErrorCode.rsErrorRollbackTransaction, ex, new object[] { dataSourceInfo.DataSourceName });
								}
							}
							if (Global.Tracer.TraceVerbose)
							{
								Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': Committing transaction.", new object[] { dataSourceInfo.DataSourceName });
							}
							try
							{
								dataSourceInfo.TransactionInfo.Transaction.Commit();
							}
							catch (Exception ex2)
							{
								throw new ReportProcessingException(ErrorCode.rsErrorCommitTransaction, ex2, new object[] { dataSourceInfo.DataSourceName });
							}
						}
						IL_01F8:
						if (dataSourceInfo.Connection != null)
						{
							try
							{
								odpContext.CreateAndSetupDataExtensionFunction.CloseConnection(dataSourceInfo.Connection, dataSourceInfo.ProcDataSourceInfo, dataSourceInfo.DataExtDataSourceInfo);
							}
							catch (Exception ex3)
							{
								throw new ReportProcessingException(ErrorCode.rsErrorClosingConnection, ex3, new object[] { dataSourceInfo.DataSourceName });
							}
						}
					}
				}
			}
			return reportSnapshot2;
		}

		// Token: 0x06007091 RID: 28817 RVA: 0x001D558C File Offset: 0x001D378C
		protected virtual void UpdateUserProfileLocation(OnDemandProcessingContext odpContext)
		{
			odpContext.ReportObjectModel.UserImpl.UpdateUserProfileLocationWithoutLocking(UserProfileState.OnDemandExpressions);
		}

		// Token: 0x06007092 RID: 28818 RVA: 0x001D55A0 File Offset: 0x001D37A0
		protected virtual void CleanupAbortHandler(OnDemandProcessingContext odpContext)
		{
			if (odpContext != null)
			{
				odpContext.UnregisterAbortInfo();
			}
		}

		// Token: 0x06007093 RID: 28819 RVA: 0x001D55AC File Offset: 0x001D37AC
		protected virtual OnDemandProcessingContext CreateOnDemandContext(OnDemandMetadata odpMetadata, ReportSnapshot reportSnapshot, UserProfileState initialUserDependency)
		{
			return new OnDemandProcessingContext(this.PublicProcessingContext, this.ReportDefinition, odpMetadata, this.m_errorContext, reportSnapshot.ExecutionTime, this.SnapshotProcessing, this.ReprocessSnapshot, this.ProcessWithCachedData, this.m_storeServerParameters, initialUserDependency, this.m_executionLogContext, this.Configuration, this.OnDemandProcessingMode, this.GetAbortHelper());
		}

		// Token: 0x06007094 RID: 28820 RVA: 0x001D5608 File Offset: 0x001D3808
		protected virtual IAbortHelper GetAbortHelper()
		{
			if (this.PublicProcessingContext.JobContext == null)
			{
				return null;
			}
			return this.PublicProcessingContext.JobContext.GetAbortHelper();
		}

		// Token: 0x06007095 RID: 28821 RVA: 0x001D5629 File Offset: 0x001D3829
		protected virtual void ResetEnvironment(OnDemandProcessingContext odpContext, ReportInstance reportInstance)
		{
			odpContext.SetupEnvironment(reportInstance);
			odpContext.ReportObjectModel.AggregatesImpl.ResetAll();
		}

		// Token: 0x06007096 RID: 28822 RVA: 0x001D5644 File Offset: 0x001D3844
		protected virtual ReportInstance CreateReportInstance(OnDemandProcessingContext odpContext, OnDemandMetadata odpMetadata, ReportSnapshot reportSnapshot, out Merge odpMerge)
		{
			odpMerge = new Merge(this.ReportDefinition, odpContext);
			ChunkManager.OnDemandProcessingManager.EnsureGroupTreeStorageSetup(odpMetadata, odpContext.ChunkFactory, odpMetadata.GlobalIDOwnerCollection, false, odpContext.GetActiveCompatibilityVersion(), odpContext.ProhibitSerializableValues);
			ReportInstance reportInstance = (reportSnapshot.ReportInstance = (odpContext.CurrentReportInstance = odpMerge.PrepareReportInstance(odpMetadata)));
			odpMerge.Init(this.PublicProcessingContext.Parameters);
			this.SetupReportLanguage(odpMerge, reportInstance);
			odpMerge.SetupReport(reportInstance);
			return reportInstance;
		}

		// Token: 0x06007097 RID: 28823
		protected abstract void PreProcessSnapshot(OnDemandProcessingContext odpContext, Merge odpMerge, ReportInstance reportInstance, ReportSnapshot reportSnapshot);

		// Token: 0x06007098 RID: 28824
		protected abstract OnDemandMetadata PrepareMetadata();

		// Token: 0x06007099 RID: 28825 RVA: 0x001D56C2 File Offset: 0x001D38C2
		protected virtual void CompleteOdpContext(OnDemandProcessingContext odpContext)
		{
		}

		// Token: 0x0600709A RID: 28826
		protected abstract void SetupReportLanguage(Merge odpMerge, ReportInstance reportInstance);

		// Token: 0x1700265C RID: 9820
		// (get) Token: 0x0600709B RID: 28827
		protected abstract bool SnapshotProcessing { get; }

		// Token: 0x1700265D RID: 9821
		// (get) Token: 0x0600709C RID: 28828
		protected abstract bool ReprocessSnapshot { get; }

		// Token: 0x1700265E RID: 9822
		// (get) Token: 0x0600709D RID: 28829
		protected abstract bool ProcessWithCachedData { get; }

		// Token: 0x1700265F RID: 9823
		// (get) Token: 0x0600709E RID: 28830
		protected abstract OnDemandProcessingContext.Mode OnDemandProcessingMode { get; }

		// Token: 0x0600709F RID: 28831 RVA: 0x001D56C4 File Offset: 0x001D38C4
		protected void SetupInitialOdpState(OnDemandProcessingContext odpContext, ReportInstance reportInstance, ReportSnapshot reportSnapshot)
		{
			reportSnapshot.HasUserSortFilter = this.ReportDefinition.ReportOrDescendentHasUserSortFilter;
			odpContext.SetupEnvironment(reportInstance);
		}

		// Token: 0x17002660 RID: 9824
		// (get) Token: 0x060070A0 RID: 28832 RVA: 0x001D56DE File Offset: 0x001D38DE
		protected IConfiguration Configuration
		{
			get
			{
				return this.m_configuration;
			}
		}

		// Token: 0x17002661 RID: 9825
		// (get) Token: 0x060070A1 RID: 28833 RVA: 0x001D56E6 File Offset: 0x001D38E6
		protected ProcessingContext PublicProcessingContext
		{
			get
			{
				return this.m_publicProcessingContext;
			}
		}

		// Token: 0x17002662 RID: 9826
		// (get) Token: 0x060070A2 RID: 28834 RVA: 0x001D56EE File Offset: 0x001D38EE
		protected Report ReportDefinition
		{
			get
			{
				return this.m_reportDefinition;
			}
		}

		// Token: 0x17002663 RID: 9827
		// (get) Token: 0x060070A3 RID: 28835 RVA: 0x001D56F6 File Offset: 0x001D38F6
		protected GlobalIDOwnerCollection GlobalIDOwnerCollection
		{
			get
			{
				return this.m_globalIDOwnerCollection;
			}
		}

		// Token: 0x17002664 RID: 9828
		// (get) Token: 0x060070A4 RID: 28836 RVA: 0x001D56FE File Offset: 0x001D38FE
		protected ErrorContext ErrorContext
		{
			get
			{
				return this.m_errorContext;
			}
		}

		// Token: 0x17002665 RID: 9829
		// (get) Token: 0x060070A5 RID: 28837 RVA: 0x001D5706 File Offset: 0x001D3906
		protected ReportProcessing.StoreServerParameters StoreServerParameters
		{
			get
			{
				return this.m_storeServerParameters;
			}
		}

		// Token: 0x17002666 RID: 9830
		// (get) Token: 0x060070A6 RID: 28838 RVA: 0x001D570E File Offset: 0x001D390E
		protected ExecutionLogContext ExecutionLogContext
		{
			get
			{
				return this.m_executionLogContext;
			}
		}

		// Token: 0x04003A31 RID: 14897
		private readonly IConfiguration m_configuration;

		// Token: 0x04003A32 RID: 14898
		private readonly ProcessingContext m_publicProcessingContext;

		// Token: 0x04003A33 RID: 14899
		private readonly Report m_reportDefinition;

		// Token: 0x04003A34 RID: 14900
		private readonly ReportProcessing.StoreServerParameters m_storeServerParameters;

		// Token: 0x04003A35 RID: 14901
		private readonly GlobalIDOwnerCollection m_globalIDOwnerCollection;

		// Token: 0x04003A36 RID: 14902
		private readonly ExecutionLogContext m_executionLogContext;

		// Token: 0x04003A37 RID: 14903
		private readonly ErrorContext m_errorContext;
	}
}
