using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200000B RID: 11
	internal class HybridClientDrmutility : IDRMUtility, IDisposable
	{
		// Token: 0x06000057 RID: 87 RVA: 0x0000368B File Offset: 0x0000188B
		public void Add(string cacheName, ClientRoutingManager clientRoutingManager)
		{
			this.routingManagerMap[cacheName] = clientRoutingManager;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000369A File Offset: 0x0000189A
		public void Remove(string cacheName)
		{
			this.routingManagerMap.Remove(cacheName);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000036A9 File Offset: 0x000018A9
		public void Dispose()
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000036AC File Offset: 0x000018AC
		public CacheLookupTable GetLookupTable(string cacheName)
		{
			if (string.IsNullOrEmpty(cacheName))
			{
				throw new ArgumentNullException("cacheName");
			}
			ClientRoutingManager clientRoutingManager = null;
			this.routingManagerMap.TryGetValue(cacheName, out clientRoutingManager);
			if (clientRoutingManager != null)
			{
				return clientRoutingManager.LookupTable;
			}
			CacheEventHelper.WriteError("HybridClientDrmutility", "Could not find ClientRoutingManager for cache {0}.", new object[] { cacheName });
			return null;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003704 File Offset: 0x00001904
		public void PrepareNotificationRequest(RequestBody requestBody, PartitionId partitionId, NamedCacheConfiguration cacheConfiguration)
		{
			if (requestBody == null)
			{
				throw new ArgumentNullException("requestBody");
			}
			if (partitionId == null)
			{
				throw new ArgumentNullException("partitionId");
			}
			if (cacheConfiguration == null)
			{
				throw new ArgumentNullException("cacheConfiguration");
			}
			requestBody.Fwd = ForwardingType.Routable;
			requestBody.PartitionId = partitionId;
			requestBody.SystemRegionCount = cacheConfiguration.SystemRegionCount;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000375C File Offset: 0x0000195C
		public ReadOnlyCollection<PartitionId> GetPartitionIdList(string cacheName)
		{
			CacheLookupTable lookupTable = this.GetLookupTable(cacheName);
			if (lookupTable != null)
			{
				List<CachePartitionId> cachePartitionIds = lookupTable.GetCachePartitionIds();
				if (cachePartitionIds != null && cachePartitionIds.Count > 0)
				{
					List<PartitionId> list = new List<PartitionId>();
					foreach (CachePartitionId cachePartitionId in lookupTable.GetCachePartitionIds())
					{
						list.Add(new PartitionId(cachePartitionId.ServiceNamespace, cachePartitionId.LowKey, cachePartitionId.HighKey));
					}
					CacheEventHelper.WriteInformation("HybridClientDrmutility", "{0} cache partitions retrieved for cache {1}.", new object[] { list.Count, cacheName });
					return new ReadOnlyCollection<PartitionId>(list);
				}
				CacheEventHelper.WriteError("HybridClientDrmutility", "No cache partitions retrieved for cache {0}.", new object[] { cacheName });
			}
			return null;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003848 File Offset: 0x00001A48
		public EndpointID GetPrimaryEndPoint(PartitionId partitionId)
		{
			if (partitionId == null)
			{
				throw new ArgumentNullException("partitionId");
			}
			CacheLookupTable lookupTable = this.GetLookupTable(partitionId.ServiceNamespace);
			string text = null;
			if (lookupTable != null)
			{
				text = lookupTable.LookupEndpointAddress(partitionId.LowKey);
			}
			if (!string.IsNullOrEmpty(text))
			{
				CacheEventHelper.WriteInformation("HybridClientDrmutility", "Endpoint address {0} found for partitionID {1}.", new object[] { text, partitionId });
				return new EndpointID(text);
			}
			CacheEventHelper.WriteError("HybridClientDrmutility", "No valid endpoint address found for partitionID {0}.", new object[] { partitionId });
			return null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000038D4 File Offset: 0x00001AD4
		public PartitionId GetPartitionId(string cacheName, string regionName, int systemRegionCount)
		{
			CacheLookupTable lookupTable = this.GetLookupTable(cacheName);
			CachePartitionId cachePartitionId = null;
			if (lookupTable != null)
			{
				int regionId = Utility.GetRegionId(regionName, systemRegionCount);
				cachePartitionId = lookupTable.LookupPartitionId(regionId);
			}
			if (cachePartitionId != null)
			{
				PartitionId partitionId = new PartitionId(cachePartitionId.ServiceNamespace, cachePartitionId.LowKey, cachePartitionId.HighKey);
				CacheEventHelper.WriteInformation("HybridClientDrmutility", "Partition {0} found for region {1} in cache {2}.", new object[] { partitionId, regionName, cacheName });
				return partitionId;
			}
			CacheEventHelper.WriteError("HybridClientDrmutility", "No partition found for region {0} in cache {1}.", new object[] { regionName, cacheName });
			return null;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003970 File Offset: 0x00001B70
		public void RaiseComplaint(PartitionId partitionId, EndpointID endpoint)
		{
			if (partitionId == null)
			{
				throw new ArgumentNullException("partitionId");
			}
			ClientRoutingManager clientRoutingManager = null;
			this.routingManagerMap.TryGetValue(partitionId.ServiceNamespace, out clientRoutingManager);
			if (clientRoutingManager != null)
			{
				clientRoutingManager.RefreshLookUpTableAsync(null);
			}
		}

		// Token: 0x0400004C RID: 76
		private const string LogSource = "HybridClientDrmutility";

		// Token: 0x0400004D RID: 77
		private Dictionary<string, ClientRoutingManager> routingManagerMap = new Dictionary<string, ClientRoutingManager>();
	}
}
