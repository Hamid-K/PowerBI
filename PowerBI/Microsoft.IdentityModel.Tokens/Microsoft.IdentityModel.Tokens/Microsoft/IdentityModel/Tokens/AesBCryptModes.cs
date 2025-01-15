using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000166 RID: 358
	internal static class AesBCryptModes
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x000401A3 File Offset: 0x0003E3A3
		internal static Lazy<SafeAlgorithmHandle> OpenAesAlgorithm(string cipherMode)
		{
			return new Lazy<SafeAlgorithmHandle>(delegate
			{
				SafeAlgorithmHandle safeAlgorithmHandle = Cng.BCryptOpenAlgorithmProvider("AES", null, Cng.OpenAlgorithmProviderFlags.NONE);
				safeAlgorithmHandle.SetCipherMode(cipherMode);
				return safeAlgorithmHandle;
			});
		}
	}
}
