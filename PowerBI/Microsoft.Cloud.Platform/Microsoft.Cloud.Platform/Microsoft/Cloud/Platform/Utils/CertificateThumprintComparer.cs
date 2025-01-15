using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001BB RID: 443
	public sealed class CertificateThumprintComparer : IEqualityComparer<X509Certificate2>
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x00027D83 File Offset: 0x00025F83
		public bool Equals(X509Certificate2 x, X509Certificate2 y)
		{
			return x.Thumbprint.Equals(y.Thumbprint, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00027D97 File Offset: 0x00025F97
		public int GetHashCode(X509Certificate2 obj)
		{
			return obj.Thumbprint.GetHashCode();
		}
	}
}
