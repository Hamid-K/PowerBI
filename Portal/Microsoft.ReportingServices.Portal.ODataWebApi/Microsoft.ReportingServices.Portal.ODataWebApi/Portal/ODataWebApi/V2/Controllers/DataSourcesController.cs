using System;
using System.Linq;
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

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x0200001C RID: 28
	public class DataSourcesController : CatalogItemController<DataSource>
	{
		// Token: 0x0600014F RID: 335 RVA: 0x000061A3 File Offset: 0x000043A3
		public DataSourcesController(ICatalogRepository catalogRepository, IDataService dataService, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager, ILogger logger)
			: base(catalogRepository, dataService, systemService, portalConfigurationManager, logger)
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000061B4 File Offset: 0x000043B4
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			CatalogItemController<DataSource>.RegisterModel(builder, "DataSources");
			builder.EntityType<DataSource>().Action("CheckConnection").Returns<DataSourceCheckResult>();
			builder.EntityType<DataSource>().Collection.Action("CheckConnection").Returns<DataSourceCheckResult>().Parameter<DataSource>("dataSource");
			ActionConfiguration actionConfiguration = builder.EntityType<DataSource>().Collection.Action("GetQueryFields").ReturnsCollection<string>();
			actionConfiguration.Parameter<DataSource>("dataSource").Optional();
			actionConfiguration.Parameter<Query>("query");
			actionConfiguration.Parameter<string>("subscriptionId").Optional();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000624E File Offset: 0x0000444E
		protected override IQueryable<DataSource> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006258 File Offset: 0x00004458
		protected override DataSource GetEntity(string Id, string castName)
		{
			string text;
			bool flag = base.CatalogItemControllerHelper.IsRequestByPath(Id, out text);
			Guid guid = default(Guid);
			if (!flag && !Guid.TryParse(Id, out guid))
			{
				return null;
			}
			if (!flag)
			{
				return base.CatalogRepository.GetDataSource(base.User, guid);
			}
			return base.CatalogRepository.GetDataSource(base.User, text);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000062B3 File Offset: 0x000044B3
		protected override bool AddEntity(DataSource entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000062BC File Offset: 0x000044BC
		[HttpPost]
		[ODataRoute("DataSources({Id})/Model.Upload")]
		[ODataRoute("DataSources(Path={Id})/Model.Upload")]
		public async Task<IHttpActionResult> UploadStream(string Id)
		{
			return await base.UploadStreamInternal(Id);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006309 File Offset: 0x00004509
		protected override bool PutEntity(string Id, DataSource entity)
		{
			return base.PutEntity(Id, entity);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006313 File Offset: 0x00004513
		protected override bool PatchEntity(string Id, DataSource entity, string[] delta)
		{
			return base.PatchEntity(Id, entity, delta);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000631E File Offset: 0x0000451E
		protected override bool DeleteEntity(string Id)
		{
			return base.DeleteEntity(Id);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006327 File Offset: 0x00004527
		[HttpGet]
		[ODataRoute("DataSources({Id})/Policies")]
		[ODataRoute("DataSources(Path={Id})/Policies")]
		public override IHttpActionResult GetPolicies(string Id)
		{
			return base.GetPolicies(Id);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006330 File Offset: 0x00004530
		[HttpPut]
		[ODataRoute("DataSources({Id})/Policies")]
		[ODataRoute("DataSources(Path={Id})/Policies")]
		public override IHttpActionResult PutPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PutPolicies(Id, policies);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000633A File Offset: 0x0000453A
		[HttpPatch]
		[ODataRoute("DataSources({Id})/Policies")]
		[ODataRoute("DataSources(Path={Id})/Policies")]
		public override IHttpActionResult PatchPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PatchPolicies(Id, policies);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006344 File Offset: 0x00004544
		[HttpPost]
		[ODataRoute("DataSources({Id})/Policies")]
		[ODataRoute("DataSources(Path={Id})/Policies")]
		public override IHttpActionResult PostPolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.PostPolicies(Id, policies);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000634E File Offset: 0x0000454E
		[HttpDelete]
		[ODataRoute("DataSources({Id})/Policies")]
		[ODataRoute("DataSources(Path={Id})/Policies")]
		public override IHttpActionResult DeletePolicies(string Id, [FromBody] ItemPolicy policies)
		{
			return base.DeletePolicies(Id, policies);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006358 File Offset: 0x00004558
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("DataSources({Id})/Properties")]
		[ODataRoute("DataSources(Path={Id})/Properties")]
		public IHttpActionResult NotAllowedPropetiesMethods(string Id)
		{
			return base.NotAllowed();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006360 File Offset: 0x00004560
		[HttpGet]
		[ODataRoute("DataSources({Id})/Properties")]
		[ODataRoute("DataSources(Path={Id})/Properties")]
		public override IHttpActionResult GetCatalogItemProperties(string Id, [FromUri] string properties)
		{
			return base.GetCatalogItemProperties(Id, properties);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000636A File Offset: 0x0000456A
		[HttpPut]
		[ODataRoute("DataSources({Id})/Properties")]
		[ODataRoute("DataSources(Path={Id})/Properties")]
		public override IHttpActionResult PutCatalogItemProperties(string Id)
		{
			return base.PutCatalogItemProperties(Id);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006373 File Offset: 0x00004573
		[HttpGet]
		[ODataRoute("DataSources({Id})/AllowedActions")]
		[ODataRoute("DataSources(Path={Id})/AllowedActions")]
		public override IHttpActionResult GetAllowedActions(string Id)
		{
			return base.GetAllowedActions(Id);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000637C File Offset: 0x0000457C
		[ODataRoute("DataSources({key})/DependentItems")]
		[ODataRoute("DataSources(Path={key})/DependentItems")]
		public IHttpActionResult GetDependentItems(string key)
		{
			return base.GetDependentItems(key, CatalogItemType.DataSource);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006386 File Offset: 0x00004586
		[HttpPost]
		public virtual IHttpActionResult CheckConnection(ODataActionParameters actionParameters)
		{
			return base.CatalogItemControllerHelper.CheckConnection(actionParameters);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006394 File Offset: 0x00004594
		[HttpPost]
		public virtual IHttpActionResult CheckConnection(string key)
		{
			return base.CatalogItemControllerHelper.CheckConnection(key);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000063A2 File Offset: 0x000045A2
		[HttpPost]
		public virtual IHttpActionResult GetQueryFields(ODataActionParameters actionParameters)
		{
			return base.CatalogItemControllerHelper.GetQueryFields(actionParameters);
		}
	}
}
