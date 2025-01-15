using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000155 RID: 341
	public interface ICryptoProvider
	{
		// Token: 0x06000FE5 RID: 4069
		bool IsSupportedAlgorithm(string algorithm, params object[] args);

		// Token: 0x06000FE6 RID: 4070
		object Create(string algorithm, params object[] args);

		// Token: 0x06000FE7 RID: 4071
		void Release(object cryptoInstance);
	}
}
