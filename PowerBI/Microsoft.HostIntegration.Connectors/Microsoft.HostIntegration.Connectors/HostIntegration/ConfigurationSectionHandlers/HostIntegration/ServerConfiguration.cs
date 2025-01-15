using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005D7 RID: 1495
	public sealed class ServerConfiguration : ConfigurationElement
	{
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060033F2 RID: 13298 RVA: 0x000ADC34 File Offset: 0x000ABE34
		// (set) Token: 0x060033F3 RID: 13299 RVA: 0x000ADC46 File Offset: 0x000ABE46
		[Description("Common Settings")]
		[Category("General")]
		[ConfigurationProperty("commonSettings", IsRequired = false)]
		public CommonSettings CommonSettings
		{
			get
			{
				return (CommonSettings)base["commonSettings"];
			}
			set
			{
				base["commonSettings"] = value;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060033F4 RID: 13300 RVA: 0x000ADC54 File Offset: 0x000ABE54
		// (set) Token: 0x060033F5 RID: 13301 RVA: 0x000ADC66 File Offset: 0x000ABE66
		[Description("Network Integration")]
		[Category("General")]
		[ConfigurationProperty("networkIntegration", IsRequired = false)]
		public NetworkIntegration NetworkIntegration
		{
			get
			{
				return (NetworkIntegration)base["networkIntegration"];
			}
			set
			{
				base["networkIntegration"] = value;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000ADC74 File Offset: 0x000ABE74
		// (set) Token: 0x060033F7 RID: 13303 RVA: 0x000ADC86 File Offset: 0x000ABE86
		[Description("Application Integration")]
		[Category("General")]
		[ConfigurationProperty("applicationIntegration", IsRequired = false)]
		public ApplicationIntegration ApplicationIntegration
		{
			get
			{
				return (ApplicationIntegration)base["applicationIntegration"];
			}
			set
			{
				base["applicationIntegration"] = value;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x000ADC94 File Offset: 0x000ABE94
		// (set) Token: 0x060033F9 RID: 13305 RVA: 0x000ADCA6 File Offset: 0x000ABEA6
		[Description("Data Integration")]
		[Category("General")]
		[ConfigurationProperty("dataIntegration", IsRequired = false)]
		public DataIntegration DataIntegration
		{
			get
			{
				return (DataIntegration)base["dataIntegration"];
			}
			set
			{
				base["dataIntegration"] = value;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x000ADCB4 File Offset: 0x000ABEB4
		// (set) Token: 0x060033FB RID: 13307 RVA: 0x000ADCC6 File Offset: 0x000ABEC6
		[Description("Session Integration")]
		[Category("General")]
		[ConfigurationProperty("sessionIntegration", IsRequired = false)]
		public SessionIntegration SessionIntegration
		{
			get
			{
				return (SessionIntegration)base["sessionIntegration"];
			}
			set
			{
				base["sessionIntegration"] = value;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x000ADCD4 File Offset: 0x000ABED4
		// (set) Token: 0x060033FD RID: 13309 RVA: 0x000ADCE6 File Offset: 0x000ABEE6
		[Description("Message Integration")]
		[Category("General")]
		[ConfigurationProperty("messageIntegration", IsRequired = false)]
		public MessageIntegration MessageIntegration
		{
			get
			{
				return (MessageIntegration)base["messageIntegration"];
			}
			set
			{
				base["messageIntegration"] = value;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x000ADCF4 File Offset: 0x000ABEF4
		// (set) Token: 0x060033FF RID: 13311 RVA: 0x000ADD06 File Offset: 0x000ABF06
		[Description("Visual Studio Integration")]
		[Category("General")]
		[ConfigurationProperty("vsIntegration", IsRequired = false)]
		public VsIntegration VsIntegration
		{
			get
			{
				return (VsIntegration)base["vsIntegration"];
			}
			set
			{
				base["vsIntegration"] = value;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x000ADD14 File Offset: 0x000ABF14
		// (set) Token: 0x06003401 RID: 13313 RVA: 0x000ADD26 File Offset: 0x000ABF26
		[Description("BizTalk Adapters and Pipelines")]
		[Category("General")]
		[ConfigurationProperty("bizTalkIntegration", IsRequired = false)]
		public BizTalkIntegration BizTalkIntegration
		{
			get
			{
				return (BizTalkIntegration)base["bizTalkIntegration"];
			}
			set
			{
				base["bizTalkIntegration"] = value;
			}
		}
	}
}
