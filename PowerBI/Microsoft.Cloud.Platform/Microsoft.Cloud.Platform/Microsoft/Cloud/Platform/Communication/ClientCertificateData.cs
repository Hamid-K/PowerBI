using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000493 RID: 1171
	public class ClientCertificateData
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000814AC File Offset: 0x0007F6AC
		// (set) Token: 0x0600240A RID: 9226 RVA: 0x000814B4 File Offset: 0x0007F6B4
		public X509Certificate2 ClientCertificate { get; set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000814BD File Offset: 0x0007F6BD
		// (set) Token: 0x0600240C RID: 9228 RVA: 0x000814C5 File Offset: 0x0007F6C5
		public string ClientCertificateKey { get; private set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x000814CE File Offset: 0x0007F6CE
		// (set) Token: 0x0600240E RID: 9230 RVA: 0x000814D6 File Offset: 0x0007F6D6
		public CertificateDataOptions CertificateOptions { get; private set; }

		// Token: 0x0600240F RID: 9231 RVA: 0x000814DF File Offset: 0x0007F6DF
		public ClientCertificateData(X509Certificate2 clientCertificate, string clientCertificateKey, CertificateDataOptions certificateDataOptions)
		{
			this.ClientCertificate = clientCertificate;
			this.ClientCertificateKey = clientCertificateKey;
			this.CertificateOptions = certificateDataOptions;
		}
	}
}
