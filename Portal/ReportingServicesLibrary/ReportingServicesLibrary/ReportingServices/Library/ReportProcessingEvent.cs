using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C7 RID: 711
	internal abstract class ReportProcessingEvent<TEventParameter, TReturn> : ReportProcessingEventBase
	{
		// Token: 0x0600196C RID: 6508 RVA: 0x00066863 File Offset: 0x00064A63
		protected ReportProcessingEvent(ClientRequest session, RSService service, CatalogItemContext reportContext)
			: base(session, service, reportContext)
		{
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00066870 File Offset: 0x00064A70
		public TReturn PerformAction(TEventParameter eventParam)
		{
			this.m_eventParams = eventParam;
			TReturn rval2;
			try
			{
				base.Service.WillDisconnectStorage();
				if (base.Session.SessionReport.Report.SnapshotData == null)
				{
					throw new ReportNotReadyException();
				}
				if (this.RequiresSessionLock)
				{
					bool flag = false;
					do
					{
						try
						{
							base.Session.SessionReport.WriteLockSession(true);
							flag = true;
						}
						catch (ReportServerStorageException ex)
						{
							if (ex.IsSqlException && ex.SqlErrorNumber == Native.SqlAdHocErrorCode && ex.SqlErrorMessage == "Invalid version locked")
							{
								base.Session.SessionReport = SessionReportItem.Load(base.Session.SessionReport.SessionDB, base.Session.SessionID, null, base.Session.SessionReport.Report.HistoryDate, base.Session.SessionReport.UserContext, null, null, null);
								RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Reloading the session since it has been changed by a previous event");
							}
						}
					}
					while (!flag);
				}
				using (CancelableReportProcessingEvent cancelableReportProcessingEvent = new CancelableReportProcessingEvent(base.Session.SessionReport.Report.ItemPath, base.Service, this))
				{
					TReturn rval = default(TReturn);
					cancelableReportProcessingEvent.Method = delegate
					{
						using (ISnapshotTransaction snapshotTransactionContext = this.GetSnapshotTransactionContext())
						{
							rval = this.Event();
							if (snapshotTransactionContext != null)
							{
								snapshotTransactionContext.Commit();
							}
						}
					};
					cancelableReportProcessingEvent.ExecuteWrapper();
					rval2 = rval;
				}
			}
			catch (Exception ex2)
			{
				base.Service.AbortTransaction();
				if (ex2 is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex2, null);
			}
			finally
			{
				try
				{
					if (this.RequiresSessionLock)
					{
						base.Session.SessionReport.ThreadNoLongerUseThisSession();
					}
				}
				finally
				{
					base.Service.DisconnectStorage();
				}
			}
			return rval2;
		}

		// Token: 0x0600196E RID: 6510
		protected abstract TReturn Event();

		// Token: 0x0600196F RID: 6511 RVA: 0x00066A94 File Offset: 0x00064C94
		internal virtual ISnapshotTransaction GetSnapshotTransactionContext()
		{
			return base.Session.SessionReport.Report.SnapshotData.EnterTransactionContext();
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00066AB0 File Offset: 0x00064CB0
		protected TEventParameter EventParameter
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_eventParams;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected bool WriteEventParameters
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x0400094A RID: 2378
		private TEventParameter m_eventParams;
	}
}
