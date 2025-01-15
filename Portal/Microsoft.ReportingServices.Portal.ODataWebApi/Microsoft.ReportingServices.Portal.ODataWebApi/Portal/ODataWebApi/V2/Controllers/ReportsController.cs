using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000021 RID: 33
	public class ReportsController : CatalogItemController<Report>
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00006DA7 File Offset: 0x00004FA7
		public ReportsController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<Report>.RegisterModel(builder, "Reports");
			builder.EntitySet<ReportParameterDefinition>("ParameterDefinitions");
			builder.EntityType<Report>().Action("UpdateCacheSnapshot").Returns<bool>();
			builder.EntityType<Report>().Action("CheckDataSourceConnection").Returns<DataSourceCheckResult>()
				.Parameter<string>("DataSourceName");
			builder.EntityType<Report>().Action("UpdateReportDataSets").CollectionParameter<DataSet>("DataSets");
			builder.EntityType<Report>().Action("GetParameters").ReturnsCollectionFromEntitySet<ReportParameterDefinition>("ParameterDefinitions")
				.CollectionParameter<ParameterValue>("ParameterValues");
			builder.EntityType<Report>().Action("ProcessParameters").ReturnsCollectionFromEntitySet<ReportParameterDefinition>("ParameterDefinitions")
				.CollectionParameter<ParameterValue>("ParameterValues");
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006E78 File Offset: 0x00005078
		[HttpPatch]
		[ODataRoute("Reports({Id})/ParameterDefinitions")]
		[ODataRoute("Reports(Path={Id})/ParameterDefinitions")]
		public IHttpActionResult PatchParameterDefinitions(string Id)
		{
			CatalogItem entity = this.GetEntity(Id, null);
			if (entity == null)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Unknown guid: {0}", Id));
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			return this.PatchParameterDefinitionsByPath(entity.Path);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006EC0 File Offset: 0x000050C0
		private IHttpActionResult PatchParameterDefinitionsByPath(string path)
		{
			string result = base.Request.Content.ReadAsStringAsync().Result;
			List<ReportParameterDefinitionPatch> list = new List<ReportParameterDefinitionPatch>();
			try
			{
				JsonConvert.PopulateObject(result, list);
			}
			catch (Exception ex)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Invalid payload: {0}", ex.Message));
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			base.CatalogRepository.UpdateReportParameterDefinition(base.User, path, list);
			return base.StatusCode(HttpStatusCode.NoContent);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006F50 File Offset: 0x00005150
		[HttpPost]
		[ODataRoute("Reports({Id})/Model.Upload")]
		[ODataRoute("Reports(Path={Id})/Model.Upload")]
		public async Task<IHttpActionResult> UploadStream(string Id)
		{
			return await base.UploadStreamInternal(Id);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006F9D File Offset: 0x0000519D
		[HttpGet]
		[ODataRoute("Reports({Id})/Policies")]
		[ODataRoute("Reports(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006FA6 File Offset: 0x000051A6
		[HttpPut]
		[ODataRoute("Reports({Id})/Policies")]
		[ODataRoute("Reports(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006FB0 File Offset: 0x000051B0
		[HttpPatch]
		[ODataRoute("Reports({Id})/Policies")]
		[ODataRoute("Reports(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006FBA File Offset: 0x000051BA
		[HttpPost]
		[ODataRoute("Reports({Id})/Policies")]
		[ODataRoute("Reports(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006FC4 File Offset: 0x000051C4
		[HttpDelete]
		[ODataRoute("Reports({Id})/Policies")]
		[ODataRoute("Reports(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006FCE File Offset: 0x000051CE
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("Reports({Id})/Properties")]
		[ODataRoute("Reports(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006FD6 File Offset: 0x000051D6
		[HttpGet]
		[ODataRoute("Reports({Id})/Properties")]
		[ODataRoute("Reports(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006FE0 File Offset: 0x000051E0
		[HttpPut]
		[ODataRoute("Reports({Id})/Properties")]
		[ODataRoute("Reports(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006FE9 File Offset: 0x000051E9
		[HttpPost]
		[ODataRoute("Reports({Id})/HistorySnapshots")]
		[ODataRoute("Reports(Path={Id})/HistorySnapshots")]
		public IHttpActionResult CreateHistorySnapshot(string Id)
		{
			return base.PostOnHistorySnapshots(Id);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006FF2 File Offset: 0x000051F2
		[HttpGet]
		[ODataRoute("Reports({Id})/HistorySnapshots({historyId})")]
		[ODataRoute("Reports(Path={Id})/HistorySnapshots({historyId})")]
		public IHttpActionResult GetHistorySnapshot(string Id, string historyId)
		{
			return base.GetOnHistorySnapshots(Id, historyId);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006FFC File Offset: 0x000051FC
		[HttpDelete]
		[ODataRoute("Reports({Id})/HistorySnapshots({historyId})")]
		[ODataRoute("Reports(Path={Id})/HistorySnapshots({historyId})")]
		public IHttpActionResult DeleteHistorySnapshot(string Id, string historyId)
		{
			return base.DeleteOnHistorySnapshots(Id, historyId);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007006 File Offset: 0x00005206
		[HttpPut]
		[ODataRoute("Reports({Id})/HistorySnapshotOptions")]
		[ODataRoute("Reports(Path={Id})/HistorySnapshotOptions")]
		public IHttpActionResult PutHistorySnapshotOptions(string Id, [FromBody] HistorySnapshotOptions historySnapshotOptions)
		{
			return base.PutOnHistorySnapshotOptions(Id, historySnapshotOptions);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007010 File Offset: 0x00005210
		[HttpPut]
		[ODataRoute("Reports({Id})/DataSources({dataSourceId})")]
		[ODataRoute("Reports(Path={Id})/DataSources({dataSourceId})")]
		public IHttpActionResult PutItemDataSources(string Id, string dataSourceId, [FromBody] DataSource dataSource)
		{
			return base.PutOnItemDataSources(Id, dataSourceId, dataSource);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000701B File Offset: 0x0000521B
		[HttpGet]
		[ODataRoute("Reports({Id})/DataSources({dataSourceId})")]
		[ODataRoute("Reports(Path={Id})/DataSources({dataSourceId})")]
		public IHttpActionResult GetItemDataSources(string Id, string dataSourceId)
		{
			return base.GetOnItemDataSources(Id, dataSourceId);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007025 File Offset: 0x00005225
		[HttpPut]
		[ODataRoute("Reports({Id})/DataSources")]
		[ODataRoute("Reports(Path={Id})/DataSources")]
		public IHttpActionResult PutDataSources(string Id)
		{
			return base.PutOnDataSourcesCollection(Id, base.Request);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007034 File Offset: 0x00005234
		[HttpPost]
		[ODataRoute("Reports({Id})/DataSources")]
		[ODataRoute("Reports(Path={Id})/DataSources")]
		public IHttpActionResult PostDataSources(string Id)
		{
			return base.StatusCode(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007034 File Offset: 0x00005234
		[HttpPatch]
		[ODataRoute("Reports({Id})/DataSources")]
		[ODataRoute("Reports(Path={Id})/DataSources")]
		public IHttpActionResult PatchDataSources(string Id)
		{
			return base.StatusCode(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007044 File Offset: 0x00005244
		[HttpGet]
		[ODataRoute("Reports({Id})/SharedDataSets")]
		[ODataRoute("Reports(Path={Id})/SharedDataSets")]
		public IHttpActionResult GetSharedDataSets(string Id)
		{
			CatalogItem catalogItemByKey = base.CatalogItemControllerHelper.GetCatalogItemByKey(Id);
			if (catalogItemByKey == null)
			{
				return base.BadRequest();
			}
			IList<DataSet> dataSetsForCatalogItem = base.CatalogRepository.GetDataSetsForCatalogItem(base.User, catalogItemByKey.Path);
			if (dataSetsForCatalogItem != null)
			{
				return this.Ok<IList<DataSet>>(dataSetsForCatalogItem);
			}
			return base.StatusCode(HttpStatusCode.NotFound);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007098 File Offset: 0x00005298
		[HttpPut]
		[ODataRoute("Reports({Id})/SharedDataSets")]
		[ODataRoute("Reports(Path={Id})/SharedDataSets")]
		public IHttpActionResult PutSharedDataSets(string Id)
		{
			Report entity = this.GetEntity(Id, null);
			string result = base.Request.Content.ReadAsStringAsync().Result;
			List<DataSet> list = new List<DataSet>();
			try
			{
				JsonConvert.PopulateObject(result, list);
			}
			catch (Exception ex)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Invalid payload: {0}", ex.Message));
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			base.CatalogRepository.SetItemDataSets(base.User, entity.Path, list);
			return base.StatusCode(HttpStatusCode.OK);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007138 File Offset: 0x00005338
		[HttpGet]
		[ODataRoute("Reports({Id})/AllowedActions")]
		[ODataRoute("Reports(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007141 File Offset: 0x00005341
		[ODataRoute("Reports({key})/DependentItems")]
		[ODataRoute("Reports(Path={key})/DependentItems")]
		public IHttpActionResult GetDependentItems(string key)
		{
			return base.GetDependentItems(key, CatalogItemType.Report);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000714B File Offset: 0x0000534B
		[HttpGet]
		[ODataRoute("Reports({Id})/CacheOptions")]
		[ODataRoute("Reports(Path={Id})/CacheOptions")]
		public override IHttpActionResult GetCacheOptions(string Id)
		{
			return base.GetCacheOptions(Id);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007154 File Offset: 0x00005354
		[HttpPut]
		[ODataRoute("Reports({Id})/CacheOptions")]
		[ODataRoute("Reports(Path={Id})/CacheOptions")]
		public override IHttpActionResult SetCacheOptions(string Id, [FromBody] CacheOptions cacheOptions)
		{
			return base.SetCacheOptions(Id, cacheOptions);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000715E File Offset: 0x0000535E
		[HttpPost]
		public IHttpActionResult CheckDataSourceConnection(string key, ODataActionParameters actionParameters)
		{
			return base.CatalogItemControllerHelper.CheckDataSourceConnection(key, actionParameters);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007170 File Offset: 0x00005370
		[HttpPost]
		[ODataRoute("Reports({key})/Model.UpdateCacheSnapshot")]
		[ODataRoute("Reports(Path={key})/Model.UpdateCacheSnapshot")]
		public IHttpActionResult UpdateCacheSnapshot(string key)
		{
			Report entity = this.GetEntity(key, null);
			base.CatalogRepository.UpdateItemExecutionSnapshot(base.User, entity.Path);
			return base.CreateOk(true);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000071AC File Offset: 0x000053AC
		[HttpPost]
		public IHttpActionResult UpdateReportDataSets(string key, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || actionParameters == null || !actionParameters.ContainsKey("DataSets") || string.IsNullOrEmpty(key))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			CatalogItem entity = this.GetEntity(key, typeof(Report).AssemblyQualifiedName);
			if (entity.Type != CatalogItemType.Report)
			{
				return base.BadRequest(string.Format("Catalog item {0} is not of type {1}", key, typeof(Report).ToString()));
			}
			IEnumerable<DataSet> enumerable = (IEnumerable<DataSet>)actionParameters["DataSets"];
			if (enumerable == null)
			{
				return base.BadRequest();
			}
			base.CatalogRepository.SetItemDataSets(base.User, entity.Path, enumerable);
			return this.Updated<bool>(true);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007266 File Offset: 0x00005466
		[HttpGet]
		[ODataRoute("Reports({Id})/Comments")]
		[ODataRoute("Reports(Path={Id})/Comments")]
		public override IHttpActionResult GetComments(string Id)
		{
			return base.GetComments(Id);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000726F File Offset: 0x0000546F
		[HttpPut]
		[ODataRoute("Reports({Id})/Comments({ItemId})")]
		[ODataRoute("Reports(Path={Id})/Comments({ItemId})")]
		public IHttpActionResult UpdateComment(string Id, string ItemId, Comment comment)
		{
			return base.UpdateComment(ItemId, comment);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007034 File Offset: 0x00005234
		[HttpPatch]
		[ODataRoute("Reports({Id})/Comments({ItemId})")]
		[ODataRoute("Reports(Path={Id})/Comments({ItemId})")]
		public IHttpActionResult NotImplementedCommentsAction()
		{
			return base.StatusCode(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007279 File Offset: 0x00005479
		[HttpDelete]
		[ODataRoute("Reports({Id})/Comments({ItemId})")]
		[ODataRoute("Reports(Path={Id})/Comments({ItemId})")]
		public IHttpActionResult DeleteComment(string Id, string ItemId)
		{
			return base.DeleteComment(ItemId);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007282 File Offset: 0x00005482
		[HttpPost]
		[ODataRoute("Reports({Id})/Comments")]
		[ODataRoute("Reports(Path={Id})/Comments")]
		public override IHttpActionResult PostComment(string Id, Comment comment)
		{
			return base.PostComment(Id, comment);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000728C File Offset: 0x0000548C
		[HttpPost]
		[ODataRoute("Reports({key})/Model.GetParameters")]
		[ODataRoute("Reports({key})/Model.ProcessParameters")]
		public IHttpActionResult ProcessParameters(string key, ODataPath path, ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || !actionParameters.ContainsKey("ParameterValues"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			CatalogItem catalogItemByKey = base.CatalogItemControllerHelper.GetCatalogItemByKey(key);
			if (catalogItemByKey.Type != CatalogItemType.Report && catalogItemByKey.Type != CatalogItemType.LinkedReport && catalogItemByKey.Type != CatalogItemType.DataSet)
			{
				return base.BadRequest(string.Format("Catalog item {0} is neither a report nor linked report nor data set", key));
			}
			IEnumerable<ParameterValue> enumerable = (IEnumerable<ParameterValue>)actionParameters["ParameterValues"];
			IEnumerable<ReportParameterDefinition> enumerable2 = base.CatalogRepository.GetReportParameterDefinitionsWithQueryAndCurrentValues(base.User, catalogItemByKey.Path, enumerable);
			if (catalogItemByKey.Type == CatalogItemType.DataSet)
			{
				enumerable2 = enumerable2.Select(delegate(ReportParameterDefinition parameter)
				{
					parameter.AllowBlank = true;
					parameter.MultiValue = true;
					parameter.PromptUser = true;
					parameter.ParameterState = ReportParameterState.HasValidValue;
					parameter.ParameterType = ReportParameterType.String;
					return parameter;
				});
			}
			return this.Ok<IEnumerable<ReportParameterDefinition>>(enumerable2);
		}

		// Token: 0x04000066 RID: 102
		private const string DataSets = "DataSets";
	}
}
