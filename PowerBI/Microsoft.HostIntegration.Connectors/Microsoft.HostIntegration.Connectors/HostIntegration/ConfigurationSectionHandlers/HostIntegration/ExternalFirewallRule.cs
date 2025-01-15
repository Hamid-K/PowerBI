using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005CE RID: 1486
	public class ExternalFirewallRule : ConfigurationElement
	{
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x060033B3 RID: 13235 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("Name")]
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

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000AD90E File Offset: 0x000ABB0E
		// (set) Token: 0x060033B5 RID: 13237 RVA: 0x000AD920 File Offset: 0x000ABB20
		[Description("Application")]
		[Category("General")]
		[ConfigurationProperty("application", IsRequired = true)]
		public string Application
		{
			get
			{
				return (string)base["application"];
			}
			set
			{
				base["application"] = value;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060033B6 RID: 13238 RVA: 0x000978D5 File Offset: 0x00095AD5
		// (set) Token: 0x060033B7 RID: 13239 RVA: 0x000978E7 File Offset: 0x00095AE7
		[Description("Description")]
		[Category("General")]
		[ConfigurationProperty("description", IsRequired = true)]
		public string Description
		{
			get
			{
				return (string)base["description"];
			}
			set
			{
				base["description"] = value;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x000AD92E File Offset: 0x000ABB2E
		// (set) Token: 0x060033B9 RID: 13241 RVA: 0x000AD940 File Offset: 0x000ABB40
		[Description("Direction")]
		[Category("General")]
		[ConfigurationProperty("direction", IsRequired = true)]
		public FirewallDirection Direction
		{
			get
			{
				return (FirewallDirection)base["direction"];
			}
			set
			{
				base["direction"] = value;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x000AD953 File Offset: 0x000ABB53
		// (set) Token: 0x060033BB RID: 13243 RVA: 0x000AD965 File Offset: 0x000ABB65
		[Description("Profile")]
		[Category("General")]
		[ConfigurationProperty("profile", IsRequired = true)]
		public FirewallProfile Profile
		{
			get
			{
				return (FirewallProfile)base["profile"];
			}
			set
			{
				base["profile"] = value;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x000AD978 File Offset: 0x000ABB78
		// (set) Token: 0x060033BD RID: 13245 RVA: 0x000AD98A File Offset: 0x000ABB8A
		[Description("Protocol")]
		[Category("General")]
		[ConfigurationProperty("protocol", IsRequired = true)]
		public FirewallProtocol Protocol
		{
			get
			{
				return (FirewallProtocol)base["protocol"];
			}
			set
			{
				base["protocol"] = value;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x000AD99D File Offset: 0x000ABB9D
		// (set) Token: 0x060033BF RID: 13247 RVA: 0x000AD9AF File Offset: 0x000ABBAF
		[Description("Service")]
		[Category("General")]
		[ConfigurationProperty("service", IsRequired = true)]
		public string Service
		{
			get
			{
				return (string)base["service"];
			}
			set
			{
				base["service"] = value;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000AD9BD File Offset: 0x000ABBBD
		// (set) Token: 0x060033C1 RID: 13249 RVA: 0x000AD9CF File Offset: 0x000ABBCF
		[Description("LocalPorts")]
		[Category("General")]
		[ConfigurationProperty("localPorts", IsRequired = false)]
		public string LocalPorts
		{
			get
			{
				return (string)base["localPorts"];
			}
			set
			{
				base["localPorts"] = value;
			}
		}
	}
}
