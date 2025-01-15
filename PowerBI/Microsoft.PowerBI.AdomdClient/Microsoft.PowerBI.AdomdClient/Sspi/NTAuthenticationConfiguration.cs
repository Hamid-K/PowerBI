using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000111 RID: 273
	internal struct NTAuthenticationConfiguration
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x0003475D File Offset: 0x0003295D
		public NTAuthenticationConfiguration(bool isSChannel, ImpersonationLevel impersonationLevel, ProtectionLevel protectionLevel, string certificateThumbprint)
		{
			this.isSChannel = isSChannel;
			this.impersonationLevel = impersonationLevel;
			this.protectionLevel = protectionLevel;
			this.certificateThumbprint = certificateThumbprint;
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0003477C File Offset: 0x0003297C
		public bool IsSChannel
		{
			get
			{
				return this.isSChannel;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00034784 File Offset: 0x00032984
		public ImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.impersonationLevel;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0003478C File Offset: 0x0003298C
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00034794 File Offset: 0x00032994
		public string CertificateThumbprint
		{
			get
			{
				return this.certificateThumbprint;
			}
		}

		// Token: 0x040008F7 RID: 2295
		private readonly bool isSChannel;

		// Token: 0x040008F8 RID: 2296
		private readonly ImpersonationLevel impersonationLevel;

		// Token: 0x040008F9 RID: 2297
		private readonly ProtectionLevel protectionLevel;

		// Token: 0x040008FA RID: 2298
		private readonly string certificateThumbprint;
	}
}
