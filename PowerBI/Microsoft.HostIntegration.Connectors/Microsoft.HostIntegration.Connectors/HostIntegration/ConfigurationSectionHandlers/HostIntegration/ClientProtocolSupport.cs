using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005C3 RID: 1475
	public class ClientProtocolSupport : ConfigurationElement
	{
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000AD485 File Offset: 0x000AB685
		// (set) Token: 0x06003349 RID: 13129 RVA: 0x000AD497 File Offset: 0x000AB697
		[Description("Enable IPv4")]
		[Category("General")]
		[ConfigurationProperty("ipv4Enabled", IsRequired = false, DefaultValue = "true")]
		public bool Ipv4Enabled
		{
			get
			{
				return (bool)base["ipv4Enabled"];
			}
			set
			{
				base["ipv4Enabled"] = value;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000AD4AA File Offset: 0x000AB6AA
		// (set) Token: 0x0600334B RID: 13131 RVA: 0x000AD4BC File Offset: 0x000AB6BC
		[Description("Enable IPv6")]
		[Category("General")]
		[ConfigurationProperty("ipv6Enabled", IsRequired = false, DefaultValue = "true")]
		public bool Ipv6Enabled
		{
			get
			{
				return (bool)base["ipv6Enabled"];
			}
			set
			{
				base["ipv6Enabled"] = value;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000AD4CF File Offset: 0x000AB6CF
		// (set) Token: 0x0600334D RID: 13133 RVA: 0x000AD4E1 File Offset: 0x000AB6E1
		[Description("Enable Named Pipes")]
		[Category("General")]
		[ConfigurationProperty("namedPipesEnabled", IsRequired = false, DefaultValue = "true")]
		public bool NamedPipesEnabled
		{
			get
			{
				return (bool)base["namedPipesEnabled"];
			}
			set
			{
				base["namedPipesEnabled"] = value;
			}
		}
	}
}
