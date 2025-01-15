using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000194 RID: 404
	public class X509EncryptingCredentials : EncryptingCredentials
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x00044207 File Offset: 0x00042407
		public X509EncryptingCredentials(X509Certificate2 certificate)
			: this(certificate, "http://www.w3.org/2001/04/xmlenc#rsa-oaep", "A128CBC-HS256")
		{
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004421A File Offset: 0x0004241A
		public X509EncryptingCredentials(X509Certificate2 certificate, string keyWrapAlgorithm, string dataEncryptionAlgorithm)
			: base(certificate, keyWrapAlgorithm, dataEncryptionAlgorithm)
		{
			this.Certificate = certificate;
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004422C File Offset: 0x0004242C
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x00044234 File Offset: 0x00042434
		public X509Certificate2 Certificate { get; private set; }
	}
}
