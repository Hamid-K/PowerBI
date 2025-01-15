using System;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x0200002C RID: 44
	public class CacheRefreshPlanController : EntitySetReflectionODataController<CacheRefreshPlan>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000087FE File Offset: 0x000069FE
		protected ISubscriptionService SubscriptionService
		{
			get
			{
				return this._subscriptionService;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008806 File Offset: 0x00006A06
		public CacheRefreshPlanController(ISubscriptionService subscriptionService, ISystemService systemService, ILogger logger)
			: base(logger)
		{
			if (subscriptionService == null)
			{
				throw new ArgumentNullException("subscriptionService");
			}
			this._subscriptionService = subscriptionService;
			this._systemService = systemService;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000882B File Offset: 0x00006A2B
		protected override IQueryable<CacheRefreshPlan> GetEntitySet(string castName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008834 File Offset: 0x00006A34
		protected override CacheRefreshPlan GetEntity(string key, string castName)
		{
			Guid guid;
			if (Guid.TryParse(key, out guid))
			{
				CacheRefreshPlan cacheRefreshPlan = this._subscriptionService.GetCacheRefreshPlan(base.User, guid);
				cacheRefreshPlan.ScheduleDescription = this._systemService.PopulateScheduleDescription(base.User, cacheRefreshPlan.Schedule, base.GetUtcOffsetInMinutes());
				return cacheRefreshPlan;
			}
			return null;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008884 File Offset: 0x00006A84
		protected override bool DeleteEntity(string key)
		{
			Guid guid;
			if (Guid.TryParse(key, out guid))
			{
				this._subscriptionService.DeleteCacheRefreshPlan(base.User, guid);
				return true;
			}
			return false;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000088B0 File Offset: 0x00006AB0
		protected override bool AddEntity(CacheRefreshPlan entity)
		{
			this._subscriptionService.CreateCacheRefreshPlan(base.User, entity);
			return true;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000088C8 File Offset: 0x00006AC8
		protected override bool PutEntity(string key, CacheRefreshPlan entity)
		{
			Guid guid;
			if (Guid.TryParse(key, out guid))
			{
				this._subscriptionService.UpdateCacheRefreshPlan(base.User, guid, entity);
				return true;
			}
			return false;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000882B File Offset: 0x00006A2B
		protected override bool PatchEntity(string key, CacheRefreshPlan entity, string[] delta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000088F5 File Offset: 0x00006AF5
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<CacheRefreshPlan>("CacheRefreshPlan");
		}

		// Token: 0x0400007D RID: 125
		private readonly ISubscriptionService _subscriptionService;

		// Token: 0x0400007E RID: 126
		private readonly ISystemService _systemService;
	}
}
