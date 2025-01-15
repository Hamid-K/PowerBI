using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.SecretManager
{
	// Token: 0x02000441 RID: 1089
	[Serializable]
	public sealed class CertificateConfiguration : ConfigurationClass
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x0007D27B File Offset: 0x0007B47B
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x0007D283 File Offset: 0x0007B483
		[ConfigurationProperty]
		public string CertificateProperty { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0007D28C File Offset: 0x0007B48C
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x0007D294 File Offset: 0x0007B494
		[ConfigurationProperty]
		public string CertificateKey { get; set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0007D29D File Offset: 0x0007B49D
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x0007D2A5 File Offset: 0x0007B4A5
		[ConfigurationProperty]
		public int PreExpirationAlertPeriod { get; set; }
	}
}
