using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Identity.Client.PlatformsCommon.Interfaces
{
	// Token: 0x020001FB RID: 507
	internal interface ICryptographyManager
	{
		// Token: 0x0600158D RID: 5517
		string CreateBase64UrlEncodedSha256Hash(string input);

		// Token: 0x0600158E RID: 5518
		string GenerateCodeVerifier();

		// Token: 0x0600158F RID: 5519
		string CreateSha256Hash(string input);

		// Token: 0x06001590 RID: 5520
		byte[] CreateSha256HashBytes(string input);

		// Token: 0x06001591 RID: 5521
		byte[] SignWithCertificate(string message, X509Certificate2 certificate, RSASignaturePadding signaturePadding);
	}
}
