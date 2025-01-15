using System;
using System.Security.Cryptography;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B6 RID: 438
	public static class CryptoUtilities
	{
		// Token: 0x06000B4B RID: 2891 RVA: 0x000275C8 File Offset: 0x000257C8
		public static byte[] RSAPKCS1EncryptSymmetricKey(AsymmetricAlgorithm asymmetricEncryptor, byte[] symmetricKey)
		{
			return new RSAPKCS1KeyExchangeFormatter(asymmetricEncryptor).CreateKeyExchange(symmetricKey);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000275D6 File Offset: 0x000257D6
		public static byte[] RSAPKCS1DecryptSymmetricKey(AsymmetricAlgorithm asymmetricDecryptor, byte[] symmetricKeyCypher)
		{
			return new RSAPKCS1KeyExchangeDeformatter(asymmetricDecryptor).DecryptKeyExchange(symmetricKeyCypher);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000275E4 File Offset: 0x000257E4
		public static byte[] EncryptData(SymmetricAlgorithm symmetricEncryptor, byte[] clearTextBuffer, int clearTextLength)
		{
			ExtendedDiagnostics.EnsureArgument("clearTextLength", clearTextLength <= clearTextBuffer.Length, "'clearTextLength' cannot be greater than 'clearTextBuffer.Length'");
			return symmetricEncryptor.CreateEncryptor().TransformFinalBlock(clearTextBuffer, 0, clearTextLength);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002760C File Offset: 0x0002580C
		public static byte[] EncryptData(SymmetricAlgorithm symmetricEncryptor, byte[] clearTextBuffer)
		{
			return CryptoUtilities.EncryptData(symmetricEncryptor, clearTextBuffer, clearTextBuffer.Length);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00027618 File Offset: 0x00025818
		public static byte[] DecryptData(SymmetricAlgorithm symmetricDecryptor, byte[] cypherTextBuffer, int cypherTextLength)
		{
			ExtendedDiagnostics.EnsureArgument("clearTextLength", cypherTextLength <= cypherTextBuffer.Length, "'clearTextLength' cannot be greater than 'clearTextBuffer.Length'");
			return symmetricDecryptor.CreateDecryptor().TransformFinalBlock(cypherTextBuffer, 0, cypherTextLength);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00027640 File Offset: 0x00025840
		public static byte[] DecryptData(SymmetricAlgorithm symmetricDecryptor, byte[] cypherTextBuffer)
		{
			return CryptoUtilities.DecryptData(symmetricDecryptor, cypherTextBuffer, cypherTextBuffer.Length);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002764C File Offset: 0x0002584C
		public static HashAlgorithm TryGetHashAlgorithm(string name)
		{
			return CryptoConfig.CreateFromName(name) as HashAlgorithm;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00027659 File Offset: 0x00025859
		public static SymmetricAlgorithm TryGetSymmetricAlgorithm(string name)
		{
			return CryptoConfig.CreateFromName(name) as SymmetricAlgorithm;
		}
	}
}
