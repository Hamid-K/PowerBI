using System;
using System.Configuration;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000D0 RID: 208
	internal class SqlAuthenticationProviderConfigurationSection : ConfigurationSection
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0002F6D6 File Offset: 0x0002D8D6
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base["providers"];
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0002F6E8 File Offset: 0x0002D8E8
		[ConfigurationProperty("initializerType")]
		public string InitializerType
		{
			get
			{
				return base["initializerType"] as string;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0002F6FA File Offset: 0x0002D8FA
		[ConfigurationProperty("applicationClientId", IsRequired = false)]
		public string ApplicationClientId
		{
			get
			{
				return base["applicationClientId"] as string;
			}
		}

		// Token: 0x0400065C RID: 1628
		public const string Name = "SqlAuthenticationProviders";
	}
}
