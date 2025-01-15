using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.PowerBI.ReportingServicesHost.Insights;
using Microsoft.PowerBI.ReportingServicesHost.Utils;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200005D RID: 93
	internal class PowerViewHandler : IPowerViewHandler, IReportingSessionProvider, IDataSourceStateProvider
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00005AF7 File Offset: 0x00003CF7
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00005AFF File Offset: 0x00003CFF
		public FeatureSwitches FeatureSwitches { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00005B08 File Offset: 0x00003D08
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00005B10 File Offset: 0x00003D10
		public IReportingSessionCreator SessionFactory
		{
			get
			{
				return this.sessionFactory;
			}
			internal set
			{
				this.sessionFactory = value;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00005B1C File Offset: 0x00003D1C
		public PowerViewHandler(ILuciaSessionFactory luciaSessionFactory = null, TelemetrySettings telemetrySettings = null, IFeatureSwitchesProxy featureSwitchProxy = null, IModelMetadataProvider modelMetadataProvider = null, IInsightsSessionFactory insightsSessionFactory = null)
		{
			this.activeSessions = new ConcurrentDictionary<string, IReportingSession>();
			this.activeSessionConnectionFactories = new ConcurrentDictionary<string, IConnectionFactory>();
			this.activeSessionConnectionPools = new ConcurrentDictionary<string, IConnectionPool>();
			this.sessionFactory = new ReportingSessionCreator(luciaSessionFactory, insightsSessionFactory);
			this.telemetrySettings = telemetrySettings;
			this.FeatureSwitches = new FeatureSwitches(featureSwitchProxy);
			this.modelMetadataProvider = modelMetadataProvider ?? ModelMetadataProvider.CreateInstance(new ExploreTelemetryService(TelemetryService.Instance), false);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00005B90 File Offset: 0x00003D90
		public void Shutdown()
		{
			foreach (IReportingSession reportingSession in this.activeSessions.Values)
			{
				((ReportingSession)reportingSession).Dispose();
			}
			this.modelMetadataProvider.Dispose();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00005BF0 File Offset: 0x00003DF0
		public void DisconnectReportingSession(string databaseID)
		{
			IReportingSession reportingSession;
			if (this.activeSessions.TryRemove(databaseID, out reportingSession))
			{
				IConnectionFactory connectionFactory;
				this.activeSessionConnectionFactories.TryRemove(databaseID, out connectionFactory);
				IConnectionPool connectionPool;
				this.activeSessionConnectionPools.TryRemove(databaseID, out connectionPool);
				reportingSession.Dispose();
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00005C31 File Offset: 0x00003E31
		public string GetModelString(string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior)
		{
			return this.GetActiveSession(databaseID).GetModelString(maxModelMetadataVersion, translationsBehavior);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00005C44 File Offset: 0x00003E44
		public string GetModelString(ASConnectionInfo asConnectionInfo, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior)
		{
			string modelMetadata;
			using (ConnectionPool connectionPool = new ConnectionPool())
			{
				ModelMetadataRequest modelMetadataRequest = new ModelMetadataRequest(DataShapingHelper.CreateDataSourceInfo("EntityDataSource", asConnectionInfo), translationsBehavior, maxModelMetadataVersion, connectionPool, DataShapingHelper.DefaultConnectionFactory, DefaultQueryExecutionOptions.Instance, null, DataShapingTelemetryService.CreateForRequest(this.telemetrySettings), ConnectionType.Transient);
				modelMetadata = this.modelMetadataProvider.GetModelMetadata(modelMetadataRequest);
			}
			return modelMetadata;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00005CAC File Offset: 0x00003EAC
		public EngineDataModel GetEngineDataModel(string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, Func<Stream, IFeatureSwitchProvider, EngineDataModel> parser)
		{
			return this.GetActiveSession(databaseID).GetEngineDataModel(maxModelMetadataVersion, translationsBehavior, parser);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00005CBE File Offset: 0x00003EBE
		public ConceptualSchemaAndCapabilities GetConceptualSchema(string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, ParseConceptualSchema parser)
		{
			return this.GetActiveSession(databaseID).GetConceptualSchema(maxModelMetadataVersion, translationsBehavior, parser);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00005CD0 File Offset: 0x00003ED0
		public ConceptualSchemaAndCapabilities GetConceptualSchema(ASConnectionInfo asConnectionInfo, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior, ParseConceptualSchema parser)
		{
			ConceptualSchemaAndCapabilities conceptualSchema;
			using (ConnectionPool connectionPool = new ConnectionPool())
			{
				ModelMetadataRequest modelMetadataRequest = new ModelMetadataRequest(DataShapingHelper.CreateDataSourceInfo("EntityDataSource", asConnectionInfo), translationsBehavior, maxModelMetadataVersion, connectionPool, DataShapingHelper.DefaultConnectionFactory, DefaultQueryExecutionOptions.Instance, null, DataShapingTelemetryService.CreateForRequest(this.telemetrySettings), ConnectionType.Transient);
				conceptualSchema = this.modelMetadataProvider.GetConceptualSchema(modelMetadataRequest, ConceptualSchemaExtensions.CreateConceptualSchemaBuilderOptions(this.FeatureSwitches), parser);
			}
			return conceptualSchema;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00005D48 File Offset: 0x00003F48
		public DataSet GetSchemaDataSet(string databaseID, string schemaName, IReadOnlyDictionary<string, object> restrictions)
		{
			return this.GetActiveSession(databaseID).GetSchemaDataSet(schemaName, restrictions);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00005D58 File Offset: 0x00003F58
		public ModelLocation GetModelLocation(string databaseID)
		{
			return this.GetActiveSession(databaseID).GetDataSource().ModelLocation;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005D6B File Offset: 0x00003F6B
		public void EnsureSession(string databaseID)
		{
			if (!this.activeSessions.ContainsKey(databaseID))
			{
				throw new InvalidOperationException("No session exists for database with ID:" + databaseID);
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00005D8C File Offset: 0x00003F8C
		public Task EnsureSessionAsync(string databaseID)
		{
			this.EnsureSession(databaseID);
			return Task.CompletedTask;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005D9A File Offset: 0x00003F9A
		public void ClearModelCache(string databaseID, DateTime? modelLastModifiedTime = null)
		{
			this.GetActiveSession(databaseID).ClearModelCache(modelLastModifiedTime);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00005DA9 File Offset: 0x00003FA9
		public void ClearAllModelCaches(DateTime? modelLastModifiedTime = null)
		{
			this.modelMetadataProvider.ClearAllModelCaches();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public IReportingSession GetActiveSession(string databaseID)
		{
			IReportingSession reportingSession;
			if (!this.activeSessions.TryGetValue(databaseID, out reportingSession))
			{
				throw new InvalidOperationException("No session exists for database with ID:" + databaseID);
			}
			return reportingSession;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00005DE7 File Offset: 0x00003FE7
		public IDataShapingDataSourceInfo GetDataSourceInfo(string databaseId)
		{
			return this.GetActiveSession(databaseId).GetDataSource();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00005DF5 File Offset: 0x00003FF5
		public IConnectionUserImpersonator GetConnectionUserImpersonator(string databaseId)
		{
			return null;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public IConnectionFactory GetConnectionFactory(string databaseID)
		{
			if (!this.activeSessionConnectionFactories.ContainsKey(databaseID))
			{
				this.activeSessionConnectionFactories.TryAdd(databaseID, new ActiveReportingSessionConnectionFactory(this, databaseID));
			}
			return this.activeSessionConnectionFactories[databaseID];
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00005E28 File Offset: 0x00004028
		public IConnectionPool GetConnectionPool(string databaseID)
		{
			if (!this.activeSessionConnectionPools.ContainsKey(databaseID))
			{
				this.activeSessionConnectionPools.TryAdd(databaseID, new ActiveReportingSessionConnectionPool(this, databaseID));
			}
			return this.activeSessionConnectionPools[databaseID];
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00005E58 File Offset: 0x00004058
		public Microsoft.DataShaping.ServiceContracts.ITelemetryService CreateTelemetryServiceForRequest(string databaseId)
		{
			return this.GetActiveSession(databaseId).CreateTelemetryServiceForRequest();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00005E66 File Offset: 0x00004066
		public QueryExecutionOptions GetDSEQueryExecutionOptions(string databaseId)
		{
			return this.GetActiveSession(databaseId).DSEQueryExecutionOptions;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00005E74 File Offset: 0x00004074
		public void CreateNewReportingSession(ASConnectionInfo connectionInfo, LuciaSessionParameters luciaSessionParameters, QueryExecutionOptionsBase queryExecutionOptions = null, SchemaOptions schemaOptions = null, InsightsSessionParameters insightsSessionParameters = null)
		{
			if (!this.activeSessions.ContainsKey(connectionInfo.DatabaseID))
			{
				TelemetrySettings telemetrySettings = (connectionInfo.EnableTelemetry ? this.telemetrySettings : null);
				IReportingSession reportingSession = this.sessionFactory.CreateReportingSession(this.modelMetadataProvider, connectionInfo, this.FeatureSwitches, luciaSessionParameters, queryExecutionOptions ?? DefaultQueryExecutionOptions.Instance, schemaOptions ?? new SchemaOptions(TranslationsBehavior.Default), telemetrySettings, insightsSessionParameters);
				this.activeSessions.TryAdd(connectionInfo.DatabaseID, reportingSession);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00005EEC File Offset: 0x000040EC
		public void ClearCachesForDataSource(string connectionString)
		{
			this.modelMetadataProvider.ClearCachesForDataSource(connectionString);
		}

		// Token: 0x0400013A RID: 314
		private readonly TelemetrySettings telemetrySettings;

		// Token: 0x0400013B RID: 315
		private readonly ConcurrentDictionary<string, IReportingSession> activeSessions;

		// Token: 0x0400013C RID: 316
		private readonly ConcurrentDictionary<string, IConnectionFactory> activeSessionConnectionFactories;

		// Token: 0x0400013D RID: 317
		private readonly ConcurrentDictionary<string, IConnectionPool> activeSessionConnectionPools;

		// Token: 0x0400013E RID: 318
		private readonly IModelMetadataProvider modelMetadataProvider;

		// Token: 0x0400013F RID: 319
		private IReportingSessionCreator sessionFactory;
	}
}
