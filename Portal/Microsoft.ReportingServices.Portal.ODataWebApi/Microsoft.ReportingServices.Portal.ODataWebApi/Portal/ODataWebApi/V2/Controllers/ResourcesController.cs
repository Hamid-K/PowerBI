using System;
using System.Linq;
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
	// Token: 0x02000016 RID: 22
	public class ResourcesController : CatalogItemController<Resource>
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000053C1 File Offset: 0x000035C1
		public ResourcesController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000053D0 File Offset: 0x000035D0
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<Resource>.RegisterModel(builder, "Resources");
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000053DD File Offset: 0x000035DD
		protected override IQueryable<Resource> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000053E6 File Offset: 0x000035E6
		protected override Resource GetEntity(string Id, string castName)
		{
			return base.GetEntity(Id, castName);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000053F0 File Offset: 0x000035F0
		protected override bool AddEntity(Resource entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000053FC File Offset: 0x000035FC
		[HttpPost]
		[ODataRoute("Resources({Id})/Model.Upload")]
		[ODataRoute("Resources(Path={Id})/Model.Upload")]
		public async Task<IHttpActionResult> UploadStream(string Id)
		{
			return await base.UploadStreamInternal(Id);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005449 File Offset: 0x00003649
		protected override bool PutEntity(string Id, Resource entity)
		{
			return base.PutEntity(Id, entity);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005453 File Offset: 0x00003653
		protected override bool PatchEntity(string Id, Resource entity, string[] delta)
		{
			return base.PatchEntity(Id, entity, delta);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000545E File Offset: 0x0000365E
		protected override bool DeleteEntity(string Id)
		{
			return base.DeleteEntity(Id);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005467 File Offset: 0x00003667
		[HttpGet]
		[ODataRoute("Resources({Id})/Policies")]
		[ODataRoute("Resources(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005470 File Offset: 0x00003670
		[HttpPut]
		[ODataRoute("Resources({Id})/Policies")]
		[ODataRoute("Resources(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000547A File Offset: 0x0000367A
		[HttpPatch]
		[ODataRoute("Resources({Id})/Policies")]
		[ODataRoute("Resources(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005484 File Offset: 0x00003684
		[HttpPost]
		[ODataRoute("Resources({Id})/Policies")]
		[ODataRoute("Resources(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000548E File Offset: 0x0000368E
		[HttpDelete]
		[ODataRoute("Resources({Id})/Policies")]
		[ODataRoute("Resources(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005498 File Offset: 0x00003698
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("Resources({Id})/Properties")]
		[ODataRoute("Resources(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000054A0 File Offset: 0x000036A0
		[HttpGet]
		[ODataRoute("Resources({Id})/Properties")]
		[ODataRoute("Resources(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000054AA File Offset: 0x000036AA
		[HttpPut]
		[ODataRoute("Resources({Id})/Properties")]
		[ODataRoute("Resources(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000054B3 File Offset: 0x000036B3
		[HttpGet]
		[ODataRoute("Resources({Id})/AllowedActions")]
		[ODataRoute("Resources(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}
	}
}
