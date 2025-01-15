using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200004A RID: 74
	internal class SqlAeadAes256CbcHmac256Algorithm : SqlClientEncryptionAlgorithm
	{
		// Token: 0x060007A0 RID: 1952 RVA: 0x000108AE File Offset: 0x0000EAAE
		internal SqlAeadAes256CbcHmac256Algorithm(SqlAeadAes256CbcHmac256EncryptionKey encryptionKey, SqlClientEncryptionType encryptionType, byte algorithmVersion)
		{
			this._columnEncryptionKey = encryptionKey;
			this._algorithmVersion = algorithmVersion;
			SqlAeadAes256CbcHmac256Algorithm._version[0] = algorithmVersion;
			if (encryptionType == SqlClientEncryptionType.Deterministic)
			{
				this._isDeterministic = true;
			}
			this._cryptoProviderPool = new ConcurrentQueue<Aes>();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000108E2 File Offset: 0x0000EAE2
		internal override byte[] EncryptData(byte[] plainText)
		{
			return this.EncryptData(plainText, true);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000108EC File Offset: 0x0000EAEC
		protected byte[] EncryptData(byte[] plainText, bool hasAuthenticationTag)
		{
			byte[] array = new byte[16];
			if (this._isDeterministic)
			{
				SqlSecurityUtility.GetHMACWithSHA256(plainText, this._columnEncryptionKey.IVKey, array);
			}
			else
			{
				SqlSecurityUtility.GenerateRandomBytes(array);
			}
			int num = plainText.Length / 16 + 1;
			int num2 = (hasAuthenticationTag ? 32 : 0);
			int num3 = 1 + num2;
			int num4 = num3 + 16;
			int num5 = 1 + num2 + array.Length + num * 16;
			byte[] array2 = new byte[num5];
			array2[0] = this._algorithmVersion;
			Buffer.BlockCopy(array, 0, array2, num3, array.Length);
			Aes aes;
			if (!this._cryptoProviderPool.TryDequeue(out aes))
			{
				aes = Aes.Create();
				try
				{
					aes.Key = this._columnEncryptionKey.EncryptionKey;
					aes.Mode = CipherMode.CBC;
					aes.Padding = PaddingMode.PKCS7;
				}
				catch (Exception)
				{
					if (aes != null)
					{
						aes.Dispose();
					}
					throw;
				}
			}
			try
			{
				aes.IV = array;
				using (ICryptoTransform cryptoTransform = aes.CreateEncryptor())
				{
					int num6 = 0;
					int num7 = num4;
					if (num > 1)
					{
						num6 = (num - 1) * 16;
						num7 += cryptoTransform.TransformBlock(plainText, 0, num6, array2, num7);
					}
					byte[] array3 = cryptoTransform.TransformFinalBlock(plainText, num6, plainText.Length - num6);
					Buffer.BlockCopy(array3, 0, array2, num7, array3.Length);
					num7 += array3.Length;
				}
				if (hasAuthenticationTag)
				{
					using (HMACSHA256 hmacsha = new HMACSHA256(this._columnEncryptionKey.MACKey))
					{
						hmacsha.TransformBlock(SqlAeadAes256CbcHmac256Algorithm._version, 0, SqlAeadAes256CbcHmac256Algorithm._version.Length, SqlAeadAes256CbcHmac256Algorithm._version, 0);
						hmacsha.TransformBlock(array, 0, array.Length, array, 0);
						hmacsha.TransformBlock(array2, num4, num * 16, array2, num4);
						hmacsha.TransformFinalBlock(SqlAeadAes256CbcHmac256Algorithm._versionSize, 0, SqlAeadAes256CbcHmac256Algorithm._versionSize.Length);
						byte[] hash = hmacsha.Hash;
						Buffer.BlockCopy(hash, 0, array2, 1, num2);
					}
				}
			}
			finally
			{
				this._cryptoProviderPool.Enqueue(aes);
			}
			return array2;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00010B2C File Offset: 0x0000ED2C
		internal override byte[] DecryptData(byte[] cipherText)
		{
			return this.DecryptData(cipherText, true);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00010B38 File Offset: 0x0000ED38
		protected byte[] DecryptData(byte[] cipherText, bool hasAuthenticationTag)
		{
			byte[] array = new byte[16];
			int num = (hasAuthenticationTag ? 65 : 33);
			if (cipherText.Length < num)
			{
				throw SQL.InvalidCipherTextSize(cipherText.Length, num);
			}
			int num2 = 0;
			if (cipherText[num2] != this._algorithmVersion)
			{
				throw SQL.InvalidAlgorithmVersion(cipherText[num2], this._algorithmVersion);
			}
			num2++;
			int num3 = 0;
			if (hasAuthenticationTag)
			{
				num3 = num2;
				num2 += 32;
			}
			Buffer.BlockCopy(cipherText, num2, array, 0, array.Length);
			num2 += array.Length;
			int num4 = num2;
			int num5 = cipherText.Length - num2;
			if (hasAuthenticationTag)
			{
				byte[] array2 = this.PrepareAuthenticationTag(array, cipherText, num4, num5);
				if (!SqlSecurityUtility.CompareBytes(array2, cipherText, num3, array2.Length))
				{
					throw SQL.InvalidAuthenticationTag();
				}
			}
			return this.DecryptData(array, cipherText, num4, num5);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00010BE4 File Offset: 0x0000EDE4
		private byte[] DecryptData(byte[] iv, byte[] cipherText, int offset, int count)
		{
			Aes aes;
			if (!this._cryptoProviderPool.TryDequeue(out aes))
			{
				aes = Aes.Create();
				try
				{
					aes.Key = this._columnEncryptionKey.EncryptionKey;
					aes.Mode = CipherMode.CBC;
					aes.Padding = PaddingMode.PKCS7;
				}
				catch (Exception)
				{
					if (aes != null)
					{
						aes.Dispose();
					}
					throw;
				}
			}
			byte[] array;
			try
			{
				aes.IV = iv;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (ICryptoTransform cryptoTransform = aes.CreateDecryptor())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
						{
							cryptoStream.Write(cipherText, offset, count);
							cryptoStream.FlushFinalBlock();
							array = memoryStream.ToArray();
						}
					}
				}
			}
			finally
			{
				this._cryptoProviderPool.Enqueue(aes);
			}
			return array;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00010CDC File Offset: 0x0000EEDC
		private byte[] PrepareAuthenticationTag(byte[] iv, byte[] cipherText, int offset, int length)
		{
			byte[] array = new byte[32];
			byte[] hash;
			using (HMACSHA256 hmacsha = new HMACSHA256(this._columnEncryptionKey.MACKey))
			{
				int num = hmacsha.TransformBlock(SqlAeadAes256CbcHmac256Algorithm._version, 0, SqlAeadAes256CbcHmac256Algorithm._version.Length, SqlAeadAes256CbcHmac256Algorithm._version, 0);
				num = hmacsha.TransformBlock(iv, 0, iv.Length, iv, 0);
				num = hmacsha.TransformBlock(cipherText, offset, length, cipherText, offset);
				hmacsha.TransformFinalBlock(SqlAeadAes256CbcHmac256Algorithm._versionSize, 0, SqlAeadAes256CbcHmac256Algorithm._versionSize.Length);
				hash = hmacsha.Hash;
			}
			Buffer.BlockCopy(hash, 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x040000F8 RID: 248
		internal const string AlgorithmName = "AEAD_AES_256_CBC_HMAC_SHA256";

		// Token: 0x040000F9 RID: 249
		private const int _KeySizeInBytes = 32;

		// Token: 0x040000FA RID: 250
		private const int _BlockSizeInBytes = 16;

		// Token: 0x040000FB RID: 251
		private const int _MinimumCipherTextLengthInBytesNoAuthenticationTag = 33;

		// Token: 0x040000FC RID: 252
		private const int _MinimumCipherTextLengthInBytesWithAuthenticationTag = 65;

		// Token: 0x040000FD RID: 253
		private const CipherMode _cipherMode = CipherMode.CBC;

		// Token: 0x040000FE RID: 254
		private const PaddingMode _paddingMode = PaddingMode.PKCS7;

		// Token: 0x040000FF RID: 255
		private readonly bool _isDeterministic;

		// Token: 0x04000100 RID: 256
		private readonly byte _algorithmVersion;

		// Token: 0x04000101 RID: 257
		private readonly SqlAeadAes256CbcHmac256EncryptionKey _columnEncryptionKey;

		// Token: 0x04000102 RID: 258
		private readonly ConcurrentQueue<Aes> _cryptoProviderPool;

		// Token: 0x04000103 RID: 259
		private static readonly byte[] _version = new byte[] { 1 };

		// Token: 0x04000104 RID: 260
		private static readonly byte[] _versionSize = new byte[] { 1 };
	}
}
