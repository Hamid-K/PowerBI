using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x0200000F RID: 15
	public class CacheRefreshPlansController : CacheRefreshPlanController
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003A8C File Offset: 0x00001C8C
		public CacheRefreshPlansController(ISubscriptionService subscriptionService, ILogger logger, ICatalogRepository catalogRepository, ISystemService systemService)
			: base(subscriptionService, systemService, logger)
		{
			this._subscriptionService = base.SubscriptionService;
			this._catalogRepository = catalogRepository;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003AAB File Offset: 0x00001CAB
		protected ICatalogRepository CatalogRepository
		{
			get
			{
				return this._catalogRepository;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003AB3 File Offset: 0x00001CB3
		public new static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<CacheRefreshPlan>("CacheRefreshPlans");
			builder.EntityType<CacheRefreshPlan>().Action("Execute");
			builder.EntitySet<SubscriptionHistory>("CacheRefreshPlanHistory");
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003ADE File Offset: 0x00001CDE
		protected override IQueryable<CacheRefreshPlan> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003AE7 File Offset: 0x00001CE7
		protected override CacheRefreshPlan GetEntity(string key, string castName)
		{
			return base.GetEntity(key, castName);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003AF1 File Offset: 0x00001CF1
		protected override bool DeleteEntity(string key)
		{
			return base.DeleteEntity(key);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003AFA File Offset: 0x00001CFA
		[HttpPost]
		protected override bool AddEntity(CacheRefreshPlan entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003B03 File Offset: 0x00001D03
		protected override bool PutEntity(string key, CacheRefreshPlan entity)
		{
			return base.PutEntity(key, entity);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003B0D File Offset: 0x00001D0D
		protected override bool PatchEntity(string key, CacheRefreshPlan entity, string[] delta)
		{
			return base.PatchEntity(key, entity, delta);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003B18 File Offset: 0x00001D18
		[HttpPost]
		public IHttpActionResult Execute(Guid key)
		{
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			this._subscriptionService.ExecuteCacheRefreshPlan(base.User, key);
			return this.Updated<bool>(true);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003B50 File Offset: 0x00001D50
		[HttpGet]
		[ODataRoute("CacheRefreshPlans({Id})/History")]
		public IHttpActionResult GetCacheRefreshPlanHistory(string Id)
		{
			string empty = string.Empty;
			Guid guid;
			if (!Guid.TryParse(Id, out guid))
			{
				return base.NotFound();
			}
			List<SubscriptionHistory> subscriptionsHistory = this.CatalogRepository.GetSubscriptionsHistory(guid);
			return base.CreateOk(subscriptionsHistory);
		}

		// Token: 0x04000040 RID: 64
		private readonly ISubscriptionService _subscriptionService;

		// Token: 0x04000041 RID: 65
		private readonly ICatalogRepository _catalogRepository;
	}
}
