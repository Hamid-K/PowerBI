using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x02000061 RID: 97
	public interface ICertificatesContainer
	{
		// Token: 0x060002AD RID: 685
		IEnumerable<X509Certificate2> GetCertificates(string key);

		// Token: 0x060002AE RID: 686
		X509Certificate2 GetPrimaryCertificate(string key);

		// Token: 0x060002AF RID: 687
		X509Certificate2 GetSecondaryCertificate(string key);
	}
}
