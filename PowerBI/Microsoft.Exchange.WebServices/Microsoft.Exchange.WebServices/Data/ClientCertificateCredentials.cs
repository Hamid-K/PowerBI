using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001CE RID: 462
	public sealed class ClientCertificateCredentials : ExchangeCredentials
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x0003B730 File Offset: 0x0003A730
		public ClientCertificateCredentials(X509CertificateCollection clientCertificates)
		{
			EwsUtilities.ValidateParam(clientCertificates, "clientCertificates");
			this.clientCertificates = clientCertificates;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0003B74A File Offset: 0x0003A74A
		internal override void PrepareWebRequest(IEwsHttpWebRequest request)
		{
			request.ClientCertificates = this.ClientCertificates;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0003B758 File Offset: 0x0003A758
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.clientCertificates;
			}
		}

		// Token: 0x04000CD1 RID: 3281
		private X509CertificateCollection clientCertificates;
	}
}
