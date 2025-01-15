using System;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000024 RID: 36
	public class SubscriptionsController : SubscriptionsController
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00007696 File Offset: 0x00005896
		public SubscriptionsController(ISubscriptionService subscriptionService, ILogger logger, ISystemService systemService)
			: base(subscriptionService, logger, systemService)
		{
			this._subscriptionService = base.SubscriptionService;
			this._systemService = base.SystemService;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000076B9 File Offset: 0x000058B9
		protected override IQueryable<Subscription> GetEntitySet(string castName)
		{
			return base.GetEntitySet(castName);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000076C2 File Offset: 0x000058C2
		protected override Subscription GetEntity(string key, string castName)
		{
			return base.GetEntity(key, castName);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000076CC File Offset: 0x000058CC
		protected override bool DeleteEntity(string key)
		{
			return base.DeleteEntity(key);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000076D5 File Offset: 0x000058D5
		protected override bool AddEntity(Subscription entity)
		{
			return base.AddEntity(entity);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000076DE File Offset: 0x000058DE
		protected override bool PutEntity(string key, Subscription entity)
		{
			return base.PutEntity(key, entity);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000076E8 File Offset: 0x000058E8
		protected override bool PatchEntity(string key, Subscription entity, string[] delta)
		{
			return base.PatchEntity(key, entity, delta);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000076F3 File Offset: 0x000058F3
		public override IHttpActionResult Enable(Guid key)
		{
			return base.Enable(key);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000076FC File Offset: 0x000058FC
		public override IHttpActionResult Disable(Guid key)
		{
			return base.Disable(key);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007705 File Offset: 0x00005905
		public IHttpActionResult Execute(Guid key)
		{
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			this._subscriptionService.ExecuteSubscription(base.User, key);
			return this.Updated<bool>(true);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000773A File Offset: 0x0000593A
		public new static void RegisterModel(ODataConventionModelBuilder builder)
		{
			SubscriptionsController.RegisterModel(builder);
			builder.EntityType<Subscription>().Action("Execute");
		}

		// Token: 0x0400006B RID: 107
		private readonly ISubscriptionService _subscriptionService;

		// Token: 0x0400006C RID: 108
		private readonly ISystemService _systemService;
	}
}
