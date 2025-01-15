using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E3 RID: 483
	public static class DataCacheItemFactory
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x00035C38 File Offset: 0x00033E38
		internal static void SetDataCacheSerializationProperties(string cacheName, DataCacheSerializationProperties dataCacheSerializationProperties)
		{
			if (string.IsNullOrEmpty(cacheName))
			{
				throw new ArgumentNullException("cacheName");
			}
			if (dataCacheSerializationProperties == null)
			{
				throw new ArgumentNullException("dataCacheSerializationProperties");
			}
			DataCacheObjectSerializationProvider dataCacheObjectSerializationProvider = new DataCacheObjectSerializationProvider(dataCacheSerializationProperties);
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.DataCacheItemFactory", "Adding DataCacheObjectSerializationProvider for cache : {0}", new object[] { cacheName });
			}
			lock (DataCacheItemFactory.cacheSerializationProviders)
			{
				DataCacheItemFactory.cacheSerializationProviders[cacheName] = dataCacheObjectSerializationProvider;
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00035CC8 File Offset: 0x00033EC8
		internal static DataCacheObjectSerializationProvider GetDataCacheSerializationProvider(string cacheName)
		{
			DataCacheObjectSerializationProvider dataCacheObjectSerializationProvider;
			if (!DataCacheItemFactory.cacheSerializationProviders.TryGetValue(cacheName, out dataCacheObjectSerializationProvider))
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.DataCacheItemFactory", "CacheObjectSerializer not found for : {0}", new object[] { cacheName });
				}
				lock (DataCacheItemFactory.cacheSerializationProviders)
				{
					if (!DataCacheItemFactory.cacheSerializationProviders.TryGetValue(cacheName, out dataCacheObjectSerializationProvider))
					{
						DataCacheItemFactory.cacheSerializationProviders[cacheName] = new DataCacheObjectSerializationProvider(new DataCacheSerializationProperties());
						dataCacheObjectSerializationProvider = DataCacheItemFactory.cacheSerializationProviders[cacheName];
					}
				}
			}
			return dataCacheObjectSerializationProvider;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00035D64 File Offset: 0x00033F64
		public static DataCacheItem GetCacheItem(DataCacheItemKey key, string cacheName, object value, IEnumerable<DataCacheTag> tags)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			List<DataCacheTag> list = new List<DataCacheTag>();
			if (tags != null)
			{
				list.AddRange(tags);
			}
			byte[][] array = DataCacheItemFactory.GetDataCacheSerializationProvider(cacheName).SerializeUserObject(value, false, ValueFlagsVersion.WireProtocolType);
			return new DataCacheItem(new Key(key.Key), array, ValueFlagsVersion.WireProtocolType, default(InternalCacheItemVersion), list.ToArray(), key.Region, cacheName, TimeSpan.Zero, TimeSpan.Zero);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00035DD4 File Offset: 0x00033FD4
		internal static DataCacheItem GetCacheItem(IOMCacheItem item)
		{
			return new DataCacheItem(item.Key, item.Value, ValueFlagsVersion.EitherType, default(InternalCacheItemVersion), item.Tags, item.RegionName, item.CacheName, TimeSpan.Zero, TimeSpan.Zero);
		}

		// Token: 0x04000AA3 RID: 2723
		private const string LogSource = "DistributedCache.DataCacheItemFactory";

		// Token: 0x04000AA4 RID: 2724
		private static readonly IDictionary<string, DataCacheObjectSerializationProvider> cacheSerializationProviders = new Dictionary<string, DataCacheObjectSerializationProvider>();
	}
}
