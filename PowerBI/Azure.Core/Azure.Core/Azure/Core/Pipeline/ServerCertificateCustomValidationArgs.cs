using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A2 RID: 162
	[NullableContext(2)]
	[Nullable(0)]
	public class ServerCertificateCustomValidationArgs
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		public X509Certificate2 Certificate { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		public X509Chain CertificateAuthorityChain { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		public SslPolicyErrors SslPolicyErrors { get; }

		// Token: 0x06000524 RID: 1316 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		public ServerCertificateCustomValidationArgs(X509Certificate2 certificate, X509Chain certificateAuthorityChain, SslPolicyErrors sslPolicyErrors)
		{
			this.Certificate = certificate;
			this.CertificateAuthorityChain = certificateAuthorityChain;
			this.SslPolicyErrors = sslPolicyErrors;
		}
	}
}
