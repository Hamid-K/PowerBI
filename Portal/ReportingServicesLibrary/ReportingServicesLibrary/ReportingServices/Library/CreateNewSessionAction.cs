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
	// Token: 0x02000092 RID: 146
	internal sealed class CreateNewSessionAction
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x00018CEC File Offset: 0x00016EEC
		public CreateNewSessionAction(ClientRequest session, RSService service, CatalogItemContext context)
		{
			try
			{
				this.m_session = session;
				this.m_service = service;
				this.m_reportContext = context;
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new InternalCatalogException(ex, null);
			}
			if (!this.m_session.IsNew)
			{
				throw new InternalCatalogException("expected new session");
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00018D54 File Offset: 0x00016F54
		public void Save()
		{
			using (MonitoredScope.New("CreateNewSessionAction.Save"))
			{
				try
				{
					this.m_service.ServiceHelper.SyncToRSCatalog(this.m_reportContext.ItemPath);
					GetDataForExecutionAction getDataForExecutionAction = new GetDataForExecutionAction(this.m_service);
					DataSourcePromptCollection dataSourcePromptCollection;
					ExecutionSettingEnum executionSettingEnum;
					DateTime dateTime;
					ReportSnapshot reportSnapshot;
					int num;
					bool flag;
					PageSettings pageSettings;
					PaginationMode paginationMode;
					using (MonitoredScope.New("CreateNewSessionAction.Save - ExecuteStep"))
					{
						getDataForExecutionAction.ExecuteStep(this.m_reportContext, this.m_session, out dataSourcePromptCollection, out executionSettingEnum, out dateTime, out reportSnapshot, out num, out flag, out pageSettings, out paginationMode);
					}
					this.m_session.SessionReport.ExecutionDateTime = dateTime;
					this.m_session.SessionReport.Timeout = Global.SessionTimeoutSeconds;
					this.m_session.SessionReport.AutoRefreshSeconds = 0;
					this.m_session.SessionReport.ExpirationDateTime = DateTime.Now.AddSeconds((double)this.m_session.SessionReport.Timeout);
					this.m_session.SessionReport.Report.SnapshotData = reportSnapshot;
					this.m_session.SessionReport.PageCount = num;
					this.m_session.SessionReport.HasDocumentMap = flag;
					this.m_session.SessionReport.PaginationMode = paginationMode;
					this.m_session.SessionReport.Report.ParametersOnSnapshot = this.m_session.SessionReport.Report.EffectiveParams;
					DateTime dateTime2 = Globals.ParseSnapshotDateParameter(this.m_reportContext.RSRequestParameters.SnapshotParamValue, true);
					this.m_session.SessionReport.Report.HistoryDate = dateTime2;
					using (MonitoredScope.New("CreateNewSessionAction.Save - AddNew"))
					{
						this.m_session.SessionReport.AddNew();
					}
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

		// Token: 0x0600060F RID: 1551 RVA: 0x00018FB8 File Offset: 0x000171B8
		public void SaveFromDefinition(byte[] reportDefinition, NameValueCollection reportParameters, bool checkAccessForSharedDataSources, ExternalItemPath pathToSite)
		{
			this.SaveFromDefinition(reportDefinition, reportParameters, checkAccessForSharedDataSources, pathToSite, false);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00018FC8 File Offset: 0x000171C8
		public void SaveFromDefinition(byte[] reportDefinition, NameValueCollection reportParameters, bool checkAccessForSharedDataSources, ExternalItemPath pathToSite, bool skipAccessCheck)
		{
			try
			{
				this.m_service.WillDisconnectStorage();
				ExternalItemPath externalItemPath = this.m_reportContext.ItemPath;
				if (ItemPathBase.IsNullOrEmpty(externalItemPath))
				{
					externalItemPath = pathToSite;
				}
				Security.SafeCheckExecuteReportDefinitionPermission(this.m_service, externalItemPath, skipAccessCheck);
				ItemProperties itemProperties = new ItemProperties();
				CatalogItemContext catalogItemContext = new CatalogItemContext(this.m_service);
				catalogItemContext.SetPath("", ItemPathOptions.None);
				catalogItemContext.RSRequestParameters.SetReportParameters(reportParameters);
				ReportSnapshot reportSnapshot;
				ParameterInfoCollection parameterInfoCollection;
				DataSourceInfoCollection dataSourceInfoCollection;
				DataSetInfoCollection dataSetInfoCollection;
				PageProperties pageProperties;
				byte[] array;
				this.m_service.ConvertToIntermediate(reportDefinition, false, itemProperties, this.m_reportContext, DateTime.Now, checkAccessForSharedDataSources, ReportProcessingFlags.NotSet, false, false, out reportSnapshot, out parameterInfoCollection, out this.m_warnings, out dataSourceInfoCollection, out dataSetInfoCollection, out pageProperties, out array);
				this.m_session.SessionReport.Report.CompiledDefinition = reportSnapshot;
				this.m_session.SessionReport.PageProperties = pageProperties;
				RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection;
				RuntimeDataSetInfoCollection runtimeDataSetInfoCollection;
				this.m_service.GetAllDataSources(null, this.m_reportContext, reportSnapshot, dataSourceInfoCollection, dataSetInfoCollection, out runtimeDataSourceInfoCollection, out runtimeDataSetInfoCollection);
				this.m_session.SessionReport.Datasources = runtimeDataSourceInfoCollection;
				this.m_session.SessionReport.DataSets = runtimeDataSetInfoCollection;
				DataSourcePromptCollection promptRepresentatives = runtimeDataSourceInfoCollection.GetPromptRepresentatives(new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity));
				this.m_session.SessionReport.Report.EffectiveParams = parameterInfoCollection;
				IStoredParameterSource storedParameterSource = new AdHocSessionParameterStorage(this.m_session.SessionReport);
				ParameterInfoCollection reportParametersForRendering = this.m_service.GetReportParametersForRendering(catalogItemContext, Guid.Empty, Guid.Empty, DateTime.MinValue, storedParameterSource, catalogItemContext.RSRequestParameters.ReportParameters, runtimeDataSourceInfoCollection, runtimeDataSetInfoCollection, JobType.UserJobType);
				this.m_session.SessionReport.Report.EffectiveParams = reportParametersForRendering;
				this.m_session.SessionReport.Timeout = Global.SessionTimeoutSeconds;
				this.m_session.SessionReport.AutoRefreshSeconds = 0;
				this.m_session.SessionReport.ExpirationDateTime = DateTime.Now.AddSeconds((double)this.m_session.SessionReport.Timeout);
				if (externalItemPath != null)
				{
					this.m_session.SessionReport.SitePath = this.m_service.ExternalToCatalogItemPath(externalItemPath);
					this.m_session.SessionReport.SiteZone = this.m_service.GetExternalRootZone(externalItemPath);
				}
				this.m_session.SessionReport.AddNew();
				PageSettings pageSettings = new PageSettings();
				pageSettings.FromProcessingPageProperties(pageProperties);
				this.m_result = ExecutionInfo.NewExecutionInfo3(this.m_session, ExecutionSettingEnum.Live, promptRepresentatives, pageSettings, this.m_service);
			}
			catch (RSException)
			{
				this.m_service.AbortTransaction();
				throw;
			}
			catch (Exception ex)
			{
				this.m_service.AbortTransaction();
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				this.m_service.DisconnectStorage();
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001929C File Offset: 0x0001749C
		public ExecutionInfo3 Result
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000192A4 File Offset: 0x000174A4
		public Warning[] Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x04000334 RID: 820
		private ClientRequest m_session;

		// Token: 0x04000335 RID: 821
		private RSService m_service;

		// Token: 0x04000336 RID: 822
		private CatalogItemContext m_reportContext;

		// Token: 0x04000337 RID: 823
		private ExecutionInfo3 m_result;

		// Token: 0x04000338 RID: 824
		private Warning[] m_warnings;
	}
}
