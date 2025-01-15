using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Edm;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.ReportingServicesHost.Utils;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000053 RID: 83
	internal class ModelMetadataProvider : IModelMetadataProvider, IDisposable
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00005154 File Offset: 0x00003354
		private ModelMetadataProvider(IExploreTelemetryService telemetryService, bool cachelessMode)
		{
			this.m_telemetryService = telemetryService;
			if (cachelessMode)
			{
				this.m_modelCache = new NoOpModelCache<string>();
				this.m_engineDataModelCache = new NoOpModelCache<EngineDataModel>();
				this.m_parsedConceptualSchemaAndCapabilitiesCache = new NoOpModelCache<ConceptualSchemaAndCapabilities>();
				return;
			}
			this.m_modelCache = new ModelCache<string>(ModelMetadataProvider.ModelCacheConfig);
			this.m_engineDataModelCache = new ModelCache<EngineDataModel>(ModelMetadataProvider.ModelCacheConfig);
			this.m_parsedConceptualSchemaAndCapabilitiesCache = new ModelCache<ConceptualSchemaAndCapabilities>(ModelMetadataProvider.ModelCacheConfig);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000051CE File Offset: 0x000033CE
		public static ModelMetadataProvider CreateInstance(IExploreTelemetryService telemetryService, bool cachelessMode = false)
		{
			return new ModelMetadataProvider(telemetryService, cachelessMode);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000051D8 File Offset: 0x000033D8
		public string GetModelMetadata(ModelMetadataRequest request)
		{
			string text;
			if (this.m_modelCache.TryGetValue(request.DataSource.ConnectionString, request.ModelMetadaVersion, request.TranslationsBehavior, out text))
			{
				return text;
			}
			object getModelLock = this.m_getModelLock;
			bool flag = false;
			try
			{
				Monitor.Enter(getModelLock, ref flag);
				if (this.m_modelCache.TryGetValue(request.DataSource.ConnectionString, request.ModelMetadaVersion, request.TranslationsBehavior, out text))
				{
					return text;
				}
				Func<IDbSchemaCommand, string> action = (IDbSchemaCommand command) => command.GetModelMetadata(request.DataSource.DatabaseName, request.DataSource.CubeName, request.ModelMetadaVersion, request.TranslationsBehavior);
				text = this.m_telemetryService.RunInActivity<string>("GetModelMetadata", (ExploreBaseEvent timedEvent) => this.ExecuteSchemaCommand<string>(request.DataSource, request.ConnectionPool, request.ConnectionFactory, request.ConnectionUserImpersonator, request.QueryExecutionOptions, request.TelemetryService, action, "GetModelMetadata", ServiceErrorStatusCode.CsdlFetching, timedEvent));
				text = text ?? ModelMetadataProvider.EmptyModelString;
				this.m_modelCache.Add(request.DataSource.ConnectionString, request.ModelMetadaVersion, request.TranslationsBehavior, text, request.ConnectionType);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(getModelLock);
				}
			}
			return text;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005364 File Offset: 0x00003564
		public DataSet GetSchemaDataSet(SchemaCommandRequest request, string schemaName, IReadOnlyDictionary<string, object> restrictions)
		{
			Func<IDbSchemaCommand, DataSet> action = (IDbSchemaCommand command) => command.GetSchemaDataSet(schemaName, restrictions);
			return this.m_telemetryService.RunInActivity<DataSet>("GetSchemaDataSet", (ExploreBaseEvent timedEvent) => this.ExecuteSchemaCommand<DataSet>(request.DataSource, request.ConnectionPool, request.ConnectionFactory, request.ConnectionUserImpersonator, request.QueryExecutionOptions, request.TelemetryService, action, StringUtil.FormatInvariant("GetSchemaDataSet({0})", schemaName), ServiceErrorStatusCode.GetSchemaDataSetError, timedEvent));
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000053C4 File Offset: 0x000035C4
		public ConceptualSchemaAndCapabilities GetConceptualSchema(ModelMetadataRequest request, ConceptualSchemaBuilderOptions builderOptions, ParseConceptualSchema parse)
		{
			ConceptualSchemaAndCapabilities conceptualSchemaAndCapabilities;
			if (this.m_parsedConceptualSchemaAndCapabilitiesCache.TryGetValue(request.DataSource.ConnectionString, request.ModelMetadaVersion, request.TranslationsBehavior, out conceptualSchemaAndCapabilities))
			{
				return conceptualSchemaAndCapabilities;
			}
			ConceptualSchemaAndCapabilities conceptualSchemaAndCapabilities2;
			using (MemoryStream memoryStream = this.GetModelMetadata(request).AsStream())
			{
				ModelDaxCapabilities modelDaxCapabilities;
				conceptualSchemaAndCapabilities = new ConceptualSchemaAndCapabilities(parse(memoryStream, builderOptions, out modelDaxCapabilities), modelDaxCapabilities);
				this.m_parsedConceptualSchemaAndCapabilitiesCache.Add(request.DataSource.ConnectionString, request.ModelMetadaVersion, request.TranslationsBehavior, conceptualSchemaAndCapabilities, request.ConnectionType);
				conceptualSchemaAndCapabilities2 = conceptualSchemaAndCapabilities;
			}
			return conceptualSchemaAndCapabilities2;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005460 File Offset: 0x00003660
		public EngineDataModel GetEngineDataModel(ModelMetadataRequest request, IFeatureSwitchProvider featureSwitchProvider, Func<Stream, IFeatureSwitchProvider, EngineDataModel> parse)
		{
			EngineDataModel engineDataModel;
			if (this.m_engineDataModelCache.TryGetValue(request.DataSource.ConnectionString, request.ModelMetadaVersion, request.TranslationsBehavior, out engineDataModel))
			{
				return engineDataModel;
			}
			EngineDataModel engineDataModel2;
			using (MemoryStream memoryStream = this.GetModelMetadata(request).AsStream())
			{
				engineDataModel = parse(memoryStream, featureSwitchProvider);
				this.m_engineDataModelCache.Add(request.DataSource.ConnectionString, request.ModelMetadaVersion, request.TranslationsBehavior, engineDataModel, request.ConnectionType);
				engineDataModel2 = engineDataModel;
			}
			return engineDataModel2;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000054F4 File Offset: 0x000036F4
		public void ClearCachesForDataSource(string connectionString)
		{
			object getModelLock = this.m_getModelLock;
			lock (getModelLock)
			{
				this.m_modelCache.Clear(connectionString);
				this.m_engineDataModelCache.Clear(connectionString);
				this.m_parsedConceptualSchemaAndCapabilitiesCache.Clear(connectionString);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005554 File Offset: 0x00003754
		public void ClearAllModelCaches()
		{
			object getModelLock = this.m_getModelLock;
			lock (getModelLock)
			{
				this.m_modelCache.ClearAll();
				this.m_engineDataModelCache.ClearAll();
				this.m_parsedConceptualSchemaAndCapabilitiesCache.ClearAll();
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000055B0 File Offset: 0x000037B0
		public void Dispose()
		{
			this.m_modelCache.Dispose();
			this.m_engineDataModelCache.Dispose();
			this.m_parsedConceptualSchemaAndCapabilitiesCache.Dispose();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000055D4 File Offset: 0x000037D4
		private T ExecuteSchemaCommand<T>(ExploreHostDataSourceInfo dataSourceInfo, IDbConnectionPool connectionPool, IConnectionFactory connectionFactory, IConnectionUserImpersonator connectionUserImpersonator, QueryExecutionOptionsBase queryExecutionOptions, Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService, Func<IDbSchemaCommand, T> action, string failTelemetryMessage, ServiceErrorStatusCode statusCode, ExploreBaseEvent exploreEvent)
		{
			for (int i = 1; i <= queryExecutionOptions.ConnectionAttempts; i++)
			{
				try
				{
					using (PoolableConnectionWrapper poolableConnectionWrapper = PoolableConnectionWrapper.Create(dataSourceInfo.ConnectionString, connectionPool, connectionFactory))
					{
						poolableConnectionWrapper.OpenAsync(connectionUserImpersonator).WaitAndUnwrapException();
						using (IDbSchemaCommand dbSchemaCommand = poolableConnectionWrapper.Connection.CreateSchemaCommand())
						{
							string text;
							string text2;
							string text3;
							if (telemetryService != null && telemetryService.TryGetTelemetryIDs(out text, out text2, out text3))
							{
								dbSchemaCommand.SetTelemetryIds(text, text2, text3);
							}
							T t = action(dbSchemaCommand);
							exploreEvent.AddProperty("AttemptCount", i);
							return t;
						}
					}
				}
				catch (DataExtensionException ex)
				{
					if (i == queryExecutionOptions.ConnectionAttempts || !queryExecutionOptions.IsRetriableFailure(ex.ProviderErrorCode))
					{
						TelemetryService.Instance.Log(new PBIWinDataExtensionException(ex.GetType().FullName, ex.FormatExceptionDetails(false), ex.StackTrace, failTelemetryMessage));
						exploreEvent.AddProperty("AttemptCount", i);
						throw new CannotRetrieveModelException(ex, statusCode, new ModelLocation?(dataSourceInfo.ModelLocation));
					}
				}
				catch (Exception ex2)
				{
					TelemetryService.Instance.Log(new PBIWinProcessingError(ex2.GetType().FullName, ex2.StackTrace, failTelemetryMessage));
					if (AsynchronousExceptionDetection.IsStoppingException(ex2))
					{
						throw;
					}
					exploreEvent.AddProperty("AttemptCount", i);
					throw new CannotRetrieveModelException(ex2, ErrorSource.Unknown, statusCode, new ModelLocation?(dataSourceInfo.ModelLocation));
				}
			}
			return default(T);
		}

		// Token: 0x0400011A RID: 282
		public static readonly string EmptyModelString = "<Schema xmlns=\"http://schemas.microsoft.com/ado/2008/09/edm\" xmlns:edm_annotation=\"http://schemas.microsoft.com/ado/2009/02/edm/annotation\" xmlns:bi=\"http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions\" bi:Version=\"2.0\" Namespace=\"Sandbox\">\r\n                                                    <EntityContainer Name=\"Sandbox\">\r\n                                                      <bi:EntityContainer Caption=\"Microsoft_SQLServer_AnalysisServices\" Culture=\"en-US\">\r\n                                                        <bi:CompareOptions IgnoreCase=\"true\" />\r\n                                                      </bi:EntityContainer>\r\n                                                    </EntityContainer>\r\n                                                  </Schema>";

		// Token: 0x0400011B RID: 283
		private const int CacheMemoryLimitMegabytes = 100;

		// Token: 0x0400011C RID: 284
		private static ModelCacheConfig ModelCacheConfig = new ModelCacheConfig(TimeSpan.FromMinutes(5.0), 100, TimeSpan.FromMinutes(30.0));

		// Token: 0x0400011D RID: 285
		private readonly object m_getModelLock = new object();

		// Token: 0x0400011E RID: 286
		private readonly IModelCache<string> m_modelCache;

		// Token: 0x0400011F RID: 287
		private readonly IModelCache<EngineDataModel> m_engineDataModelCache;

		// Token: 0x04000120 RID: 288
		private readonly IModelCache<ConceptualSchemaAndCapabilities> m_parsedConceptualSchemaAndCapabilitiesCache;

		// Token: 0x04000121 RID: 289
		private readonly IExploreTelemetryService m_telemetryService;
	}
}
