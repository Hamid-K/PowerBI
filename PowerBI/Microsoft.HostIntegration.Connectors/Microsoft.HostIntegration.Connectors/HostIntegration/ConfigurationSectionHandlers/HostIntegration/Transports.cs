using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005D6 RID: 1494
	public class Transports : ConfigurationElement
	{
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x000AD485 File Offset: 0x000AB685
		// (set) Token: 0x060033EC RID: 13292 RVA: 0x000AD497 File Offset: 0x000AB697
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

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000AD4AA File Offset: 0x000AB6AA
		// (set) Token: 0x060033EE RID: 13294 RVA: 0x000AD4BC File Offset: 0x000AB6BC
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

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x000ADC0F File Offset: 0x000ABE0F
		// (set) Token: 0x060033F0 RID: 13296 RVA: 0x000ADC21 File Offset: 0x000ABE21
		[Description("Try IPv4 First")]
		[Category("General")]
		[ConfigurationProperty("tryIpv4First", IsRequired = false, DefaultValue = "true")]
		public bool TryIpv4First
		{
			get
			{
				return (bool)base["tryIpv4First"];
			}
			set
			{
				base["tryIpv4First"] = value;
			}
		}
	}
}
