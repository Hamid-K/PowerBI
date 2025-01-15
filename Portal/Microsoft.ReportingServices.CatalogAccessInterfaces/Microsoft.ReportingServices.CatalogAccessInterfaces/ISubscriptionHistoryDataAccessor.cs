using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000015 RID: 21
	public interface ISubscriptionHistoryDataAccessor
	{
		// Token: 0x060000DD RID: 221
		Task<IList<SubscriptionHistoryEntity>> GetSubscriptionHistory(Guid subscriptionId);

		// Token: 0x060000DE RID: 222
		Task<int> AddSubscriptionHistoryAsync(SubscriptionHistoryEntity subscriptionHistoryEntity);

		// Token: 0x060000DF RID: 223
		Task<int> UpdateSubscriptionHistoryAsync(SubscriptionHistoryEntity subscriptionHistoryEntity);
	}
}
