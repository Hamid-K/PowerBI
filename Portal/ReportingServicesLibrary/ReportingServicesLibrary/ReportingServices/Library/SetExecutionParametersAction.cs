using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000096 RID: 150
	internal sealed class SetExecutionParametersAction
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x000199F0 File Offset: 0x00017BF0
		public SetExecutionParametersAction(NameValueCollection parameters, string parameterLanguage, ClientRequest session, RSService service)
		{
			this.m_parameters = parameters;
			this.m_parameterLanguage = parameterLanguage;
			this.m_service = service;
			this.m_session = session;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00019A18 File Offset: 0x00017C18
		public void Save()
		{
			try
			{
				CatalogItemContext catalogItemContext = SessionReportItem.CreateContextFromSession(this.m_service, this.m_session);
				RSParameterTranslator rsparameterTranslator = new RSParameterTranslator(this.m_service.GetScopedStorage());
				catalogItemContext.RSRequestParameters.SetReportParameters(this.m_parameters, rsparameterTranslator);
				if (this.m_parameterLanguage != null)
				{
					Localization.SetReportParameterCulture(this.m_parameterLanguage);
				}
				DataSourcePromptCollection dataSourcePromptCollection;
				ExecutionSettingEnum executionSettingEnum;
				DateTime dateTime;
				ReportSnapshot reportSnapshot;
				int num;
				bool flag;
				PageSettings pageSettings;
				PaginationMode paginationMode;
				new GetDataForExecutionAction(this.m_service).ExecuteStep(catalogItemContext, this.m_session, out dataSourcePromptCollection, out executionSettingEnum, out dateTime, out reportSnapshot, out num, out flag, out pageSettings, out paginationMode);
				if (this.m_session.SessionReport.Report.SnapshotData != null && this.m_session.SessionReport.Report.QueryParametersHaveChanged)
				{
					if (executionSettingEnum != ExecutionSettingEnum.Live)
					{
						throw new ReportSnapshotEnabledException();
					}
					this.m_session.SessionReport.ClearSnapshot();
				}
				this.m_session.SessionReport.SetParameters();
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

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00019B34 File Offset: 0x00017D34
		public ExecutionInfo3 Result
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x04000340 RID: 832
		private ClientRequest m_session;

		// Token: 0x04000341 RID: 833
		private RSService m_service;

		// Token: 0x04000342 RID: 834
		private NameValueCollection m_parameters;

		// Token: 0x04000343 RID: 835
		private string m_parameterLanguage;

		// Token: 0x04000344 RID: 836
		private ExecutionInfo3 m_result;
	}
}
