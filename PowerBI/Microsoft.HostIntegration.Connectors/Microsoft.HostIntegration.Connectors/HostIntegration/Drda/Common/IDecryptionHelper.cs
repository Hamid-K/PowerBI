using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000816 RID: 2070
	public interface IDecryptionHelper
	{
		// Token: 0x0600419F RID: 16799
		void GetPublicKey(out byte[] publicKey);

		// Token: 0x060041A0 RID: 16800
		byte[] EncryptText(string Data, byte[] Key, byte[] IV);

		// Token: 0x060041A1 RID: 16801
		string DecryptText(byte[] Data, byte[] sourcePublicKey, byte[] initVector);

		// Token: 0x060041A2 RID: 16802
		byte[] DecryptKey(byte[] sectoken);

		// Token: 0x060041A3 RID: 16803
		byte[] GetKeyIV(byte[] sectoken);
	}
}
