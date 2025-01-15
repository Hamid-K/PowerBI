using System;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x02000106 RID: 262
	internal struct NTAuthenticationConfiguration
	{
		// Token: 0x06000FDA RID: 4058 RVA: 0x00037391 File Offset: 0x00035591
		public NTAuthenticationConfiguration(bool isSChannel, ImpersonationLevel impersonationLevel, ProtectionLevel protectionLevel, string certificateThumbprint)
		{
			this.isSChannel = isSChannel;
			this.impersonationLevel = impersonationLevel;
			this.protectionLevel = protectionLevel;
			this.certificateThumbprint = certificateThumbprint;
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x000373B0 File Offset: 0x000355B0
		public bool IsSChannel
		{
			get
			{
				return this.isSChannel;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x000373B8 File Offset: 0x000355B8
		public ImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.impersonationLevel;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x000373C0 File Offset: 0x000355C0
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x000373C8 File Offset: 0x000355C8
		public string CertificateThumbprint
		{
			get
			{
				return this.certificateThumbprint;
			}
		}

		// Token: 0x040008BD RID: 2237
		private readonly bool isSChannel;

		// Token: 0x040008BE RID: 2238
		private readonly ImpersonationLevel impersonationLevel;

		// Token: 0x040008BF RID: 2239
		private readonly ProtectionLevel protectionLevel;

		// Token: 0x040008C0 RID: 2240
		private readonly string certificateThumbprint;
	}
}
