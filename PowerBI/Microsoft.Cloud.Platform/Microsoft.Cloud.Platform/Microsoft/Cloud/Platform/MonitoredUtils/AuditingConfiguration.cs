using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000114 RID: 276
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.None)]
	[Serializable]
	public sealed class AuditingConfiguration : EncryptedConfigurationClass
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001A05E File Offset: 0x0001825E
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001A066 File Offset: 0x00018266
		[ConfigurationProperty]
		public bool AuditSwitch { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001A06F File Offset: 0x0001826F
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0001A077 File Offset: 0x00018277
		[ConfigurationProperty]
		public string Environment { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001A080 File Offset: 0x00018280
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0001A088 File Offset: 0x00018288
		[ConfigurationProperty]
		public string OfficeNrtEndpoint { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001A091 File Offset: 0x00018291
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0001A099 File Offset: 0x00018299
		[ConfigurationProperty]
		public string LogFileFolder { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001A0A2 File Offset: 0x000182A2
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0001A0AA File Offset: 0x000182AA
		[ConfigurationProperty]
		public int LogFileGenerationIntervalInMinutes { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001A0B3 File Offset: 0x000182B3
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0001A0BB File Offset: 0x000182BB
		[ConfigurationProperty]
		public int OfficeUploadBatchSize { get; set; }
	}
}
