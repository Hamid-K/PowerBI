using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.Engine;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.DAX;
using Microsoft.PowerBI.ExploreHost.DocumentConversion;
using Microsoft.PowerBI.ExploreHost.Insights;
using Microsoft.PowerBI.ExploreHost.Lucia;
using Microsoft.PowerBI.ExploreHost.SemanticQuery;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.ExploreServiceCommon.Interfaces;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.Query.Contracts.DaxCapabilities;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.Reporting;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x02000027 RID: 39
	internal sealed class ExploreClient : IExploreClient, ISemanticQueryHandler, IConceptualSchemaRetriever, IExplorationConversionHandler, IInsightsHandler, IDaxCapabilitiesHandler, ILuciaHandler
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00003CA9 File Offset: 0x00001EA9
		public ExploreClient(IPowerViewHandler powerViewHandler, Microsoft.InfoNav.Explore.ServiceContracts.Internal.IFeatureSwitchesProxy featureSwitchesProxy)
			: this(powerViewHandler, featureSwitchesProxy, new QueryCancellationManager())
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public ExploreClient(IPowerViewHandler powerViewHandler, Microsoft.InfoNav.Explore.ServiceContracts.Internal.IFeatureSwitchesProxy featureSwitchesProxy, bool initRsConfig)
			: this(powerViewHandler, featureSwitchesProxy)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003CC2 File Offset: 0x00001EC2
		public ExploreClient(IPowerViewHandler powerViewHandler, Microsoft.InfoNav.Explore.ServiceContracts.Internal.IFeatureSwitchesProxy featureSwitchesProxy, IQueryCancellationManager cancelManager)
			: this(powerViewHandler, featureSwitchesProxy, DataShapeEngine.Instance, cancelManager)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public ExploreClient(IPowerViewHandler powerViewHandler, Microsoft.InfoNav.Explore.ServiceContracts.Internal.IFeatureSwitchesProxy featureSwitchesProxy, IDataShapeEngine dataShapeEngine, IQueryCancellationManager cancelManager)
		{
			FeatureSwitches featureSwitches = new FeatureSwitches(featureSwitchesProxy);
			this.m_clientExploreHandlerContext = new ExploreClientHandlerContext(powerViewHandler, dataShapeEngine, featureSwitches, cancelManager);
			DataShapingHelper.InitializeDefaultConnectionFactory(featureSwitches.MsolapTracingEnabled);
			this.m_daxCapabilitiesManager = new DaxCapabilitiesManager(powerViewHandler);
			Dumper.Current.Init();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003D1F File Offset: 0x00001F1F
		public FeatureSwitches FeatureSwitches
		{
			get
			{
				return this.m_clientExploreHandlerContext.FeatureSwitches;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003D2C File Offset: 0x00001F2C
		public bool RegisterScriptHandler(IScriptHandler scriptHandler)
		{
			this.m_clientExploreHandlerContext.ScriptHandlers[scriptHandler.Name] = scriptHandler;
			return true;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003D46 File Offset: 0x00001F46
		public bool RegisterScriptEditor(IScriptEditor scriptEditor)
		{
			this.m_clientExploreHandlerContext.ScriptEditors[scriptEditor.Name] = scriptEditor;
			return true;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003D60 File Offset: 0x00001F60
		public string ConvertToExploration(Stream stream, string databaseName, Dictionary<string, string> workSheetNames = null, Dictionary<string, bool> workSheetNameToDataSourceMapping = null, bool convertFromRdlx = false)
		{
			ExplorationConversionFlow explorationConversionFlow = new ExplorationConversionFlow(stream, databaseName, this.m_clientExploreHandlerContext, workSheetNames, workSheetNameToDataSourceMapping, convertFromRdlx);
			explorationConversionFlow.Run();
			return explorationConversionFlow.SerializedExploration;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003D80 File Offset: 0x00001F80
		public async Task<string> ExecuteSemanticQueryAsync(string jsonCommands, string databaseName, ASQueryLimits asQueryLimits)
		{
			ExecuteSemanticQueryFlow executeSemanticQueryFlow = new ExecuteSemanticQueryFlow(jsonCommands, databaseName, this.m_clientExploreHandlerContext, asQueryLimits);
			await executeSemanticQueryFlow.RunAsync();
			return executeSemanticQueryFlow.SerializedSemanticQueryResult;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003DDC File Offset: 0x00001FDC
		public async Task<ExportDataMetadata> ExecuteExportDataQueryAsync(string jsonCommands, string databaseName, Stream output, ASQueryLimits asQueryLimits)
		{
			ExecuteExportDataQueryFlow executeSemanticQueryFlow = new ExecuteExportDataQueryFlow(jsonCommands, databaseName, this.m_clientExploreHandlerContext, output, asQueryLimits);
			await executeSemanticQueryFlow.RunAsync();
			return executeSemanticQueryFlow.QueryResultMetadata;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003E40 File Offset: 0x00002040
		public async Task<string> GetClientConceptualSchemaAsync(string databaseName, string jsonRequest, IModel model, bool isExtendable, TranslationsBehavior translationsBehavior)
		{
			GetConceptualSchemaFlow getConceptualSchemaFlow = new GetConceptualSchemaFlow(jsonRequest, databaseName, this.m_clientExploreHandlerContext, model, isExtendable, translationsBehavior);
			await getConceptualSchemaFlow.RunAsync();
			return getConceptualSchemaFlow.SerializedConceptualSchema;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003EB0 File Offset: 0x000020B0
		public async Task<string> GetPerspectivesInfoAsync(string databaseID)
		{
			GetPerspectivesFlow perspectivesFlow = this.CreateGetPerspectivesFlow(databaseID);
			await perspectivesFlow.RunAsync();
			return perspectivesFlow.SerializedPerspectivesInfo;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003EFB File Offset: 0x000020FB
		public IConceptualSchema GetConceptualSchema(string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior = null)
		{
			this.m_clientExploreHandlerContext.PowerViewHandler.EnsureSession(databaseID);
			return ExploreHostUtils.GetConceptualSchema(this.m_clientExploreHandlerContext.PowerViewHandler, databaseID, maxModelMetadataVersion, translationsBehavior);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003F21 File Offset: 0x00002121
		public IConceptualSchema GetConceptualSchema(ASConnectionInfo connectionInfo, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior = TranslationsBehavior.Default)
		{
			return ExploreHostUtils.GetConceptualSchema(this.m_clientExploreHandlerContext.PowerViewHandler, connectionInfo, maxModelMetadataVersion, translationsBehavior);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003F36 File Offset: 0x00002136
		public DataSet GetSchemaDataSet(string databaseID, string schemaName, IReadOnlyDictionary<string, object> restrictions)
		{
			this.m_clientExploreHandlerContext.PowerViewHandler.EnsureSession(databaseID);
			return ExploreHostUtils.GetSchemaDataSet(this.m_clientExploreHandlerContext.PowerViewHandler, databaseID, schemaName, restrictions);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003F5C File Offset: 0x0000215C
		public DaxCapabilities GetDaxCapabilities(string databaseID)
		{
			this.m_clientExploreHandlerContext.PowerViewHandler.EnsureSession(databaseID);
			return this.m_daxCapabilitiesManager.GetDaxCapabilities(databaseID);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003F7C File Offset: 0x0000217C
		public async Task<object> EditScriptVisualCommandAsync(string jsonCommands, string databaseName, ASQueryLimits asQueryLimits)
		{
			await new EditScriptVisualCommandFlow(jsonCommands, databaseName, this.m_clientExploreHandlerContext, asQueryLimits).RunAsync();
			return true;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003FD7 File Offset: 0x000021D7
		public TranslatedGroupingDefinition TranslateGroupingDefinition(GroupingDefinition definition, string databaseID)
		{
			TranslateGroupingDefinitionFlow translateGroupingDefinitionFlow = this.CreateTranslateGroupingDefinitionFlow(definition, databaseID);
			translateGroupingDefinitionFlow.Run();
			return translateGroupingDefinitionFlow.Result;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003FEC File Offset: 0x000021EC
		public async Task<TranslatedGroupingDefinition> TranslateGroupingDefinitionAsync(GroupingDefinition definition, string databaseID)
		{
			TranslateGroupingDefinitionFlow translationFlow = this.CreateTranslateGroupingDefinitionFlow(definition, databaseID);
			await translationFlow.RunAsync();
			return translationFlow.Result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000403F File Offset: 0x0000223F
		public TranslatedPartitionColumn TranslatePartitionColumn(GroupingDefinition definition, string databaseID)
		{
			TranslatePartitionColumnFlow translatePartitionColumnFlow = this.CreateTranslatePartitionColumnFlow(definition, databaseID);
			translatePartitionColumnFlow.Run();
			return translatePartitionColumnFlow.Result;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004054 File Offset: 0x00002254
		public async Task<TranslatedPartitionColumn> TranslatePartitionColumnAsync(GroupingDefinition definition, string databaseID)
		{
			TranslatePartitionColumnFlow translationFlow = this.CreateTranslatePartitionColumnFlow(definition, databaseID);
			await translationFlow.RunAsync();
			return translationFlow.Result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000040A7 File Offset: 0x000022A7
		public DataViewQueryTranslationResult TranslateDataViewQueryDefinition(DataViewQueryDefinition definition, string databaseID)
		{
			TranslateDataViewQueryFlow translateDataViewQueryFlow = this.CreateTranslateDataViewQueryFlow(definition, databaseID);
			translateDataViewQueryFlow.Run();
			return translateDataViewQueryFlow.Result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000040BC File Offset: 0x000022BC
		public async Task<DataViewQueryTranslationResult> TranslateDataViewQueryDefinitionAsync(DataViewQueryDefinition definition, string databaseID)
		{
			TranslateDataViewQueryFlow translationFlow = this.CreateTranslateDataViewQueryFlow(definition, databaseID);
			await translationFlow.RunAsync();
			return translationFlow.Result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000410F File Offset: 0x0000230F
		public Task<string> DeriveInsightsAsync(string request, string databaseID, CancellationToken cancellationToken, IMeasureExpressionProvider expressionProvider, IModel model)
		{
			return new InsightsHandler(this.m_clientExploreHandlerContext.PowerViewHandler).DeriveInsightsAsync(request, databaseID, cancellationToken, expressionProvider, model);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000412D File Offset: 0x0000232D
		public Task<string> ExecuteAnalysisAsync(string request, string databaseID)
		{
			IInsightsSession insightsSession = this.m_clientExploreHandlerContext.PowerViewHandler.GetActiveSession(databaseID).GetInsightsSession();
			if (insightsSession == null)
			{
				throw new InvalidOperationException("No insights session exists for database with ID: " + databaseID);
			}
			return insightsSession.ExecuteAnalysisAsync(request);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000415F File Offset: 0x0000235F
		public void CancelAnalysis(string request, string databaseID)
		{
			IInsightsSession insightsSession = this.m_clientExploreHandlerContext.PowerViewHandler.GetActiveSession(databaseID).GetInsightsSession();
			if (insightsSession == null)
			{
				throw new InvalidOperationException("No insights session exists for database with ID: " + databaseID);
			}
			insightsSession.CancelAnalysis(request);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004191 File Offset: 0x00002391
		public Task<string> InterpretAsync(string interpretRequest, string databaseID)
		{
			return this.m_clientExploreHandlerContext.PowerViewHandler.GetActiveSession(databaseID).GetLuciaSession().InterpretAsync(interpretRequest);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000041AF File Offset: 0x000023AF
		private TranslateGroupingDefinitionFlow CreateTranslateGroupingDefinitionFlow(GroupingDefinition definition, string databaseID)
		{
			return new TranslateGroupingDefinitionFlow(this.m_clientExploreHandlerContext, databaseID, definition);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000041BE File Offset: 0x000023BE
		private TranslatePartitionColumnFlow CreateTranslatePartitionColumnFlow(GroupingDefinition definition, string databaseID)
		{
			return new TranslatePartitionColumnFlow(this.m_clientExploreHandlerContext, databaseID, definition);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000041CD File Offset: 0x000023CD
		private TranslateDataViewQueryFlow CreateTranslateDataViewQueryFlow(DataViewQueryDefinition definition, string databaseID)
		{
			return new TranslateDataViewQueryFlow(this.m_clientExploreHandlerContext, databaseID, definition);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000041DC File Offset: 0x000023DC
		private GetPerspectivesFlow CreateGetPerspectivesFlow(string databaseID)
		{
			return new GetPerspectivesFlow(this.m_clientExploreHandlerContext, this.m_clientExploreHandlerContext.PowerViewHandler, databaseID);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000041F8 File Offset: 0x000023F8
		public SemanticQueryDiagnosticsInfo GetSemanticQueryDiagnosticsInfo(string databaseID, string maxModelMetadataVersion = null)
		{
			return new SemanticQueryDiagnosticsInfo(this.m_clientExploreHandlerContext.PowerViewHandler.GetModelString(databaseID, maxModelMetadataVersion.IsNullOrEmpty<char>() ? "2.5" : maxModelMetadataVersion, null));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004234 File Offset: 0x00002434
		public void Shutdown()
		{
			this.m_clientExploreHandlerContext.RunningQueriesCancellationManager.Clear();
			this.m_clientExploreHandlerContext.PowerViewHandler.Shutdown();
		}

		// Token: 0x0400008D RID: 141
		private readonly ExploreClientHandlerContext m_clientExploreHandlerContext;

		// Token: 0x0400008E RID: 142
		private readonly DaxCapabilitiesManager m_daxCapabilitiesManager;
	}
}
