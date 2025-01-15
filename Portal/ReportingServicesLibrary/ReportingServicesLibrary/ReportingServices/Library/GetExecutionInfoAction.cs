using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000098 RID: 152
	internal sealed class GetExecutionInfoAction
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x00019CE8 File Offset: 0x00017EE8
		public GetExecutionInfoAction(ClientRequest session, RSService service)
		{
			this.m_service = service;
			this.m_session = session;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00019D00 File Offset: 0x00017F00
		public void Execute()
		{
			using (MonitoredScope.New("GetExecutionInfoAction.Execute"))
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
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00019DAC File Offset: 0x00017FAC
		public ExecutionInfo3 Result
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x04000348 RID: 840
		private ClientRequest m_session;

		// Token: 0x04000349 RID: 841
		private RSService m_service;

		// Token: 0x0400034A RID: 842
		private ExecutionInfo3 m_result;
	}
}
