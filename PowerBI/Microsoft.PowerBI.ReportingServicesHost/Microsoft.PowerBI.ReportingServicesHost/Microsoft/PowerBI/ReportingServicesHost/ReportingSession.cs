using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Edm;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Msolap;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.PowerBI.ReportingServicesHost.Insights;
using Microsoft.PowerBI.ReportingServicesHost.Utils;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.Library;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200005E RID: 94
	internal sealed class ReportingSession : IReportingSession, IDisposable
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00005EFC File Offset: 0x000040FC
		internal ReportingSession(IModelMetadataProvider modelMetadataProvider, IASConnectionInfo asConnectionInfo, FeatureSwitches featureSwitches, ILuciaSessionFactory luciaSessionFactory, LuciaSessionParameters luciaSessionParameters, QueryExecutionOptionsBase queryExecutionOptions, SchemaOptions schemaOptions, TelemetrySettings telemetrySettings = null, IInsightsSessionFactory insightsSessionFactory = null, InsightsSessionParameters insightsSessionParameters = null)
		{
			this.m_modelMetadataProvider = modelMetadataProvider;
			this.m_connectionPool = new ConnectionPool();
			this.m_dataShapingConnectionPool = new DataShapingConnectionPoolAdapter(this.m_connectionPool);
			this.m_telemetrySettings = telemetrySettings;
			this.m_featureSwitches = featureSwitches;
			this.m_featureSwitchProvider = DataShapingFeatureSwitchProvider.Create(featureSwitches);
			this.m_schemaBuilderOptions = ConceptualSchemaExtensions.CreateConceptualSchemaBuilderOptions(this.m_featureSwitches);
			this.m_dataSourceInfo = DataShapingHelper.CreateDataSourceInfo("EntityDataSource", asConnectionInfo);
			this.m_luciaSession = this.CreateLuciaSession(luciaSessionFactory, luciaSessionParameters);
			this.m_insightsSession = this.CreateInsightsSession(insightsSessionFactory, insightsSessionParameters);
			this.m_queryExecutionOptions = queryExecutionOptions;
			this.m_schemaOptions = schemaOptions;
			this.DSEQueryExecutionOptions = queryExecutionOptions.ToDSEQueryExecutionOptions();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00005FAC File Offset: 0x000041AC
		public ModelMetadataRequest GetModelMetadataRequest(string modelMetadataVersion, TranslationsBehavior? translationsBehavior)
		{
			return new ModelMetadataRequest(this.m_dataSourceInfo, this.ResolveTranslationsBehaviorToSessionDefault(translationsBehavior), modelMetadataVersion, this.m_connectionPool, this.GetConnectionFactory(), this.m_queryExecutionOptions, null, this.CreateTelemetryServiceForRequest(), ConnectionType.Stable);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00005FE6 File Offset: 0x000041E6
		public SchemaCommandRequest GetSchemaCommandRequest()
		{
			return new SchemaCommandRequest(this.m_dataSourceInfo, this.m_connectionPool, this.GetConnectionFactory(), this.m_queryExecutionOptions, null, this.CreateTelemetryServiceForRequest());
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000600C File Offset: 0x0000420C
		public QueryExecutionOptions DSEQueryExecutionOptions { get; }

		// Token: 0x0600021B RID: 539 RVA: 0x00006014 File Offset: 0x00004214
		public ILuciaSession GetLuciaSession()
		{
			return this.m_luciaSession;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000601C File Offset: 0x0000421C
		public IInsightsSession GetInsightsSession()
		{
			return this.m_insightsSession;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00006024 File Offset: 0x00004224
		public string GetModelString(string modelMetadataVersion, TranslationsBehavior? translationsBehavior)
		{
			ModelMetadataRequest modelMetadataRequest = this.GetModelMetadataRequest(modelMetadataVersion, translationsBehavior);
			return this.m_modelMetadataProvider.GetModelMetadata(modelMetadataRequest);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006048 File Offset: 0x00004248
		public DataSet GetSchemaDataSet(string schemaName, IReadOnlyDictionary<string, object> restrictions)
		{
			SchemaCommandRequest schemaCommandRequest = this.GetSchemaCommandRequest();
			return this.m_modelMetadataProvider.GetSchemaDataSet(schemaCommandRequest, schemaName, restrictions);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000606C File Offset: 0x0000426C
		public EngineDataModel GetEngineDataModel(string modelMetadataVersion, TranslationsBehavior? translationsBehavior, Func<Stream, IFeatureSwitchProvider, EngineDataModel> parse)
		{
			ModelMetadataRequest modelMetadataRequest = this.GetModelMetadataRequest(modelMetadataVersion, translationsBehavior);
			return this.m_modelMetadataProvider.GetEngineDataModel(modelMetadataRequest, this.m_featureSwitchProvider, parse);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006098 File Offset: 0x00004298
		public ConceptualSchemaAndCapabilities GetConceptualSchema(string modelMetadataVersion, TranslationsBehavior? translationsBehavior, ParseConceptualSchema parse)
		{
			ModelMetadataRequest modelMetadataRequest = this.GetModelMetadataRequest(modelMetadataVersion, translationsBehavior);
			return this.m_modelMetadataProvider.GetConceptualSchema(modelMetadataRequest, this.m_schemaBuilderOptions, parse);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000060C1 File Offset: 0x000042C1
		public ExploreHostDataSourceInfo GetDataSource()
		{
			return this.m_dataSourceInfo;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000060CC File Offset: 0x000042CC
		private TranslationsBehavior ResolveTranslationsBehaviorToSessionDefault(TranslationsBehavior? translationsBehavior)
		{
			TranslationsBehavior? translationsBehavior2 = translationsBehavior;
			if (translationsBehavior2 == null)
			{
				return this.m_schemaOptions.TranslationsBehavior;
			}
			return translationsBehavior2.GetValueOrDefault();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000060F8 File Offset: 0x000042F8
		private IInsightsSession CreateInsightsSession(IInsightsSessionFactory insightsSessionFactory, InsightsSessionParameters insightsSessionParameters)
		{
			if (insightsSessionFactory == null || insightsSessionParameters == null)
			{
				return null;
			}
			Func<string, ParseConceptualSchema, IConceptualSchema> func = (string version, ParseConceptualSchema parser) => this.GetConceptualSchema(version, null, parser).ConceptualSchema;
			return insightsSessionFactory.CreateInsightsSession(insightsSessionParameters, this.GetConnectionFactory(), this.GetConnectionPool(), this.m_dataSourceInfo, this.m_featureSwitches, this.CreateTelemetryServiceForRequest(), func);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006140 File Offset: 0x00004340
		private ILuciaSession CreateLuciaSession(ILuciaSessionFactory luciaSessionFactory, LuciaSessionParameters luciaSessionParameters)
		{
			if (luciaSessionFactory != null)
			{
				this.GetDataSource();
				Func<string> func = () => this.GetModelString("2.5", null);
				return luciaSessionFactory.CreateLuciaSession(this.GetConnectionFactory(), this.GetConnectionPool(), this.m_dataSourceInfo, func, new Func<string, IReadOnlyDictionary<string, object>, DataSet>(this.GetSchemaDataSet), luciaSessionParameters, this.m_featureSwitches);
			}
			return null;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006194 File Offset: 0x00004394
		public void Dispose()
		{
			this.m_modelMetadataProvider.ClearCachesForDataSource(this.m_dataSourceInfo.ConnectionString);
			if (this.m_connectionPool != null)
			{
				this.m_connectionPool.Dispose();
				this.m_connectionPool = null;
			}
			if (this.m_luciaSession != null)
			{
				this.m_luciaSession.Dispose();
			}
			if (this.m_insightsSession != null)
			{
				this.m_insightsSession.Dispose();
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000061F8 File Offset: 0x000043F8
		public void ClearModelCache(DateTime? modelLastModifiedTime)
		{
			this.m_modelMetadataProvider.ClearCachesForDataSource(this.m_dataSourceInfo.ConnectionString);
			if (this.m_luciaSession != null && modelLastModifiedTime != null)
			{
				this.m_luciaSession.NotifyModelChanged(new LuciaSessionModelChangedArgs(modelLastModifiedTime.Value, null, null));
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006245 File Offset: 0x00004445
		public IConnectionFactory GetConnectionFactory()
		{
			return DataExtensionFactory.CreateDefaultConnectionFactory(DataShapingTracer.Instance, DataExtensionPrivateInformationService.Instance, this.m_featureSwitches.MsolapTracingEnabled);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006261 File Offset: 0x00004461
		public IConnectionPool GetConnectionPool()
		{
			return this.m_dataShapingConnectionPool;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006269 File Offset: 0x00004469
		public Microsoft.DataShaping.ServiceContracts.ITelemetryService CreateTelemetryServiceForRequest()
		{
			return DataShapingTelemetryService.CreateForRequest(this.m_telemetrySettings);
		}

		// Token: 0x04000141 RID: 321
		public const string DefaultDataSourceName = "EntityDataSource";

		// Token: 0x04000142 RID: 322
		private readonly IModelMetadataProvider m_modelMetadataProvider;

		// Token: 0x04000143 RID: 323
		private readonly ILuciaSession m_luciaSession;

		// Token: 0x04000144 RID: 324
		private readonly IInsightsSession m_insightsSession;

		// Token: 0x04000145 RID: 325
		private readonly TelemetrySettings m_telemetrySettings;

		// Token: 0x04000146 RID: 326
		private readonly QueryExecutionOptionsBase m_queryExecutionOptions;

		// Token: 0x04000147 RID: 327
		private readonly SchemaOptions m_schemaOptions;

		// Token: 0x04000148 RID: 328
		private readonly IConnectionPool m_dataShapingConnectionPool;

		// Token: 0x04000149 RID: 329
		private readonly FeatureSwitches m_featureSwitches;

		// Token: 0x0400014A RID: 330
		private readonly DataShapingFeatureSwitchProvider m_featureSwitchProvider;

		// Token: 0x0400014B RID: 331
		private readonly ConceptualSchemaBuilderOptions m_schemaBuilderOptions;

		// Token: 0x0400014C RID: 332
		private ExploreHostDataSourceInfo m_dataSourceInfo;

		// Token: 0x0400014D RID: 333
		private ConnectionPool m_connectionPool;
	}
}
