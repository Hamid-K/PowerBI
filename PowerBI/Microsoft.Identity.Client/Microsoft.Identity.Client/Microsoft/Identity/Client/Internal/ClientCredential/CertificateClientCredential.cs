using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Identity.Client.Internal.ClientCredential
{
	// Token: 0x02000258 RID: 600
	internal class CertificateClientCredential : CertificateAndClaimsClientCredential
	{
		// Token: 0x0600181A RID: 6170 RVA: 0x000507A0 File Offset: 0x0004E9A0
		public CertificateClientCredential(X509Certificate2 certificate)
			: base(certificate, null, true)
		{
		}
	}
}
