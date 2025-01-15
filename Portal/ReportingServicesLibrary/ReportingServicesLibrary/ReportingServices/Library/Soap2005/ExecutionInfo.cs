using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x02000300 RID: 768
	public class ExecutionInfo
	{
		// Token: 0x06001B05 RID: 6917 RVA: 0x0006D740 File Offset: 0x0006B940
		public ExecutionInfo()
		{
			this.HasSnapshot = false;
			this.NeedsProcessing = true;
			this.AllowQueryExecution = true;
			this.CredentialsRequired = true;
			this.ParametersRequired = false;
			this.ExpirationDateTime = DateTime.MinValue;
			this.ExecutionDateTime = DateTime.MinValue;
			this.NumPages = 0;
			this.Parameters = null;
			this.DataSourcePrompts = null;
			this.HasDocumentMap = false;
			this.ExecutionID = null;
			this.ReportPath = null;
			this.HistoryID = null;
			this.ReportPageSettings = null;
			this.AutoRefreshInterval = 0;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0006D7CC File Offset: 0x0006B9CC
		internal static ExecutionInfo3 ConstructFromUserSession(ClientRequest session, CatalogItemContext catalogItemContext, RSService service)
		{
			RSTrace.CatalogTrace.Assert(session != null, "session");
			RSTrace.CatalogTrace.Assert(catalogItemContext != null, "catalogItemContext");
			RSTrace.CatalogTrace.Assert(service != null, "service");
			RSTrace.CatalogTrace.Assert(session.SessionReport != null, "session.SessionReport");
			RSTrace.CatalogTrace.Assert(session.SessionReport.Report != null, "session.SessionReport.Report");
			RSTrace.CatalogTrace.Assert(session.SessionReport.Report.SnapshotData != null, "session.SessionReport.Report.SnapshotData");
			ExecutionSettingEnum executionSettingEnum = (session.SessionReport.Report.SnapshotData.IsPermanentSnapshot ? ExecutionSettingEnum.Snapshot : ExecutionSettingEnum.Live);
			DataSourcePromptCollection dataSourcePromptCollection = null;
			if (session.SessionReport.Datasources != null)
			{
				dataSourcePromptCollection = session.SessionReport.Datasources.GetPromptRepresentatives(new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity));
			}
			PageProperties pageProperties;
			if (session.SessionReport.IsAdhocReport)
			{
				pageProperties = session.SessionReport.PageProperties;
			}
			else
			{
				ReportPageProperties reportPageProperties = new ReportPageProperties(service, catalogItemContext.ItemPath);
				reportPageProperties.Load();
				pageProperties = reportPageProperties;
			}
			PageSettings pageSettings = new PageSettings();
			pageSettings.FromProcessingPageProperties(pageProperties);
			return ExecutionInfo.NewExecutionInfo3(session, executionSettingEnum, dataSourcePromptCollection, pageSettings, service);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0006D900 File Offset: 0x0006BB00
		private static void PopulateExecutionInfo2(ExecutionInfo2 execInfo, ClientRequest session, ExecutionSettingEnum executionSettings, DataSourcePromptCollection promptCollection, PageSettings reportPageSettings, RSService service)
		{
			execInfo.HasSnapshot = session.SessionReport.Report.SnapshotData != null && session.SessionReport.Report.SnapshotData.SnapshotDataID != Guid.Empty;
			execInfo.NeedsProcessing = !execInfo.HasSnapshot || session.SessionReport.Report.SnapshotParametersHaveChanged;
			execInfo.AllowQueryExecution = executionSettings != ExecutionSettingEnum.Snapshot;
			execInfo.CredentialsRequired = promptCollection != null && promptCollection.NeedPrompt;
			ParameterInfoCollection effectiveParams = session.SessionReport.Report.EffectiveParams;
			execInfo.ParametersRequired = effectiveParams != null && effectiveParams.NeedPrompts();
			execInfo.ExpirationDateTime = session.SessionReport.ExpirationDateTime.ToUniversalTime();
			execInfo.ExecutionDateTime = session.SessionReport.ExecutionDateTime;
			execInfo.NumPages = session.SessionReport.PageCount;
			execInfo.Parameters = ReportParameter.CollectionToParameterArray(session.SessionReport.Report.EffectiveParams);
			execInfo.DataSourcePrompts = DataSourcePrompt.CollectionToPromptArray(promptCollection);
			execInfo.HasDocumentMap = session.SessionReport.HasDocumentMap;
			execInfo.ExecutionID = session.SessionID;
			execInfo.ReportPath = session.SessionReport.Report.ItemPath.FullEditSessionIdentifier;
			execInfo.HistoryID = Globals.ToSnapshotDateFormat(session.SessionReport.Report.HistoryDate);
			execInfo.ReportPageSettings = reportPageSettings;
			execInfo.AutoRefreshInterval = session.SessionReport.AutoRefreshSeconds;
			execInfo.PageCountMode = new PageCountModeValue(session.SessionReport.PaginationMode).Mode;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0006DA96 File Offset: 0x0006BC96
		internal static ExecutionInfo2 NewExecutionInfo2(ClientRequest session, ExecutionSettingEnum executionSettings, DataSourcePromptCollection promptCollection, PageSettings reportPageSettings, RSService service)
		{
			ExecutionInfo2 executionInfo = new ExecutionInfo2();
			ExecutionInfo.PopulateExecutionInfo2(executionInfo, session, executionSettings, promptCollection, reportPageSettings, service);
			return executionInfo;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0006DAAC File Offset: 0x0006BCAC
		internal static ExecutionInfo3 NewExecutionInfo3(ClientRequest session, ExecutionSettingEnum executionSettings, DataSourcePromptCollection promptCollection, PageSettings reportPageSettings, RSService service)
		{
			ExecutionInfo3 executionInfo = new ExecutionInfo3();
			ExecutionInfo.PopulateExecutionInfo2(executionInfo, session, executionSettings, promptCollection, reportPageSettings, service);
			if (session.SessionReport.Report.EffectiveParams.ParametersLayout != null)
			{
				ParametersGridLayout parametersLayout = session.SessionReport.Report.EffectiveParams.ParametersLayout;
				executionInfo.ParametersLayout = new ParametersGridLayoutDefinition
				{
					NumberOfColumns = parametersLayout.NumberOfColumns,
					NumberOfRows = parametersLayout.NumberOfRows,
					CellDefinitions = ((parametersLayout.CellDefinitions != null) ? new ParametersGridCellDefinition[parametersLayout.CellDefinitions.Count] : null)
				};
				if (parametersLayout.CellDefinitions != null)
				{
					for (int i = 0; i < parametersLayout.CellDefinitions.Count; i++)
					{
						executionInfo.ParametersLayout.CellDefinitions[i] = new ParametersGridCellDefinition
						{
							ColumnsIndex = parametersLayout.CellDefinitions[i].ColumnIndex,
							RowIndex = parametersLayout.CellDefinitions[i].RowIndex,
							ParameterName = parametersLayout.CellDefinitions[i].ParameterName
						};
					}
				}
			}
			return executionInfo;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0006DBB8 File Offset: 0x0006BDB8
		internal static ExecutionInfo ConvertFromExecutionInfo2(ExecutionInfo2 executionInfo)
		{
			ExecutionInfo executionInfo2 = new ExecutionInfo();
			executionInfo2.HasSnapshot = executionInfo.HasSnapshot;
			executionInfo2.NeedsProcessing = executionInfo.NeedsProcessing;
			executionInfo2.AllowQueryExecution = executionInfo.AllowQueryExecution;
			executionInfo2.CredentialsRequired = executionInfo.CredentialsRequired;
			executionInfo2.ParametersRequired = executionInfo.ParametersRequired;
			executionInfo2.ExpirationDateTime = executionInfo.ExpirationDateTime;
			executionInfo2.ExecutionDateTime = executionInfo.ExecutionDateTime;
			executionInfo2.NumPages = executionInfo.NumPages;
			executionInfo2.Parameters = executionInfo.Parameters;
			executionInfo2.DataSourcePrompts = executionInfo.DataSourcePrompts;
			executionInfo2.HasDocumentMap = executionInfo.HasDocumentMap;
			executionInfo2.ExecutionID = executionInfo.ExecutionID;
			executionInfo2.ReportPath = executionInfo.ReportPath;
			executionInfo2.HistoryID = executionInfo.HistoryID;
			executionInfo2.ReportPageSettings = executionInfo.ReportPageSettings;
			executionInfo2.AutoRefreshInterval = executionInfo.AutoRefreshInterval;
			if (executionInfo.PageCountMode != PageCountMode.Actual)
			{
				executionInfo2.NumPages = 0;
				executionInfo2.NeedsProcessing = true;
			}
			return executionInfo2;
		}

		// Token: 0x04000A4D RID: 2637
		public bool HasSnapshot;

		// Token: 0x04000A4E RID: 2638
		public bool NeedsProcessing;

		// Token: 0x04000A4F RID: 2639
		public bool AllowQueryExecution;

		// Token: 0x04000A50 RID: 2640
		public bool CredentialsRequired;

		// Token: 0x04000A51 RID: 2641
		public bool ParametersRequired;

		// Token: 0x04000A52 RID: 2642
		public DateTime ExpirationDateTime;

		// Token: 0x04000A53 RID: 2643
		public DateTime ExecutionDateTime;

		// Token: 0x04000A54 RID: 2644
		public int NumPages;

		// Token: 0x04000A55 RID: 2645
		public ReportParameter[] Parameters;

		// Token: 0x04000A56 RID: 2646
		public DataSourcePrompt[] DataSourcePrompts;

		// Token: 0x04000A57 RID: 2647
		public bool HasDocumentMap;

		// Token: 0x04000A58 RID: 2648
		public string ExecutionID;

		// Token: 0x04000A59 RID: 2649
		public string ReportPath;

		// Token: 0x04000A5A RID: 2650
		public string HistoryID;

		// Token: 0x04000A5B RID: 2651
		public PageSettings ReportPageSettings;

		// Token: 0x04000A5C RID: 2652
		public int AutoRefreshInterval;
	}
}
