using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient
{
	// Token: 0x02000516 RID: 1302
	public class Queue : ConfigurationElement
	{
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x00097199 File Offset: 0x00095399
		// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x000971AB File Offset: 0x000953AB
		[Description("Alias of the Queue")]
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

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x000971B9 File Offset: 0x000953B9
		// (set) Token: 0x06002BF8 RID: 11256 RVA: 0x000971CB File Offset: 0x000953CB
		[Description("Alias of the Queue Manager defined in this configuration")]
		[Category("General")]
		[ConfigurationProperty("queueManagerAlias", IsRequired = true)]
		public string QueueManagerAlias
		{
			get
			{
				return (string)base["queueManagerAlias"];
			}
			set
			{
				base["queueManagerAlias"] = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06002BFA RID: 11258 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("Name of the WebSphere MQ Server Queue")]
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

		// Token: 0x06002BFB RID: 11259 RVA: 0x000971D9 File Offset: 0x000953D9
		public object GetElementKey()
		{
			return this.Alias;
		}
	}
}
