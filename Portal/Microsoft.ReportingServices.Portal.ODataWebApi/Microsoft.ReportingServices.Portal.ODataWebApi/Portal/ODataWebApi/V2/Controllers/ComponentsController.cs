using System;
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
	// Token: 0x02000013 RID: 19
	public class ComponentsController : CatalogItemController<Component>
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00004FA3 File Offset: 0x000031A3
		public ComponentsController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004FB2 File Offset: 0x000031B2
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<Component>.RegisterModel(builder, "Components");
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004FBF File Offset: 0x000031BF
		[HttpGet]
		[ODataRoute("Components({Id})/Policies")]
		[ODataRoute("Components(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004FC8 File Offset: 0x000031C8
		[HttpPut]
		[ODataRoute("Components({Id})/Policies")]
		[ODataRoute("Components(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004FD2 File Offset: 0x000031D2
		[HttpPatch]
		[ODataRoute("Components({Id})/Policies")]
		[ODataRoute("Components(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004FDC File Offset: 0x000031DC
		[HttpPost]
		[ODataRoute("Components({Id})/Policies")]
		[ODataRoute("Components(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004FE6 File Offset: 0x000031E6
		[HttpDelete]
		[ODataRoute("Components({Id})/Policies")]
		[ODataRoute("Components(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004FF0 File Offset: 0x000031F0
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("Components({Id})/Properties")]
		[ODataRoute("Components(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004FF8 File Offset: 0x000031F8
		[HttpGet]
		[ODataRoute("Components({Id})/Properties")]
		[ODataRoute("Components(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005002 File Offset: 0x00003202
		[HttpPut]
		[ODataRoute("Components({Id})/Properties")]
		[ODataRoute("Components(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000500B File Offset: 0x0000320B
		[HttpGet]
		[ODataRoute("Components({Id})/AllowedActions")]
		[ODataRoute("Components(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}
	}
}
