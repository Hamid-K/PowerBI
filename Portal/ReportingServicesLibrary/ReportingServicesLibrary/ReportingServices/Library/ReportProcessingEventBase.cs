using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C6 RID: 710
	internal abstract class ReportProcessingEventBase
	{
		// Token: 0x0600195F RID: 6495 RVA: 0x00066694 File Offset: 0x00064894
		protected ReportProcessingEventBase(ClientRequest session, RSService service, CatalogItemContext reportContext)
		{
			RSTrace.CatalogTrace.Assert(session != null, "session");
			RSTrace.CatalogTrace.Assert(service != null, "service");
			this.m_session = session;
			this.m_service = service;
			this.m_procEngine = Global.GetProcessingEngine();
			if (!this.RequiresItemContext)
			{
				this.m_reportContext = null;
				return;
			}
			if (reportContext != null)
			{
				this.m_reportContext = reportContext;
				return;
			}
			this.m_reportContext = SessionReportItem.CreateContextFromSession(service, session);
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001960 RID: 6496
		internal abstract ReportEventType EventType { get; }

		// Token: 0x06001961 RID: 6497 RVA: 0x00066710 File Offset: 0x00064910
		internal virtual void AddExecutionInfo(ReportExecutionInfo execInfo)
		{
			RSTrace.CatalogTrace.Assert(execInfo != null, "execInfo");
			execInfo.Source = ExecutionLogExecType.Session;
			execInfo.EventType = this.EventType;
			execInfo.ExecutionId = this.Session.SessionID;
			try
			{
				if (this.Session.SessionReport.Report.EffectiveParams.Count > 0)
				{
					execInfo.Parameters = this.Session.SessionReport.Report.EffectiveParams.ToUrl(false);
				}
				else
				{
					execInfo.Parameters = null;
				}
			}
			catch (InvalidParameterException ex)
			{
				execInfo.Parameters = ex.Message;
				RSTrace.CatalogTrace.TraceException(TraceLevel.Warning, ex.ToString());
			}
			catch (Exception ex2)
			{
				RSTrace.CatalogTrace.TraceException(TraceLevel.Warning, ex2.ToString());
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x000667F0 File Offset: 0x000649F0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected ClientRequest Session
		{
			get
			{
				return this.m_session;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x000667F8 File Offset: 0x000649F8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected RSService Service
		{
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00066800 File Offset: 0x00064A00
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected CatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x00066808 File Offset: 0x00064A08
		protected string UserName
		{
			get
			{
				return this.m_service.UserName;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x00066815 File Offset: 0x00064A15
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected ReportProcessing ProcessingEngine
		{
			get
			{
				return this.m_procEngine;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x0006681D File Offset: 0x00064A1D
		protected IChunkFactory ReadOnlySessionSnapshot
		{
			get
			{
				return ReadOnlyChunkFactory.FromSnapshot(this.Session.SessionReport.Report.SnapshotData);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0006683E File Offset: 0x00064A3E
		protected virtual IChunkFactory ExecutionSnapshotChunkFactory
		{
			get
			{
				return this.Session.SessionReport.Report.SnapshotData;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x000053DC File Offset: 0x000035DC
		protected virtual bool RequiresSessionLock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected virtual bool RequiresItemContext
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00066855 File Offset: 0x00064A55
		protected static string InvariantFormatNumber(int x)
		{
			return x.ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x04000946 RID: 2374
		private readonly ClientRequest m_session;

		// Token: 0x04000947 RID: 2375
		private readonly RSService m_service;

		// Token: 0x04000948 RID: 2376
		private readonly CatalogItemContext m_reportContext;

		// Token: 0x04000949 RID: 2377
		private readonly ReportProcessing m_procEngine;
	}
}
