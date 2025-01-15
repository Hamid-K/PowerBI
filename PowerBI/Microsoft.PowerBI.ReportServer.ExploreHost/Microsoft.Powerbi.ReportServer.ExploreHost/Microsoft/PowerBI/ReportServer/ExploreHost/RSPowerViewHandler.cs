using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Edm;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.ReportingServicesHost.Insights;
using Microsoft.PowerBI.ReportingServicesHost.Utils;
using Microsoft.PowerBI.ReportServer.ExploreHost.DataSource;
using Microsoft.PowerBI.ReportServer.ExploreHost.Telemetry;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x0200001A RID: 26
	internal sealed class RSPowerViewHandler : IPowerViewHandler, IReportingSessionProvider, IDataSourceStateProvider
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003380 File Offset: 0x00001580
		public FeatureSwitches FeatureSwitches { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003388 File Offset: 0x00001588
		public DataShapingFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003390 File Offset: 0x00001590
		public ConceptualSchemaBuilderOptions SchemaBuilderOptions { get; }

		// Token: 0x06000097 RID: 151 RVA: 0x00003398 File Offset: 0x00001598
		public RSPowerViewHandler(IRSPowerViewDataSourceProvider dataSourceProvider, RSTelemetryConfiguration telemetryConfiguration, IFeatureSwitchesProxy featureSwitchProxy)
			: this(dataSourceProvider, telemetryConfiguration, ModelMetadataProvider.CreateInstance(new ExploreTelemetryService(TelemetryService.Instance), true), featureSwitchProxy)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000033B4 File Offset: 0x000015B4
		internal RSPowerViewHandler(IRSPowerViewDataSourceProvider repository, RSTelemetryConfiguration telemetryConfiguration, IModelMetadataProvider modelMetadataProvider, IFeatureSwitchesProxy featureSwitchProxy)
		{
			this._dataSourceProvider = repository;
			this._telemetryService = new RSTelemetryService(telemetryConfiguration);
			this._connectionPoolAdapter = new RSDataShapingConnectionPoolAdapter(RSPowerViewHandler._connectionPool);
			this._modelMetadataProvider = modelMetadataProvider;
			this.FeatureSwitches = new FeatureSwitches(featureSwitchProxy);
			this.FeatureSwitchProvider = DataShapingFeatureSwitchProvider.Create(this.FeatureSwitches);
			this.SchemaBuilderOptions = ConceptualSchemaExtensions.CreateConceptualSchemaBuilderOptions(this.FeatureSwitches);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003420 File Offset: 0x00001620
		public void Shutdown()
		{
			RSPowerViewHandler._modelCache.Clear();
			if (RSPowerViewHandler._connectionPool != null)
			{
				RSPowerViewHandler._connectionPool.CloseConnections();
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003440 File Offset: 0x00001640
		public Stream GetModelStream(string modelIdFromClient, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior)
		{
			ModelKey modelKey = this.CreateModelKeyFromId(modelIdFromClient, maxModelMetadataVersion, TranslationsBehavior.Default);
			ModelInfo orAddModel = this.GetOrAddModel(modelKey);
			return new MemoryStream(Encoding.UTF8.GetBytes(orAddModel.ModelMetaData));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003474 File Offset: 0x00001674
		public EngineDataModel GetEngineDataModel(string modelIdFromClient, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, Func<Stream, IFeatureSwitchProvider, EngineDataModel> parse)
		{
			EngineDataModel engineDataModel;
			using (Stream modelStream = this.GetModelStream(modelIdFromClient, maxModelMetadataVersion, translationsBehavior))
			{
				engineDataModel = parse(modelStream, this.FeatureSwitchProvider);
			}
			return engineDataModel;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000034B8 File Offset: 0x000016B8
		public ConceptualSchemaAndCapabilities GetConceptualSchema(string modelIdFromClient, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, ParseConceptualSchema parse)
		{
			ModelDaxCapabilities modelDaxCapabilities;
			IConceptualSchema conceptualSchema;
			using (Stream modelStream = this.GetModelStream(modelIdFromClient, maxModelMetadataVersion, translationsBehavior))
			{
				conceptualSchema = parse(modelStream, this.SchemaBuilderOptions, out modelDaxCapabilities);
			}
			return new ConceptualSchemaAndCapabilities(conceptualSchema, modelDaxCapabilities);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003504 File Offset: 0x00001704
		public DataSet GetSchemaDataSet(string modelIdFromClient, string schemaName, IReadOnlyDictionary<string, object> restrictions)
		{
			string modelId = this.CreateModelKeyFromIdWithDefaultVersion(modelIdFromClient).ModelId;
			RSDataSourceConnection rsdataSourceConnection;
			IASConnectionInfo iasconnectionInfo;
			SchemaCommandRequest schemaCommandRequest = new SchemaCommandRequest(this._dataSourceProvider.GetDataSourceInfo(modelId, out rsdataSourceConnection, out iasconnectionInfo), RSPowerViewHandler._connectionPool, DataShapingHelper.DefaultConnectionFactory, DefaultQueryExecutionOptions.Instance, RSPowerViewHandler.GetImpersonatorOrDefault(rsdataSourceConnection.IdentityToImpersonate), null);
			return this._modelMetadataProvider.GetSchemaDataSet(schemaCommandRequest, schemaName, restrictions);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000355D File Offset: 0x0000175D
		public IConnectionFactory GetConnectionFactory(string databaseID)
		{
			throw new NotSupportedException("RSPowerViewHandler.GetConnectionFactory");
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003569 File Offset: 0x00001769
		public IConnectionPool GetConnectionPool(string databaseID)
		{
			return this._connectionPoolAdapter;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003574 File Offset: 0x00001774
		public IDataShapingDataSourceInfo GetDataSourceInfo(string databaseId)
		{
			RSDataSourceConnection rsdataSourceConnection;
			IASConnectionInfo iasconnectionInfo;
			ExploreHostDataSourceInfo dataSourceInfo = this._dataSourceProvider.GetDataSourceInfo(databaseId, out rsdataSourceConnection, out iasconnectionInfo);
			return new RSDataShapingDataSourceInfo(dataSourceInfo.Name, dataSourceInfo.Extension, dataSourceInfo.ConnectionString, this.ToProcessingConnectionSecurity(rsdataSourceConnection.ConnectionSecurity), dataSourceInfo.Category, rsdataSourceConnection.IdentityToImpersonate.Name);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000035C8 File Offset: 0x000017C8
		public IConnectionUserImpersonator GetConnectionUserImpersonator(string databaseId)
		{
			RSDataSourceConnection rsdataSourceConnection;
			IASConnectionInfo iasconnectionInfo;
			this._dataSourceProvider.GetDataSourceInfo(databaseId, out rsdataSourceConnection, out iasconnectionInfo);
			return RSPowerViewHandler.GetImpersonatorOrDefault(rsdataSourceConnection.IdentityToImpersonate);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000035F1 File Offset: 0x000017F1
		private ConnectionSecurity ToProcessingConnectionSecurity(RSConnectionSecurity connectionSecurity)
		{
			if (connectionSecurity == RSConnectionSecurity.Integrated)
			{
				return ConnectionSecurity.UseIntegratedSecurity;
			}
			return ConnectionSecurity.ImpersonateWindowsUser;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000035F9 File Offset: 0x000017F9
		public IReportingSession GetActiveSession(string databaseID)
		{
			throw new NotSupportedException("RSPowerViewHandler.GetActiveSession");
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003605 File Offset: 0x00001805
		public string GetModelString(ASConnectionInfo asConnectionInfo, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior)
		{
			throw new NotSupportedException("RSPowerViewHandler.GetModelString");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003611 File Offset: 0x00001811
		public Microsoft.DataShaping.ServiceContracts.ITelemetryService CreateTelemetryServiceForRequest(string databaseID)
		{
			return this._telemetryService;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003619 File Offset: 0x00001819
		public void EnsureSession(string modelId)
		{
			if (this.GetOrAddModel(this.CreateModelKeyFromIdWithDefaultVersion(modelId)) == null)
			{
				throw new InvalidOperationException("No model exists for database with ID: " + modelId);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000363C File Offset: 0x0000183C
		public async Task EnsureSessionAsync(string modelId)
		{
			ModelKey modelKey = await this.CreateModelKeyFromIdWithDefaultVersionAsync(modelId);
			if (this.GetOrAddModel(modelKey) == null)
			{
				throw new InvalidOperationException("No model exists for database with ID: " + modelId);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003688 File Offset: 0x00001888
		public ModelLocation GetModelLocation(string databaseID)
		{
			ModelInfo orAddModel = this.GetOrAddModel(this.CreateModelKeyFromIdWithDefaultVersion(databaseID));
			if (orAddModel != null)
			{
				return orAddModel.ModelLocation;
			}
			return ModelLocation.Internal;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000036AE File Offset: 0x000018AE
		public void CreateNewReportingSession(ASConnectionInfo connectionInfo, LuciaSessionParameters luciaSessionParameters, QueryExecutionOptionsBase queryExecutionOptions = null, SchemaOptions schemaOptions = null, InsightsSessionParameters insightsSessionParameters = null)
		{
			throw new NotSupportedException("RSPowerViewHandler.CreateNewReportingSession");
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000036BA File Offset: 0x000018BA
		public void DisconnectReportingSession(string databaseID)
		{
			throw new NotSupportedException("RSPowerViewHandler.DisconnectReportingSession");
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000036C6 File Offset: 0x000018C6
		public void ClearAllModelCaches(DateTime? modelLastModifiedTime = null)
		{
			RSPowerViewHandler._modelCache.Clear();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000036D4 File Offset: 0x000018D4
		public void ClearModelCache(string modelId, DateTime? modelLastModifiedTime = null)
		{
			foreach (string text in RSPowerViewHandler._modelVersions)
			{
				RSPowerViewHandler._modelCache.Remove(this.CreateModelKeyFromId(modelId, text, TranslationsBehavior.PrintAll));
				RSPowerViewHandler._modelCache.Remove(this.CreateModelKeyFromId(modelId, text, TranslationsBehavior.Default));
				RSPowerViewHandler._modelCache.Remove(this.CreateModelKeyFromId(modelId, text, TranslationsBehavior.Ignore));
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003758 File Offset: 0x00001958
		public QueryExecutionOptions GetDSEQueryExecutionOptions(string databaseId)
		{
			return QueryExecutionOptions.Default;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000375F File Offset: 0x0000195F
		public ConceptualSchemaAndCapabilities GetConceptualSchema(ASConnectionInfo asConnectionInfo, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior, ParseConceptualSchema parser)
		{
			throw new NotImplementedException("Getting ConceptualSchema requires an active ReportingSession");
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000376B File Offset: 0x0000196B
		public void ClearCachesForDataSource(string connectionString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003774 File Offset: 0x00001974
		private ModelInfo GetOrAddModel(ModelKey key)
		{
			return RSPowerViewHandler._modelCache.GetOrAddModel(key, delegate
			{
				RSConnectionTrace.Trace(TraceType.Verbose, "Model {0} not found in cache, will be fetched", new object[] { key.CacheKey });
				return this.GetModelInfo(key.ModelId, key.ModelMetadataVersion);
			});
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000037B4 File Offset: 0x000019B4
		private ModelInfo GetModelInfo(string id, string maxModelMetadataVersion)
		{
			IASConnectionInfo iasconnectionInfo;
			string modelCSDL = this.GetModelCSDL(id, maxModelMetadataVersion, out iasconnectionInfo);
			return new ModelInfo(id, iasconnectionInfo, modelCSDL);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000037D4 File Offset: 0x000019D4
		private static IConnectionUserImpersonator GetImpersonatorOrDefault(IIdentity effectiveUserIdentity)
		{
			if (effectiveUserIdentity == null)
			{
				return null;
			}
			return UserIdentityConnectionImpersonatorFactory.CreateImpersonator(effectiveUserIdentity);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000037E4 File Offset: 0x000019E4
		private ModelKey CreateModelKeyFromId(string modelIdFromClient, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior)
		{
			RSDataSourceConnection rsdataSourceConnection;
			IASConnectionInfo iasconnectionInfo;
			this._dataSourceProvider.GetDataSourceInfo(modelIdFromClient, out rsdataSourceConnection, out iasconnectionInfo);
			return new ModelKey(modelIdFromClient, rsdataSourceConnection.GetConnectionKey(), maxModelMetadataVersion, translationsBehavior);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003810 File Offset: 0x00001A10
		private async Task<ModelKey> CreateModelKeyFromIdAsync(string modelIdFromClient, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior)
		{
			ExploreHostDataSourceConnectionInfo exploreHostDataSourceConnectionInfo = await this._dataSourceProvider.GetDataSourceInfoAsync(modelIdFromClient);
			return new ModelKey(modelIdFromClient, exploreHostDataSourceConnectionInfo.RsDataSourceConnection.GetConnectionKey(), maxModelMetadataVersion, translationsBehavior);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000386C File Offset: 0x00001A6C
		private ModelKey CreateModelKeyFromIdWithDefaultVersion(string modelIdFromClient)
		{
			string text = "2.0";
			return this.CreateModelKeyFromId(modelIdFromClient, text, TranslationsBehavior.Default);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003888 File Offset: 0x00001A88
		private async Task<ModelKey> CreateModelKeyFromIdWithDefaultVersionAsync(string modelIdFromClient)
		{
			string text = "2.0";
			return await this.CreateModelKeyFromIdAsync(modelIdFromClient, text, TranslationsBehavior.Default);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000038D4 File Offset: 0x00001AD4
		public string GetModelString(string id, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior)
		{
			IASConnectionInfo iasconnectionInfo;
			return this.GetModelCSDL(id, maxModelMetadataVersion, out iasconnectionInfo);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000038EC File Offset: 0x00001AEC
		private string GetModelCSDL(string id, string maxModelMetadataVersion, out IASConnectionInfo asConnectionInfo)
		{
			RSDataSourceConnection rsdataSourceConnection;
			ModelMetadataRequest modelMetadataRequest = new ModelMetadataRequest(this._dataSourceProvider.GetDataSourceInfo(id, out rsdataSourceConnection, out asConnectionInfo), TranslationsBehavior.Default, maxModelMetadataVersion, RSPowerViewHandler._connectionPool, DataShapingHelper.DefaultConnectionFactory, DefaultQueryExecutionOptions.Instance, RSPowerViewHandler.GetImpersonatorOrDefault(rsdataSourceConnection.IdentityToImpersonate), null, ConnectionType.Stable);
			return this._modelMetadataProvider.GetModelMetadata(modelMetadataRequest);
		}

		// Token: 0x04000059 RID: 89
		private readonly IModelMetadataProvider _modelMetadataProvider;

		// Token: 0x0400005A RID: 90
		private readonly IRSPowerViewDataSourceProvider _dataSourceProvider;

		// Token: 0x0400005B RID: 91
		private static readonly IDbConnectionPool _connectionPool = new RSConnectionPool();

		// Token: 0x0400005C RID: 92
		private static readonly RSModelCache _modelCache = new RSModelCache();

		// Token: 0x0400005D RID: 93
		private static readonly List<string> _modelVersions = new List<string> { "2.0", "2.5" };

		// Token: 0x0400005E RID: 94
		private readonly Microsoft.DataShaping.ServiceContracts.ITelemetryService _telemetryService;

		// Token: 0x0400005F RID: 95
		private readonly IConnectionPool _connectionPoolAdapter;
	}
}
