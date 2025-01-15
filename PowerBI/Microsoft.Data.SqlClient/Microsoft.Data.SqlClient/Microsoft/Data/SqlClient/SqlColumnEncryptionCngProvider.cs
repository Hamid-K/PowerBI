using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000DF RID: 223
	public class SqlColumnEncryptionCngProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x00034204 File Offset: 0x00032404
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			this.ValidateNonEmptyKeyPath(masterKeyPath, true);
			if (encryptedColumnEncryptionKey == null)
			{
				throw SQL.NullEncryptedColumnEncryptionKey();
			}
			if (encryptedColumnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyEncryptedColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, true);
			RSACng rsacng = this.CreateRSACngProvider(masterKeyPath, true);
			int keySize = this.GetKeySize(rsacng);
			if (encryptedColumnEncryptionKey[0] != this._version[0])
			{
				throw SQL.InvalidAlgorithmVersionInEncryptedCEK(encryptedColumnEncryptionKey[0], this._version[0]);
			}
			int num = this._version.Length;
			ushort num2 = BitConverter.ToUInt16(encryptedColumnEncryptionKey, num);
			num += 2;
			ushort num3 = BitConverter.ToUInt16(encryptedColumnEncryptionKey, num);
			num += 2;
			num += (int)num2;
			if ((int)num3 != keySize)
			{
				throw SQL.InvalidCiphertextLengthInEncryptedCEKCng((int)num3, keySize, masterKeyPath);
			}
			int num4 = encryptedColumnEncryptionKey.Length - num - (int)num3;
			if (num4 != keySize)
			{
				throw SQL.InvalidSignatureInEncryptedCEKCng(num4, keySize, masterKeyPath);
			}
			byte[] array = new byte[(int)num3];
			Buffer.BlockCopy(encryptedColumnEncryptionKey, num, array, 0, array.Length);
			num += (int)num3;
			byte[] array2 = new byte[num4];
			Buffer.BlockCopy(encryptedColumnEncryptionKey, num, array2, 0, array2.Length);
			byte[] hash;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				sha256Cng.TransformFinalBlock(encryptedColumnEncryptionKey, 0, encryptedColumnEncryptionKey.Length - array2.Length);
				hash = sha256Cng.Hash;
			}
			if (!this.RSAVerifySignature(hash, array2, rsacng))
			{
				throw SQL.InvalidSignature(masterKeyPath);
			}
			return this.RSADecrypt(rsacng, array);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00034344 File Offset: 0x00032544
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			this.ValidateNonEmptyKeyPath(masterKeyPath, false);
			if (columnEncryptionKey == null)
			{
				throw SQL.NullColumnEncryptionKey();
			}
			if (columnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, false);
			RSACng rsacng = this.CreateRSACngProvider(masterKeyPath, false);
			int keySize = this.GetKeySize(rsacng);
			byte[] array = new byte[] { this._version[0] };
			byte[] bytes = Encoding.Unicode.GetBytes(masterKeyPath.ToLowerInvariant());
			byte[] bytes2 = BitConverter.GetBytes((short)bytes.Length);
			byte[] array2 = this.RSAEncrypt(rsacng, columnEncryptionKey);
			byte[] bytes3 = BitConverter.GetBytes((short)array2.Length);
			byte[] hash;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				sha256Cng.TransformBlock(array, 0, array.Length, array, 0);
				sha256Cng.TransformBlock(bytes2, 0, bytes2.Length, bytes2, 0);
				sha256Cng.TransformBlock(bytes3, 0, bytes3.Length, bytes3, 0);
				sha256Cng.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				sha256Cng.TransformFinalBlock(array2, 0, array2.Length);
				hash = sha256Cng.Hash;
			}
			byte[] array3 = this.RSASignHashedData(hash, rsacng);
			int num = array.Length + bytes3.Length + bytes2.Length + array2.Length + bytes.Length + array3.Length;
			byte[] array4 = new byte[num];
			int num2 = 0;
			Buffer.BlockCopy(array, 0, array4, num2, array.Length);
			num2 += array.Length;
			Buffer.BlockCopy(bytes2, 0, array4, num2, bytes2.Length);
			num2 += bytes2.Length;
			Buffer.BlockCopy(bytes3, 0, array4, num2, bytes3.Length);
			num2 += bytes3.Length;
			Buffer.BlockCopy(bytes, 0, array4, num2, bytes.Length);
			num2 += bytes.Length;
			Buffer.BlockCopy(array2, 0, array4, num2, array2.Length);
			num2 += array2.Length;
			Buffer.BlockCopy(array3, 0, array4, num2, array3.Length);
			return array4;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00034508 File Offset: 0x00032708
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00034508 File Offset: 0x00032708
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003450F File Offset: 0x0003270F
		private void ValidateEncryptionAlgorithm(string encryptionAlgorithm, bool isSystemOp)
		{
			if (encryptionAlgorithm == null)
			{
				throw SQL.NullKeyEncryptionAlgorithm(isSystemOp);
			}
			if (!string.Equals(encryptionAlgorithm, "RSA_OAEP", StringComparison.OrdinalIgnoreCase))
			{
				throw SQL.InvalidKeyEncryptionAlgorithm(encryptionAlgorithm, "RSA_OAEP", isSystemOp);
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00034536 File Offset: 0x00032736
		private void ValidateNonEmptyKeyPath(string masterKeyPath, bool isSystemOp)
		{
			if (!string.IsNullOrWhiteSpace(masterKeyPath))
			{
				return;
			}
			if (masterKeyPath == null)
			{
				throw SQL.NullCngKeyPath(isSystemOp);
			}
			throw SQL.InvalidCngPath(masterKeyPath, isSystemOp);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00034552 File Offset: 0x00032752
		private byte[] RSAEncrypt(RSACng rsaCngProvider, byte[] columnEncryptionKey)
		{
			return rsaCngProvider.Encrypt(columnEncryptionKey, RSAEncryptionPadding.OaepSHA1);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00034560 File Offset: 0x00032760
		private byte[] RSADecrypt(RSACng rsaCngProvider, byte[] encryptedColumnEncryptionKey)
		{
			return rsaCngProvider.Decrypt(encryptedColumnEncryptionKey, RSAEncryptionPadding.OaepSHA1);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003456E File Offset: 0x0003276E
		private byte[] RSASignHashedData(byte[] dataToSign, RSACng rsaCngProvider)
		{
			return rsaCngProvider.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00034581 File Offset: 0x00032781
		private bool RSAVerifySignature(byte[] dataToVerify, byte[] signature, RSACng rsaCngProvider)
		{
			return rsaCngProvider.VerifyData(dataToVerify, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00034595 File Offset: 0x00032795
		private int GetKeySize(RSACng rsaCngProvider)
		{
			return rsaCngProvider.KeySize / 8;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000345A0 File Offset: 0x000327A0
		private RSACng CreateRSACngProvider(string keyPath, bool isSystemOp)
		{
			string text;
			string text2;
			this.GetCngProviderAndKeyId(keyPath, isSystemOp, out text, out text2);
			CngProvider cngProvider = new CngProvider(text);
			CngKey cngKey;
			try
			{
				cngKey = CngKey.Open(text2, cngProvider);
			}
			catch (CryptographicException)
			{
				throw SQL.InvalidCngKey(keyPath, text, text2, isSystemOp);
			}
			return new RSACng(cngKey);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000345EC File Offset: 0x000327EC
		private void GetCngProviderAndKeyId(string keyPath, bool isSystemOp, out string cngProvider, out string keyIdentifier)
		{
			int num = keyPath.IndexOf("/");
			if (num == -1)
			{
				throw SQL.InvalidCngPath(keyPath, isSystemOp);
			}
			cngProvider = keyPath.Substring(0, num);
			keyIdentifier = keyPath.Substring(num + 1, keyPath.Length - (num + 1));
			if (cngProvider.Length == 0)
			{
				throw SQL.EmptyCngName(keyPath, isSystemOp);
			}
			if (keyIdentifier.Length == 0)
			{
				throw SQL.EmptyCngKeyId(keyPath, isSystemOp);
			}
		}

		// Token: 0x040006A4 RID: 1700
		public const string ProviderName = "MSSQL_CNG_STORE";

		// Token: 0x040006A5 RID: 1701
		private const string RSAEncryptionAlgorithmWithOAEP = "RSA_OAEP";

		// Token: 0x040006A6 RID: 1702
		private readonly byte[] _version = new byte[] { 1 };
	}
}
