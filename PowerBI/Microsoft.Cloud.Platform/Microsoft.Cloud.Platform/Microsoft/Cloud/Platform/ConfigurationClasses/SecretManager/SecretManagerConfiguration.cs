using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.SecretManager
{
	// Token: 0x02000440 RID: 1088
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.None)]
	[Serializable]
	public sealed class SecretManagerConfiguration : ConfigurationClass
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0007D26A File Offset: 0x0007B46A
		// (set) Token: 0x060021BB RID: 8635 RVA: 0x0007D272 File Offset: 0x0007B472
		[ConfigurationProperty]
		public ConfigurationCollection<CertificateConfiguration> Certificates { get; set; }
	}
}
