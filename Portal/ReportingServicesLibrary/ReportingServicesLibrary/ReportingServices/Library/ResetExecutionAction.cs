using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000097 RID: 151
	internal sealed class ResetExecutionAction
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00019B3C File Offset: 0x00017D3C
		public ResetExecutionAction(ClientRequest session, RSService service)
		{
			this.m_service = service;
			this.m_session = session;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00019B54 File Offset: 0x00017D54
		public void Save()
		{
			try
			{
				CatalogItemContext catalogItemContext = SessionReportItem.CreateContextFromSession(this.m_service, this.m_session);
				DataSourcePromptCollection dataSourcePromptCollection;
				ExecutionSettingEnum executionSettingEnum;
				DateTime dateTime;
				ReportSnapshot reportSnapshot;
				int num;
				bool flag;
				PageSettings pageSettings;
				PaginationMode paginationMode;
				new GetDataForExecutionAction(this.m_service, false).ExecuteStep(catalogItemContext, this.m_session, out dataSourcePromptCollection, out executionSettingEnum, out dateTime, out reportSnapshot, out num, out flag, out pageSettings, out paginationMode);
				this.m_session.SessionReport.Timeout = Global.SessionTimeoutSeconds;
				this.m_session.SessionReport.AutoRefreshSeconds = 0;
				this.m_session.SessionReport.ExpirationDateTime = DateTime.Now.AddSeconds((double)this.m_session.SessionReport.Timeout);
				this.m_session.SessionReport.Report.SnapshotData = reportSnapshot;
				this.m_session.SessionReport.PageCount = num;
				this.m_session.SessionReport.HasDocumentMap = flag;
				this.m_session.SessionReport.PaginationMode = paginationMode;
				this.m_session.SessionReport.ExecutionDateTime = dateTime;
				this.m_session.SessionReport.AwaitingFirstExecution = true;
				this.m_session.SessionReport.EventInfo = null;
				this.m_session.SessionReport.Save(SessionReportItem.SaveAction.SaveSession, true);
				this.m_result = ExecutionInfo.NewExecutionInfo3(this.m_session, executionSettingEnum, dataSourcePromptCollection, pageSettings, this.m_service);
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new InternalCatalogException(ex, null);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00019CE0 File Offset: 0x00017EE0
		public ExecutionInfo3 Result
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x04000345 RID: 837
		private ClientRequest m_session;

		// Token: 0x04000346 RID: 838
		private RSService m_service;

		// Token: 0x04000347 RID: 839
		private ExecutionInfo3 m_result;
	}
}
