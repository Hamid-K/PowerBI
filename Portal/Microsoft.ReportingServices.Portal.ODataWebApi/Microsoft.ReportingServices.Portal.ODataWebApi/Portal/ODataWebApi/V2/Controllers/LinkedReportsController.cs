using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
	// Token: 0x02000018 RID: 24
	public class LinkedReportsController : CatalogItemController<LinkedReport>
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00005B89 File Offset: 0x00003D89
		public LinkedReportsController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005B98 File Offset: 0x00003D98
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<LinkedReport>.RegisterModel(builder, "LinkedReports");
			builder.EntityType<LinkedReport>().Action("UpdateCacheSnapshot").Returns<bool>();
			builder.EntityType<LinkedReport>().Action("Reparent").Returns<string>()
				.Parameter<string>("ParentPath");
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005BE6 File Offset: 0x00003DE6
		protected override IQueryable<LinkedReport> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override LinkedReport GetEntity(string Id, string castName)
		{
			return base.GetEntity(Id, castName);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005BFC File Offset: 0x00003DFC
		[HttpPatch]
		[ODataRoute("LinkedReports({Id})/ParameterDefinitions")]
		[ODataRoute("LinkedReports(Path={Id})/ParameterDefinitions")]
		public IHttpActionResult PatchParameterDefinitions(string Id)
		{
			LinkedReport entityByGuid = this.GetEntityByGuid(Id);
			if (entityByGuid == null)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Unknown guid: {0}", Id));
				return base.StatusCode(HttpStatusCode.BadRequest);
			}
			return this.PatchParameterDefinitionsByPath(entityByGuid.Path);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005C44 File Offset: 0x00003E44
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

		// Token: 0x060000FF RID: 255 RVA: 0x00005CD4 File Offset: 0x00003ED4
		protected override bool AddEntity(LinkedReport entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005CDD File Offset: 0x00003EDD
		[HttpPost]
		[ODataRoute("LinkedReports({Id})/Model.Upload")]
		[ODataRoute("LinkedReports(Path={Id})/Model.Upload")]
		public IHttpActionResult UploadStream(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005CE8 File Offset: 0x00003EE8
		[HttpPost]
		[ODataRoute("LinkedReports({Id})/Model.Reparent")]
		[ODataRoute("LinkedReports(Path={Id})/Model.Reparent")]
		public IHttpActionResult Reparent(string Id, ODataActionParameters parameters)
		{
			if (!base.ModelState.IsValid || !parameters.ContainsKey("ParentPath"))
			{
				return base.BadRequest();
			}
			string text = (string)parameters["ParentPath"];
			LinkedReport entity = this.GetEntity(Id, null);
			Report report;
			try
			{
				report = (Report)base.CatalogRepository.GetCatalogItem(base.User, text);
			}
			catch (InvalidCastException ex)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Invalid payload: new parent is not an item of type Report", ex.Message));
				return base.BadRequest();
			}
			base.CatalogRepository.SetLinkedReportLink(base.User, entity, report.Path);
			return base.Ok();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005DA4 File Offset: 0x00003FA4
		protected override bool PutEntity(string Id, LinkedReport entity)
		{
			return base.PutEntity(Id, entity);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005DAE File Offset: 0x00003FAE
		protected override bool PatchEntity(string Id, LinkedReport entity, string[] delta)
		{
			return base.PatchEntity(Id, entity, delta);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005DB9 File Offset: 0x00003FB9
		protected override bool DeleteEntity(string Id)
		{
			return base.DeleteEntity(Id);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005DC2 File Offset: 0x00003FC2
		[HttpGet]
		[ODataRoute("LinkedReports({Id})/Policies")]
		[ODataRoute("LinkedReports(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005DCB File Offset: 0x00003FCB
		[HttpPut]
		[ODataRoute("LinkedReports({Id})/Policies")]
		[ODataRoute("LinkedReports(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005DD5 File Offset: 0x00003FD5
		[HttpPatch]
		[ODataRoute("LinkedReports({Id})/Policies")]
		[ODataRoute("LinkedReports(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005DDF File Offset: 0x00003FDF
		[HttpPost]
		[ODataRoute("LinkedReports({Id})/Policies")]
		[ODataRoute("LinkedReports(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005DE9 File Offset: 0x00003FE9
		[HttpDelete]
		[ODataRoute("LinkedReports({Id})/Policies")]
		[ODataRoute("LinkedReports(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005CDD File Offset: 0x00003EDD
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("LinkedReports({Id})/Properties")]
		[ODataRoute("LinkedReports(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005DF3 File Offset: 0x00003FF3
		[HttpGet]
		[ODataRoute("LinkedReports({Id})/Properties")]
		[ODataRoute("LinkedReports(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005DFD File Offset: 0x00003FFD
		[HttpPut]
		[ODataRoute("LinkedReports({Id})/Properties")]
		[ODataRoute("LinkedReports(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005E06 File Offset: 0x00004006
		[HttpPost]
		[ODataRoute("LinkedReports({Id})/HistorySnapshots")]
		[ODataRoute("LinkedReports(Path={Id})/HistorySnapshots")]
		public IHttpActionResult CreateHistorySnapshot(string Id)
		{
			return base.PostOnHistorySnapshots(Id);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005E0F File Offset: 0x0000400F
		[HttpGet]
		[ODataRoute("LinkedReports({Id})/HistorySnapshots({historyId})")]
		[ODataRoute("LinkedReports(Path={Id})/HistorySnapshots({historyId})")]
		public IHttpActionResult GetHistorySnapshot(string Id, string historyId)
		{
			return base.GetOnHistorySnapshots(Id, historyId);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005E19 File Offset: 0x00004019
		[HttpDelete]
		[ODataRoute("LinkedReports({Id})/HistorySnapshots({historyId})")]
		[ODataRoute("LinkedReports(Path={Id})/HistorySnapshots({historyId})")]
		public IHttpActionResult DeleteHistorySnapshot(string Id, string historyId)
		{
			return base.DeleteOnHistorySnapshots(Id, historyId);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005E23 File Offset: 0x00004023
		[HttpPut]
		[ODataRoute("LinkedReports({Id})/HistorySnapshotOptions")]
		[ODataRoute("LinkedReports(Path={Id})/HistorySnapshotOptions")]
		public IHttpActionResult PutHistorySnapshotOptions(string Id, [FromBody] HistorySnapshotOptions historySnapshotOptions)
		{
			return base.PutOnHistorySnapshotOptions(Id, historySnapshotOptions);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005E2D File Offset: 0x0000402D
		[HttpGet]
		[ODataRoute("LinkedReports({Id})/AllowedActions")]
		[ODataRoute("LinkedReports(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005E38 File Offset: 0x00004038
		[HttpPost]
		public IHttpActionResult UpdateCacheSnapshot(string key)
		{
			LinkedReport entity = this.GetEntity(key, null);
			base.CatalogRepository.UpdateItemExecutionSnapshot(base.User, entity.Path);
			return base.CreateOk(true);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005E71 File Offset: 0x00004071
		[HttpGet]
		[ODataRoute("LinkedReports({Id})/CacheOptions")]
		[ODataRoute("LinkedReports(Path={Id})/CacheOptions")]
		public override IHttpActionResult GetCacheOptions(string Id)
		{
			return base.GetCacheOptions(Id);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005E7A File Offset: 0x0000407A
		[HttpPut]
		[ODataRoute("LinkedReports({Id})/CacheOptions")]
		[ODataRoute("LinkedReports(Path={Id})/CacheOptions")]
		public override IHttpActionResult SetCacheOptions(string Id, [FromBody] CacheOptions cacheOptions)
		{
			return base.SetCacheOptions(Id, cacheOptions);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005E84 File Offset: 0x00004084
		[HttpGet]
		[ODataRoute("LinkedReports({Id})/Comments")]
		[ODataRoute("LinkedReports(Path={Id})/Comments")]
		public override IHttpActionResult GetComments(string Id)
		{
			return base.GetComments(Id);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005E8D File Offset: 0x0000408D
		[HttpPut]
		[ODataRoute("LinkedReports({Id})/Comments({ItemID})")]
		[ODataRoute("LinkedReports(Path={Id})/Comments({ItemID})")]
		public IHttpActionResult UpdateComment(string Id, string ItemId, Comment comment)
		{
			return base.UpdateComment(ItemId, comment);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005E97 File Offset: 0x00004097
		[HttpPatch]
		[ODataRoute("LinkedReports({Id})/Comments({ItemId})")]
		[ODataRoute("LinkedReports(Path={Id})/Comments({ItemId})")]
		public IHttpActionResult NotImplementedCommentsAction()
		{
			return base.StatusCode(HttpStatusCode.NotImplemented);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005EA4 File Offset: 0x000040A4
		[HttpDelete]
		[ODataRoute("LinkedReports({Id})/Comments({ItemId})")]
		[ODataRoute("LinkedReports(Path={Id})/Comments({ItemId})")]
		public IHttpActionResult DeleteComment(string Id, string ItemId)
		{
			return base.DeleteComment(ItemId);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005EAD File Offset: 0x000040AD
		[HttpPost]
		[ODataRoute("LinkedReports({Id})/Comments")]
		[ODataRoute("LinkedReports(Path={Id})/Comments")]
		public override IHttpActionResult PostComment(string Id, Comment comment)
		{
			return base.PostComment(Id, comment);
		}
	}
}
