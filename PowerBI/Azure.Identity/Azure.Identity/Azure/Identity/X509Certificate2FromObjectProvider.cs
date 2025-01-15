using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Identity
{
	// Token: 0x0200008F RID: 143
	internal class X509Certificate2FromObjectProvider : IX509Certificate2Provider
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000E3EB File Offset: 0x0000C5EB
		private X509Certificate2 Certificate { get; }

		// Token: 0x0600049F RID: 1183 RVA: 0x0000E3F3 File Offset: 0x0000C5F3
		public X509Certificate2FromObjectProvider(X509Certificate2 clientCertificate)
		{
			if (clientCertificate == null)
			{
				throw new ArgumentNullException("clientCertificate");
			}
			this.Certificate = clientCertificate;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000E411 File Offset: 0x0000C611
		public ValueTask<X509Certificate2> GetCertificateAsync(bool async, CancellationToken cancellationToken)
		{
			return new ValueTask<X509Certificate2>(this.Certificate);
		}
	}
}
