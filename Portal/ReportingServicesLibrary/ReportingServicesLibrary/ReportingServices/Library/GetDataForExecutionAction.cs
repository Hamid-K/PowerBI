using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000093 RID: 147
	internal class GetDataForExecutionAction
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x000192AC File Offset: 0x000174AC
		internal GetDataForExecutionAction(RSService service)
			: this(service, true)
		{
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000192B6 File Offset: 0x000174B6
		internal GetDataForExecutionAction(RSService service, bool validateParameters)
		{
			this.m_service = service;
			this.m_validateParameters = validateParameters;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000192CC File Offset: 0x000174CC
		internal void ExecuteStep(CatalogItemContext reportContext, ClientRequest session, out DataSourcePromptCollection prompts, out ExecutionSettingEnum execSetting, out DateTime executionDateTime, out ReportSnapshot snapshotData, out int pageCount, out bool hasDocMap, out PageSettings reportPageSettings, out PaginationMode paginationMode)
		{
			this.m_service.WillDisconnectStorage();
			try
			{
				try
				{
					this._GetDataForExecution(reportContext, session, reportContext.RSRequestParameters.SnapshotParamValue, out prompts, out execSetting, out executionDateTime, out snapshotData, out pageCount, out hasDocMap, out reportPageSettings, out paginationMode);
				}
				catch (VersionMismatchException ex)
				{
					if (!(ex.ReportID != Guid.Empty))
					{
						throw new InternalCatalogException(ex, "empty report ID in version mismatch exception");
					}
					SnapshotConverter.ConvertFromV1(reportContext, ReportSnapshot.Create(ex.ReportID, ex.IsPermanentSnapshot, false, ReportProcessingFlags.NotSet), true);
					this._GetDataForExecution(reportContext, session, reportContext.RSRequestParameters.SnapshotParamValue, out prompts, out execSetting, out executionDateTime, out snapshotData, out pageCount, out hasDocMap, out reportPageSettings, out paginationMode);
				}
			}
			catch (Exception ex2)
			{
				this.m_service.AbortTransaction();
				if (ex2 is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex2, null);
			}
			finally
			{
				this.m_service.DisconnectStorage();
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000193BC File Offset: 0x000175BC
		private void _GetDataForExecution(CatalogItemContext reportContext, ClientRequest session, string historyID, out DataSourcePromptCollection prompts, out ExecutionSettingEnum execSetting, out DateTime snapshotExecutionDate, out ReportSnapshot snapshotData, out int pageCount, out bool hasDocMap, out PageSettings reportPageSettings, out PaginationMode paginationMode)
		{
			using (MonitoredScope.New("GetDataForExecutionAction._GetDataForExecution"))
			{
				prompts = null;
				execSetting = ExecutionSettingEnum.Live;
				snapshotExecutionDate = DateTime.MinValue;
				snapshotData = null;
				pageCount = 0;
				hasDocMap = false;
				paginationMode = PaginationMode.TotalPages;
				reportPageSettings = new PageSettings();
				BaseReportCatalogItem baseReportCatalogItem = null;
				Guid guid = Guid.Empty;
				int num;
				IStoredParameterSource storedParameterSource;
				Guid guid2;
				if (session.SessionReport.IsAdhocReport)
				{
					Security.SafeCheckExecuteReportDefinitionPermission(this.m_service, reportContext.ItemPath, false);
					num = ExecutionOptions.Live;
					storedParameterSource = new AdHocSessionParameterStorage(session.SessionReport);
					guid2 = Guid.Empty;
					reportPageSettings.FromProcessingPageProperties(session.SessionReport.PageProperties);
				}
				else
				{
					using (MonitoredScope.New("GetDataForExecutionAction._GetDataForExecution - data from catalog"))
					{
						CatalogItem catalogItem = this.m_service.CatalogItemFactory.GetCatalogItem(reportContext);
						catalogItem.ThrowIfWrongItemType(new ItemType[]
						{
							ItemType.Report,
							ItemType.LinkedReport
						});
						baseReportCatalogItem = (BaseReportCatalogItem)catalogItem;
					}
					using (MonitoredScope.New("GetDataForExecutionAction._GetDataForExecution - load parameters"))
					{
						ItemParameterDefinition itemParameterDefinition = baseReportCatalogItem.LoadParametersForExecution(historyID, true);
						num = itemParameterDefinition.ExecutionOptions;
						snapshotData = itemParameterDefinition.SnapshotData;
						guid = itemParameterDefinition.ReportId;
						guid2 = itemParameterDefinition.LinkId;
						snapshotExecutionDate = itemParameterDefinition.SnapshotExecutionDate;
						storedParameterSource = new CatalogSessionParameterStorage(session.SessionReport, itemParameterDefinition);
					}
					using (MonitoredScope.New("GetDataForExecutionAction._GetDataForExecution - load properties"))
					{
						ItemProperties itemProperties;
						if (!this.m_service.Storage.CatalogGetAllProperties(reportContext.ItemPath, out itemProperties))
						{
							throw new ItemNotFoundException(reportContext.OriginalItemPath.Value);
						}
						ReportPageProperties reportPageProperties = new ReportPageProperties(itemProperties);
						reportPageProperties.Load();
						reportPageSettings.FromProcessingPageProperties(reportPageProperties);
					}
				}
				execSetting = ExecutionOptions.ToExecutionSettingEnum(num);
				if (ExecutionOptions.IsLiveExecution(num))
				{
					using (MonitoredScope.New("GetDataForExecutionAction._GetDataForExecution - live execution"))
					{
						RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection;
						if (session.SessionReport.Datasources != null)
						{
							runtimeDataSourceInfoCollection = session.SessionReport.Datasources;
						}
						else
						{
							RSTrace.CatalogTrace.Assert(baseReportCatalogItem != null, "Ad-hoc report should always populate DataSources in the SaveFromDefinition()");
							if (guid2 != Guid.Empty)
							{
								CatalogItemPath pathById = this.m_service.Storage.GetPathById(guid2);
								ExternalItemPath externalItemPath = this.m_service.CatalogToExternal(pathById);
								reportContext.SetReportDefinitionPath(externalItemPath);
								session.SessionReport.Report.ReportDefinitionPath = externalItemPath;
							}
							runtimeDataSourceInfoCollection = baseReportCatalogItem.RuntimeDataSources;
							session.SessionReport.Datasources = runtimeDataSourceInfoCollection;
							session.SessionReport.DataSets = baseReportCatalogItem.RuntimeSharedDataSets;
						}
						runtimeDataSourceInfoCollection.SetCredentials(reportContext.RSRequestParameters.DatasourcesCred, DataProtection.Instance);
						prompts = runtimeDataSourceInfoCollection.GetPromptRepresentatives(new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity));
						goto IL_02FE;
					}
				}
				using (MonitoredScope.New("GetDataForExecutionAction._GetDataForExecution - NOT live execution"))
				{
					if (snapshotData == null || snapshotData.SnapshotDataID == Guid.Empty)
					{
						throw new ReportNotReadyException();
					}
					ReportProcessingFlags reportProcessingFlags;
					pageCount = this.m_service.Storage.GetSnapshotPromotedInfo(snapshotData, out hasDocMap, out paginationMode, out reportProcessingFlags);
				}
				IL_02FE:
				if (this.m_validateParameters || session.SessionReport.Report.EffectiveParams == null)
				{
					using (MonitoredScope.New("GetDataForExecutionAction._GetDataForExecution - GetReportParametersForRendering"))
					{
						ParameterInfoCollection reportParametersForRendering = this.m_service.GetReportParametersForRendering(reportContext, guid, guid2, snapshotExecutionDate, storedParameterSource, reportContext.RSRequestParameters.ReportParameters, session.SessionReport.Datasources, session.SessionReport.DataSets, JobType.UserJobType);
						session.SessionReport.Report.EffectiveParams = reportParametersForRendering;
						return;
					}
				}
				session.SessionReport.Report.EffectiveParams.Validated = true;
			}
		}

		// Token: 0x04000339 RID: 825
		private RSService m_service;

		// Token: 0x0400033A RID: 826
		private bool m_validateParameters;
	}
}
