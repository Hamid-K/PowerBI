using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000054 RID: 84
	[Obsolete("Deprecated!")]
	public sealed class TokenSigner
	{
		// Token: 0x06000391 RID: 913 RVA: 0x00014FBB File Offset: 0x000131BB
		public TokenSigner(X509Certificate2 certificate, SignatureHashAlgorithm hashAlgorithm)
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00014FCD File Offset: 0x000131CD
		public TokenSigner(SigningKey key, SignatureHashAlgorithm hashAlgorithm)
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00014FDF File Offset: 0x000131DF
		public byte[] CertificateThumbprint
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00014FEB File Offset: 0x000131EB
		public string DigestMethod
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00014FF7 File Offset: 0x000131F7
		public byte[] Sign(byte[] data)
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}
	}
}
