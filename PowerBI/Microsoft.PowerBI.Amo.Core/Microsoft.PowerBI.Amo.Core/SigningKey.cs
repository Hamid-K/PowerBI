using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200004E RID: 78
	[Obsolete("Deprecated!")]
	public abstract class SigningKey
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00011E77 File Offset: 0x00010077
		public static SigningKey CreateFromCertificate(X509Certificate2 certificate)
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00011E83 File Offset: 0x00010083
		public X509Certificate2 Certificate
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00011E8F File Offset: 0x0001008F
		public byte[] CertificateThumbprint
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x0600036A RID: 874
		public abstract byte[] Sign(byte[] data, SignatureHashAlgorithm hashAlgorithm);
	}
}
