using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200002E RID: 46
	public class SubscriptionHistoryDataAccessor : ISubscriptionHistoryDataAccessor
	{
		// Token: 0x06000131 RID: 305 RVA: 0x0000700C File Offset: 0x0000520C
		public async Task<IList<SubscriptionHistoryEntity>> GetSubscriptionHistory(Guid subscriptionId)
		{
			var <>f__AnonymousType = new
			{
				SubscriptionID = subscriptionId
			};
			return await CatalogAccessFactory.QueryAsync<SubscriptionHistoryEntity>("GetSubscriptionHistory", <>f__AnonymousType);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007054 File Offset: 0x00005254
		public async Task<int> AddSubscriptionHistoryAsync(SubscriptionHistoryEntity subscriptionHistoryEntity)
		{
			var <>f__AnonymousType = new
			{
				SubscriptionID = subscriptionHistoryEntity.SubscriptionID,
				Type = Convert.ToInt16(subscriptionHistoryEntity.Type),
				StartTime = subscriptionHistoryEntity.StartTime,
				Status = Convert.ToInt16(subscriptionHistoryEntity.Status),
				Message = subscriptionHistoryEntity.Message
			};
			return await CatalogAccessFactory.QueryFirstOrDefaultAsync<int>("AddSubscriptionHistoryEntry", <>f__AnonymousType);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000709C File Offset: 0x0000529C
		public async Task<int> UpdateSubscriptionHistoryAsync(SubscriptionHistoryEntity subscriptionHistoryEntity)
		{
			var <>f__AnonymousType = new
			{
				SubscriptionHistoryID = subscriptionHistoryEntity.SubscriptionHistoryID,
				EndTime = subscriptionHistoryEntity.EndTime,
				Status = Convert.ToInt16(subscriptionHistoryEntity.Status),
				Message = subscriptionHistoryEntity.Message,
				Details = subscriptionHistoryEntity.Details
			};
			return await CatalogAccessFactory.ExecuteAsync("UpdateSubscriptionHistoryEntry", <>f__AnonymousType);
		}
	}
}
