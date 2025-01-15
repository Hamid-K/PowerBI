using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x02000031 RID: 49
	public class SubscriptionsController : EntitySetReflectionODataController<Subscription>
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A157 File Offset: 0x00008357
		protected ISubscriptionService SubscriptionService
		{
			get
			{
				return this._subscriptionService;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A15F File Offset: 0x0000835F
		protected ISystemService SystemService
		{
			get
			{
				return this._systemService;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A167 File Offset: 0x00008367
		public SubscriptionsController(ISubscriptionService subscriptionService, ILogger logger, ISystemService systemService)
			: base(logger)
		{
			if (subscriptionService == null)
			{
				throw new ArgumentNullException("subscriptionService");
			}
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			this._systemService = systemService;
			this._subscriptionService = subscriptionService;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A19C File Offset: 0x0000839C
		protected override IQueryable<Subscription> GetEntitySet(string castName)
		{
			List<Subscription> list = this._subscriptionService.GetSubscriptions(base.User).ToList<Subscription>();
			this._systemService.PopulateScheduleDescriptions(base.User, list, base.GetUtcOffsetInMinutes());
			this._systemService.PopulateLocalizedExtensionNames(base.User, list);
			return list.AsQueryable<Subscription>();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A1F0 File Offset: 0x000083F0
		protected override Subscription GetEntity(string key, string castName)
		{
			Guid guid;
			if (Guid.TryParse(key, out guid))
			{
				return this._subscriptionService.GetSubscription(base.User, guid);
			}
			return null;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A21C File Offset: 0x0000841C
		protected override bool DeleteEntity(string key)
		{
			Guid guid;
			if (Guid.TryParse(key, out guid))
			{
				this._subscriptionService.DeleteSubscription(base.User, guid);
				return true;
			}
			return false;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A248 File Offset: 0x00008448
		protected override bool AddEntity(Subscription entity)
		{
			Guid guid;
			Guid.TryParse(this._subscriptionService.CreateSubscription(base.User, entity), out guid);
			entity.Id = guid;
			return true;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A278 File Offset: 0x00008478
		protected override bool PutEntity(string key, Subscription entity)
		{
			Guid guid;
			if (Guid.TryParse(key, out guid))
			{
				this._subscriptionService.UpdateSubscription(base.User, guid, entity);
				return true;
			}
			return false;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A2A8 File Offset: 0x000084A8
		protected override bool PatchEntity(string key, Subscription entity, string[] delta)
		{
			Guid guid;
			if (Guid.TryParse(key, out guid))
			{
				this._subscriptionService.PatchSubscription(base.User, guid, entity, delta);
				return true;
			}
			return false;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A2D6 File Offset: 0x000084D6
		public virtual IHttpActionResult Enable(Guid key)
		{
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			this._subscriptionService.EnableSubscription(base.User, key);
			return this.Updated<bool>(true);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A30B File Offset: 0x0000850B
		public virtual IHttpActionResult Disable(Guid key)
		{
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			this._subscriptionService.DisableSubscription(base.User, key);
			return this.Updated<bool>(true);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A340 File Offset: 0x00008540
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<Subscription>("Subscriptions");
			builder.EntityType<Subscription>().Action("Enable");
			builder.EntityType<Subscription>().Action("Disable");
		}

		// Token: 0x0400008B RID: 139
		private readonly ISubscriptionService _subscriptionService;

		// Token: 0x0400008C RID: 140
		private readonly ISystemService _systemService;
	}
}
