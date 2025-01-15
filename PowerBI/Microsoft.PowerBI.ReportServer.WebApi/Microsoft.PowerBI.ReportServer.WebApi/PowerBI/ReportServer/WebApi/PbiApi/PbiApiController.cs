using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AnalysisServices;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Contracts;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.BIServer.HostingEnvironment.Request;
using Microsoft.BIServer.Telemetry.Services;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline;
using Microsoft.PowerBI.ExploreHost;
using Microsoft.PowerBI.Packaging.Storage;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.ReportServer.AsServer;
using Microsoft.PowerBI.ReportServer.AsServer.Artifacts;
using Microsoft.PowerBI.ReportServer.ExploreHost;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;
using Microsoft.PowerBI.ReportServer.WebApi.ASConnection;
using Microsoft.PowerBI.ReportServer.WebApi.Catalog;
using Microsoft.PowerBI.ReportServer.WebApi.Error;
using Microsoft.PowerBI.ReportServer.WebApi.Excel;
using Microsoft.PowerBI.ReportServer.WebApi.Extensions;
using Microsoft.PowerBI.ReportServer.WebApi.Logging;
using Microsoft.PowerBI.ReportServer.WebApi.Logon;
using Microsoft.PowerBI.ReportServer.WebApi.Pbix;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;
using Microsoft.ReportingServices.Portal.ODataClient.V2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x0200002B RID: 43
	[DisableCaching]
	public sealed class PbiApiController : ApiController
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000037E0 File Offset: 0x000019E0
		public PbiApiController(ICatalogService catalogService, IRSExploreHostFactory exploreHostFactory, IServiceErrorExtractor errorExtractor, IAnalysisServicesServer asServer, IPbixParser pbixParser, IPbixComponentsBuilder pbixComponentsBuilder, IDataModelArtifactsProvider dataSourceProvider, ICatalogDataModelDataSourceAccessor dataModelDataSourceAccessor, ICatalogDataModelRoleAccessor dataModelRoleAccessor, ICheckConnectionServices checkConnectionServices, ICatalogItemAccessor catalogItemAcessor, IPowerBIConfiguration configurationInfo, ICatalogDataAccessor catalogDataAccessor, IPbixTelemetryLogger pbixTelemetryLogger)
		{
			ContractExtensions.NotNull<ICatalogService>(catalogService, "catalogService");
			ContractExtensions.NotNull<IRSExploreHostFactory>(exploreHostFactory, "exploreHostFactory");
			ContractExtensions.NotNull<IServiceErrorExtractor>(errorExtractor, "errorExtractor");
			ContractExtensions.NotNull<IAnalysisServicesServer>(asServer, "asServer");
			ContractExtensions.NotNull<IPbixParser>(pbixParser, "pbixParser");
			ContractExtensions.NotNull<IPbixComponentsBuilder>(pbixComponentsBuilder, "pbixComponentsBuilder");
			ContractExtensions.NotNull<IDataModelArtifactsProvider>(dataSourceProvider, "dataSourceProvider");
			ContractExtensions.NotNull<ICatalogDataModelDataSourceAccessor>(dataModelDataSourceAccessor, "dataModelDataSourceAccessor");
			ContractExtensions.NotNull<ICatalogDataModelRoleAccessor>(dataModelRoleAccessor, "dataModelRoleAccessor");
			ContractExtensions.NotNull<ICheckConnectionServices>(checkConnectionServices, "checkConnectionServices");
			ContractExtensions.NotNull<ICatalogItemAccessor>(catalogItemAcessor, "catalogItemAcessor");
			ContractExtensions.NotNull<IPowerBIConfiguration>(configurationInfo, "configurationInfo");
			ContractExtensions.NotNull<IPbixTelemetryLogger>(pbixTelemetryLogger, "pbixTelemetryLogger");
			this._catalogService = catalogService;
			this._exploreHostFactory = exploreHostFactory;
			this._errorExtractor = errorExtractor;
			this._asServer = asServer;
			this._pbixParser = pbixParser;
			this._pbixComponentsBuilder = pbixComponentsBuilder;
			this._modelArtifactsProvider = dataSourceProvider;
			this._dataModelDataSourceAccessor = dataModelDataSourceAccessor;
			this._dataModelRoleAccessor = dataModelRoleAccessor;
			this._checkConnectionServices = checkConnectionServices;
			this._catalogItemAcessor = catalogItemAcessor;
			this._powerBiConfiguration = configurationInfo;
			this._catalogDataAccessor = catalogDataAccessor;
			this._pbixTelemetryLogger = pbixTelemetryLogger;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000038FC File Offset: 0x00001AFC
		private IRSExploreHost CreateExploreHost(Guid catalogItemId, bool passIdToAs, bool throwRlsError)
		{
			RequestContext requestContext = this.GetRequestContext();
			ReportServerLogger reportServerLogger = new ReportServerLogger();
			RSDataSourceProvider rsdataSourceProvider = new RSDataSourceProvider(base.User, catalogItemId, requestContext, this._catalogService, this._dataModelDataSourceAccessor, this._dataModelRoleAccessor, new UserService(), this._asServer, throwRlsError);
			string text = (passIdToAs ? requestContext.RequestID : null);
			string text2 = (passIdToAs ? requestContext.ClientSessionID : null);
			return this._exploreHostFactory.CreateRSExploreHost(reportServerLogger, rsdataSourceProvider, text, text2);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000396D File Offset: 0x00001B6D
		private RequestContext GetRequestContext()
		{
			return Microsoft.BIServer.HostingEnvironment.Request.RequestContext.GetFromCallContext() ?? new RequestContext("", "");
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003987 File Offset: 0x00001B87
		private long CalculateExternalConnectionModelId(Guid catalogItemId)
		{
			return (long)Math.Abs(catalogItemId.ToString().GetHashCode());
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000039A4 File Offset: 0x00001BA4
		[Route("~/api/explore/featureswitches")]
		[HttpGet]
		public IHttpActionResult GetFeatureSwitches()
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "GET", "~/api/explore/featureswitches" }))
			{
				JObject jobject = FeatureSwitchResponseFactory.CreateFeatureSwitchResponse();
				httpActionResult = this.Ok<JObject>(jobject);
			}
			return httpActionResult;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000039F8 File Offset: 0x00001BF8
		[Route("~/api/explore/reports/{catalogItemId}/modelsandexploration")]
		[HttpGet]
		public async Task<IHttpActionResult> GetModelsAndExplorationAsync(string catalogItemId)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "GET", "~/api/explore/reports/{catalogItemId}/modelsandexploration" }))
			{
				string requestId = this.GetRequestContext().RequestID;
				string clientSessionId = this.GetRequestContext().ClientSessionID;
				PbixComponents pbix;
				long num;
				try
				{
					Guid id = Guid.Parse(catalogItemId);
					PbixComponents pbixComponents = await this._catalogService.GetPbixComponentsAsync(base.User, id, requestId, clientSessionId, PbixReportComponents.PbixMetadata);
					pbix = pbixComponents;
					num = await this.PrepareModelAsync(id, pbix, requestId, clientSessionId);
					id = default(Guid);
				}
				catch (AmoException)
				{
					return this.InternalServerError();
				}
				catch (AsConnectionException ex)
				{
					return this.JsonStringResult(HttpStatusCode.InternalServerError, MinervaErrorBuilder.FromAsConnectionException(ex, requestId).ToString());
				}
				catch (FormatException)
				{
					return this.BadRequest();
				}
				catch (CatalogAccessException ex2)
				{
					if (ex2.HttpCode.ToString() != "0")
					{
						return this.StatusCode(ex2.HttpCode);
					}
					return this.NotFound();
				}
				JObject jobject = ModelAndExplorationResponseFactory.CreateExploration(this._powerBiConfiguration, num.ToString(), pbix.ReportDocument, pbix.ReportMobileState, pbix.HasCustomVisuals);
				httpActionResult = this.Ok<JObject>(jobject);
			}
			return httpActionResult;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A48 File Offset: 0x00001C48
		private async Task<long> PrepareModelAsync(Guid catalogItemId, PbixComponents pbix, string requestId, string clientSessionId)
		{
			Lazy<Stream> model = new Lazy<Stream>(() => new MemoryStream(this._catalogService.GetPbixComponentsAsync(this.User, catalogItemId, requestId, clientSessionId, PbixReportComponents.PbixMetadataAndModel).GetAwaiter().GetResult()
				.DataModel));
			long num;
			if (pbix.HasEmbeddedModels)
			{
				DateTime dateTime = await this._catalogItemAcessor.GetCatalogExtendedContentLastUpdate(catalogItemId, ExtendedContentType.DataModel);
				DateTime lastModelUpdate = dateTime;
				dateTime = await this._catalogItemAcessor.GetCatalogExtendedContentLastUpdate(catalogItemId, ExtendedContentType.CatalogItem);
				DateTime pbiReportLastUpdate = dateTime;
				DateTime mostRecentUpdate = ((pbiReportLastUpdate > lastModelUpdate) ? pbiReportLastUpdate : lastModelUpdate);
				bool wasInMemory;
				string databaseName = this._asServer.GetMostRecentDatabaseName(catalogItemId, mostRecentUpdate, out wasInMemory);
				CatalogItemPropertiesEntity itemProperties = await this._catalogItemAcessor.GetCatalogItemPropertiesAsync(catalogItemId);
				Logger.Info(string.Format("Loading model for execution with pbiReportLastUpdate={0}, lastModelUpdate={1}, mostRecentUpdate={2}, resolved to database name={3}, wasInMemory={4}", new object[] { pbiReportLastUpdate, lastModelUpdate, mostRecentUpdate, databaseName, wasInMemory }), Array.Empty<object>());
				IList<DataModelDataSourceEntity> list = await this._dataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(catalogItemId);
				IEnumerable<PbixDataSource> enumerable;
				if (list != null)
				{
					enumerable = list.Select((DataModelDataSourceEntity p) => PbixDataSourceExtensions.GetPbixDataSourceFromODataClientV2DataSource(p.ToOData()));
				}
				else
				{
					enumerable = null;
				}
				IEnumerable<PbixDataSource> dataSourcesToLoad = enumerable;
				using (PbiExecutionLogScope execLogScope = new PbiExecutionLogScope(base.User, catalogItemId, this._catalogDataAccessor))
				{
					execLogScope.ItemID = catalogItemId;
					execLogScope.Operation = ExecutionLogInfoEntity.ReportEventType.ASModelStream;
					execLogScope.SessionID = requestId;
					execLogScope.SourceType = ExecutionLogInfoEntity.ExecutionLogExecType.Cache;
					try
					{
						LoadDatabaseResult loadDatabaseResult = await this._asServer.LoadDatabaseForExecutionAsync(base.User, databaseName, model, requestId, clientSessionId, dataSourcesToLoad, itemProperties.ConvertPbixModelParameters());
						this.TrackTelemetryEvent(catalogItemId, dataSourcesToLoad);
						num = loadDatabaseResult.DatabaseId;
						if (loadDatabaseResult.Loaded)
						{
							execLogScope.DontLogOnDispose = true;
						}
					}
					catch (AmoException ex)
					{
						Logger.Error(ex, "Failure in modelsandexploration", Array.Empty<object>());
						execLogScope.ErrorCode = ExecutionLogInfoEntity.ErrorCode.rsInternalError;
						throw;
					}
					catch (AsConnectionException ex2)
					{
						Logger.Error(ex2, "Failure in modelsandexploration", Array.Empty<object>());
						execLogScope.ErrorCode = ExecutionLogInfoEntity.ErrorCode.rsInternalError;
						throw;
					}
				}
				PbiExecutionLogScope execLogScope = null;
				databaseName = null;
				itemProperties = null;
				dataSourcesToLoad = null;
			}
			else
			{
				num = this.CalculateExternalConnectionModelId(catalogItemId);
			}
			return num;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003AB0 File Offset: 0x00001CB0
		private void TrackTelemetryEvent(Guid catalogItemId, IEnumerable<PbixDataSource> pbixDataSources)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"ItemId",
				catalogItemId.ToString()
			} };
			List<DatasourceTelemetryInfo> list = new List<DatasourceTelemetryInfo>();
			if (pbixDataSources != null)
			{
				foreach (PbixDataSource pbixDataSource in pbixDataSources)
				{
					list.Add(new DatasourceTelemetryInfo
					{
						Type = pbixDataSource.Type.ToString(),
						Kind = pbixDataSource.Kind.ToString(),
						AuthType = pbixDataSource.AuthType.ToString()
					});
				}
			}
			dictionary.Add("Datasources", JsonConvert.SerializeObject(list));
			TelemetryService.Current.TrackEvent("RS.PBI.LoadModel", dictionary, null);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003B9C File Offset: 0x00001D9C
		[Route("~/api/explore/reports/{catalogItemId}/conceptualschema")]
		[HttpPost]
		public async Task<IHttpActionResult> GetConceptualSchemaAsync(string catalogItemId, JObject request)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "POST", "~/api/explore/reports/{catalogItemId}/conceptualschema" }))
			{
				Guid id;
				if (!Guid.TryParse(catalogItemId, out id))
				{
					httpActionResult = this.BadRequest();
				}
				else
				{
					using (PbiExecutionLogScope execLogScope = new PbiExecutionLogScope(base.User, id, this._catalogDataAccessor))
					{
						try
						{
							if (!(request["modelIds"] is JArray) || request["modelIds"][0] == null)
							{
								httpActionResult = this.BadRequest();
							}
							else
							{
								string modelId = (string)((JArray)request["modelIds"])[0];
								bool passClientIds = !string.Equals(modelId, this.CalculateExternalConnectionModelId(id).ToString(), StringComparison.OrdinalIgnoreCase);
								string requestId = this.GetRequestContext().RequestID;
								execLogScope.ItemID = id;
								execLogScope.Operation = ExecutionLogInfoEntity.ReportEventType.ConceptualSchema;
								execLogScope.SessionID = requestId;
								PbiExecutionLogScope pbiExecutionLogScope = execLogScope;
								ExecutionLogInfoEntity.ExecutionLogExecType executionLogExecType = await this.GetExecutionLogExecType(id);
								pbiExecutionLogScope.SourceType = executionLogExecType;
								pbiExecutionLogScope = null;
								using (IRSExploreHost host = this.CreateExploreHost(id, passClientIds, false))
								{
									int num = 0;
									string conceptualSchema;
									try
									{
										conceptualSchema = await host.GetConceptualSchemaAsync(modelId, execLogScope.SessionID, request.ToString());
									}
									catch (AsConnectionException obj)
									{
										num = 1;
									}
									object obj;
									if (num == 1)
									{
										AsConnectionException ex = (AsConnectionException)obj;
										if (ex.ErrorCode != AsConnectionExceptionErrorCode.LostConnection)
										{
											Logger.Error(ex, string.Format("AsConnectionException in conceptualschema catalogObjectId={0}", catalogItemId), Array.Empty<object>());
											Exception ex2 = obj as Exception;
											if (ex2 == null)
											{
												throw obj;
											}
											ExceptionDispatchInfo.Capture(ex2).Throw();
										}
										Logger.Info(string.Format("Request for conceptualschema catalogObjectId={0} Failed, will be retried after republishing...", catalogItemId), Array.Empty<object>());
										string clientSessionID = this.GetRequestContext().ClientSessionID;
										await this.ReLoadDatabaseAsync(id, requestId, clientSessionID);
										conceptualSchema = await host.GetConceptualSchemaAsync(modelId, execLogScope.SessionID, request.ToString());
									}
									obj = null;
									execLogScope.Size = (long)conceptualSchema.Length;
									httpActionResult = this.JsonStringResult(HttpStatusCode.OK, conceptualSchema);
								}
							}
						}
						catch (FormatException)
						{
							httpActionResult = this.BadRequest();
						}
						catch (Exception ex3)
						{
							Logger.Error(ex3, string.Format("Failure in conceptualschema catalogObjectId={0}", catalogItemId), Array.Empty<object>());
							execLogScope.ErrorCode = ExecutionLogInfoEntity.ErrorCode.rsInternalError;
							httpActionResult = this.Ok<JObject>(JObject.Parse(RSExploreHost.CreateConceptualSchemaErrorObject(ex3, this._errorExtractor)));
						}
					}
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003BF4 File Offset: 0x00001DF4
		[Route("~/api/explore/reports/{catalogItemId}/querydata")]
		[HttpPost]
		public async Task<IHttpActionResult> GetQueryDataAsync(string catalogItemId, JObject commands)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "POST", "~/api/explore/reports/{catalogItemId}/querydata" }))
			{
				PbiApiController.<>c__DisplayClass26_0 CS$<>8__locals1 = new PbiApiController.<>c__DisplayClass26_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.requestId = this.GetRequestContext().RequestID;
				CS$<>8__locals1.clientSessionId = this.GetRequestContext().ClientSessionID;
				if (!Guid.TryParse(catalogItemId, out CS$<>8__locals1.id))
				{
					httpActionResult = this.BadRequest();
				}
				else
				{
					using (PbiExecutionLogScope execLogScope = new PbiExecutionLogScope(base.User, CS$<>8__locals1.id, this._catalogDataAccessor))
					{
						try
						{
							PbiApiController.<>c__DisplayClass26_1 CS$<>8__locals2 = new PbiApiController.<>c__DisplayClass26_1();
							CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
							QueryDataRequest queryDataRequest = commands.ToObject<QueryDataRequest>();
							if (queryDataRequest == null || queryDataRequest.ModelId == null || queryDataRequest.Queries == null)
							{
								httpActionResult = this.BadRequest();
							}
							else
							{
								CS$<>8__locals2.convertedCommands = JObject.FromObject(new
								{
									Queries = queryDataRequest.Queries.Select((DataQueryRequest query) => query.Query).ToArray<DataQuery>()
								}).ToString();
								execLogScope.ItemID = CS$<>8__locals2.CS$<>8__locals1.id;
								execLogScope.Operation = ExecutionLogInfoEntity.ReportEventType.QueryData;
								execLogScope.SessionID = CS$<>8__locals2.CS$<>8__locals1.requestId;
								PbiExecutionLogScope pbiExecutionLogScope = execLogScope;
								ExecutionLogInfoEntity.ExecutionLogExecType executionLogExecType = await this.GetExecutionLogExecType(CS$<>8__locals2.CS$<>8__locals1.id);
								pbiExecutionLogScope.SourceType = executionLogExecType;
								pbiExecutionLogScope = null;
								string queryData = string.Empty;
								bool retriableError = false;
								try
								{
									queryData = await this.EnsureModelLoaded<string>(CS$<>8__locals2.CS$<>8__locals1.id, queryDataRequest.ModelId, CS$<>8__locals2.CS$<>8__locals1.requestId, CS$<>8__locals2.CS$<>8__locals1.clientSessionId, delegate(string modelIdToUse)
									{
										PbiApiController.<>c__DisplayClass26_1.<<GetQueryDataAsync>b__1>d <<GetQueryDataAsync>b__1>d;
										<<GetQueryDataAsync>b__1>d.<>4__this = CS$<>8__locals2;
										<<GetQueryDataAsync>b__1>d.modelIdToUse = modelIdToUse;
										<<GetQueryDataAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
										<<GetQueryDataAsync>b__1>d.<>1__state = -1;
										AsyncTaskMethodBuilder<string> <>t__builder = <<GetQueryDataAsync>b__1>d.<>t__builder;
										<>t__builder.Start<PbiApiController.<>c__DisplayClass26_1.<<GetQueryDataAsync>b__1>d>(ref <<GetQueryDataAsync>b__1>d);
										return <<GetQueryDataAsync>b__1>d.<>t__builder.Task;
									});
								}
								catch (AsConnectionException ex)
								{
									if (ex.ErrorCode != AsConnectionExceptionErrorCode.LostConnection)
									{
										Logger.Error(ex, string.Format("AsConnectionException in querydata catalogObjectId={0}", catalogItemId), Array.Empty<object>());
										throw;
									}
									retriableError = true;
								}
								int num = ((queryData.Length >= 4096) ? 4096 : (queryData.Length - 1));
								retriableError = retriableError || (queryData.IndexOf("odata.error", 0, num, StringComparison.Ordinal) > 0 && this.ShouldReStream(queryData));
								if (retriableError)
								{
									Logger.Info(string.Format("There was a retriable error in querydata catalogObjectId={0}, requestId={1}, clientSessionId={2}, will be retried after republishing...", catalogItemId, CS$<>8__locals2.CS$<>8__locals1.requestId, CS$<>8__locals2.CS$<>8__locals1.clientSessionId), Array.Empty<object>());
									await this.ReLoadDatabaseAsync(CS$<>8__locals2.CS$<>8__locals1.id, CS$<>8__locals2.CS$<>8__locals1.requestId, CS$<>8__locals2.CS$<>8__locals1.clientSessionId);
									httpActionResult = this.JsonStringResult(HttpStatusCode.OK, await this.GetQueryDataInternalAsync(CS$<>8__locals2.CS$<>8__locals1.id, queryDataRequest.ModelId, CS$<>8__locals2.convertedCommands, CS$<>8__locals2.CS$<>8__locals1.requestId, CS$<>8__locals2.CS$<>8__locals1.clientSessionId));
								}
								else
								{
									httpActionResult = this.JsonStringResult(HttpStatusCode.OK, queryData);
								}
							}
						}
						catch (RlsNotAuthorizedForModelException ex2)
						{
							Logger.Error(ex2, string.Format("Failure in querydata catalogObjectId={0}, requestId={1}, clientSessionId={2}", catalogItemId, CS$<>8__locals1.requestId, CS$<>8__locals1.clientSessionId), Array.Empty<object>());
							httpActionResult = this.JsonStringResult(HttpStatusCode.Unauthorized, MinervaErrorBuilder.FromErrorCode(ex2.ErrorCode, ex2.ErrorCode, CS$<>8__locals1.requestId).ToString());
						}
						catch (Exception ex3)
						{
							Logger.Error(ex3, string.Format("Failure in querydata catalogObjectId={0}, requestId={1}, clientSessionId={2}", catalogItemId, CS$<>8__locals1.requestId, CS$<>8__locals1.clientSessionId), Array.Empty<object>());
							execLogScope.ErrorCode = ExecutionLogInfoEntity.ErrorCode.rsInternalError;
							throw;
						}
					}
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C4C File Offset: 0x00001E4C
		[Route("~/api/explore/reports/{catalogItemId}/export/xlsx")]
		[HttpPost]
		public async Task<IHttpActionResult> GetExportDataXlsxAsync(string catalogItemId, JObject commands)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "POST", "~/api/explore/reports/{catalogItemId}/export/xlsx" }))
			{
				PbiApiController.<>c__DisplayClass27_0 CS$<>8__locals1 = new PbiApiController.<>c__DisplayClass27_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.requestId = this.GetRequestContext().RequestID;
				CS$<>8__locals1.clientSessionId = this.GetRequestContext().ClientSessionID;
				if (!this._powerBiConfiguration.ExportDataEnabled)
				{
					Logger.Error(string.Format("ExportDataEnabled is disabled. RequestId: {0}", CS$<>8__locals1.requestId), Array.Empty<object>());
					httpActionResult = this.BadRequest();
				}
				else
				{
					CS$<>8__locals1.id = this.GetGuid(catalogItemId);
					if (CS$<>8__locals1.id == Guid.Empty)
					{
						Logger.Error(string.Format("Null or empty catalogItemId. RequestId: {0}", CS$<>8__locals1.requestId), Array.Empty<object>());
						httpActionResult = this.BadRequest();
					}
					else
					{
						ExportToExcelRequest exportToExcelRequest = commands.ToObject<ExportToExcelRequest>();
						string text;
						if (exportToExcelRequest == null)
						{
							text = null;
						}
						else
						{
							QueryDataRequest executeSemanticQueryRequest = exportToExcelRequest.ExecuteSemanticQueryRequest;
							text = ((executeSemanticQueryRequest != null) ? executeSemanticQueryRequest.ModelId : null);
						}
						if (!string.IsNullOrWhiteSpace(text))
						{
							bool flag;
							if (exportToExcelRequest == null)
							{
								flag = true;
							}
							else
							{
								QueryDataRequest executeSemanticQueryRequest2 = exportToExcelRequest.ExecuteSemanticQueryRequest;
								int? num;
								if (executeSemanticQueryRequest2 == null)
								{
									num = null;
								}
								else
								{
									IList<DataQueryRequest> queries = executeSemanticQueryRequest2.Queries;
									num = ((queries != null) ? new int?(queries.Count) : null);
								}
								flag = num != 1;
							}
							if (!flag)
							{
								string modelId = exportToExcelRequest.ExecuteSemanticQueryRequest.ModelId;
								CS$<>8__locals1.queries = JObject.FromObject(new
								{
									Queries = exportToExcelRequest.ExecuteSemanticQueryRequest.Queries.Select((DataQueryRequest query) => query.Query).ToArray<DataQuery>()
								}).ToString();
								DataQueryRequest dataQueryRequest = exportToExcelRequest.ExecuteSemanticQueryRequest.Queries.First<DataQueryRequest>();
								DataQuery query2 = dataQueryRequest.Query;
								ExportDataCommand exportDataCommand;
								if (query2 == null)
								{
									exportDataCommand = null;
								}
								else
								{
									QueryCommand queryCommand = query2.Commands.Where((QueryCommand x) => x.ExportDataCommand != null).FirstOrDefault<QueryCommand>();
									exportDataCommand = ((queryCommand != null) ? queryCommand.ExportDataCommand : null);
								}
								ExportDataCommand exportDataOptions = exportDataCommand;
								if (exportDataOptions == null)
								{
									Logger.Error(string.Format("ExportData request does not contain an Export Data command in the underlying query. RequestId: {0}, ModelId: {1}", CS$<>8__locals1.requestId, modelId), Array.Empty<object>());
									return this.BadRequest();
								}
								if (exportToExcelRequest.ExportDataType == ExportDataType.Underlying && !this._powerBiConfiguration.ExportUnderlyingDataEnabled)
								{
									Logger.Error(string.Format("Exporting underlying data is disabled. RequestId: {0}, ModelId: {1}", CS$<>8__locals1.requestId, modelId), Array.Empty<object>());
									return this.BadRequest();
								}
								using (PbiExecutionLogScope execLogScope = new PbiExecutionLogScope(base.User, CS$<>8__locals1.id, this._catalogDataAccessor))
								{
									execLogScope.ItemID = CS$<>8__locals1.id;
									execLogScope.Operation = ExecutionLogInfoEntity.ReportEventType.QueryData;
									execLogScope.SessionID = CS$<>8__locals1.requestId;
									PbiExecutionLogScope pbiExecutionLogScope = execLogScope;
									ExecutionLogInfoEntity.ExecutionLogExecType executionLogExecType = await this.GetExecutionLogExecType(CS$<>8__locals1.id);
									pbiExecutionLogScope.SourceType = executionLogExecType;
									pbiExecutionLogScope = null;
									PbiApiController.<>c__DisplayClass27_1 CS$<>8__locals2 = new PbiApiController.<>c__DisplayClass27_1();
									CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
									CS$<>8__locals2.queryData = new MemoryStream();
									try
									{
										int num3;
										int num2 = num3 - 1;
										try
										{
											ExportDataMetadata exportDataMetadata = await this.EnsureModelLoaded<ExportDataMetadata>(CS$<>8__locals2.CS$<>8__locals1.id, modelId, CS$<>8__locals2.CS$<>8__locals1.requestId, CS$<>8__locals2.CS$<>8__locals1.clientSessionId, delegate(string modelIdToUse)
											{
												PbiApiController.<>c__DisplayClass27_1.<<GetExportDataXlsxAsync>b__2>d <<GetExportDataXlsxAsync>b__2>d;
												<<GetExportDataXlsxAsync>b__2>d.<>4__this = CS$<>8__locals2;
												<<GetExportDataXlsxAsync>b__2>d.modelIdToUse = modelIdToUse;
												<<GetExportDataXlsxAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder<ExportDataMetadata>.Create();
												<<GetExportDataXlsxAsync>b__2>d.<>1__state = -1;
												AsyncTaskMethodBuilder<ExportDataMetadata> <>t__builder = <<GetExportDataXlsxAsync>b__2>d.<>t__builder;
												<>t__builder.Start<PbiApiController.<>c__DisplayClass27_1.<<GetExportDataXlsxAsync>b__2>d>(ref <<GetExportDataXlsxAsync>b__2>d);
												return <<GetExportDataXlsxAsync>b__2>d.<>t__builder.Task;
											});
											Logger.Verbose(string.Format("Retreived raw data stream. RequestId: {0}, ModelId: {1}, Size bytes: {2}, Metadata: {3}", new object[]
											{
												CS$<>8__locals2.CS$<>8__locals1.requestId,
												modelId,
												CS$<>8__locals2.queryData.Length,
												JsonConvert.SerializeObject(exportDataMetadata)
											}), Array.Empty<object>());
											CS$<>8__locals2.queryData.Position = 0L;
											using (MemoryStream memoryStream = new MemoryStream())
											{
												ExcelStreamWriterMetadata excelStreamWriterMetadata = new ExcelStreamWriterMetadata();
												excelStreamWriterMetadata.PrimarySelectsMap = exportDataMetadata.PrimarySelectsMap;
												excelStreamWriterMetadata.ColumnsFormatting = exportDataMetadata.ColumnsFormatting;
												excelStreamWriterMetadata.Ordering = exportDataOptions.Ordering;
												excelStreamWriterMetadata.TableDescription = exportDataOptions.FiltersDescription;
												await ExcelGenerator.CreateTableAsync(memoryStream, CS$<>8__locals2.queryData, excelStreamWriterMetadata);
												memoryStream.Seek(0L, SeekOrigin.Begin);
												Logger.Verbose(string.Format("Successfully generated xlsx. RequestId: {0}, ModelId: {1}, Size bytes: {2}.", CS$<>8__locals2.CS$<>8__locals1.requestId, modelId, memoryStream.Length), Array.Empty<object>());
												return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.OK)
												{
													Content = new ByteArrayContent(memoryStream.ToArray())
												});
											}
										}
										catch (Exception ex)
										{
											Logger.Error(ex, string.Format("Error exporting data. RequestId: {0}, ModelId: {1}", CS$<>8__locals2.CS$<>8__locals1.requestId, modelId), Array.Empty<object>());
											if (ex is AsConnectionException)
											{
												return this.JsonStringResult(HttpStatusCode.InternalServerError, MinervaErrorBuilder.FromAsConnectionException((AsConnectionException)ex, CS$<>8__locals2.CS$<>8__locals1.requestId).ToString());
											}
											return this.JsonStringResult(HttpStatusCode.InternalServerError, MinervaErrorBuilder.FromErrorCode("ExportXlsxError", ex.Message, CS$<>8__locals2.CS$<>8__locals1.requestId).ToString());
										}
									}
									finally
									{
										if (CS$<>8__locals2.queryData != null)
										{
											((IDisposable)CS$<>8__locals2.queryData).Dispose();
										}
									}
								}
							}
						}
						Logger.Error(string.Format("ExportData request requires a valid ExecuteSemanticQueryRequest containing a single query. RequestId: {0}", CS$<>8__locals1.requestId), Array.Empty<object>());
						httpActionResult = this.BadRequest();
					}
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003CA4 File Offset: 0x00001EA4
		private async Task<T> EnsureModelLoaded<T>(Guid catalogItemId, string originalModelId, string requestId, string clientSessionId, Func<string, Task<T>> innerAction)
		{
			int num = 0;
			try
			{
				return await innerAction(originalModelId);
			}
			catch (DatabaseNotFoundException obj)
			{
				num = 1;
			}
			catch (PowerBIExploreException obj)
			{
				num = 2;
			}
			T t;
			if (num != 1)
			{
				if (num == 2)
				{
					object obj;
					PowerBIExploreException ex = (PowerBIExploreException)obj;
					if (ex.ErrorCode == "CannotRetrieveModelError" || ex.ErrorStatusCode == ServiceErrorStatusCode.CsdlFetching)
					{
						Logger.Error(ex, string.Format("Failed to fetch the model during action. RequestId: {0}, ModelId: {1}, CatalogItemId: {2}.", requestId, originalModelId, catalogItemId), Array.Empty<object>());
						string text = await this.ReLoadDatabaseAsync(catalogItemId, requestId, clientSessionId);
						Logger.Verbose(string.Format("Database was republished with modelId={0}, retrying action. RequestId: {1}, OldModelId: {2}, CatalogItemId: {3}.", new object[] { text, requestId, originalModelId, catalogItemId }), Array.Empty<object>());
						t = await innerAction(text);
					}
					else
					{
						Exception ex2 = obj as Exception;
						if (ex2 == null)
						{
							throw obj;
						}
						ExceptionDispatchInfo.Capture(ex2).Throw();
					}
				}
			}
			else
			{
				object obj;
				Logger.Error((DatabaseNotFoundException)obj, string.Format("Failed to execute action requiring model. Reloading model and retrying. RequestId: {0}, ModelId: {1}, CatalogItemId: {2}.", requestId, originalModelId, catalogItemId), Array.Empty<object>());
				string text2 = await this.ReLoadDatabaseAsync(catalogItemId, requestId, clientSessionId);
				Logger.Verbose(string.Format("Database was republished with modelId={0}, retrying action. RequestId: {1}, OldModelId: {2}, CatalogItemId: {3}.", new object[] { text2, requestId, originalModelId, catalogItemId }), Array.Empty<object>());
				t = await innerAction(text2);
			}
			return t;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003D14 File Offset: 0x00001F14
		private bool ShouldReStream(string queryData)
		{
			foreach (ExecuteSemanticQueryResult executeSemanticQueryResult in JsonConvert.DeserializeObject<ExecuteSemanticQueryResponse>(queryData).Results)
			{
				ODataError odataError;
				if (executeSemanticQueryResult.Result != null && executeSemanticQueryResult.Result.Data != null && DataShapeResultParser.TryGetErrorObject(DataShapeResultParser.LoadJObject((string)executeSemanticQueryResult.Result.Data.Value)["dsr"], out odataError))
				{
					if ("OpenConnectionError".Equals(odataError.Code, StringComparison.OrdinalIgnoreCase))
					{
						Logger.Trace("Retriable dataShape error {0} found, will retry", new object[] { odataError.Code });
						return true;
					}
					Logger.Trace("Non-Retriable dataShape error {0} found", new object[] { odataError.Code });
				}
			}
			return false;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003DF8 File Offset: 0x00001FF8
		private ResponseMessageResult JsonStringResult(HttpStatusCode statusCode, string content)
		{
			return this.ResponseMessage(new HttpResponseMessage(statusCode)
			{
				Content = new StringContent(content, Encoding.UTF8, "application/json")
			});
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003E2C File Offset: 0x0000202C
		private async Task<string> ReLoadDatabaseAsync(Guid catalogItemId, string requestId, string clientSessionId)
		{
			string text;
			try
			{
				Logger.Info("Attempting to re-stream the model {0} for request {1}, session {2}", new object[] { catalogItemId, requestId, clientSessionId });
				PbixComponents pbixComponents = await this._catalogService.GetPbixComponentsAsync(base.User, catalogItemId, requestId, clientSessionId, PbixReportComponents.PbixMetadataAndModel);
				text = (await this.PrepareModelAsync(catalogItemId, pbixComponents, requestId, clientSessionId)).ToString();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error re-streaming the model", Array.Empty<object>());
				throw;
			}
			return text;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003E8C File Offset: 0x0000208C
		private async Task<string> GetQueryDataInternalAsync(Guid catalogItemId, string modelId, string commands, string requestId, string clientSessionId)
		{
			bool flag = !string.Equals(modelId, this.CalculateExternalConnectionModelId(catalogItemId).ToString(), StringComparison.OrdinalIgnoreCase);
			string text;
			using (IRSExploreHost host = this.CreateExploreHost(catalogItemId, flag, true))
			{
				text = await host.ExecuteSemanticQueryAsync(modelId, clientSessionId, commands.ToString(), ASQueryLimits.Unlimited);
			}
			return text;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003EF4 File Offset: 0x000020F4
		private async Task<ExportDataMetadata> GetExportDataInternalAsync(Guid catalogItemId, string modelId, string commands, string requestId, string clientSessionId, Stream outputStream)
		{
			bool flag = !string.Equals(modelId, this.CalculateExternalConnectionModelId(catalogItemId).ToString(), StringComparison.OrdinalIgnoreCase);
			ExportDataMetadata exportDataMetadata;
			using (IRSExploreHost host = this.CreateExploreHost(catalogItemId, flag, true))
			{
				exportDataMetadata = await host.ExecuteExportDataQueryAsync(modelId, clientSessionId, commands, outputStream, ASQueryLimits.Unlimited);
			}
			return exportDataMetadata;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003F64 File Offset: 0x00002164
		[Route("~/api/explore/resourcePackageItem/{catalogItemId}/{resourcePackageType}/{*fileName}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetStaticResource(string catalogItemId, string resourcePackageType, string fileName)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "GET", "~/api/explore/resourcePackageItem/{catalogItemId}/{resourcePackageType}/{*fileName}" }))
			{
				try
				{
					Guid guid = new Guid(catalogItemId);
					string clientSessionID = this.GetRequestContext().ClientSessionID;
					if (string.IsNullOrEmpty(clientSessionID))
					{
						httpActionResult = this.StatusCode(HttpStatusCode.Forbidden);
					}
					else
					{
						string requestID = this.GetRequestContext().RequestID;
						TaskAwaiter<PbixComponents> taskAwaiter = this._catalogService.GetPbixComponentsAsync(base.User, guid, requestID, clientSessionID, PbixReportComponents.PbixMetadata).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<PbixComponents> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<PbixComponents>);
						}
						byte[] array;
						if (taskAwaiter.GetResult().StaticResources.TryGetValue(resourcePackageType, fileName, out array))
						{
							MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue(MimeTypesExtensions.GetMimeType(fileName));
							httpActionResult = new ByteArrayResponse(array, mediaTypeHeaderValue);
						}
						else
						{
							httpActionResult = this.NotFound();
						}
					}
				}
				catch (FormatException)
				{
					httpActionResult = this.BadRequest();
				}
				catch (CatalogAccessException)
				{
					httpActionResult = this.NotFound();
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003FC4 File Offset: 0x000021C4
		[Route("~/api/reportpropertiesfromlocalfiles")]
		[HttpPost]
		public async Task<IHttpActionResult> ShredFromPreshreddedFiles(PreShreddedPbixFilesServer filePaths)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "POST", "~/api/reportpropertiesfromlocalfiles" }))
			{
				if (!base.RequestContext.IsLocal)
				{
					Logger.Error("HttpRequestContext returned false for IsLocal.", Array.Empty<object>());
					httpActionResult = this.BadRequest();
				}
				else
				{
					string errorMessageIfExists = filePaths.GetErrorMessageIfExists();
					if (errorMessageIfExists != null)
					{
						Logger.Debug(errorMessageIfExists, Array.Empty<object>());
						httpActionResult = this.BadRequest(errorMessageIfExists);
					}
					else
					{
						try
						{
							if (string.IsNullOrWhiteSpace(filePaths.Model))
							{
								httpActionResult = this.ShredNoModel(filePaths);
							}
							else
							{
								httpActionResult = await this.ShredWithModel(filePaths);
							}
						}
						catch (Exception ex)
						{
							if (ex is NewerFileVersionException || ex is NewerPackagePartException || ex is UnsupportedProviderException)
							{
								httpActionResult = this.Ok<JObject>(FileFormatCheckResponse.Create(Microsoft.PowerBI.ReportServer.WebApi.PbiApi.StatusCode.PowerBIReportNotSupportedVersion, null));
							}
							else
							{
								Logger.Error(ex, "Failure in reportproperties", Array.Empty<object>());
								httpActionResult = this.BadRequest();
							}
						}
					}
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004014 File Offset: 0x00002214
		[Route("~/api/reportproperties")]
		[HttpPost]
		public async Task<IHttpActionResult> ShredFromBody()
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "POST", "~/api/reportproperties" }))
			{
				if (!base.RequestContext.IsLocal)
				{
					Logger.Error("HttpRequestContext returned false for IsLocal.", Array.Empty<object>());
					httpActionResult = this.BadRequest();
				}
				else
				{
					byte[] array = await base.Request.Content.ReadAsByteArrayAsync();
					using (Stream stream = new MemoryStream(array))
					{
						try
						{
							httpActionResult = this.GetReportPropertiesResponse(await this.ShredFromStream(stream));
						}
						catch (Exception ex)
						{
							if (ex is NewerFileVersionException || ex is NewerPackagePartException || ex is UnsupportedProviderException)
							{
								httpActionResult = this.Ok<JObject>(FileFormatCheckResponse.Create(Microsoft.PowerBI.ReportServer.WebApi.PbiApi.StatusCode.PowerBIReportNotSupportedVersion, null));
							}
							else
							{
								Logger.Error(ex, "Failure in reportproperties", Array.Empty<object>());
								httpActionResult = this.BadRequest();
							}
						}
					}
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000405C File Offset: 0x0000225C
		[Route("~/api/report/{catalogItemId}/parameters")]
		[HttpGet]
		public async Task<IHttpActionResult> GetModelParameters(string catalogItemId)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "GET", "~/api/report/{catalogItemId}/parameters" }))
			{
				Guid guid;
				if (!Guid.TryParse(catalogItemId, out guid))
				{
					httpActionResult = this.BadRequest();
				}
				else
				{
					string databaseName = string.Format("params_{0}", Guid.NewGuid());
					string clientSessionID = this.GetRequestContext().ClientSessionID;
					string requestID = this.GetRequestContext().RequestID;
					Stream fullpbix = this._catalogItemAcessor.GetExtendedContentReadable(guid, ExtendedContentType.DataModel);
					LoadDatabaseResult loadResult = null;
					try
					{
						LoadDatabaseResult loadDatabaseResult = await this._asServer.LoadDatabaseAsync(databaseName, new Lazy<Stream>(() => fullpbix), requestID, clientSessionID);
						loadResult = loadDatabaseResult;
						List<PbixModelParameter> list = this._asServer.GetModelParameters(databaseName).ToList<PbixModelParameter>();
						httpActionResult = this.Ok<PbixModelParameter[]>(list.ToArray());
					}
					finally
					{
						LoadDatabaseResult loadDatabaseResult2 = loadResult;
						if (loadDatabaseResult2 != null && !loadDatabaseResult2.Loaded)
						{
							this._asServer.DeleteDatabase(loadResult.DatabaseId);
						}
					}
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000040AC File Offset: 0x000022AC
		[Route("~/api/report/{catalogItemId}/parameters")]
		[HttpPost]
		public async Task<IHttpActionResult> PostModelParameters(string catalogItemId, [FromBody] PbixModelParameter[] parameters)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "POST", "~/api/report/{catalogItemId}/parameters" }))
			{
				Guid guid;
				if (!Guid.TryParse(catalogItemId, out guid) || parameters.Length == 0)
				{
					httpActionResult = this.BadRequest();
				}
				else
				{
					string databaseName = string.Format("params_{0}", Guid.NewGuid());
					string clientSessionID = this.GetRequestContext().ClientSessionID;
					string requestID = this.GetRequestContext().RequestID;
					Stream fullpbix = this._catalogItemAcessor.GetExtendedContentReadable(guid, ExtendedContentType.DataModel);
					LoadDatabaseResult loadResult = null;
					try
					{
						LoadDatabaseResult loadDatabaseResult = await this._asServer.LoadDatabaseAsync(databaseName, new Lazy<Stream>(() => fullpbix), requestID, clientSessionID);
						loadResult = loadDatabaseResult;
						httpActionResult = this.Ok<PbixDataSource[]>(this._asServer.SetModelParameters(databaseName, parameters).ToArray<PbixDataSource>());
					}
					finally
					{
						LoadDatabaseResult loadDatabaseResult2 = loadResult;
						if (loadDatabaseResult2 != null && !loadDatabaseResult2.Loaded)
						{
							this._asServer.DeleteDatabase(loadResult.DatabaseId);
						}
					}
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004104 File Offset: 0x00002304
		internal async Task<PbixComponents> ShredFromStream(Stream stream)
		{
			string clientSessionID = this.GetRequestContext().ClientSessionID;
			string requestID = this.GetRequestContext().RequestID;
			PbixReportElements pbixReportElements = this._pbixParser.ParsePbixFileIntoParts(stream, requestID, clientSessionID);
			PbixComponents pbixComponents = await this._pbixComponentsBuilder.BuildPbixComponentsFromPbixReportElements(pbixReportElements, requestID, clientSessionID, true);
			pbixReportElements.ModelVersion = pbixComponents.ModelVersion;
			this._pbixTelemetryLogger.LogPbixTelemetry(pbixReportElements);
			return pbixComponents;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004154 File Offset: 0x00002354
		[Route("~/api/explore/resourcePackageItem/{catalogItemId}/{fileName}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetCustomVisualsAsync(string catalogItemId, string fileName)
		{
			IHttpActionResult httpActionResult;
			using (ScopeMeter.Use(new string[] { "GET", "~/api/explore/resourcePackageItem/{catalogItemId}/{fileName}" }))
			{
				try
				{
					string clientSessionID = this.GetRequestContext().ClientSessionID;
					string requestID = this.GetRequestContext().RequestID;
					Guid guid = new Guid(catalogItemId);
					PbixComponents pbixComponents = await this._catalogService.GetPbixComponentsAsync(base.User, guid, requestID, clientSessionID, PbixReportComponents.PbixMetadata);
					if (pbixComponents.CustomVisuals.ContainsKey(fileName))
					{
						MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue(MimeTypesExtensions.GetMimeType(fileName));
						httpActionResult = new ByteArrayResponse(pbixComponents.CustomVisuals[fileName], mediaTypeHeaderValue);
					}
					else
					{
						httpActionResult = this.NotFound();
					}
				}
				catch (CatalogAccessException)
				{
					httpActionResult = this.NotFound();
				}
			}
			return httpActionResult;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000041A9 File Offset: 0x000023A9
		[Route("~/api/servicestate")]
		[HttpGet]
		public IHttpActionResult GetServiceState()
		{
			return this.Ok<ServiceState>(new ServiceState());
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000041B8 File Offset: 0x000023B8
		[Route("~/api/mobile.appcache")]
		[HttpGet]
		public HttpResponseMessage GetMobileAppCache()
		{
			HttpResponseMessage httpResponseMessage;
			using (ScopeMeter.Use(new string[] { "GET", "~/api/mobile.appcache" }))
			{
				StringBuilder stringBuilder = new StringBuilder();
				Assembly assembly = Assembly.LoadFrom("Microsoft.PowerBI.ReportServer.Resources.dll");
				string fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
				using (Stream manifestResourceStream = assembly.GetManifestResourceStream("reportserver.wwwroot.mobile.appCacheManifest.txt"))
				{
					using (StreamReader streamReader = new StreamReader(manifestResourceStream))
					{
						stringBuilder.AppendLine(streamReader.ReadToEnd());
					}
				}
				stringBuilder.AppendLine("# " + fileVersion);
				httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(stringBuilder.ToString(), Encoding.UTF8, "text/cache-manifest")
				};
			}
			return httpResponseMessage;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000042AC File Offset: 0x000024AC
		[Route("~/api/checkdatasourceconnection")]
		[HttpPost]
		public DataSourceCheckResult CheckDataSourceConnection(DataSource dataSource)
		{
			DataSourceCheckResult dataSourceCheckResult;
			using (ScopeMeter.Use(new string[] { "POST", "~/api/checkdatasourceconnection" }))
			{
				PbixDataSource pbixDataSource = PbixDataSourceExtensions.GetPbixDataSourceFromODataClientV2DataSource(dataSource);
				WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
				if (pbixDataSource.AuthType == AuthorizationType.Integrated && base.RequestContext.Principal.Identity is WindowsIdentity)
				{
					windowsIdentity = (WindowsIdentity)base.RequestContext.Principal.Identity;
				}
				PbixDataSourceCheckResult pbixDataSourceCheckResult = WindowsIdentity.RunImpersonated<PbixDataSourceCheckResult>(windowsIdentity.AccessToken, () => this._checkConnectionServices.TestCredentials(pbixDataSource));
				string text = ((!pbixDataSourceCheckResult.IsSuccessful && !HostingState.Current.IsConfigSwitchEnabled(ConfigSwitches.EnableTestConnectionDetailedErrors, true)) ? SR.Error_DataSourceConnectionErrorNotVisible : pbixDataSourceCheckResult.ErrorMessage);
				dataSourceCheckResult = new DataSourceCheckResult
				{
					IsSuccessful = pbixDataSourceCheckResult.IsSuccessful,
					ErrorMessage = text
				};
			}
			return dataSourceCheckResult;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000043A8 File Offset: 0x000025A8
		internal IHttpActionResult GetReportPropertiesResponse(PbixComponents pbixComponents)
		{
			JObject jobject = FileFormatCheckResponse.Create(Microsoft.PowerBI.ReportServer.WebApi.PbiApi.StatusCode.Ok, pbixComponents);
			return this.Ok<JObject>(jobject);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000043C8 File Offset: 0x000025C8
		private IHttpActionResult ShredNoModel(PreShreddedPbixFilesServer filePaths)
		{
			IHttpActionResult reportPropertiesResponse;
			using (ScopeMeter.Use(new string[] { "Shred", "NoModel" }))
			{
				string clientSessionID = this.GetRequestContext().ClientSessionID;
				string requestID = this.GetRequestContext().RequestID;
				PbixComponents pbixComponents = this.Shred(filePaths.Pbix, requestID, clientSessionID);
				reportPropertiesResponse = this.GetReportPropertiesResponse(pbixComponents);
			}
			return reportPropertiesResponse;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004440 File Offset: 0x00002640
		private async Task<IHttpActionResult> ShredWithModel(PreShreddedPbixFilesServer filePaths)
		{
			IHttpActionResult reportPropertiesResponse;
			using (ScopeMeter.Use(new string[] { "Shred", "WithModel" }))
			{
				using (FileStream modelStream = new FileStream(filePaths.Model, FileMode.Open))
				{
					string clientSessionId = this.GetRequestContext().ClientSessionID;
					string requestId = this.GetRequestContext().RequestID;
					DataModelArtifacts dataModelArtifacts = await this._modelArtifactsProvider.RetrieveArtifactsAsync(modelStream, requestId, clientSessionId);
					PbixComponents pbixComponents = this.Shred(filePaths.Pbix, requestId, clientSessionId);
					pbixComponents.ModelVersion = dataModelArtifacts.ModelVersion;
					PbixComponentsBuilder.AppendEmbeddedDataSources(dataModelArtifacts, pbixComponents);
					reportPropertiesResponse = this.GetReportPropertiesResponse(pbixComponents);
				}
			}
			return reportPropertiesResponse;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004490 File Offset: 0x00002690
		private PbixComponents Shred(string pbixFilePath, string requestId, string clientSessionId)
		{
			PbixComponents pbixComponents;
			using (ScopeMeter.Use("Shred"))
			{
				using (FileStream fileStream = new FileStream(pbixFilePath, FileMode.Open))
				{
					PbixComponents result = this.ShredFromStream(fileStream).Result;
					result.ModelRefreshAllowed = false;
					pbixComponents = result;
				}
			}
			return pbixComponents;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000044F8 File Offset: 0x000026F8
		private async Task<ExecutionLogInfoEntity.ExecutionLogExecType> GetExecutionLogExecType(Guid catalogItemId)
		{
			IList<DataModelDataSourceEntity> list = await this._dataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(catalogItemId);
			return this.GetExecutionLogExecType(list);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004545 File Offset: 0x00002745
		internal ExecutionLogInfoEntity.ExecutionLogExecType GetExecutionLogExecType(IList<DataModelDataSourceEntity> dataSources)
		{
			if (dataSources != null)
			{
				if (dataSources.Any((DataModelDataSourceEntity x) => x.DSType == DataModelDataSourceEntity.DataModelDataSourceType.Live || x.DSType == DataModelDataSourceEntity.DataModelDataSourceType.DirectQuery))
				{
					return ExecutionLogInfoEntity.ExecutionLogExecType.Live;
				}
			}
			return ExecutionLogInfoEntity.ExecutionLogExecType.Cache;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004574 File Offset: 0x00002774
		private Guid GetGuid(string id)
		{
			Guid empty = Guid.Empty;
			Guid.TryParse(id, out empty);
			return empty;
		}

		// Token: 0x0400006F RID: 111
		private readonly ICatalogService _catalogService;

		// Token: 0x04000070 RID: 112
		private readonly IRSExploreHostFactory _exploreHostFactory;

		// Token: 0x04000071 RID: 113
		private readonly IServiceErrorExtractor _errorExtractor;

		// Token: 0x04000072 RID: 114
		private readonly IAnalysisServicesServer _asServer;

		// Token: 0x04000073 RID: 115
		private readonly IPbixParser _pbixParser;

		// Token: 0x04000074 RID: 116
		private readonly IPbixComponentsBuilder _pbixComponentsBuilder;

		// Token: 0x04000075 RID: 117
		private readonly IDataModelArtifactsProvider _modelArtifactsProvider;

		// Token: 0x04000076 RID: 118
		private readonly ICatalogDataModelDataSourceAccessor _dataModelDataSourceAccessor;

		// Token: 0x04000077 RID: 119
		private readonly ICatalogDataModelRoleAccessor _dataModelRoleAccessor;

		// Token: 0x04000078 RID: 120
		private readonly ICatalogItemAccessor _catalogItemAcessor;

		// Token: 0x04000079 RID: 121
		private readonly ICheckConnectionServices _checkConnectionServices;

		// Token: 0x0400007A RID: 122
		private readonly IPowerBIConfiguration _powerBiConfiguration;

		// Token: 0x0400007B RID: 123
		private readonly ICatalogDataAccessor _catalogDataAccessor;

		// Token: 0x0400007C RID: 124
		private readonly IPbixTelemetryLogger _pbixTelemetryLogger;

		// Token: 0x0400007D RID: 125
		private const string ApplicationJsonContentType = "application/json";

		// Token: 0x0400007E RID: 126
		private const string ApplicationExcelFile = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

		// Token: 0x0400007F RID: 127
		private const string PbixParseEventName = "RS.PBI.Shred";
	}
}
