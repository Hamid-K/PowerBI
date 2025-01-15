using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000025 RID: 37
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class DatabaseConfiguration : EncryptedConfigurationClass
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000044ED File Offset: 0x000026ED
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000044F5 File Offset: 0x000026F5
		[ConfigurationProperty]
		public ConfigurationCollection<DatabaseSpecificationConfiguration> Specifications { get; set; }
	}
}
