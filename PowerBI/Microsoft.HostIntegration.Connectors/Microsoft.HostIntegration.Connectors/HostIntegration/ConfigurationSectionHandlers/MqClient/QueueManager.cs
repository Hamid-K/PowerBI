using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient
{
	// Token: 0x02000519 RID: 1305
	public class QueueManager : ConfigurationElement
	{
		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x00097199 File Offset: 0x00095399
		// (set) Token: 0x06002C18 RID: 11288 RVA: 0x000971AB File Offset: 0x000953AB
		[Description("Alias of the Queue Manager")]
		[Category("General")]
		[ConfigurationProperty("alias", IsRequired = true)]
		public string Alias
		{
			get
			{
				return (string)base["alias"];
			}
			set
			{
				base["alias"] = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("Name of the WebSphere MQ Server Queue Manager")]
		[Category("General")]
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000972F9 File Offset: 0x000954F9
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x0009730B File Offset: 0x0009550B
		[Description("Name of the Channel used to connect to the WebSphere MQ Server")]
		[Category("General")]
		[ConfigurationProperty("channel", IsRequired = true)]
		public string Channel
		{
			get
			{
				return (string)base["channel"];
			}
			set
			{
				base["channel"] = value;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x00097319 File Offset: 0x00095519
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x0009732B File Offset: 0x0009552B
		[Description("IP Address/Host Name of the WebSphere MQ Server")]
		[Category("General")]
		[ConfigurationProperty("host", IsRequired = true)]
		public string Host
		{
			get
			{
				return (string)base["host"];
			}
			set
			{
				base["host"] = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x00097339 File Offset: 0x00095539
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x0009734B File Offset: 0x0009554B
		[Description("The TCP Port used by the WebSphere MQ Server")]
		[Category("General")]
		[ConfigurationProperty("port", IsRequired = true, DefaultValue = 1414)]
		[DisplayName("Port")]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x06002C22 RID: 11298 RVA: 0x00097370 File Offset: 0x00095570
		[Description("Does the WebSphere MQ Server use SSL on the Host/Port specified")]
		[Category("General")]
		[ConfigurationProperty("useSsl", IsRequired = false, DefaultValue = false)]
		[DisplayName("UseSsl")]
		public bool UseSsl
		{
			get
			{
				return (bool)base["useSsl"];
			}
			set
			{
				base["useSsl"] = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x00097383 File Offset: 0x00095583
		// (set) Token: 0x06002C24 RID: 11300 RVA: 0x00097395 File Offset: 0x00095595
		[Description("User Id to use to perform MQ Authentication")]
		[Category("General")]
		[ConfigurationProperty("connectAs", IsRequired = false)]
		public string ConnectAs
		{
			get
			{
				return (string)base["connectAs"];
			}
			set
			{
				base["connectAs"] = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x000973A3 File Offset: 0x000955A3
		// (set) Token: 0x06002C26 RID: 11302 RVA: 0x000973B5 File Offset: 0x000955B5
		[Description("Name prefix to use for dynamic Queues created on this Queue Manager")]
		[Category("General")]
		[ConfigurationProperty("dynamicQueueNamePrefix", IsRequired = false, DefaultValue = "AMQ.*")]
		public string DynamicQueueNamePrefix
		{
			get
			{
				return (string)base["dynamicQueueNamePrefix"];
			}
			set
			{
				base["dynamicQueueNamePrefix"] = value;
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000973C3 File Offset: 0x000955C3
		public object GetElementKey()
		{
			return this.Alias;
		}
	}
}
