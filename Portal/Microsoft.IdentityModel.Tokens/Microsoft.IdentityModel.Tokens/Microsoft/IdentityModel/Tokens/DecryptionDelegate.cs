using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000132 RID: 306
	// (Invoke) Token: 0x06000EEE RID: 3822
	internal delegate byte[] DecryptionDelegate(byte[] cipherText, byte[] authenticatedData, byte[] iv, byte[] authenticationTag);
}
