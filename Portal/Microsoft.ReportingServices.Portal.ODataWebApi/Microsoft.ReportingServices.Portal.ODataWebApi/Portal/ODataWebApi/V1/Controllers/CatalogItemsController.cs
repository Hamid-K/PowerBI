using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.OData.UriParser;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.Common;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x02000030 RID: 48
	public class CatalogItemsController : EntitySetReflectionODataController<CatalogItem>
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008C9F File Offset: 0x00006E9F
		protected ICatalogRepository CatalogRepository
		{
			get
			{
				return this._catalogRepository;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008CA7 File Offset: 0x00006EA7
		internal IDataService DataService
		{
			get
			{
				return this._dataService;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008CAF File Offset: 0x00006EAF
		internal CatalogItemControllerHelper<CatalogItem> CatalogItemControllerHelper
		{
			get
			{
				return this._catalogItemControllerHelper;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public CatalogItemsController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(logger)
		{
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			if (dataService == null)
			{
				throw new ArgumentNullException("dataService");
			}
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			if (portalConfigurationManager == null)
			{
				throw new ArgumentNullException("portalConfigurationManager");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._catalogRepository = catalogRepository;
			this._dataService = dataService;
			this._systemService = systemService;
			this._portalConfigurationManager = portalConfigurationManager;
			this._logger = logger;
			this._catalogItemControllerHelper = new CatalogItemControllerHelper<CatalogItem>(catalogRepository, this, systemService, portalConfigurationManager, logger);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00008D4C File Offset: 0x00006F4C
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<CatalogItem>("CatalogItems");
			builder.EntityType<CatalogItem>().Action("AddToFavorites").Returns<bool>();
			builder.EntityType<CatalogItem>().Action("RemoveFromFavorites").Returns<bool>();
			builder.EntityType<CatalogItem>().Action("GetProperties").ReturnsCollection<Property>()
				.CollectionParameter<Property>("RequestedProperties");
			builder.EntityType<CatalogItem>().Action("SetProperties").Returns<bool>()
				.CollectionParameter<Property>("Properties");
			builder.EntityType<CatalogItem>().Function("GetDependentItems").ReturnsCollectionFromEntitySet<CatalogItem>("CatalogItems");
			builder.EntityType<CatalogItem>().Function("SearchItems").ReturnsCollectionFromEntitySet<CatalogItem>("CatalogItems")
				.Parameter<string>("SearchText");
			builder.EntityType<CatalogItem>().Collection.Action("DeleteItems").Returns<BulkOperationsResult>().CollectionParameter<string>("CatalogItemPaths");
			ActionConfiguration actionConfiguration = builder.EntityType<CatalogItem>().Collection.Action("MoveItems").Returns<BulkOperationsResult>();
			actionConfiguration.Parameter<string>("TargetPath");
			actionConfiguration.CollectionParameter<string>("CatalogItemPaths");
			builder.EntityType<CatalogItem>().Function("GetRoles").ReturnsCollection<Role>();
			builder.EntityType<CatalogItem>().Function("GetPolicies").ReturnsCollection<ItemPolicy>();
			builder.EntityType<CatalogItem>().Action("SetPolicies").Returns<bool>()
				.Parameter<ItemPolicy>("Policy");
			builder.EntitySet<ReportParameterDefinition>("ReportParameters");
			builder.EntityType<Report>().Action("SetParameterProperties").CollectionParameter<ReportParameterDefinitionPatch>("ParameterProperties");
			builder.EntityType<Report>().Action("SetReportHistorySnapshotsOptions").Parameter<ReportHistorySnapshotsOptions>("ReportHistorySnapshotsOptions");
			builder.EntityType<LinkedReport>().Action("SetReportHistorySnapshotsOptions").Parameter<ReportHistorySnapshotsOptions>("ReportHistorySnapshotsOptions");
			builder.EntityType<Report>().Action("CreateSnapshot").Returns<string>();
			builder.EntityType<Report>().Action("DeleteSnapshot").Returns<bool>()
				.Parameter<string>("HistoryId");
			builder.EntityType<Report>().Action("UpdateExecutionSnapshot").Returns<bool>();
			builder.EntityType<LinkedReport>().Action("CreateSnapshot").Returns<string>();
			builder.EntityType<LinkedReport>().Action("DeleteSnapshot").Returns<bool>()
				.Parameter<string>("HistoryId");
			builder.EntityType<Report>().Function("GetHistoryOptions").Returns<ItemHistoryOptions>();
			builder.EntityType<Report>().Function("GetReportHistorySnapshotsOptions").Returns<ReportHistorySnapshotsOptions>();
			builder.EntityType<LinkedReport>().Function("GetReportHistorySnapshotsOptions").Returns<ReportHistorySnapshotsOptions>();
			builder.EntityType<DataSet>().Function("GetSchema").Returns<DataSetSchema>();
			builder.EntityType<DataSet>().Function("GetTable").Returns<string>();
			builder.EntityType<DataSet>().Function("GetTable").Returns<string>()
				.Parameter<int>("maxRows");
			ActionConfiguration actionConfiguration2 = builder.EntityType<DataSet>().Action("GetData").Returns<string>();
			actionConfiguration2.CollectionParameter<DataSetParameter>("Parameters");
			actionConfiguration2.Parameter<int?>("maxRows");
			ActionConfiguration actionConfiguration3 = builder.EntityType<DataSet>().Action("GetKpiTrendsetData").Returns<string>();
			actionConfiguration3.CollectionParameter<DataSetParameter>("Parameters");
			actionConfiguration3.Parameter<string>("columnName");
			builder.EntityType<DataSet>().Action("GetAggregatedValue").Returns<string>()
				.CollectionParameter<DataSetParameter>("Parameters");
			builder.EntityType<Report>().Action("CheckDataSourceConnection").Returns<DataSourceCheckResult>()
				.Parameter<string>("DataSourceName");
			builder.EntityType<DataSource>().Action("CheckConnection").Returns<DataSourceCheckResult>();
			builder.EntityType<DataSource>().Collection.Action("CheckConnection").Returns<DataSourceCheckResult>().Parameter<DataSource>("dataSource");
			ActionConfiguration actionConfiguration4 = builder.EntityType<DataSource>().Collection.Action("GetQueryFields").ReturnsCollection<string>();
			actionConfiguration4.Parameter<DataSource>("dataSource");
			actionConfiguration4.Parameter<Query>("query");
			actionConfiguration4.Parameter<string>("subscriptionId").Optional();
			builder.EntityType<Report>().Action("UpdateItemDataSources").CollectionParameter<DataSource>("dataSources");
			builder.EntityType<DataSet>().Action("UpdateItemDataSources").CollectionParameter<DataSource>("dataSources");
			builder.EntityType<Report>().Function("GetCacheOptions").Returns<CacheOptions>();
			builder.EntityType<LinkedReport>().Function("GetCacheOptions").Returns<CacheOptions>();
			builder.EntityType<DataSet>().Function("GetCacheOptions").Returns<CacheOptions>();
			builder.EntityType<Report>().Action("SetCacheOptions").Parameter<CacheOptions>("cacheOptions");
			builder.EntityType<LinkedReport>().Action("SetCacheOptions").Parameter<CacheOptions>("cacheOptions");
			builder.EntityType<DataSet>().Action("SetCacheOptions").Parameter<CacheOptions>("cacheOptions");
			builder.EntityType<Report>().Action("UpdateReportDataSets").CollectionParameter<DataSet>("dataSets");
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009219 File Offset: 0x00007419
		protected override IQueryable<CatalogItem> GetEntitySet(string castName)
		{
			if (castName == null)
			{
				return this._catalogRepository.GetCatalogItems(base.User);
			}
			Type.GetType(castName) == null;
			return this._catalogRepository.GetCatalogItems(base.User);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009250 File Offset: 0x00007450
		protected override CatalogItem GetEntity(string key, string castName)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return null;
			}
			if (castName == null)
			{
				return this._catalogRepository.GetCatalogItem(base.User, guid);
			}
			if (castName == CatalogItemsController.DataSourceClassName)
			{
				return this._catalogRepository.GetDataSource(base.User, guid);
			}
			if (castName == CatalogItemsController.DataSetClassName)
			{
				return this._catalogRepository.GetDataSet(base.User, guid);
			}
			return this._catalogRepository.GetCatalogItem(base.User, guid);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000092D4 File Offset: 0x000074D4
		protected override bool DeleteEntity(string key)
		{
			Guid guid;
			return Guid.TryParse(key, out guid) && this._catalogRepository.Delete(base.User, guid);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009300 File Offset: 0x00007500
		protected override bool AddEntity(CatalogItem entity)
		{
			CatalogItem catalogItem;
			return this._catalogRepository.Create(base.User, entity, out catalogItem);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009321 File Offset: 0x00007521
		protected override bool AddEntity(CatalogItem entity, out CatalogItem createdEntity)
		{
			return this._catalogRepository.Create(base.User, entity, out createdEntity);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009338 File Offset: 0x00007538
		protected override bool PutEntity(string key, CatalogItem entity)
		{
			Guid guid;
			return Guid.TryParse(key, out guid) && this._catalogRepository.Update(base.User, guid, entity, null);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009368 File Offset: 0x00007568
		protected override bool PatchEntity(string key, CatalogItem entity, string[] delta)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return false;
			}
			string text = "Name";
			string text2 = "Path";
			if (delta.Contains(text) && delta.Contains(text2) && !entity.Path.EndsWith(entity.Name))
			{
				base.Logger.Trace(TraceLevel.Error, "Invalid PATCH request, Path does not end with Name of catalog item.");
				throw new ArgumentException("Path does not end with Name of catalog item.");
			}
			if (delta.Contains(text) && !delta.Contains(text2))
			{
				entity.Path = entity.Path.Substring(0, entity.Path.LastIndexOf("/")) + "/" + entity.Name;
			}
			return this._catalogRepository.Update(base.User, guid, entity, delta);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000942C File Offset: 0x0000762C
		protected IHttpActionResult GenerateContent(CatalogItem item, Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			if (item != null)
			{
				Stream contentStream = item.GetContentStream();
				if (contentStream != null)
				{
					MediaTypeHeaderValue mediaTypeHeaderValue = null;
					MediaTypeHeaderValue.TryParse(item.ContentType, out mediaTypeHeaderValue);
					HttpResponseMessage httpResponseMessage = new HttpResponseMessage
					{
						StatusCode = HttpStatusCode.OK,
						Content = new StreamContent(contentStream, this._catalogItemControllerHelper.StreamResponseBufferSizeInBytes)
					};
					if (oDataPath.Segments.Last<ODataPathSegment>() is ValueSegment)
					{
						string text = "attachment";
						if (mediaTypeHeaderValue != null)
						{
							httpResponseMessage.Content.Headers.ContentType = mediaTypeHeaderValue;
						}
						if (item.Type == CatalogItemType.Resource)
						{
							text = "inline";
						}
						httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(text)
						{
							FileName = this._catalogItemControllerHelper.EncodeFileNameForMimeHeader(this._catalogRepository.GetCatalogItemDownloadFileName(item))
						};
						return base.ResponseMessage(httpResponseMessage);
					}
				}
				return base.StatusCode(HttpStatusCode.NoContent);
			}
			return base.NotFound();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000950C File Offset: 0x0000770C
		public virtual IHttpActionResult GetContent(string key, Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			CatalogItem catalogItemWithContent = this._catalogRepository.GetCatalogItemWithContent(base.User, guid);
			return this.GenerateContent(catalogItemWithContent, oDataPath);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009548 File Offset: 0x00007748
		public virtual IQueryable<CatalogItem> GetDependentItems(string key)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return null;
			}
			return this._catalogRepository.GetDependentItems(base.User, guid);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009574 File Offset: 0x00007774
		[HttpGet]
		public virtual IHttpActionResult SearchItems(string key, string searchText)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			if (string.IsNullOrEmpty(searchText))
			{
				return base.BadRequest(SR.Error_SearchTextNullOrEmpty);
			}
			return this.Ok<IQueryable<CatalogItem>>(this._catalogRepository.SearchItems(base.User, guid, searchText));
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000095C0 File Offset: 0x000077C0
		[HttpPost]
		public virtual IHttpActionResult DeleteItems(ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || !actionParameters.ContainsKey("CatalogItemPaths"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			IEnumerable<string> enumerable = actionParameters["CatalogItemPaths"] as IEnumerable<string>;
			if (enumerable == null)
			{
				return base.BadRequest(SR.ParameterValueNotSupplied);
			}
			return this.Ok<BulkOperationsResult>(this._catalogRepository.DeleteItems(base.User, enumerable));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000962C File Offset: 0x0000782C
		[HttpPost]
		public virtual IHttpActionResult MoveItems(ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || !actionParameters.ContainsKey("CatalogItemPaths") || !actionParameters.ContainsKey("TargetPath"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			IEnumerable<string> enumerable = actionParameters["CatalogItemPaths"] as IEnumerable<string>;
			if (enumerable == null)
			{
				return base.BadRequest(SR.ParameterValueNotSupplied);
			}
			string text = actionParameters["TargetPath"] as string;
			if (text == null)
			{
				return base.BadRequest(SR.ParameterValueNotSupplied);
			}
			return this.Ok<BulkOperationsResult>(this._catalogRepository.MoveItems(base.User, enumerable, text));
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000096C8 File Offset: 0x000078C8
		[HttpPost]
		public virtual IHttpActionResult GetProperties(string key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || !actionParameters.ContainsKey("RequestedProperties"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			CatalogItem catalogItem = this._catalogRepository.GetCatalogItem(base.User, guid);
			IEnumerable<Property> enumerable = (IEnumerable<Property>)actionParameters["RequestedProperties"];
			IQueryable<Property> itemProperties = this._catalogRepository.GetItemProperties(base.User, catalogItem.Path, enumerable);
			return this.Ok<IQueryable<Property>>(itemProperties);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009754 File Offset: 0x00007954
		[HttpPost]
		public virtual IHttpActionResult SetProperties(string key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || !actionParameters.ContainsKey("Properties"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			CatalogItem catalogItemWithContent = this._catalogRepository.GetCatalogItemWithContent(base.User, guid);
			IEnumerable<Property> enumerable = (IEnumerable<Property>)actionParameters["Properties"];
			this._catalogRepository.SetItemProperties(base.User, catalogItemWithContent.Path, enumerable);
			return this.Ok<bool>(true);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000097DC File Offset: 0x000079DC
		[HttpPost]
		public virtual IHttpActionResult SetParameterProperties(string key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || !actionParameters.ContainsKey("ParameterProperties"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			CatalogItem entity = this.GetEntity(key, typeof(Report).AssemblyQualifiedName);
			if (entity.Type != CatalogItemType.Report && entity.Type != CatalogItemType.LinkedReport)
			{
				return base.BadRequest(string.Format("Catalog item {0} is neither a report or a linked report", key));
			}
			IEnumerable<ReportParameterProperties> enumerable = (IEnumerable<ReportParameterProperties>)actionParameters["ParameterProperties"];
			this._catalogRepository.UpdateReportParameterDefinition(base.User, entity.Path, enumerable);
			return base.StatusCode(HttpStatusCode.NoContent);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009880 File Offset: 0x00007A80
		public virtual IHttpActionResult GetCacheOptions(string key)
		{
			CatalogItem entity = this.GetEntity(key, null);
			CacheOptions itemCacheOptions = this._catalogRepository.GetItemCacheOptions(base.User, entity.Path);
			return this.Ok<CacheOptions>(itemCacheOptions);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000098B8 File Offset: 0x00007AB8
		[HttpPost]
		public virtual IHttpActionResult SetCacheOptions([FromODataUri] Guid key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || actionParameters == null || !actionParameters.ContainsKey("cacheOptions"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			CacheOptions cacheOptions = (CacheOptions)actionParameters["cacheOptions"];
			CatalogItem entity = this.GetEntity(key.ToString(), null);
			this._catalogRepository.SetItemCacheOptions(base.User, entity.Path, cacheOptions);
			return base.StatusCode(HttpStatusCode.NoContent);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009938 File Offset: 0x00007B38
		[HttpPost]
		public virtual IHttpActionResult UpdateExecutionSnapshot(string key)
		{
			CatalogItem entity = this.GetEntity(key, null);
			this._catalogRepository.UpdateItemExecutionSnapshot(base.User, entity.Path);
			return this.Ok<bool>(true);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000996C File Offset: 0x00007B6C
		[HttpPost]
		public virtual IHttpActionResult CreateSnapshot(string key)
		{
			CatalogItem entity = this.GetEntity(key, null);
			string text = this._catalogRepository.CreateItemHistorySnapshot(base.User, entity.Path);
			return this.Ok<string>(text);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000099A4 File Offset: 0x00007BA4
		[HttpPost]
		public virtual IHttpActionResult DeleteSnapshot(string key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || !actionParameters.ContainsKey("HistoryId"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			CatalogItem entity = this.GetEntity(key, null);
			bool flag = this._catalogRepository.DeleteItemHistorySnapshot(base.User, entity.Path, actionParameters["HistoryId"].ToString());
			return this.Ok<bool>(flag);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009A10 File Offset: 0x00007C10
		public virtual IHttpActionResult GetHistoryOptions(string key)
		{
			Report report = this.GetEntity(key, typeof(Report).AssemblyQualifiedName) as Report;
			if (report == null)
			{
				return base.BadRequest(string.Format("Catalog item {0} is not of type {1}", key, typeof(Report).ToString()));
			}
			ItemHistoryOptions itemHistoryOptions = this._catalogRepository.GetItemHistoryOptions(base.User, report.Path);
			return this.Ok<ItemHistoryOptions>(itemHistoryOptions);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009A7C File Offset: 0x00007C7C
		public virtual IHttpActionResult GetReportHistorySnapshotsOptions(string key)
		{
			CatalogItem entity = this.GetEntity(key, null);
			ReportHistorySnapshotsOptions reportHistorySnapshotsOptions = this._catalogRepository.GetReportHistorySnapshotsOptions(base.User, entity.Path);
			return this.Ok<ReportHistorySnapshotsOptions>(reportHistorySnapshotsOptions);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009AB4 File Offset: 0x00007CB4
		[HttpPost]
		public virtual IHttpActionResult SetReportHistorySnapshotsOptions([FromODataUri] Guid key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || actionParameters == null || !actionParameters.ContainsKey("ReportHistorySnapshotsOptions"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			ReportHistorySnapshotsOptions reportHistorySnapshotsOptions = (ReportHistorySnapshotsOptions)actionParameters["ReportHistorySnapshotsOptions"];
			CatalogItem entity = this.GetEntity(key.ToString(), null);
			this._catalogRepository.SetReportHistorySnapshotOptions(base.User, entity.Path, reportHistorySnapshotsOptions);
			return base.StatusCode(HttpStatusCode.NoContent);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009B34 File Offset: 0x00007D34
		public virtual IHttpActionResult GetSchema(string key)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			DataSetSchema dataSetSchema = this._dataService.GetDataSetSchema(base.User, guid);
			return this.Ok<DataSetSchema>(dataSetSchema);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009B6C File Offset: 0x00007D6C
		[HttpPost]
		public virtual IHttpActionResult GetData(string key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters)
		{
			return this.GetDataInternal(key, actionParameters, null);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009B8A File Offset: 0x00007D8A
		[HttpPost]
		public virtual IHttpActionResult GetData(string key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters, [FromODataUri] int? maxRows)
		{
			return this.GetDataInternal(key, actionParameters, maxRows);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009B98 File Offset: 0x00007D98
		[HttpPost]
		public virtual IHttpActionResult GetKpiTrendsetData(string key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters, [FromODataUri] string columnName)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			if (string.IsNullOrEmpty(columnName))
			{
				return base.BadRequest(SR.PropertyNullException);
			}
			actionParameters = actionParameters ?? new ODataActionParameters();
			IEnumerable<DataSetParameter> enumerable2;
			if (!actionParameters.ContainsKey("Parameters"))
			{
				IEnumerable<DataSetParameter> enumerable = new DataSetParameter[0];
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = actionParameters["Parameters"] as IEnumerable<DataSetParameter>;
			}
			IEnumerable<DataSetParameter> enumerable3 = enumerable2;
			string dataSetColumnJson = this._dataService.GetDataSetColumnJson(base.User, guid, enumerable3, columnName, 30);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(dataSetColumnJson)
			};
			httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return base.ResponseMessage(httpResponseMessage);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009C58 File Offset: 0x00007E58
		[HttpPost]
		public virtual IHttpActionResult GetAggregatedValue(string key, Microsoft.AspNet.OData.Routing.ODataPath path, ODataActionParameters actionParameters, [FromODataUri] string columnName, [FromODataUri] KpiSharedDataItemAggregation aggregation)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			if (string.IsNullOrEmpty(columnName))
			{
				return base.BadRequest(SR.PropertyNullException);
			}
			if (!Enum.IsDefined(typeof(KpiSharedDataItemAggregation), aggregation) || aggregation == KpiSharedDataItemAggregation.None)
			{
				return base.BadRequest(SR.InvalidAggregation);
			}
			actionParameters = actionParameters ?? new ODataActionParameters();
			IEnumerable<DataSetParameter> enumerable2;
			if (!actionParameters.ContainsKey("Parameters"))
			{
				IEnumerable<DataSetParameter> enumerable = new DataSetParameter[0];
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = actionParameters["Parameters"] as IEnumerable<DataSetParameter>;
			}
			IEnumerable<DataSetParameter> enumerable3 = enumerable2;
			string dataSetAggregatedValuesJson = this._dataService.GetDataSetAggregatedValuesJson(base.User, guid, enumerable3, columnName, aggregation);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(dataSetAggregatedValuesJson)
			};
			httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return base.ResponseMessage(httpResponseMessage);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009D40 File Offset: 0x00007F40
		[Obsolete("Use 'GetData'")]
		public virtual IHttpActionResult GetTable(string key)
		{
			return this.GetDataInternal(key, new ODataActionParameters(), null);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009D62 File Offset: 0x00007F62
		[Obsolete("Use 'GetData'")]
		public virtual IHttpActionResult GetTable(string key, [FromODataUri] int maxRows)
		{
			return this.GetDataInternal(key, new ODataActionParameters(), new int?(maxRows));
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009D78 File Offset: 0x00007F78
		internal virtual IHttpActionResult GetDataInternal(string key, ODataActionParameters actionParameters, int? maxRows)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			actionParameters = actionParameters ?? new ODataActionParameters();
			IEnumerable<DataSetParameter> enumerable2;
			if (!actionParameters.ContainsKey("Parameters"))
			{
				IEnumerable<DataSetParameter> enumerable = new DataSetParameter[0];
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = actionParameters["Parameters"] as IEnumerable<DataSetParameter>;
			}
			IEnumerable<DataSetParameter> enumerable3 = enumerable2;
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK
			};
			bool flag;
			if (base.Request.GetOwinContext().Get<bool>("CanCompress"))
			{
				byte[] compressedDataSetTableJson = this._dataService.GetCompressedDataSetTableJson(base.User, guid, enumerable3, maxRows, out flag);
				httpResponseMessage.Content = new ByteArrayContent(compressedDataSetTableJson);
				httpResponseMessage.Content.Headers.ContentEncoding.Add("gzip");
			}
			else
			{
				string dataSetTableJson = this._dataService.GetDataSetTableJson(base.User, guid, enumerable3, maxRows, out flag);
				httpResponseMessage.Content = new StringContent(dataSetTableJson);
			}
			httpResponseMessage.Headers.Add("DataSetJsonCached", flag.ToString().ToLowerInvariant());
			httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return base.ResponseMessage(httpResponseMessage);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009E98 File Offset: 0x00008098
		[HttpPost]
		public virtual IHttpActionResult UpdateReportDataSets(string key, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || actionParameters == null || !actionParameters.ContainsKey("dataSets") || string.IsNullOrEmpty(key))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			CatalogItem entity = this.GetEntity(key, typeof(Report).AssemblyQualifiedName);
			if (entity.Type != CatalogItemType.Report)
			{
				return base.BadRequest(string.Format("Catalog item {0} is not of type {1}", key, typeof(Report).ToString()));
			}
			IEnumerable<DataSet> enumerable = (IEnumerable<DataSet>)actionParameters["dataSets"];
			if (enumerable == null)
			{
				return base.BadRequest();
			}
			this._catalogRepository.SetItemDataSets(base.User, entity.Path, enumerable);
			return base.Ok();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009F54 File Offset: 0x00008154
		[HttpPost]
		public virtual IHttpActionResult UpdateItemDataSources(string key, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || actionParameters == null || !actionParameters.ContainsKey("dataSources"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			CatalogItem entity = this.GetEntity(key, null);
			IEnumerable<DataSource> enumerable = (IEnumerable<DataSource>)actionParameters["dataSources"];
			this._catalogRepository.SetItemDataSources(base.User, entity.Path, enumerable);
			return base.StatusCode(HttpStatusCode.NoContent);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009FC8 File Offset: 0x000081C8
		[HttpPost]
		public virtual IHttpActionResult GetQueryFields(ODataActionParameters actionParameters)
		{
			return this._catalogItemControllerHelper.GetQueryFields(actionParameters);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009FD8 File Offset: 0x000081D8
		[HttpPost]
		public virtual IHttpActionResult AddToFavorites(string key)
		{
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			bool flag = this._catalogRepository.AddToFavorites(base.User, new Guid(key));
			return this.Ok<bool>(flag);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A020 File Offset: 0x00008220
		public virtual IHttpActionResult RemoveFromFavorites(string key)
		{
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			bool flag = this._catalogRepository.RemoveFromFavorites(base.User, new Guid(key));
			return this.Ok<bool>(flag);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A066 File Offset: 0x00008266
		[HttpPost]
		public virtual IHttpActionResult CheckConnection(ODataActionParameters actionParameters)
		{
			return this._catalogItemControllerHelper.CheckConnection(actionParameters);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A074 File Offset: 0x00008274
		[HttpPost]
		public virtual IHttpActionResult CheckConnection(string key)
		{
			return this._catalogItemControllerHelper.CheckConnection(key);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A084 File Offset: 0x00008284
		[HttpPost]
		public virtual IHttpActionResult CheckDataSourceConnection(string key, ODataActionParameters actionParameters)
		{
			Guid guid;
			if (!Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			if (actionParameters == null || !base.ModelState.IsValid || !actionParameters.ContainsKey("DataSourceName"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			DataSourceCheckResult dataSourceCheckResult = this._catalogRepository.TestDataSource(base.User, guid, actionParameters["DataSourceName"].ToString());
			return this.Ok<DataSourceCheckResult>(dataSourceCheckResult);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A0F8 File Offset: 0x000082F8
		[HttpGet]
		public virtual IHttpActionResult GetRoles()
		{
			IQueryable<Role> catalogRoles = this._catalogRepository.GetCatalogRoles(base.User);
			return base.CreateOk(catalogRoles);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A11E File Offset: 0x0000831E
		[HttpPost]
		public virtual IHttpActionResult SetPolicies([FromODataUri] Guid key, ODataActionParameters actionParameters)
		{
			return this.CatalogItemControllerHelper.SetPolicies(key, actionParameters);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00004ECE File Offset: 0x000030CE
		[HttpGet]
		public virtual IHttpActionResult GetPolicies(string key)
		{
			return this.CatalogItemControllerHelper.GetPolicies(key);
		}

		// Token: 0x04000083 RID: 131
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000084 RID: 132
		private readonly IDataService _dataService;

		// Token: 0x04000085 RID: 133
		private readonly ISystemService _systemService;

		// Token: 0x04000086 RID: 134
		private readonly IPortalConfigurationManager _portalConfigurationManager;

		// Token: 0x04000087 RID: 135
		private readonly ILogger _logger;

		// Token: 0x04000088 RID: 136
		private readonly CatalogItemControllerHelper<CatalogItem> _catalogItemControllerHelper;

		// Token: 0x04000089 RID: 137
		protected static readonly string DataSourceClassName = typeof(DataSource).FullName;

		// Token: 0x0400008A RID: 138
		protected static readonly string DataSetClassName = typeof(DataSet).FullName;
	}
}
