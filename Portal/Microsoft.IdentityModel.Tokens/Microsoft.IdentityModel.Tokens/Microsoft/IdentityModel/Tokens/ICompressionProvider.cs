using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000154 RID: 340
	public interface ICompressionProvider
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000FE1 RID: 4065
		string Algorithm { get; }

		// Token: 0x06000FE2 RID: 4066
		bool IsSupportedAlgorithm(string algorithm);

		// Token: 0x06000FE3 RID: 4067
		byte[] Decompress(byte[] value);

		// Token: 0x06000FE4 RID: 4068
		byte[] Compress(byte[] value);
	}
}
