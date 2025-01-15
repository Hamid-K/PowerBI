using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x02000452 RID: 1106
	[Serializable]
	public sealed class ClientCertificateConfiguration : ConfigurationClass
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x0007E0E4 File Offset: 0x0007C2E4
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x0007E0EC File Offset: 0x0007C2EC
		[ConfigurationProperty]
		public string ClientCertificateKey { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x0007E0F5 File Offset: 0x0007C2F5
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x0007E0FD File Offset: 0x0007C2FD
		[ConfigurationProperty]
		public bool VerifyServiceCertificateRevocation { get; set; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x0007E106 File Offset: 0x0007C306
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x0007E10E File Offset: 0x0007C30E
		[ConfigurationProperty]
		public bool VerifyServiceCertificateName { get; set; }
	}
}
