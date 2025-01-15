using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005D8 RID: 1496
	public sealed class ClientConfiguration : ConfigurationElement
	{
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000ADC34 File Offset: 0x000ABE34
		// (set) Token: 0x06003404 RID: 13316 RVA: 0x000ADC46 File Offset: 0x000ABE46
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

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000ADD34 File Offset: 0x000ABF34
		// (set) Token: 0x06003406 RID: 13318 RVA: 0x000ADC66 File Offset: 0x000ABE66
		[Description("Network Integration")]
		[Category("General")]
		[ConfigurationProperty("networkIntegration", IsRequired = false)]
		public ClientNetworkIntegration NetworkIntegration
		{
			get
			{
				return (ClientNetworkIntegration)base["networkIntegration"];
			}
			set
			{
				base["networkIntegration"] = value;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x000ADD46 File Offset: 0x000ABF46
		// (set) Token: 0x06003408 RID: 13320 RVA: 0x000ADCA6 File Offset: 0x000ABEA6
		[Description("Data Integration")]
		[Category("General")]
		[ConfigurationProperty("dataIntegration", IsRequired = false)]
		public ClientDataIntegration DataIntegration
		{
			get
			{
				return (ClientDataIntegration)base["dataIntegration"];
			}
			set
			{
				base["dataIntegration"] = value;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000ADD58 File Offset: 0x000ABF58
		// (set) Token: 0x0600340A RID: 13322 RVA: 0x000ADD6A File Offset: 0x000ABF6A
		[Description("Print Server")]
		[Category("General")]
		[ConfigurationProperty("clientPrintServer", IsRequired = false)]
		public ClientPrintServer ClientPrintServer
		{
			get
			{
				return (ClientPrintServer)base["clientPrintServer"];
			}
			set
			{
				base["clientPrintServer"] = value;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000ADCF4 File Offset: 0x000ABEF4
		// (set) Token: 0x0600340C RID: 13324 RVA: 0x000ADD06 File Offset: 0x000ABF06
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
	}
}
