using System;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000007 RID: 7
	internal sealed class AsPaasEndpointInfo
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002216 File Offset: 0x00000416
		public AsPaasEndpointInfo(Uri cluster, string server, string tenantId)
		{
			this.Cluster = cluster;
			this.Server = server;
			this.TenantId = tenantId;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002233 File Offset: 0x00000433
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000223B File Offset: 0x0000043B
		public Uri Cluster { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002244 File Offset: 0x00000444
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000224C File Offset: 0x0000044C
		public string Server { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002255 File Offset: 0x00000455
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000225D File Offset: 0x0000045D
		public string TenantId { get; private set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002268 File Offset: 0x00000468
		public static bool TryGetEndpointInfoFromCache(Uri dataSource, string serverName, out AsPaasEndpointInfo info)
		{
			object obj;
			if (AsPaasEndpointInfo.cache.Lookup(AsPaasEndpointInfo.GetCacheKey(dataSource, serverName), out obj))
			{
				info = (AsPaasEndpointInfo)obj;
				return true;
			}
			info = null;
			return false;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002298 File Offset: 0x00000498
		public static void AddEndpointInfoToCache(Uri dataSource, string serverName, AsPaasEndpointInfo info)
		{
			AsPaasEndpointInfo.cache.Insert(AsPaasEndpointInfo.GetCacheKey(dataSource, serverName), info);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022AC File Offset: 0x000004AC
		public static void InvalidateCache()
		{
			AsPaasEndpointInfo.cache.Clear();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B8 File Offset: 0x000004B8
		private static AsPaasEndpointInfo FromGlobalObject(object @object)
		{
			object[] array = (object[])@object;
			return new AsPaasEndpointInfo((Uri)array[0], (string)array[1], (string)array[2]);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022EC File Offset: 0x000004EC
		private static void PrepareItemForCaching(string cacheName, ref object item, out string typeCode)
		{
			AsPaasEndpointInfo asPaasEndpointInfo = item as AsPaasEndpointInfo;
			if (asPaasEndpointInfo != null)
			{
				item = asPaasEndpointInfo.ToGlobalObject();
				typeCode = "AsAzureConnectionInfo";
				return;
			}
			typeCode = null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002317 File Offset: 0x00000517
		private static object ConvertCachedItem(string cacheName, object cachedItem, string typeCode)
		{
			if (typeCode == "AsAzureConnectionInfo")
			{
				return AsPaasEndpointInfo.FromGlobalObject(cachedItem);
			}
			return cachedItem;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000232E File Offset: 0x0000052E
		private static string GetCacheKey(Uri dataSource, string serverName)
		{
			return string.Format("{0} + {1}", dataSource, serverName);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000233C File Offset: 0x0000053C
		private object ToGlobalObject()
		{
			return new object[] { this.Cluster, this.Server, this.TenantId };
		}

		// Token: 0x04000036 RID: 54
		private const string CacheName = "XmlaLibPaaSConnectionsCache";

		// Token: 0x04000037 RID: 55
		private const string CacheTypeCode = "AsAzureConnectionInfo";

		// Token: 0x04000038 RID: 56
		private const int CacheTimeoutInMinutes = 60;

		// Token: 0x04000039 RID: 57
		private const string CacheKeyTemplate = "{0} + {1}";

		// Token: 0x0400003A RID: 58
		private static SharedMemoryCache cache = SharedMemoryCache.Create("XmlaLibPaaSConnectionsCache", MemoryCacheRetentionPolicy.BuildSlidingWindowExpirationPolicy(TimeSpan.FromMinutes(60.0)), null, new PrepareItemForCaching(AsPaasEndpointInfo.PrepareItemForCaching), new ConvertCachedItem(AsPaasEndpointInfo.ConvertCachedItem));
	}
}
