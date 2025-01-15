using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001BC RID: 444
	public sealed class CertificateSubjectNameComparer : IEqualityComparer<X509Certificate2>
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x00027DA4 File Offset: 0x00025FA4
		public bool Equals(X509Certificate2 x, X509Certificate2 y)
		{
			return CertificateUtilities.GetTrimmedSubjectName(x.SubjectName.Name).Equals(CertificateUtilities.GetTrimmedSubjectName(y.SubjectName.Name), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00027DCC File Offset: 0x00025FCC
		public int GetHashCode(X509Certificate2 obj)
		{
			return CertificateUtilities.GetTrimmedSubjectName(obj.SubjectName.Name).GetHashCode();
		}
	}
}
