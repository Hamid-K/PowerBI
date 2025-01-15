using System;
using System.Security.Cryptography;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B41 RID: 6977
	public static class SymmetricAlgorithmExtensions
	{
		// Token: 0x0600AE9E RID: 44702 RVA: 0x0023C29C File Offset: 0x0023A49C
		public static SymmetricAlgorithm CreateAes()
		{
			SymmetricAlgorithm symmetricAlgorithm = CryptoAlgorithmSuite.Default.CreateDataEncryptionAlgorithm();
			symmetricAlgorithm.KeySize = 256;
			symmetricAlgorithm.Padding = PaddingMode.PKCS7;
			symmetricAlgorithm.Mode = CipherMode.CBC;
			return symmetricAlgorithm;
		}

		// Token: 0x0600AE9F RID: 44703 RVA: 0x0023C2C1 File Offset: 0x0023A4C1
		public static int GetBlockSizeInBytes(this SymmetricAlgorithm algorithm)
		{
			return algorithm.BlockSize / 8;
		}

		// Token: 0x0600AEA0 RID: 44704 RVA: 0x0023C2CC File Offset: 0x0023A4CC
		public static long GetDecryptedLength(this SymmetricAlgorithm algorithm, int decryptedPageSize, long encryptedLength)
		{
			int encryptedPageSize = algorithm.GetEncryptedPageSize(decryptedPageSize);
			long num = PageHelpers.PageCount(encryptedLength, (long)encryptedPageSize);
			long num2 = encryptedLength - (num - 1L) * (long)encryptedPageSize;
			long num3 = 0L;
			if (num2 < (long)encryptedPageSize)
			{
				num -= 1L;
				num3 = (long)algorithm.GetDecryptedPageSize((int)num2);
			}
			return num * (long)decryptedPageSize + num3;
		}

		// Token: 0x0600AEA1 RID: 44705 RVA: 0x0023C314 File Offset: 0x0023A514
		public static long GetEncryptedLength(this SymmetricAlgorithm algorithm, int decryptedPageSize, long decryptedLength)
		{
			int encryptedPageSize = algorithm.GetEncryptedPageSize(decryptedPageSize);
			long num = PageHelpers.PageCount(decryptedLength, (long)decryptedPageSize);
			long num2 = decryptedLength - (num - 1L) * (long)decryptedPageSize;
			long num3 = 0L;
			if (num2 < (long)decryptedPageSize)
			{
				num -= 1L;
				num3 = (long)algorithm.GetEncryptedPageSize((int)num2);
			}
			return num * (long)encryptedPageSize + num3;
		}

		// Token: 0x0600AEA2 RID: 44706 RVA: 0x0023C35C File Offset: 0x0023A55C
		public static int GetDecryptedPageSize(this SymmetricAlgorithm algorithm, int encryptedPageSize)
		{
			int blockSizeInBytes = algorithm.GetBlockSizeInBytes();
			return encryptedPageSize - blockSizeInBytes - 1;
		}

		// Token: 0x0600AEA3 RID: 44707 RVA: 0x0023C378 File Offset: 0x0023A578
		public static int GetEncryptedPageSize(this SymmetricAlgorithm algorithm, int decryptedPageSize)
		{
			int blockSizeInBytes = algorithm.GetBlockSizeInBytes();
			return (int)PageHelpers.Align((long)decryptedPageSize + 1L, (long)blockSizeInBytes) + blockSizeInBytes;
		}
	}
}
