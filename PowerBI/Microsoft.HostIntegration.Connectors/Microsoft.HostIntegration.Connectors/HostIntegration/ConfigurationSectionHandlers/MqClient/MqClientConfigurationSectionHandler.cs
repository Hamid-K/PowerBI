using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient
{
	// Token: 0x02000518 RID: 1304
	public class MqClientConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x06002C09 RID: 11273 RVA: 0x00097253 File Offset: 0x00095453
		public static MqClientConfigurationSectionHandler LoadFromCache(string cacheName, string fileName, string region)
		{
			return HisConfigurationSectionHandler.LoadFromCache(cacheName, fileName, region, "hostIntegration.mqClient") as MqClientConfigurationSectionHandler;
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x06002C0B RID: 11275 RVA: 0x00096DFD File Offset: 0x00094FFD
		[Description("XML Namespace of the schema associated with the Microsoft Client for MQ configuration file.")]
		[Category("General")]
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/MqClient/2013")]
		public string XmlNs
		{
			get
			{
				return (string)base["xmlns"];
			}
			set
			{
				base["xmlns"] = value;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x00097267 File Offset: 0x00095467
		// (set) Token: 0x06002C0D RID: 11277 RVA: 0x00097279 File Offset: 0x00095479
		[Description("All of the Queue Manager Aliases")]
		[Category("General")]
		[ConfigurationProperty("queueManagers", IsRequired = false)]
		public QueueManagerCollection QueueManagers
		{
			get
			{
				return (QueueManagerCollection)base["queueManagers"];
			}
			set
			{
				base["queueManagers"] = value;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x00097287 File Offset: 0x00095487
		// (set) Token: 0x06002C0F RID: 11279 RVA: 0x00097299 File Offset: 0x00095499
		[Description("All of the Queue Aliases")]
		[Category("General")]
		[ConfigurationProperty("queues", IsRequired = false)]
		public QueueCollection Queues
		{
			get
			{
				return (QueueCollection)base["queues"];
			}
			set
			{
				base["queues"] = value;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000972A7 File Offset: 0x000954A7
		// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000972B9 File Offset: 0x000954B9
		[Description("Pooling behavior")]
		[Category("General")]
		[ConfigurationProperty("pooling", IsRequired = false)]
		public Pooling Pooling
		{
			get
			{
				return (Pooling)base["pooling"];
			}
			set
			{
				base["pooling"] = value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000972C7 File Offset: 0x000954C7
		// (set) Token: 0x06002C13 RID: 11283 RVA: 0x000972D9 File Offset: 0x000954D9
		[Description("The Configuration Read Order provides a means of defining the order in which the Microsoft Client for MQ Runtime obtains configuration information. The values First, Second and Unused can be specified. At least one property must be set to First. Property values cannot be duplicated. The Microsoft Client for MQ Runtime attempts to resolve configuration information in the order specified in the AppConfig and Cache properties.")]
		[Category("General")]
		[ConfigurationProperty("readOrder", IsRequired = true)]
		[DisplayName("Read Order")]
		public ReadOrder ReadOrder
		{
			get
			{
				return (ReadOrder)base["readOrder"];
			}
			set
			{
				base["readOrder"] = value;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000972E7 File Offset: 0x000954E7
		// (set) Token: 0x06002C15 RID: 11285 RVA: 0x00096FEF File Offset: 0x000951EF
		[Description("The Cache can be used to store configuration information such that any process on any machine can gain access to a single instance of the Microsoft Client for MQ Configuration information. HostName, Port and CacheName provide the details to allow the Microsoft Client for MQ Runtime to obtain the configuration information. Before the Microsoft Client for MQ Runtime can use the Cache, an Administrator must define the Cache using the AppFabric utilities.")]
		[Category("General")]
		[ConfigurationProperty("cache", IsRequired = false)]
		[DisplayName("Cache")]
		public MqClientCache Cache
		{
			get
			{
				return (MqClientCache)base["cache"];
			}
			set
			{
				base["cache"] = value;
			}
		}
	}
}
