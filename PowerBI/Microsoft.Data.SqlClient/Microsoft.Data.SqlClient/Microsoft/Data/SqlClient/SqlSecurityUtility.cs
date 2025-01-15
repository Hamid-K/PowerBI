using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000099 RID: 153
	internal static class SqlSecurityUtility
	{
		// Token: 0x06000C49 RID: 3145 RVA: 0x00024F00 File Offset: 0x00023100
		internal static void GetHMACWithSHA256(byte[] plainText, byte[] key, byte[] hash)
		{
			using (HMACSHA256 hmacsha = new HMACSHA256(key))
			{
				byte[] array = hmacsha.ComputeHash(plainText);
				Buffer.BlockCopy(array, 0, hash, 0, hash.Length);
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00024F44 File Offset: 0x00023144
		internal static string GetSHA256Hash(byte[] input)
		{
			string hexString;
			using (SHA256 sha = SHA256.Create())
			{
				byte[] array = sha.ComputeHash(input);
				hexString = SqlSecurityUtility.GetHexString(array);
			}
			return hexString;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00024F84 File Offset: 0x00023184
		internal static void GenerateRandomBytes(byte[] randomBytes)
		{
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(randomBytes);
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00024FBC File Offset: 0x000231BC
		internal static bool CompareBytes(byte[] buffer1, byte[] buffer2, int buffer2Index, int lengthToCompare)
		{
			if (buffer1 == null || buffer2 == null)
			{
				return false;
			}
			if (buffer2.Length - buffer2Index < lengthToCompare)
			{
				return false;
			}
			int num = 0;
			while (num < buffer1.Length && num < lengthToCompare)
			{
				if (buffer1[num] != buffer2[buffer2Index + num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00024FFC File Offset: 0x000231FC
		internal static string GetHexString(byte[] input)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in input)
			{
				stringBuilder.AppendFormat(b.ToString("X2"), Array.Empty<object>());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00025044 File Offset: 0x00023244
		internal static string GetCurrentFunctionName()
		{
			StackTrace stackTrace = new StackTrace();
			StackFrame frame = stackTrace.GetFrame(1);
			MethodBase method = frame.GetMethod();
			return string.Format("{0}.{1}", method.DeclaringType.Name, method.Name);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00025081 File Offset: 0x00023281
		private static string ValidateAndGetEncryptionAlgorithmName(byte cipherAlgorithmId, string cipherAlgorithmName)
		{
			if (cipherAlgorithmId == 0)
			{
				if (cipherAlgorithmName == null)
				{
					throw SQL.NullColumnEncryptionAlgorithm(SqlClientEncryptionAlgorithmFactoryList.GetInstance().GetRegisteredCipherAlgorithmNames());
				}
				return cipherAlgorithmName;
			}
			else
			{
				if (2 == cipherAlgorithmId)
				{
					return "AEAD_AES_256_CBC_HMAC_SHA256";
				}
				throw SQL.UnknownColumnEncryptionAlgorithmId((int)cipherAlgorithmId, SqlSecurityUtility.GetRegisteredCipherAlgorithmIds());
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000250B0 File Offset: 0x000232B0
		private static string GetRegisteredCipherAlgorithmIds()
		{
			return "'1', '2'";
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000250B8 File Offset: 0x000232B8
		internal static byte[] EncryptWithKey(byte[] plainText, SqlCipherMetadata md, SqlConnection connection, SqlCommand command)
		{
			if (!md.IsAlgorithmInitialized())
			{
				SqlSecurityUtility.DecryptSymmetricKey(md, connection, command);
			}
			byte[] array = md.CipherAlgorithm.EncryptData(plainText);
			if (array == null || array.Length == 0)
			{
				throw SQL.NullCipherText();
			}
			return array;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000250F0 File Offset: 0x000232F0
		internal static string GetBytesAsString(byte[] buff, bool fLast, int countOfBytes)
		{
			int num = ((buff.Length > countOfBytes) ? countOfBytes : buff.Length);
			int num2 = 0;
			if (fLast)
			{
				num2 = buff.Length - num;
			}
			return BitConverter.ToString(buff, num2, num);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00025120 File Offset: 0x00023320
		internal static byte[] DecryptWithKey(byte[] cipherText, SqlCipherMetadata md, SqlConnection connection, SqlCommand command)
		{
			if (!md.IsAlgorithmInitialized())
			{
				SqlSecurityUtility.DecryptSymmetricKey(md, connection, command);
			}
			byte[] array2;
			try
			{
				byte[] array = md.CipherAlgorithm.DecryptData(cipherText);
				if (array == null)
				{
					throw SQL.NullPlainText();
				}
				array2 = array;
			}
			catch (Exception ex)
			{
				string bytesAsString = SqlSecurityUtility.GetBytesAsString(md.EncryptionKeyInfo.encryptedKey, true, 10);
				string bytesAsString2 = SqlSecurityUtility.GetBytesAsString(cipherText, false, 10);
				throw SQL.ThrowDecryptionFailed(bytesAsString, bytesAsString2, ex);
			}
			return array2;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00025194 File Offset: 0x00023394
		internal static void DecryptSymmetricKey(SqlCipherMetadata md, SqlConnection connection, SqlCommand command)
		{
			SqlClientSymmetricKey sqlClientSymmetricKey = null;
			SqlEncryptionKeyInfo sqlEncryptionKeyInfo = null;
			SqlSecurityUtility.DecryptSymmetricKey(md.EncryptionInfo, out sqlClientSymmetricKey, out sqlEncryptionKeyInfo, connection, command);
			md.CipherAlgorithm = null;
			SqlClientEncryptionAlgorithm sqlClientEncryptionAlgorithm = null;
			string text = SqlSecurityUtility.ValidateAndGetEncryptionAlgorithmName(md.CipherAlgorithmId, md.CipherAlgorithmName);
			SqlClientEncryptionAlgorithmFactoryList.GetInstance().GetAlgorithm(sqlClientSymmetricKey, md.EncryptionType, text, out sqlClientEncryptionAlgorithm);
			md.CipherAlgorithm = sqlClientEncryptionAlgorithm;
			md.EncryptionKeyInfo = sqlEncryptionKeyInfo;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x000251F4 File Offset: 0x000233F4
		internal static void DecryptSymmetricKey(SqlTceCipherInfoEntry sqlTceCipherInfoEntry, out SqlClientSymmetricKey sqlClientSymmetricKey, out SqlEncryptionKeyInfo encryptionkeyInfoChosen, SqlConnection connection, SqlCommand command)
		{
			sqlClientSymmetricKey = null;
			encryptionkeyInfoChosen = null;
			Exception ex = null;
			SqlSymmetricKeyCache instance = SqlSymmetricKeyCache.GetInstance();
			foreach (SqlEncryptionKeyInfo sqlEncryptionKeyInfo in sqlTceCipherInfoEntry.ColumnEncryptionKeyValues)
			{
				try
				{
					sqlClientSymmetricKey = (SqlSecurityUtility.ShouldUseInstanceLevelProviderFlow(sqlEncryptionKeyInfo.keyStoreName, connection, command) ? SqlSecurityUtility.GetKeyFromLocalProviders(sqlEncryptionKeyInfo, connection, command) : instance.GetKey(sqlEncryptionKeyInfo, connection, command));
					encryptionkeyInfoChosen = sqlEncryptionKeyInfo;
					break;
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
			}
			if (sqlClientSymmetricKey == null)
			{
				throw ex;
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00025294 File Offset: 0x00023494
		private static SqlClientSymmetricKey GetKeyFromLocalProviders(SqlEncryptionKeyInfo keyInfo, SqlConnection connection, SqlCommand command)
		{
			string dataSource = connection.DataSource;
			SqlSecurityUtility.ThrowIfKeyPathIsNotTrustedForServer(dataSource, keyInfo.keyPath);
			SqlColumnEncryptionKeyStoreProvider sqlColumnEncryptionKeyStoreProvider;
			if (!SqlSecurityUtility.TryGetColumnEncryptionKeyStoreProvider(keyInfo.keyStoreName, out sqlColumnEncryptionKeyStoreProvider, connection, command))
			{
				throw SQL.UnrecognizedKeyStoreProviderName(keyInfo.keyStoreName, SqlConnection.GetColumnEncryptionSystemKeyStoreProvidersNames(), SqlSecurityUtility.GetListOfProviderNamesThatWereSearched(connection, command));
			}
			byte[] array;
			try
			{
				array = sqlColumnEncryptionKeyStoreProvider.DecryptColumnEncryptionKey(keyInfo.keyPath, keyInfo.algorithmName, keyInfo.encryptedKey);
			}
			catch (Exception ex)
			{
				string bytesAsString = SqlSecurityUtility.GetBytesAsString(keyInfo.encryptedKey, true, 10);
				throw SQL.KeyDecryptionFailed(keyInfo.keyStoreName, bytesAsString, ex);
			}
			return new SqlClientSymmetricKey(array);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00025330 File Offset: 0x00023530
		internal static int GetBase64LengthFromByteLength(int byteLength)
		{
			return (int)((double)byteLength * 4.0 / 3.0) + 4;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002534C File Offset: 0x0002354C
		internal static void VerifyColumnMasterKeySignature(string keyStoreName, string keyPath, bool isEnclaveEnabled, byte[] CMKSignature, SqlConnection connection, SqlCommand command)
		{
			bool flag = false;
			try
			{
				if (CMKSignature == null || CMKSignature.Length == 0)
				{
					throw SQL.ColumnMasterKeySignatureNotFound(keyPath);
				}
				SqlSecurityUtility.ThrowIfKeyPathIsNotTrustedForServer(connection.DataSource, keyPath);
				SqlColumnEncryptionKeyStoreProvider sqlColumnEncryptionKeyStoreProvider;
				if (!SqlSecurityUtility.TryGetColumnEncryptionKeyStoreProvider(keyStoreName, out sqlColumnEncryptionKeyStoreProvider, connection, command))
				{
					throw SQL.InvalidKeyStoreProviderName(keyStoreName, SqlConnection.GetColumnEncryptionSystemKeyStoreProvidersNames(), SqlSecurityUtility.GetListOfProviderNamesThatWereSearched(connection, command));
				}
				if (SqlSecurityUtility.ShouldUseInstanceLevelProviderFlow(keyStoreName, connection, command))
				{
					flag = sqlColumnEncryptionKeyStoreProvider.VerifyColumnMasterKeyMetadata(keyPath, isEnclaveEnabled, CMKSignature);
				}
				else
				{
					bool? signatureVerificationResult = SqlSecurityUtility.ColumnMasterKeyMetadataSignatureVerificationCache.GetSignatureVerificationResult(keyStoreName, keyPath, isEnclaveEnabled, CMKSignature);
					if (signatureVerificationResult == null)
					{
						flag = sqlColumnEncryptionKeyStoreProvider.VerifyColumnMasterKeyMetadata(keyPath, isEnclaveEnabled, CMKSignature);
						SqlSecurityUtility.ColumnMasterKeyMetadataSignatureVerificationCache.AddSignatureVerificationResult(keyStoreName, keyPath, isEnclaveEnabled, CMKSignature, flag);
					}
					else
					{
						flag = signatureVerificationResult.Value;
					}
				}
			}
			catch (Exception ex)
			{
				throw SQL.UnableToVerifyColumnMasterKeySignature(ex);
			}
			if (!flag)
			{
				throw SQL.ColumnMasterKeySignatureVerificationFailed(keyPath);
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00025410 File Offset: 0x00023610
		private static bool ShouldUseInstanceLevelProviderFlow(string keyStoreName, SqlConnection connection, SqlCommand command)
		{
			return SqlSecurityUtility.InstanceLevelProvidersAreRegistered(connection, command) && !keyStoreName.StartsWith("MSSQL_");
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002542B File Offset: 0x0002362B
		private static bool InstanceLevelProvidersAreRegistered(SqlConnection connection, SqlCommand command)
		{
			return connection.HasColumnEncryptionKeyStoreProvidersRegistered || (command != null && command.HasColumnEncryptionKeyStoreProvidersRegistered);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00025444 File Offset: 0x00023644
		internal static void ThrowIfKeyPathIsNotTrustedForServer(string serverName, string keyPath)
		{
			IList<string> list;
			if (SqlConnection.ColumnEncryptionTrustedMasterKeyPaths.TryGetValue(serverName, out list) && (list == null || list.Count<string>() == 0 || !list.Any((string trustedKeyPath) => trustedKeyPath.Equals(keyPath, StringComparison.InvariantCultureIgnoreCase))))
			{
				throw SQL.UntrustedKeyPath(keyPath, serverName);
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00025499 File Offset: 0x00023699
		internal static bool TryGetColumnEncryptionKeyStoreProvider(string keyStoreName, out SqlColumnEncryptionKeyStoreProvider provider, SqlConnection connection, SqlCommand command)
		{
			if (SqlConnection.TryGetSystemColumnEncryptionKeyStoreProvider(keyStoreName, out provider))
			{
				return true;
			}
			if (command != null && command.HasColumnEncryptionKeyStoreProvidersRegistered)
			{
				return command.TryGetColumnEncryptionKeyStoreProvider(keyStoreName, out provider);
			}
			return connection.TryGetColumnEncryptionKeyStoreProvider(keyStoreName, out provider);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x000254C2 File Offset: 0x000236C2
		internal static List<string> GetListOfProviderNamesThatWereSearched(SqlConnection connection, SqlCommand command)
		{
			if (command != null && command.HasColumnEncryptionKeyStoreProvidersRegistered)
			{
				return command.GetColumnEncryptionCustomKeyStoreProvidersNames();
			}
			return connection.GetColumnEncryptionCustomKeyStoreProvidersNames();
		}

		// Token: 0x04000326 RID: 806
		private static readonly ColumnMasterKeyMetadataSignatureVerificationCache ColumnMasterKeyMetadataSignatureVerificationCache = ColumnMasterKeyMetadataSignatureVerificationCache.Instance;
	}
}
