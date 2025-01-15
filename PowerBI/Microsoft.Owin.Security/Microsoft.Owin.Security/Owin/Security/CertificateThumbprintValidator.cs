using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000009 RID: 9
	public class CertificateThumbprintValidator : ICertificateValidator
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000240C File Offset: 0x0000060C
		public CertificateThumbprintValidator(IEnumerable<string> validThumbprints)
		{
			if (validThumbprints == null)
			{
				throw new ArgumentNullException("validThumbprints");
			}
			this._validCertificateThumbprints = new HashSet<string>(validThumbprints, StringComparer.OrdinalIgnoreCase);
			if (this._validCertificateThumbprints.Count == 0)
			{
				throw new ArgumentOutOfRangeException("validThumbprints");
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000244C File Offset: 0x0000064C
		public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors != SslPolicyErrors.None)
			{
				return false;
			}
			if (chain == null)
			{
				throw new ArgumentNullException("chain");
			}
			if (chain.ChainElements.Count < 2)
			{
				return false;
			}
			foreach (X509ChainElement chainElement in chain.ChainElements)
			{
				string thumbprintToCheck = chainElement.Certificate.Thumbprint;
				if (thumbprintToCheck != null && this._validCertificateThumbprints.Contains(thumbprintToCheck))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400000E RID: 14
		private readonly HashSet<string> _validCertificateThumbprints;
	}
}
