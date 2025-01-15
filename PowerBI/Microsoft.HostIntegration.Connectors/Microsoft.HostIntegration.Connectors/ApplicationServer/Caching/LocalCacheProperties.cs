using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000CE RID: 206
	internal class LocalCacheProperties : ConfigurationElement
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00015FAF File Offset: 0x000141AF
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00015FC1 File Offset: 0x000141C1
		[TypeConverter(typeof(IsLocalCacheEnabledConverter))]
		[ConfigurationProperty("isEnabled", DefaultValue = false, IsRequired = false)]
		public bool IsEnabled
		{
			get
			{
				return (bool)base["isEnabled"];
			}
			set
			{
				base["isEnabled"] = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00017C4C File Offset: 0x00015E4C
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00017C5E File Offset: 0x00015E5E
		[ConfigurationProperty("sync", IsRequired = false, DefaultValue = DataCacheLocalCacheInvalidationPolicy.TimeoutBased)]
		public DataCacheLocalCacheInvalidationPolicy SyncPolicy
		{
			get
			{
				return (DataCacheLocalCacheInvalidationPolicy)base["sync"];
			}
			set
			{
				base["sync"] = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00017C71 File Offset: 0x00015E71
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00017C83 File Offset: 0x00015E83
		[ConfigurationProperty("ttlValue", IsRequired = false, DefaultValue = 300)]
		public int DefaultTTL
		{
			get
			{
				return (int)base["ttlValue"];
			}
			set
			{
				base["ttlValue"] = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00017C96 File Offset: 0x00015E96
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x00017CA8 File Offset: 0x00015EA8
		[ConfigurationProperty("objectCount", IsRequired = false, DefaultValue = 10000L)]
		public long ObjectCount
		{
			get
			{
				return (long)base["objectCount"];
			}
			set
			{
				base["objectCount"] = value;
			}
		}

		// Token: 0x040003BF RID: 959
		internal const string SYNC_POLICY = "sync";

		// Token: 0x040003C0 RID: 960
		internal const string IS_ENABLED = "isEnabled";

		// Token: 0x040003C1 RID: 961
		internal const string DEFAULT_TTL = "ttlValue";

		// Token: 0x040003C2 RID: 962
		internal const string LOCAL_CACHE_COUNT = "objectCount";
	}
}
