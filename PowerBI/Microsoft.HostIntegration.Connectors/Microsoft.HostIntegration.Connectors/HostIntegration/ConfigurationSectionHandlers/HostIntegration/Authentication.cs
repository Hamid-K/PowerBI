using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005D2 RID: 1490
	public class Authentication : ConfigurationElement
	{
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000ADA5D File Offset: 0x000ABC5D
		// (set) Token: 0x060033CF RID: 13263 RVA: 0x000ADA6F File Offset: 0x000ABC6F
		[Description("Run SNABase as an Application")]
		[Category("General")]
		[ConfigurationProperty("snaBaseAsApplication", IsRequired = false, DefaultValue = "false")]
		public bool SnaBaseAsApplication
		{
			get
			{
				return (bool)base["snaBaseAsApplication"];
			}
			set
			{
				base["snaBaseAsApplication"] = value;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x000ADA82 File Offset: 0x000ABC82
		// (set) Token: 0x060033D1 RID: 13265 RVA: 0x000ADA94 File Offset: 0x000ABC94
		[Description("Authenticate with Logged-on User")]
		[Category("General")]
		[ConfigurationProperty("useLoggedOnUser", IsRequired = false, DefaultValue = "true")]
		public bool UseLoggedOnUser
		{
			get
			{
				return (bool)base["useLoggedOnUser"];
			}
			set
			{
				base["useLoggedOnUser"] = value;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060033D2 RID: 13266 RVA: 0x000ADAA7 File Offset: 0x000ABCA7
		// (set) Token: 0x060033D3 RID: 13267 RVA: 0x0001DB89 File Offset: 0x0001BD89
		[Description("Authentication Account")]
		[Category("General")]
		[ConfigurationProperty("account", IsRequired = false)]
		public ServiceCredential Account
		{
			get
			{
				return (ServiceCredential)base["account"];
			}
			set
			{
				base["account"] = value;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060033D4 RID: 13268 RVA: 0x000ADAB9 File Offset: 0x000ABCB9
		// (set) Token: 0x060033D5 RID: 13269 RVA: 0x000ADACB File Offset: 0x000ABCCB
		[Description("Use Per-User Settings")]
		[Category("General")]
		[ConfigurationProperty("usePerUserSettings", IsRequired = false, DefaultValue = "false")]
		public bool UsePerUserSettings
		{
			get
			{
				return (bool)base["usePerUserSettings"];
			}
			set
			{
				base["usePerUserSettings"] = value;
			}
		}
	}
}
