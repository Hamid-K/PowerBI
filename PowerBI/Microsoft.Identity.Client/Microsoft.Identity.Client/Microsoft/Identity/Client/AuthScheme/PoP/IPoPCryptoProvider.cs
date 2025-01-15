using System;

namespace Microsoft.Identity.Client.AuthScheme.PoP
{
	// Token: 0x020002C3 RID: 707
	public interface IPoPCryptoProvider
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001A93 RID: 6803
		string CannonicalPublicKeyJwk { get; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001A94 RID: 6804
		string CryptographicAlgorithm { get; }

		// Token: 0x06001A95 RID: 6805
		byte[] Sign(byte[] data);
	}
}
