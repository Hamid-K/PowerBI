using System;
using System.Security.Cryptography;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019A5 RID: 6565
	internal abstract class CryptoAlgorithmSuite
	{
		// Token: 0x17002A71 RID: 10865
		// (get) Token: 0x0600A66B RID: 42603
		public abstract int DataEncryptionKeyByteCount { get; }

		// Token: 0x17002A72 RID: 10866
		// (get) Token: 0x0600A66C RID: 42604
		public abstract int DataEncryptionBlockByteCount { get; }

		// Token: 0x0600A66D RID: 42605
		public abstract HashAlgorithm CreateHashAlgorithm();

		// Token: 0x0600A66E RID: 42606
		public abstract SymmetricAlgorithm CreateDataEncryptionAlgorithm();

		// Token: 0x0600A66F RID: 42607
		public abstract AsymmetricKeyExchangeDeformatter CreateKeyExchangeDeformatter(AsymmetricAlgorithm privateKey);

		// Token: 0x0600A670 RID: 42608
		public abstract AsymmetricKeyExchangeFormatter CreateKeyExchangeFormatter(AsymmetricAlgorithm publicKey);

		// Token: 0x0400569C RID: 22172
		public static readonly CryptoAlgorithmSuite Default = new CryptoAlgorithmSuite.DefaultCryptoAlgorithmSuite();

		// Token: 0x020019A6 RID: 6566
		private class DefaultCryptoAlgorithmSuite : CryptoAlgorithmSuite
		{
			// Token: 0x17002A73 RID: 10867
			// (get) Token: 0x0600A673 RID: 42611 RVA: 0x0022702C File Offset: 0x0022522C
			private static Func<HashAlgorithm> HashAlgorithmFactory
			{
				get
				{
					return () => SHA256CryptoProvider.Create();
				}
			}

			// Token: 0x17002A74 RID: 10868
			// (get) Token: 0x0600A674 RID: 42612 RVA: 0x0022704D File Offset: 0x0022524D
			private static Func<SymmetricAlgorithm> DataEncryptionAlgorithmFactory
			{
				get
				{
					return () => AesCryptoProvider.Create();
				}
			}

			// Token: 0x17002A75 RID: 10869
			// (get) Token: 0x0600A675 RID: 42613 RVA: 0x0022706E File Offset: 0x0022526E
			public override int DataEncryptionKeyByteCount
			{
				get
				{
					return 32;
				}
			}

			// Token: 0x17002A76 RID: 10870
			// (get) Token: 0x0600A676 RID: 42614 RVA: 0x00227072 File Offset: 0x00225272
			public override int DataEncryptionBlockByteCount
			{
				get
				{
					return 16;
				}
			}

			// Token: 0x0600A677 RID: 42615 RVA: 0x00227076 File Offset: 0x00225276
			public override HashAlgorithm CreateHashAlgorithm()
			{
				return CryptoAlgorithmSuite.DefaultCryptoAlgorithmSuite.HashAlgorithmFactory();
			}

			// Token: 0x0600A678 RID: 42616 RVA: 0x00227082 File Offset: 0x00225282
			public override SymmetricAlgorithm CreateDataEncryptionAlgorithm()
			{
				SymmetricAlgorithm symmetricAlgorithm = CryptoAlgorithmSuite.DefaultCryptoAlgorithmSuite.DataEncryptionAlgorithmFactory();
				symmetricAlgorithm.Mode = CipherMode.CBC;
				symmetricAlgorithm.Padding = PaddingMode.ISO10126;
				return symmetricAlgorithm;
			}

			// Token: 0x0600A679 RID: 42617 RVA: 0x0022709C File Offset: 0x0022529C
			public override AsymmetricKeyExchangeDeformatter CreateKeyExchangeDeformatter(AsymmetricAlgorithm privateKey)
			{
				return new RSAOAEPKeyExchangeDeformatter(privateKey);
			}

			// Token: 0x0600A67A RID: 42618 RVA: 0x002270A4 File Offset: 0x002252A4
			public override AsymmetricKeyExchangeFormatter CreateKeyExchangeFormatter(AsymmetricAlgorithm publicKey)
			{
				return new RSAOAEPKeyExchangeFormatter(publicKey);
			}

			// Token: 0x0400569D RID: 22173
			private const int Aes256SymmetricKeySize = 256;

			// Token: 0x0400569E RID: 22174
			private const int Aes256BlockSize = 128;
		}
	}
}
