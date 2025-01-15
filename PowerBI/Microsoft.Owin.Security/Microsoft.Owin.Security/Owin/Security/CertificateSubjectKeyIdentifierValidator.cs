using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000007 RID: 7
	public class CertificateSubjectKeyIdentifierValidator : ICertificateValidator
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000216A File Offset: 0x0000036A
		public CertificateSubjectKeyIdentifierValidator(IEnumerable<string> validSubjectKeyIdentifiers)
		{
			if (validSubjectKeyIdentifiers == null)
			{
				throw new ArgumentNullException("validSubjectKeyIdentifiers");
			}
			this._validSubjectKeyIdentifiers = new HashSet<string>(validSubjectKeyIdentifiers, StringComparer.OrdinalIgnoreCase);
			if (this._validSubjectKeyIdentifiers.Count == 0)
			{
				throw new ArgumentOutOfRangeException("validSubjectKeyIdentifiers");
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021AC File Offset: 0x000003AC
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
				string subjectKeyIdentifier = CertificateSubjectKeyIdentifierValidator.GetSubjectKeyIdentifier(chainElement.Certificate);
				if (!string.IsNullOrWhiteSpace(subjectKeyIdentifier) && this._validSubjectKeyIdentifiers.Contains(subjectKeyIdentifier))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002220 File Offset: 0x00000420
		private static string GetSubjectKeyIdentifier(X509Certificate2 certificate)
		{
			X509SubjectKeyIdentifierExtension extension = certificate.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
			if (extension != null)
			{
				return extension.SubjectKeyIdentifier;
			}
			return null;
		}

		// Token: 0x0400000B RID: 11
		private readonly HashSet<string> _validSubjectKeyIdentifiers;
	}
}
