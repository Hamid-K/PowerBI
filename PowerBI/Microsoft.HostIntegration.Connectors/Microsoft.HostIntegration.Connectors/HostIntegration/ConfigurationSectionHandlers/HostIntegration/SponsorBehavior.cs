using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005D5 RID: 1493
	public class SponsorBehavior : ConfigurationElement
	{
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000ADBA0 File Offset: 0x000ABDA0
		// (set) Token: 0x060033E3 RID: 13283 RVA: 0x000ADBB2 File Offset: 0x000ABDB2
		[Description("Enable Dynamic Updates")]
		[Category("General")]
		[ConfigurationProperty("enableDynamicUpdates", IsRequired = false, DefaultValue = "true")]
		public bool EnableDynamicUpdates
		{
			get
			{
				return (bool)base["enableDynamicUpdates"];
			}
			set
			{
				base["enableDynamicUpdates"] = value;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x000ADBC5 File Offset: 0x000ABDC5
		// (set) Token: 0x060033E5 RID: 13285 RVA: 0x000ADBD7 File Offset: 0x000ABDD7
		[Description("Randomly select Sponsor")]
		[Category("General")]
		[ConfigurationProperty("useRandomSelection", IsRequired = false, DefaultValue = "false")]
		public bool UseRandomSelection
		{
			get
			{
				return (bool)base["useRandomSelection"];
			}
			set
			{
				base["useRandomSelection"] = value;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x000ADBEA File Offset: 0x000ABDEA
		// (set) Token: 0x060033E7 RID: 13287 RVA: 0x000ADBFC File Offset: 0x000ABDFC
		[Description("Accept Backup Sponsors")]
		[Category("General")]
		[ConfigurationProperty("acceptBackupSponsors", IsRequired = false, DefaultValue = "false")]
		public bool AcceptBackupSponsors
		{
			get
			{
				return (bool)base["acceptBackupSponsors"];
			}
			set
			{
				base["acceptBackupSponsors"] = value;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x00019FAC File Offset: 0x000181AC
		// (set) Token: 0x060033E9 RID: 13289 RVA: 0x00019FBE File Offset: 0x000181BE
		[Description("Sponsor Connection Timeout")]
		[Category("General")]
		[ConfigurationProperty("timeout", IsRequired = false, DefaultValue = "0")]
		[IntegerValidator(MinValue = 0, MaxValue = 10000)]
		public int Timeout
		{
			get
			{
				return (int)base["timeout"];
			}
			set
			{
				base["timeout"] = value;
			}
		}
	}
}
