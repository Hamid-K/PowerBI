using System;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200001B RID: 27
	internal sealed class AsPaasEndpointInfo
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000056CE File Offset: 0x000038CE
		public AsPaasEndpointInfo(Uri cluster, string server, string tenantId)
		{
			this.Cluster = cluster;
			this.Server = server;
			this.TenantId = tenantId;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000056EB File Offset: 0x000038EB
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000056F3 File Offset: 0x000038F3
		public Uri Cluster { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000056FC File Offset: 0x000038FC
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00005704 File Offset: 0x00003904
		public string Server { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000570D File Offset: 0x0000390D
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00005715 File Offset: 0x00003915
		public string TenantId { get; private set; }

		// Token: 0x0600009E RID: 158 RVA: 0x00005720 File Offset: 0x00003920
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

		// Token: 0x0600009F RID: 159 RVA: 0x00005750 File Offset: 0x00003950
		public static void AddEndpointInfoToCache(Uri dataSource, string serverName, AsPaasEndpointInfo info)
		{
			AsPaasEndpointInfo.cache.Insert(AsPaasEndpointInfo.GetCacheKey(dataSource, serverName), info);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005764 File Offset: 0x00003964
		public static void InvalidateCache()
		{
			AsPaasEndpointInfo.cache.Clear();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005770 File Offset: 0x00003970
		private static AsPaasEndpointInfo FromGlobalObject(object @object)
		{
			object[] array = (object[])@object;
			return new AsPaasEndpointInfo((Uri)array[0], (string)array[1], (string)array[2]);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000057A4 File Offset: 0x000039A4
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

		// Token: 0x060000A3 RID: 163 RVA: 0x000057CF File Offset: 0x000039CF
		private static object ConvertCachedItem(string cacheName, object cachedItem, string typeCode)
		{
			if (typeCode == "AsAzureConnectionInfo")
			{
				return AsPaasEndpointInfo.FromGlobalObject(cachedItem);
			}
			return cachedItem;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000057E6 File Offset: 0x000039E6
		private static string GetCacheKey(Uri dataSource, string serverName)
		{
			return string.Format("{0} + {1}", dataSource, serverName);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000057F4 File Offset: 0x000039F4
		private object ToGlobalObject()
		{
			return new object[] { this.Cluster, this.Server, this.TenantId };
		}

		// Token: 0x04000082 RID: 130
		private const string CacheName = "XmlaLibPaaSConnectionsCache";

		// Token: 0x04000083 RID: 131
		private const string CacheTypeCode = "AsAzureConnectionInfo";

		// Token: 0x04000084 RID: 132
		private const int CacheTimeoutInMinutes = 60;

		// Token: 0x04000085 RID: 133
		private const string CacheKeyTemplate = "{0} + {1}";

		// Token: 0x04000086 RID: 134
		private static SharedMemoryCache cache = SharedMemoryCache.Create("XmlaLibPaaSConnectionsCache", MemoryCacheRetentionPolicy.BuildSlidingWindowExpirationPolicy(TimeSpan.FromMinutes(60.0)), null, new PrepareItemForCaching(AsPaasEndpointInfo.PrepareItemForCaching), new ConvertCachedItem(AsPaasEndpointInfo.ConvertCachedItem));
	}
}
