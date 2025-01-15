using System;
using System.Linq;
using System.Security.Principal;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Repositories
{
	// Token: 0x02000095 RID: 149
	public interface ISubscriptionService
	{
		// Token: 0x060004D3 RID: 1235
		IQueryable<Subscription> GetSubscriptions(IPrincipal userPrincipal);

		// Token: 0x060004D4 RID: 1236
		Subscription GetSubscription(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004D5 RID: 1237
		string CreateSubscription(IPrincipal userPrincipal, Subscription subcription);

		// Token: 0x060004D6 RID: 1238
		void UpdateSubscription(IPrincipal userPrincipal, Guid key, Subscription subcription);

		// Token: 0x060004D7 RID: 1239
		void PatchSubscription(IPrincipal userPrincipal, Guid key, Subscription subcription, string[] delta);

		// Token: 0x060004D8 RID: 1240
		void DeleteSubscription(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004D9 RID: 1241
		void DeleteCacheRefreshPlan(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004DA RID: 1242
		void EnableSubscription(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004DB RID: 1243
		void DisableSubscription(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004DC RID: 1244
		CacheRefreshPlan GetCacheRefreshPlan(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004DD RID: 1245
		void CreateCacheRefreshPlan(IPrincipal usePrincipal, CacheRefreshPlan cacheRefreshingPlan);

		// Token: 0x060004DE RID: 1246
		void UpdateCacheRefreshPlan(IPrincipal userPrincipal, Guid key, CacheRefreshPlan cacheRefreshingPlan);

		// Token: 0x060004DF RID: 1247
		void ExecuteCacheRefreshPlan(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004E0 RID: 1248
		void ExecuteSubscription(IPrincipal userPrincipal, Guid key);
	}
}
