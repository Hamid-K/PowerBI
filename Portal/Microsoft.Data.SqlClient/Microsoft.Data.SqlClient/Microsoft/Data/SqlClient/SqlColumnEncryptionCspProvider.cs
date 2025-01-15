using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000E0 RID: 224
	public class SqlColumnEncryptionCspProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06000F9A RID: 3994 RVA: 0x0003466C File Offset: 0x0003286C
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			this.ValidateNonEmptyCSPKeyPath(masterKeyPath, true);
			if (encryptedColumnEncryptionKey == null)
			{
				throw SQL.NullEncryptedColumnEncryptionKey();
			}
			if (encryptedColumnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyEncryptedColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, true);
			RSACryptoServiceProvider rsacryptoServiceProvider = this.CreateRSACryptoProvider(masterKeyPath, true);
			int keySize = this.GetKeySize(rsacryptoServiceProvider);
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
				throw SQL.InvalidCiphertextLengthInEncryptedCEKCsp((int)num3, keySize, masterKeyPath);
			}
			int num4 = encryptedColumnEncryptionKey.Length - num - (int)num3;
			if (num4 != keySize)
			{
				throw SQL.InvalidSignatureInEncryptedCEKCsp(num4, keySize, masterKeyPath);
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
			if (!this.RSAVerifySignature(hash, array2, rsacryptoServiceProvider))
			{
				throw SQL.InvalidSignature(masterKeyPath);
			}
			return this.RSADecrypt(rsacryptoServiceProvider, array);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000347AC File Offset: 0x000329AC
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			this.ValidateNonEmptyCSPKeyPath(masterKeyPath, false);
			if (columnEncryptionKey == null)
			{
				throw SQL.NullColumnEncryptionKey();
			}
			if (columnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, false);
			RSACryptoServiceProvider rsacryptoServiceProvider = this.CreateRSACryptoProvider(masterKeyPath, false);
			int keySize = this.GetKeySize(rsacryptoServiceProvider);
			byte[] array = new byte[] { this._version[0] };
			byte[] bytes = Encoding.Unicode.GetBytes(masterKeyPath.ToLowerInvariant());
			byte[] bytes2 = BitConverter.GetBytes((short)bytes.Length);
			byte[] array2 = this.RSAEncrypt(rsacryptoServiceProvider, columnEncryptionKey);
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
			byte[] array3 = this.RSASignHashedData(hash, rsacryptoServiceProvider);
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

		// Token: 0x06000F9C RID: 3996 RVA: 0x00034508 File Offset: 0x00032708
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00034508 File Offset: 0x00032708
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00034970 File Offset: 0x00032B70
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

		// Token: 0x06000F9F RID: 3999 RVA: 0x00034997 File Offset: 0x00032B97
		private void ValidateNonEmptyCSPKeyPath(string masterKeyPath, bool isSystemOp)
		{
			if (!string.IsNullOrWhiteSpace(masterKeyPath))
			{
				return;
			}
			if (masterKeyPath == null)
			{
				throw SQL.NullCspKeyPath(isSystemOp);
			}
			throw SQL.InvalidCspPath(masterKeyPath, isSystemOp);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000349B3 File Offset: 0x00032BB3
		private byte[] RSAEncrypt(RSACryptoServiceProvider rscp, byte[] columnEncryptionKey)
		{
			return rscp.Encrypt(columnEncryptionKey, true);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000349BD File Offset: 0x00032BBD
		private byte[] RSADecrypt(RSACryptoServiceProvider rscp, byte[] encryptedColumnEncryptionKey)
		{
			return rscp.Decrypt(encryptedColumnEncryptionKey, true);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000349C7 File Offset: 0x00032BC7
		private byte[] RSASignHashedData(byte[] dataToSign, RSACryptoServiceProvider rscp)
		{
			return rscp.SignData(dataToSign, "SHA256");
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000349D5 File Offset: 0x00032BD5
		private bool RSAVerifySignature(byte[] dataToVerify, byte[] signature, RSACryptoServiceProvider rscp)
		{
			return rscp.VerifyData(dataToVerify, "SHA256", signature);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00034595 File Offset: 0x00032795
		private int GetKeySize(RSACryptoServiceProvider rscp)
		{
			return rscp.KeySize / 8;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x000349E4 File Offset: 0x00032BE4
		private RSACryptoServiceProvider CreateRSACryptoProvider(string keyPath, bool isSystemOp)
		{
			string text;
			string text2;
			this.GetCspProviderAndKeyName(keyPath, isSystemOp, out text, out text2);
			int providerType = this.GetProviderType(text, keyPath, isSystemOp);
			CspParameters cspParameters = new CspParameters(providerType, text, text2);
			cspParameters.Flags = CspProviderFlags.UseExistingKey;
			RSACryptoServiceProvider rsacryptoServiceProvider = null;
			try
			{
				rsacryptoServiceProvider = new RSACryptoServiceProvider(cspParameters);
			}
			catch (CryptographicException ex)
			{
				if (ex.HResult == -2146893802)
				{
					throw SQL.InvalidCspKeyIdentifier(text2, keyPath, isSystemOp);
				}
				throw;
			}
			return rsacryptoServiceProvider;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00034A54 File Offset: 0x00032C54
		private void GetCspProviderAndKeyName(string keyPath, bool isSystemOp, out string cspProviderName, out string keyIdentifier)
		{
			int num = keyPath.IndexOf("/");
			if (num == -1)
			{
				throw SQL.InvalidCspPath(keyPath, isSystemOp);
			}
			cspProviderName = keyPath.Substring(0, num);
			keyIdentifier = keyPath.Substring(num + 1, keyPath.Length - (num + 1));
			if (cspProviderName.Length == 0)
			{
				throw SQL.EmptyCspName(keyPath, isSystemOp);
			}
			if (keyIdentifier.Length == 0)
			{
				throw SQL.EmptyCspKeyId(keyPath, isSystemOp);
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00034ABC File Offset: 0x00032CBC
		private int GetProviderType(string providerName, string keyPath, bool isSystemOp)
		{
			string text = string.Format("SOFTWARE\\Microsoft\\Cryptography\\Defaults\\Provider\\{0}", providerName);
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(text);
			if (registryKey == null)
			{
				throw SQL.InvalidCspName(providerName, keyPath, isSystemOp);
			}
			int num = (int)registryKey.GetValue("Type");
			registryKey.Close();
			return num;
		}

		// Token: 0x040006A7 RID: 1703
		public const string ProviderName = "MSSQL_CSP_PROVIDER";

		// Token: 0x040006A8 RID: 1704
		private const string RSAEncryptionAlgorithmWithOAEP = "RSA_OAEP";

		// Token: 0x040006A9 RID: 1705
		private const string HashingAlgorithm = "SHA256";

		// Token: 0x040006AA RID: 1706
		private readonly byte[] _version = new byte[] { 1 };
	}
}
