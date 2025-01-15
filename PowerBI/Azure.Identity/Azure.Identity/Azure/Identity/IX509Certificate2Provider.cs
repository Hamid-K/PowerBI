using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Identity
{
	// Token: 0x0200006A RID: 106
	internal interface IX509Certificate2Provider
	{
		// Token: 0x060003A4 RID: 932
		ValueTask<X509Certificate2> GetCertificateAsync(bool async, CancellationToken cancellationToken);
	}
}
