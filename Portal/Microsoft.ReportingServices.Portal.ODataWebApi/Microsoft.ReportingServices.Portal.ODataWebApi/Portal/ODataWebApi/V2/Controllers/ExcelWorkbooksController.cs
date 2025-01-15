using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x0200001B RID: 27
	public class ExcelWorkbooksController : CatalogItemController<ExcelWorkbook>
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00006077 File Offset: 0x00004277
		public ExcelWorkbooksController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006086 File Offset: 0x00004286
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<ExcelWorkbook>.RegisterModel(builder, "ExcelWorkbooks");
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005557 File Offset: 0x00003757
		protected override bool EntityUsesStreamingStorage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006093 File Offset: 0x00004293
		protected override IQueryable<ExcelWorkbook> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000609C File Offset: 0x0000429C
		protected override ExcelWorkbook GetEntity(string Id, string castName)
		{
			return base.GetEntity(Id, castName);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000060A6 File Offset: 0x000042A6
		protected override bool AddEntity(ExcelWorkbook entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000060B0 File Offset: 0x000042B0
		[HttpPost]
		[ODataRoute("ExcelWorkbooks({Id})/Model.Upload")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Model.Upload")]
		public async Task<IHttpActionResult> UploadStream(string Id)
		{
			return await base.UploadStreamInternal(Id);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000060FD File Offset: 0x000042FD
		protected override bool PutEntity(string Id, ExcelWorkbook entity)
		{
			return base.PutEntity(Id, entity);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006107 File Offset: 0x00004307
		protected override bool PatchEntity(string Id, ExcelWorkbook entity, string[] delta)
		{
			return base.PatchEntity(Id, entity, delta);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006112 File Offset: 0x00004312
		protected override bool DeleteEntity(string Id)
		{
			return base.DeleteEntity(Id);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000611B File Offset: 0x0000431B
		[HttpGet]
		[ODataRoute("ExcelWorkbooks({Id})/Policies")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006124 File Offset: 0x00004324
		[HttpPut]
		[ODataRoute("ExcelWorkbooks({Id})/Policies")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000612E File Offset: 0x0000432E
		[HttpPatch]
		[ODataRoute("ExcelWorkbooks({Id})/Policies")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006138 File Offset: 0x00004338
		[HttpPost]
		[ODataRoute("ExcelWorkbooks({Id})/Policies")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006142 File Offset: 0x00004342
		[HttpDelete]
		[ODataRoute("ExcelWorkbooks({Id})/Policies")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000614C File Offset: 0x0000434C
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("ExcelWorkbooks({Id})/Properties")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006154 File Offset: 0x00004354
		[HttpGet]
		[ODataRoute("ExcelWorkbooks({Id})/Properties")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000615E File Offset: 0x0000435E
		[HttpPut]
		[ODataRoute("ExcelWorkbooks({Id})/Properties")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006167 File Offset: 0x00004367
		[HttpGet]
		[ODataRoute("ExcelWorkbooks({Id})/AllowedActions")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006170 File Offset: 0x00004370
		[HttpGet]
		[ODataRoute("ExcelWorkbooks({Id})/Comments")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Comments")]
		public override IHttpActionResult GetComments(string Id)
		{
			return base.GetComments(Id);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006179 File Offset: 0x00004379
		[HttpPut]
		[ODataRoute("ExcelWorkbooks({Id})/Comments({ItemID})")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Comments({ItemID})")]
		public IHttpActionResult UpdateComment(string Id, string ItemId, Comment comment)
		{
			return base.UpdateComment(ItemId, comment);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006183 File Offset: 0x00004383
		[HttpPatch]
		[ODataRoute("ExcelWorkbooks({Id})/Comments({ItemId})")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Comments({ItemId})")]
		public IHttpActionResult NotImplementedCommentsAction()
		{
			return base.StatusCode(HttpStatusCode.NotImplemented);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006190 File Offset: 0x00004390
		[HttpDelete]
		[ODataRoute("ExcelWorkbooks({Id})/Comments({ItemId})")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Comments({ItemId})")]
		public IHttpActionResult DeleteComment(string Id, string ItemId)
		{
			return base.DeleteComment(ItemId);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006199 File Offset: 0x00004399
		[HttpPost]
		[ODataRoute("ExcelWorkbooks({Id})/Comments")]
		[ODataRoute("ExcelWorkbooks(Path={Id})/Comments")]
		public override IHttpActionResult PostComment(string Id, Comment comment)
		{
			return base.PostComment(Id, comment);
		}
	}
}
