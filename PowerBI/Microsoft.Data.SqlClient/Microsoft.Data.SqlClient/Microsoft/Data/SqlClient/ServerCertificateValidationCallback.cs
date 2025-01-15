using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000D8 RID: 216
	// (Invoke) Token: 0x06000F4D RID: 3917
	internal delegate bool ServerCertificateValidationCallback(X509Certificate2 certificate);
}
