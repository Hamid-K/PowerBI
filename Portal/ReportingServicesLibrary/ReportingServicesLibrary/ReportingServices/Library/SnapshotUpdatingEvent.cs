using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C9 RID: 713
	internal abstract class SnapshotUpdatingEvent<TEventParameter, TReturn> : ReportProcessingEvent<TEventParameter, TReturn>
	{
		// Token: 0x06001977 RID: 6519 RVA: 0x00066B77 File Offset: 0x00064D77
		protected SnapshotUpdatingEvent(ClientRequest session, RSService service, CatalogItemContext reportContext)
			: base(session, service, reportContext)
		{
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00066B82 File Offset: 0x00064D82
		protected override TReturn Event()
		{
			this.BuildSnapshotManager();
			return this.ExecuteEventWrapper();
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00005C88 File Offset: 0x00003E88
		internal override ISnapshotTransaction GetSnapshotTransactionContext()
		{
			return null;
		}

		// Token: 0x0600197A RID: 6522
		protected abstract TReturn RunEvent(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc);

		// Token: 0x0600197B RID: 6523 RVA: 0x00066B90 File Offset: 0x00064D90
		protected TReturn ExecuteEventWrapper()
		{
			ReportProcessing.ExecutionType executionType;
			TReturn treturn;
			using (SurrogateContextFactory.CreateContext(out executionType))
			{
				using (ISnapshotTransaction snapshotTransaction = this.m_snapshotManager.EnterTransactionContext())
				{
					Microsoft.ReportingServices.ReportProcessing.ProcessingContext processingContext = this.BuildProcessingContext(executionType);
					treturn = this.RunEvent(processingContext);
					snapshotTransaction.Commit();
				}
			}
			if (this.ProcessingResult != null)
			{
				base.Session.SessionReport.ProcessingResult = this.ProcessingResult;
				SessionReportItem.SaveAction saveAction = SessionReportItem.SaveAction.SaveNone;
				if (this.ProcessingResult.SnapshotChanged)
				{
					saveAction = SessionReportItem.SaveAction.SaveSnapshot;
				}
				else if (this.m_snapshotManager.UpdatedSnapshot != null)
				{
					saveAction = SessionReportItem.SaveAction.SaveSession;
				}
				base.Session.SessionReport.SnapshotTransactionFactory = this.m_snapshotManager;
				base.Session.SessionReport.Save(saveAction);
			}
			return treturn;
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x00066C68 File Offset: 0x00064E68
		protected override IChunkFactory ExecutionSnapshotChunkFactory
		{
			get
			{
				RSTrace.CatalogTrace.Assert(this.m_snapshotManager != null, "m_snapshotManager");
				return this.m_snapshotManager;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool RequiresItemContext
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x00066C88 File Offset: 0x00064E88
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected IChunkFactory CompiledDefinitionChunkFactory
		{
			get
			{
				return this.ExecutionSnapshotChunkFactory;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00066C90 File Offset: 0x00064E90
		// (set) Token: 0x06001980 RID: 6528 RVA: 0x00066C98 File Offset: 0x00064E98
		protected OnDemandProcessingResult ProcessingResult
		{
			get
			{
				return this.m_procResult;
			}
			set
			{
				this.m_procResult = value;
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00066CA4 File Offset: 0x00064EA4
		private Microsoft.ReportingServices.ReportProcessing.ProcessingContext BuildProcessingContext(ReportProcessing.ExecutionType execType)
		{
			return new ReportProcessingContext(base.ReportContext, base.UserName, base.Session.SessionReport.Report.EffectiveParams, null, null, null, new ServerGetResourceForProcessing(base.Service), this.ExecutionSnapshotChunkFactory, execType, Localization.ClientPrimaryCulture, UserProfileState.Both, UserProfileState.None, new ServerDataExtensionConnection(base.Service.HowToCreateDataExtensionInstance, base.Service.UserContext, execType, new ServerAdditionalToken(base.Service, base.ReportContext)), ReportRuntimeSetup.GetDefault(), new CreateAndRegisterStream(base.Service.StreamManager.GetNewStream), false, ServerJobContext.ConstructJobContext(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext), new ServerExtensionFactory(), DataProtection.Instance, null);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00066D54 File Offset: 0x00064F54
		private void BuildSnapshotManager()
		{
			this.m_snapshotManager = new SnapshotManager();
			RSTrace.CatalogTrace.Assert(base.Session.SessionReport.Report.SnapshotData != null, "SnapshotData");
			this.m_snapshotManager.OriginalSnapshot = base.Session.SessionReport.Report.SnapshotData;
			bool isPermanentSnapshot = base.Session.SessionReport.Report.SnapshotData.IsPermanentSnapshot;
			bool flag = isPermanentSnapshot || base.Session.SessionReport.FoundInCache || Global.UseLocalFileStore(isPermanentSnapshot);
			if (flag)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Snaphsot versioning enabled for event processing");
			}
			this.m_snapshotManager.SnapshotVersioningEnabled = flag;
			if (this.m_snapshotManager.SnapshotVersioningEnabled)
			{
				this.m_snapshotManager.EnrollInPersistedEvent(base.Session.SessionReport);
				this.m_snapshotManager.SnapshotUpdated += delegate(object sender, SnapshotUpdatedEventArgs e)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "SnapshotUpdated event handled");
					base.Session.SessionReport.Report.SnapshotData = e.NewSnapshot;
				};
			}
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00066E46 File Offset: 0x00065046
		private void CheckSessionHasReport()
		{
			if (base.Session.SessionReport.Report.SnapshotData == null)
			{
				throw new ReportNotReadyException();
			}
		}

		// Token: 0x0400094D RID: 2381
		private SnapshotManager m_snapshotManager;

		// Token: 0x0400094E RID: 2382
		private OnDemandProcessingResult m_procResult;
	}
}
