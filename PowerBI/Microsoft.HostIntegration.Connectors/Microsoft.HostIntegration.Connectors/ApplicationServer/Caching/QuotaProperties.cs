using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C8 RID: 200
	[Serializable]
	internal class QuotaProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x0001746C File Offset: 0x0001566C
		internal QuotaProperties()
		{
			this.DefaultQuotaThresholds = new CacheQuotaThresholds();
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001747F File Offset: 0x0001567F
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00017491 File Offset: 0x00015691
		[ConfigurationProperty("cacheSizeQuotaEnabled", IsRequired = false, DefaultValue = false)]
		internal bool CacheSizeQuotaEnabled
		{
			get
			{
				return (bool)base["cacheSizeQuotaEnabled"];
			}
			set
			{
				base["cacheSizeQuotaEnabled"] = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000174A4 File Offset: 0x000156A4
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x000174B6 File Offset: 0x000156B6
		[ConfigurationProperty("cacheEvictionHighWatermarkPercent", IsRequired = false, DefaultValue = 110)]
		internal int CacheEvictionHighWatermarkPercent
		{
			get
			{
				return (int)base["cacheEvictionHighWatermarkPercent"];
			}
			set
			{
				base["cacheEvictionHighWatermarkPercent"] = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x000174C9 File Offset: 0x000156C9
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x000174DB File Offset: 0x000156DB
		[ConfigurationProperty("defaultQuotaPerCache", IsRequired = false)]
		internal CacheSizeQuotaConfig DefaultQuotaPerCache
		{
			get
			{
				return (CacheSizeQuotaConfig)base["defaultQuotaPerCache"];
			}
			set
			{
				base["defaultQuotaPerCache"] = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x000174E9 File Offset: 0x000156E9
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x000174FB File Offset: 0x000156FB
		[ConfigurationProperty("quotasThrottlingEnabled", IsRequired = false, DefaultValue = false)]
		internal bool QuotasThrottlingEnabled
		{
			get
			{
				return (bool)base["quotasThrottlingEnabled"];
			}
			set
			{
				base["quotasThrottlingEnabled"] = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001750E File Offset: 0x0001570E
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x00017520 File Offset: 0x00015720
		[ConfigurationProperty("defaultQuotaThresholds", IsRequired = false)]
		internal CacheQuotaThresholds DefaultQuotaThresholds
		{
			get
			{
				return (CacheQuotaThresholds)base["defaultQuotaThresholds"];
			}
			set
			{
				base["defaultQuotaThresholds"] = value;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00017530 File Offset: 0x00015730
		protected QuotaProperties(SerializationInfo info, StreamingContext context)
		{
			Version version = null;
			if (context.Context != null)
			{
				SerializationContext serializationContext = context.Context as SerializationContext;
				version = serializationContext.StoreVersion;
			}
			this.CacheSizeQuotaEnabled = info.GetBoolean("cacheSizeQuotaEnabled");
			this.QuotasThrottlingEnabled = info.GetBoolean("quotasThrottlingEnabled");
			try
			{
				this.DefaultQuotaPerCache = (CacheSizeQuotaConfig)info.GetValue("defaultQuotaPerCache", typeof(CacheSizeQuotaConfig));
			}
			catch (SerializationException)
			{
				this.DefaultQuotaPerCache = new CacheSizeQuotaConfig();
			}
			try
			{
				this.CacheEvictionHighWatermarkPercent = info.GetInt32("cacheEvictionHighWatermarkPercent");
			}
			catch (SerializationException)
			{
				this.CacheEvictionHighWatermarkPercent = 110;
			}
			if (ConfigManager.IsStoreVersionHigherThan2000(version))
			{
				this.DefaultQuotaThresholds = (CacheQuotaThresholds)info.GetValue("defaultQuotaThresholds", typeof(CacheQuotaThresholds));
				return;
			}
			this.DefaultQuotaThresholds = new CacheQuotaThresholds();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00017624 File Offset: 0x00015824
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("cacheSizeQuotaEnabled", this.CacheSizeQuotaEnabled);
			info.AddValue("quotasThrottlingEnabled", this.QuotasThrottlingEnabled);
			info.AddValue("defaultQuotaPerCache", this.DefaultQuotaPerCache);
			info.AddValue("defaultQuotaThresholds", this.DefaultQuotaThresholds);
			info.AddValue("cacheEvictionHighWatermarkPercent", this.CacheEvictionHighWatermarkPercent);
		}

		// Token: 0x040003A0 RID: 928
		internal const string CACHE_SIZE_QUOTA_ENABLED = "cacheSizeQuotaEnabled";

		// Token: 0x040003A1 RID: 929
		internal const string QUOTAS_THROTTLING_ENABLED = "quotasThrottlingEnabled";

		// Token: 0x040003A2 RID: 930
		internal const string DEFAULT_QUOTA_THRESHOLDS = "defaultQuotaThresholds";

		// Token: 0x040003A3 RID: 931
		internal const string DEFAULT_QUOTA_PER_CACHE = "defaultQuotaPerCache";

		// Token: 0x040003A4 RID: 932
		internal const string CACHE_EVICTION_HIGH_WATERMARK_PERCENT = "cacheEvictionHighWatermarkPercent";
	}
}
