using System;
using System.Linq;
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
	// Token: 0x0200001A RID: 26
	public class FoldersController : CatalogItemController<Folder>
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00005F62 File Offset: 0x00004162
		public FoldersController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005F71 File Offset: 0x00004171
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<Folder>.RegisterModel(builder, "Folders");
			builder.EntityType<Folder>().Function("SearchItems").ReturnsCollectionFromEntitySet<CatalogItem>("CatalogItems")
				.Parameter<string>("SearchText");
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005FA3 File Offset: 0x000041A3
		[HttpGet]
		[ODataRoute("Folders({Id})/Policies")]
		[ODataRoute("Folders(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005FAC File Offset: 0x000041AC
		[HttpPut]
		[ODataRoute("Folders({Id})/Policies")]
		[ODataRoute("Folders(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005FB6 File Offset: 0x000041B6
		[HttpPatch]
		[ODataRoute("Folders({Id})/Policies")]
		[ODataRoute("Folders(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005FC0 File Offset: 0x000041C0
		[HttpPost]
		[ODataRoute("Folders({Id})/Policies")]
		[ODataRoute("Folders(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005FCA File Offset: 0x000041CA
		[HttpDelete]
		[ODataRoute("Folders({Id})/Policies")]
		[ODataRoute("Folders(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005FD4 File Offset: 0x000041D4
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("Folders({Id})/Properties")]
		[ODataRoute("Folders(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005FDC File Offset: 0x000041DC
		[HttpGet]
		[ODataRoute("Folders({Id})/Properties")]
		[ODataRoute("Folders(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005FE6 File Offset: 0x000041E6
		[HttpPut]
		[ODataRoute("Folders({Id})/Properties")]
		[ODataRoute("Folders(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005FEF File Offset: 0x000041EF
		[HttpGet]
		[ODataRoute("Folders({Id})/AllowedActions")]
		[ODataRoute("Folders(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005FF8 File Offset: 0x000041F8
		[HttpGet]
		public virtual IHttpActionResult SearchItems(string key, string searchText)
		{
			string text;
			bool flag = base.CatalogItemControllerHelper.IsRequestByPath(key, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(key, out guid))
			{
				return base.NotFound();
			}
			if (string.IsNullOrEmpty(searchText))
			{
				return base.BadRequest(SR.Error_SearchTextNullOrEmpty);
			}
			IQueryable<CatalogItem> queryable = (flag ? base.CatalogRepository.SearchItems(base.User, text, searchText) : base.CatalogRepository.SearchItems(base.User, guid, searchText));
			return this.Ok<IQueryable<CatalogItem>>(queryable);
		}
	}
}
