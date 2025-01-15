using System;
using System.Threading.Tasks;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000014 RID: 20
	public interface ISubscriptionDataAccessor
	{
		// Token: 0x060000DB RID: 219
		Task<int> UpdateSubscriptionLastRunInfoAsync(Guid subscriptionId, int stateFlag, DateTime lastRunTime, string lastStatus);

		// Token: 0x060000DC RID: 220
		Task<SubscriptionEntity> GetSubscription(Guid subscriptionId);
	}
}
