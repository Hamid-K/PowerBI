using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000095 RID: 149
	internal sealed class SetCredentialsAction
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x0001987A File Offset: 0x00017A7A
		public SetCredentialsAction(DataSourceCredentials[] credentials, ClientRequest session, RSService service)
		{
			this.m_credentials = credentials;
			this.m_service = service;
			this.m_session = session;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00019898 File Offset: 0x00017A98
		public void Save()
		{
			try
			{
				CatalogItemContext catalogItemContext = SessionReportItem.CreateContextFromSession(this.m_service, this.m_session);
				if (this.m_session.SessionReport.Report.HistoryDate != DateTime.MinValue)
				{
					throw new ReportSnapshotEnabledException();
				}
				catalogItemContext.RSRequestParameters.DatasourcesCred = DataSourceCredentials.ThisArrayToDatasourcesCredentials(this.m_credentials);
				DataSourcePromptCollection dataSourcePromptCollection;
				ExecutionSettingEnum executionSettingEnum;
				DateTime dateTime;
				ReportSnapshot reportSnapshot;
				int num;
				bool flag;
				PageSettings pageSettings;
				PaginationMode paginationMode;
				new GetDataForExecutionAction(this.m_service).ExecuteStep(catalogItemContext, this.m_session, out dataSourcePromptCollection, out executionSettingEnum, out dateTime, out reportSnapshot, out num, out flag, out pageSettings, out paginationMode);
				if (executionSettingEnum == ExecutionSettingEnum.Snapshot)
				{
					throw new ReportSnapshotEnabledException();
				}
				this.m_session.SessionReport.Timeout = Global.SessionTimeoutSeconds;
				this.m_session.SessionReport.AutoRefreshSeconds = 0;
				this.m_session.SessionReport.ExpirationDateTime = DateTime.Now.AddSeconds((double)this.m_session.SessionReport.Timeout);
				this.m_session.SessionReport.SetCredentials();
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

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000199E8 File Offset: 0x00017BE8
		public ExecutionInfo3 Result
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x0400033C RID: 828
		private ClientRequest m_session;

		// Token: 0x0400033D RID: 829
		private RSService m_service;

		// Token: 0x0400033E RID: 830
		private DataSourceCredentials[] m_credentials;

		// Token: 0x0400033F RID: 831
		private ExecutionInfo3 m_result;
	}
}
