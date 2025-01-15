using System;
using System.Security.Cryptography;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.AuthScheme.PoP
{
	// Token: 0x020002C2 RID: 706
	internal class InMemoryCryptoProvider : IPoPCryptoProvider
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x00056766 File Offset: 0x00054966
		public InMemoryCryptoProvider()
		{
			this.InitializeSigningKey();
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x00056774 File Offset: 0x00054974
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x0005677C File Offset: 0x0005497C
		public string CannonicalPublicKeyJwk { get; private set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00056785 File Offset: 0x00054985
		public string CryptographicAlgorithm
		{
			get
			{
				return "RS256";
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0005678C File Offset: 0x0005498C
		private void InitializeSigningKey()
		{
			this._signingKey = RSA.Create("RSAPSS");
			this._signingKey.KeySize = 2048;
			RSAParameters rsaparameters = this._signingKey.ExportParameters(false);
			this.CannonicalPublicKeyJwk = InMemoryCryptoProvider.ComputeCanonicalJwk(rsaparameters);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000567D2 File Offset: 0x000549D2
		public byte[] Sign(byte[] payload)
		{
			return this._signingKey.SignData(payload, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000567EC File Offset: 0x000549EC
		private static string ComputeCanonicalJwk(RSAParameters rsaPublicKey)
		{
			return string.Concat(new string[]
			{
				"{\"e\":\"",
				Base64UrlHelpers.Encode(rsaPublicKey.Exponent),
				"\",\"kty\":\"RSA\",\"n\":\"",
				Base64UrlHelpers.Encode(rsaPublicKey.Modulus),
				"\"}"
			});
		}

		// Token: 0x04000BFA RID: 3066
		internal const int RsaKeySize = 2048;

		// Token: 0x04000BFB RID: 3067
		private RSA _signingKey;
	}
}
