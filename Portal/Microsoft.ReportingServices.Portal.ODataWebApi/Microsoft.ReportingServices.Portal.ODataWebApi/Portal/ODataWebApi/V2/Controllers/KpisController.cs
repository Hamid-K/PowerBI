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
	// Token: 0x02000019 RID: 25
	public class KpisController : CatalogItemController<Kpi>
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00005EB7 File Offset: 0x000040B7
		public KpisController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005EC6 File Offset: 0x000040C6
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<Kpi>.RegisterModel(builder, "Kpis");
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005ED3 File Offset: 0x000040D3
		protected override IQueryable<Kpi> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005EDC File Offset: 0x000040DC
		protected override Kpi GetEntity(string Id, string castName)
		{
			return base.GetEntity(Id, castName);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005EE6 File Offset: 0x000040E6
		protected override bool AddEntity(Kpi entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005EEF File Offset: 0x000040EF
		protected override bool PutEntity(string Id, Kpi entity)
		{
			return base.PutEntity(Id, entity);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005EF9 File Offset: 0x000040F9
		protected override bool PatchEntity(string Id, Kpi entity, string[] delta)
		{
			return base.PatchEntity(Id, entity, delta);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005F04 File Offset: 0x00004104
		protected override bool DeleteEntity(string Id)
		{
			return base.DeleteEntity(Id);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005F0D File Offset: 0x0000410D
		[HttpGet]
		[ODataRoute("Kpis({Id})/Policies")]
		[ODataRoute("Kpis(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005F16 File Offset: 0x00004116
		[HttpPut]
		[ODataRoute("Kpis({Id})/Policies")]
		[ODataRoute("Kpis(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005F20 File Offset: 0x00004120
		[HttpPatch]
		[ODataRoute("Kpis({Id})/Policies")]
		[ODataRoute("Kpis(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005F2A File Offset: 0x0000412A
		[HttpPost]
		[ODataRoute("Kpis({Id})/Policies")]
		[ODataRoute("Kpis(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005F34 File Offset: 0x00004134
		[HttpDelete]
		[ODataRoute("Kpis({Id})/Policies")]
		[ODataRoute("Kpis(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005F3E File Offset: 0x0000413E
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("Kpis({Id})/Properties")]
		[ODataRoute("Kpis(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005F46 File Offset: 0x00004146
		[HttpGet]
		[ODataRoute("Kpis({Id})/Properties")]
		[ODataRoute("Kpis(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005F50 File Offset: 0x00004150
		[HttpPut]
		[ODataRoute("Kpis({Id})/Properties")]
		[ODataRoute("Kpis(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005F59 File Offset: 0x00004159
		[HttpGet]
		[ODataRoute("Kpis({Id})/AllowedActions")]
		[ODataRoute("Kpis(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}
	}
}
