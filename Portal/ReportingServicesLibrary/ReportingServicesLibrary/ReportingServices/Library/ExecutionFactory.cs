using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200010E RID: 270
	internal sealed class ExecutionFactory
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x000285CD File Offset: 0x000267CD
		public ExecutionFactory(RSService service, ExecutionParameters requestInfo)
		{
			this.m_service = service;
			this.m_requestInfo = requestInfo;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000285E4 File Offset: 0x000267E4
		public IReportExecution CreateReportExecution()
		{
			bool flag = this.IsForNewSession();
			IExecutionDataProvider executionDataProvider = new RSServiceDataProvider(this.m_service);
			return this.InternalCreateReportExecution(flag, executionDataProvider);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002860C File Offset: 0x0002680C
		public IReportExecution CreatePersistedStreamReportExecution()
		{
			return this.CreatePersistedStreamReportExecution(false);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00028618 File Offset: 0x00026818
		public IReportExecution CreatePersistedStreamReportExecution(bool waitPersistedStreamCompletion)
		{
			bool flag = this.IsForNewSession();
			IExecutionDataProvider executionDataProvider = new RSServiceDataProvider(this.m_service);
			return new PersistedStreamExecutionProxy(this.InternalCreateReportExecution(flag, executionDataProvider), executionDataProvider, this.m_requestInfo.Session, true, waitPersistedStreamCompletion);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00028653 File Offset: 0x00026853
		private IReportExecution InternalCreateReportExecution(bool isNewSession, IExecutionDataProvider provider)
		{
			return new ExecutionDisposerProxy(isNewSession, provider, this.m_requestInfo);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00028664 File Offset: 0x00026864
		private bool IsForNewSession()
		{
			RSTrace.CatalogTrace.Assert(this.m_requestInfo != null);
			ClientRequest session = this.m_requestInfo.Session;
			CatalogItemContext reportContext = this.m_requestInfo.ReportContext;
			RSTrace.CatalogTrace.Assert(session != null);
			SessionReportItem sessionReport = session.SessionReport;
			bool flag = true;
			if (session.IsNew && reportContext.RSRequestParameters.ImageIDParamValue == null)
			{
				flag = true;
			}
			else if (sessionReport != null)
			{
				flag = !sessionReport.HasSnapshotData || sessionReport.AwaitingFirstExecution;
			}
			else
			{
				if (reportContext.RSRequestParameters.ImageIDParamValue != null)
				{
					throw new ReportNotReadyException();
				}
				RSTrace.CatalogTrace.Assert(false, "cannot determine if execution is for new session");
			}
			return flag;
		}

		// Token: 0x0400049B RID: 1179
		private readonly RSService m_service;

		// Token: 0x0400049C RID: 1180
		private readonly ExecutionParameters m_requestInfo;
	}
}
