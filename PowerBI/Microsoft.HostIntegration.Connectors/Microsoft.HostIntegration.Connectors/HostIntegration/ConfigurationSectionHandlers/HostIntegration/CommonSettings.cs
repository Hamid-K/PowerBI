using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005B2 RID: 1458
	public class CommonSettings : Feature
	{
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x000AD07B File Offset: 0x000AB27B
		// (set) Token: 0x06003304 RID: 13060 RVA: 0x000AD08D File Offset: 0x000AB28D
		[Description("HIS Runtime Group Name")]
		[Category("General")]
		[ConfigurationProperty("runtimeGroupName", IsRequired = false, DefaultValue = "HIS Runtime")]
		public string RuntimeGroupName
		{
			get
			{
				return (string)base["runtimeGroupName"];
			}
			set
			{
				base["runtimeGroupName"] = value;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000AD09B File Offset: 0x000AB29B
		// (set) Token: 0x06003306 RID: 13062 RVA: 0x000AD0AD File Offset: 0x000AB2AD
		[Description("HIS Administrators Group Name")]
		[Category("General")]
		[ConfigurationProperty("administratorsGroupName", IsRequired = false, DefaultValue = "HIS Administrators")]
		public string AdministratorsGroupName
		{
			get
			{
				return (string)base["administratorsGroupName"];
			}
			set
			{
				base["administratorsGroupName"] = value;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x000AD0BB File Offset: 0x000AB2BB
		// (set) Token: 0x06003308 RID: 13064 RVA: 0x000AD0CD File Offset: 0x000AB2CD
		[Description("Turn on Telemetry and Error Reporting to help improve the quality, reliability and performance of Microsoft Host Integration Server 2020")]
		[Category("General")]
		[ConfigurationProperty("enableTelemetry", IsRequired = false, DefaultValue = "true")]
		public bool EnableTelemetry
		{
			get
			{
				return (bool)base["enableTelemetry"];
			}
			set
			{
				base["enableTelemetry"] = value;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x000AD0E0 File Offset: 0x000AB2E0
		// (set) Token: 0x0600330A RID: 13066 RVA: 0x000AD0F2 File Offset: 0x000AB2F2
		[Description("Use Microsoft Update when I check for updates")]
		[Category("General")]
		[ConfigurationProperty("enableMicrosoftUpdate", IsRequired = false, DefaultValue = "false")]
		public bool EnableMicrosoftUpdate
		{
			get
			{
				return (bool)base["enableMicrosoftUpdate"];
			}
			set
			{
				base["enableMicrosoftUpdate"] = value;
			}
		}
	}
}
