using System;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200002D RID: 45
	public class SubscriptionDataAccessor : ISubscriptionDataAccessor
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00006F64 File Offset: 0x00005164
		public async Task<int> UpdateSubscriptionLastRunInfoAsync(Guid subscriptionId, int stateFlag, DateTime lastRunTime, string lastStatus)
		{
			var <>f__AnonymousType = new
			{
				SubscriptionID = subscriptionId,
				Flags = stateFlag,
				LastRunTime = lastRunTime,
				LastStatus = lastStatus
			};
			return await CatalogAccessFactory.ExecuteAsync("UpdateSubscriptionLastRunInfo", <>f__AnonymousType);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006FC4 File Offset: 0x000051C4
		public async Task<SubscriptionEntity> GetSubscription(Guid subscriptionId)
		{
			var <>f__AnonymousType = new
			{
				SubscriptionID = subscriptionId
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<SubscriptionEntity>("GetSubscription", <>f__AnonymousType);
		}
	}
}
