using System;
using System.Collections.ObjectModel;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000256 RID: 598
	internal interface IDRMUtility : IDisposable
	{
		// Token: 0x06001433 RID: 5171
		ReadOnlyCollection<PartitionId> GetPartitionIdList(string cacheName);

		// Token: 0x06001434 RID: 5172
		PartitionId GetPartitionId(string cacheName, string regionName, int systemRegionCount);

		// Token: 0x06001435 RID: 5173
		EndpointID GetPrimaryEndPoint(PartitionId partitionId);

		// Token: 0x06001436 RID: 5174
		void RaiseComplaint(PartitionId partitionId, EndpointID endpoint);

		// Token: 0x06001437 RID: 5175
		void PrepareNotificationRequest(RequestBody requestBody, PartitionId partitionId, NamedCacheConfiguration cacheConfiguration);
	}
}
