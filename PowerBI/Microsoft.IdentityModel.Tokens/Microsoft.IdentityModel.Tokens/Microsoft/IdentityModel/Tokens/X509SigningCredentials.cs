using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000196 RID: 406
	public class X509SigningCredentials : SigningCredentials
	{
		// Token: 0x06001247 RID: 4679 RVA: 0x000444DB File Offset: 0x000426DB
		public X509SigningCredentials(X509Certificate2 certificate)
			: base(certificate)
		{
			this.Certificate = certificate;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000444EB File Offset: 0x000426EB
		public X509SigningCredentials(X509Certificate2 certificate, string algorithm)
			: base(certificate, algorithm)
		{
			this.Certificate = certificate;
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x000444FC File Offset: 0x000426FC
		// (set) Token: 0x0600124A RID: 4682 RVA: 0x00044504 File Offset: 0x00042704
		public X509Certificate2 Certificate { get; private set; }
	}
}
