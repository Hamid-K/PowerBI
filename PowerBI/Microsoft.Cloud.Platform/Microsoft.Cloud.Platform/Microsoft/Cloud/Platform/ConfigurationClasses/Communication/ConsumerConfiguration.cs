using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x02000451 RID: 1105
	[Serializable]
	public sealed class ConsumerConfiguration : ConfigurationClass
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x0007E08F File Offset: 0x0007C28F
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x0007E097 File Offset: 0x0007C297
		[ConfigurationProperty]
		public EndpointConfiguration EndpointConfiguration { get; set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0007E0A0 File Offset: 0x0007C2A0
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x0007E0A8 File Offset: 0x0007C2A8
		[ConfigurationProperty]
		public ClientCertificateConfiguration Certificate { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x0007E0B1 File Offset: 0x0007C2B1
		// (set) Token: 0x06002258 RID: 8792 RVA: 0x0007E0B9 File Offset: 0x0007C2B9
		[ConfigurationProperty]
		public bool UseDoubleWrap { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x0007E0C2 File Offset: 0x0007C2C2
		// (set) Token: 0x0600225A RID: 8794 RVA: 0x0007E0CA File Offset: 0x0007C2CA
		[ConfigurationProperty]
		public bool AllowImpersonation { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x0007E0D3 File Offset: 0x0007C2D3
		// (set) Token: 0x0600225C RID: 8796 RVA: 0x0007E0DB File Offset: 0x0007C2DB
		[ConfigurationProperty]
		public bool TraceHttpResponseHeadersOnFaultException { get; set; }
	}
}
