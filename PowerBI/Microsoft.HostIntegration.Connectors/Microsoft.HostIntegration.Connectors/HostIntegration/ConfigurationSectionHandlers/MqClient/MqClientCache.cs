using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient
{
	// Token: 0x02000512 RID: 1298
	public class MqClientCache : ConfigurationElement
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x00096A10 File Offset: 0x00094C10
		// (set) Token: 0x06002BE3 RID: 11235 RVA: 0x00096A22 File Offset: 0x00094C22
		[Description("An identifier that defines the actual name of the AppFabric Cache.")]
		[Category("General")]
		[ConfigurationProperty("cacheName", IsRequired = true, DefaultValue = "MqClientCache")]
		[DisplayName("Cache Name")]
		public string CacheName
		{
			get
			{
				return (string)base["cacheName"];
			}
			set
			{
				base["cacheName"] = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x00020600 File Offset: 0x0001E800
		// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x00020612 File Offset: 0x0001E812
		[Description("An identifier that defines the cached configuration information.")]
		[Category("General")]
		[ConfigurationProperty("key", IsRequired = true, DefaultValue = "MqClientCacheKey")]
		[DisplayName("Key")]
		public string Key
		{
			get
			{
				return (string)base["key"];
			}
			set
			{
				base["key"] = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x00096A30 File Offset: 0x00094C30
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x00096A42 File Offset: 0x00094C42
		[Description("An identifier that defines where in the cache the configuration object resides.")]
		[Category("General")]
		[ConfigurationProperty("region", IsRequired = true, DefaultValue = "HostIntegrationServerCacheRegionMqCache")]
		[DisplayName("Region")]
		public string Region
		{
			get
			{
				return (string)base["region"];
			}
			set
			{
				base["region"] = value;
			}
		}
	}
}
