using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000111 RID: 273
	internal struct NTAuthenticationConfiguration
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x00034A8D File Offset: 0x00032C8D
		public NTAuthenticationConfiguration(bool isSChannel, ImpersonationLevel impersonationLevel, ProtectionLevel protectionLevel, string certificateThumbprint)
		{
			this.isSChannel = isSChannel;
			this.impersonationLevel = impersonationLevel;
			this.protectionLevel = protectionLevel;
			this.certificateThumbprint = certificateThumbprint;
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00034AAC File Offset: 0x00032CAC
		public bool IsSChannel
		{
			get
			{
				return this.isSChannel;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00034AB4 File Offset: 0x00032CB4
		public ImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.impersonationLevel;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00034ABC File Offset: 0x00032CBC
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x00034AC4 File Offset: 0x00032CC4
		public string CertificateThumbprint
		{
			get
			{
				return this.certificateThumbprint;
			}
		}

		// Token: 0x04000904 RID: 2308
		private readonly bool isSChannel;

		// Token: 0x04000905 RID: 2309
		private readonly ImpersonationLevel impersonationLevel;

		// Token: 0x04000906 RID: 2310
		private readonly ProtectionLevel protectionLevel;

		// Token: 0x04000907 RID: 2311
		private readonly string certificateThumbprint;
	}
}
