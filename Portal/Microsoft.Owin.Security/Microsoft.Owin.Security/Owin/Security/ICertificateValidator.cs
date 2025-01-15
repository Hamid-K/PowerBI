using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Owin.Security
{
	// Token: 0x0200000C RID: 12
	public interface ICertificateValidator
	{
		// Token: 0x0600001B RID: 27
		bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);
	}
}
