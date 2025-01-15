using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x02000064 RID: 100
	// (Invoke) Token: 0x060002B7 RID: 695
	public delegate void UpdateCertificateHandler(Dictionary<string, IEnumerable<X509Certificate2>> updatedCertificates);
}
