using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005BE RID: 1470
	public class Feature : ConfigurationElement
	{
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x00015FAF File Offset: 0x000141AF
		// (set) Token: 0x06003329 RID: 13097 RVA: 0x00015FC1 File Offset: 0x000141C1
		[Description("Is the Feature Enabled")]
		[Category("General")]
		[ConfigurationProperty("isEnabled", IsRequired = false, DefaultValue = "false")]
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

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000AD266 File Offset: 0x000AB466
		// (set) Token: 0x0600332B RID: 13099 RVA: 0x000AD278 File Offset: 0x000AB478
		[Description("External Services, defined by the SNA Manager, TIC Tool or other")]
		[Category("General")]
		[ConfigurationProperty("externalServices", IsRequired = false)]
		public ExternalServiceCollection ExternalServices
		{
			get
			{
				return (ExternalServiceCollection)base["externalServices"];
			}
			set
			{
				base["externalServices"] = value;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000AD286 File Offset: 0x000AB486
		// (set) Token: 0x0600332D RID: 13101 RVA: 0x000AD298 File Offset: 0x000AB498
		[Description("Registry Settings associated with the Feature (different to the defaults)")]
		[Category("General")]
		[ConfigurationProperty("extraRegistrySettings", IsRequired = false)]
		public ExtraRegistrySettingCollection ExtraRegistrySettings
		{
			get
			{
				return (ExtraRegistrySettingCollection)base["extraRegistrySettings"];
			}
			set
			{
				base["extraRegistrySettings"] = value;
			}
		}
	}
}
