using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000028 RID: 40
	internal sealed class EnclaveDelegate
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0000D6BA File Offset: 0x0000B8BA
		public static EnclaveDelegate Instance
		{
			get
			{
				return EnclaveDelegate.s_enclaveDelegate;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000D6C1 File Offset: 0x0000B8C1
		private EnclaveDelegate()
		{
			this._lock = new object();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		private byte[] GetUintBytes(string enclaveType, int intValue, string variableName)
		{
			byte[] bytes;
			try
			{
				uint num = Convert.ToUInt32(intValue);
				bytes = BitConverter.GetBytes(num);
			}
			catch (Exception ex)
			{
				throw SQL.InvalidAttestationParameterUnableToConvertToUnsignedInt(variableName, intValue, enclaveType, ex);
			}
			return bytes;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000D710 File Offset: 0x0000B910
		private List<ColumnEncryptionKeyInfo> GetDecryptedKeysToBeSentToEnclave(ConcurrentDictionary<int, SqlTceCipherInfoEntry> keysTobeSentToEnclave, string serverName, SqlConnection connection, SqlCommand command)
		{
			List<ColumnEncryptionKeyInfo> list = new List<ColumnEncryptionKeyInfo>();
			foreach (SqlTceCipherInfoEntry sqlTceCipherInfoEntry in keysTobeSentToEnclave.Values)
			{
				SqlClientSymmetricKey sqlClientSymmetricKey;
				SqlEncryptionKeyInfo sqlEncryptionKeyInfo;
				SqlSecurityUtility.DecryptSymmetricKey(sqlTceCipherInfoEntry, out sqlClientSymmetricKey, out sqlEncryptionKeyInfo, connection, command);
				if (sqlClientSymmetricKey == null)
				{
					throw SQL.NullArgumentInternal("sqlClientSymmetricKey", "EnclaveDelegate", "GetDecryptedKeysToBeSentToEnclave");
				}
				if (sqlTceCipherInfoEntry.ColumnEncryptionKeyValues == null)
				{
					throw SQL.NullArgumentInternal("ColumnEncryptionKeyValues", "EnclaveDelegate", "GetDecryptedKeysToBeSentToEnclave");
				}
				if (sqlTceCipherInfoEntry.ColumnEncryptionKeyValues.Count <= 0)
				{
					throw SQL.ColumnEncryptionKeysNotFound();
				}
				list.Add(new ColumnEncryptionKeyInfo(sqlClientSymmetricKey.RootKey, sqlTceCipherInfoEntry.ColumnEncryptionKeyValues[0].databaseId, sqlTceCipherInfoEntry.ColumnEncryptionKeyValues[0].cekMdVersion, sqlTceCipherInfoEntry.ColumnEncryptionKeyValues[0].cekId));
			}
			return list;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000D800 File Offset: 0x0000BA00
		private byte[] GenerateBytePackageForKeys(long enclaveSessionCounter, byte[] queryStringHashBytes, List<ColumnEncryptionKeyInfo> keys)
		{
			byte[] array = Guid.NewGuid().ToByteArray();
			byte[] bytes = BitConverter.GetBytes(enclaveSessionCounter);
			int num = array.Length;
			num += bytes.Length;
			num += queryStringHashBytes.Length;
			foreach (ColumnEncryptionKeyInfo columnEncryptionKeyInfo in keys)
			{
				num += columnEncryptionKeyInfo.GetLengthForSerialization();
			}
			byte[] array2 = new byte[num];
			int num2 = 0;
			Buffer.BlockCopy(array, 0, array2, num2, array.Length);
			num2 += array.Length;
			Buffer.BlockCopy(bytes, 0, array2, num2, bytes.Length);
			num2 += bytes.Length;
			Buffer.BlockCopy(queryStringHashBytes, 0, array2, num2, queryStringHashBytes.Length);
			num2 += queryStringHashBytes.Length;
			foreach (ColumnEncryptionKeyInfo columnEncryptionKeyInfo2 in keys)
			{
				num2 = columnEncryptionKeyInfo2.SerializeToBuffer(array2, num2);
			}
			return array2;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000D90C File Offset: 0x0000BB0C
		private byte[] EncryptBytePackage(byte[] bytePackage, byte[] sessionKey, string serverName)
		{
			if (sessionKey == null)
			{
				throw SQL.NullArgumentInternal("sessionKey", "EnclaveDelegate", "EncryptBytePackage");
			}
			if (sessionKey.Length == 0)
			{
				throw SQL.EmptyArgumentInternal("sessionKey", "EnclaveDelegate", "EncryptBytePackage");
			}
			byte[] array;
			try
			{
				SqlClientSymmetricKey sqlClientSymmetricKey = new SqlClientSymmetricKey(sessionKey);
				SqlClientEncryptionAlgorithm sqlClientEncryptionAlgorithm = EnclaveDelegate.s_sqlAeadAes256CbcHmac256Factory.Create(sqlClientSymmetricKey, SqlClientEncryptionType.Randomized, "AEAD_AES_256_CBC_HMAC_SHA256");
				array = sqlClientEncryptionAlgorithm.EncryptData(bytePackage);
			}
			catch (Exception ex)
			{
				throw SQL.FailedToEncryptRegisterRulesBytePackage(ex);
			}
			return array;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000D988 File Offset: 0x0000BB88
		private byte[] CombineByteArrays(byte[] arr1, byte[] arr2)
		{
			int num = arr1.Length + arr2.Length;
			byte[] array = new byte[num];
			Buffer.BlockCopy(arr1, 0, array, 0, arr1.Length);
			Buffer.BlockCopy(arr2, 0, array, arr1.Length, arr2.Length);
			return array;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		private byte[] CombineByteArrays(byte[] arr1, byte[] arr2, byte[] arr3, byte[] arr4, byte[] arr5)
		{
			int num = arr1.Length + arr2.Length + arr3.Length + arr4.Length + arr5.Length;
			byte[] array = new byte[num];
			Buffer.BlockCopy(arr1, 0, array, 0, arr1.Length);
			int num2 = arr1.Length;
			Buffer.BlockCopy(arr2, 0, array, num2, arr2.Length);
			num2 += arr2.Length;
			Buffer.BlockCopy(arr3, 0, array, num2, arr3.Length);
			num2 += arr3.Length;
			Buffer.BlockCopy(arr4, 0, array, num2, arr4.Length);
			num2 += arr4.Length;
			Buffer.BlockCopy(arr5, 0, array, num2, arr5.Length);
			return array;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000DA44 File Offset: 0x0000BC44
		private byte[] ComputeQueryStringHash(string queryString)
		{
			if (!string.IsNullOrWhiteSpace(queryString))
			{
				byte[] bytes = Encoding.Unicode.GetBytes(queryString);
				byte[] hash;
				using (SHA256 sha = SHA256.Create())
				{
					sha.TransformFinalBlock(bytes, 0, bytes.Length);
					hash = sha.Hash;
				}
				return hash;
			}
			if (queryString == null)
			{
				throw SQL.NullArgumentInternal("queryString", "EnclaveDelegate", "ComputeQueryStringHash");
			}
			throw SQL.EmptyArgumentInternal("queryString", "EnclaveDelegate", "ComputeQueryStringHash");
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		internal void CreateEnclaveSession(SqlConnectionAttestationProtocol attestationProtocol, string enclaveType, EnclaveSessionParameters enclaveSessionParameters, byte[] attestationInfo, SqlEnclaveAttestationParameters attestationParameters, byte[] customData, int customDataLength, bool isRetry)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				SqlColumnEncryptionEnclaveProvider enclaveProvider = this.GetEnclaveProvider(attestationProtocol, enclaveType);
				SqlEnclaveSession sqlEnclaveSession;
				long num;
				byte[] array;
				int num2;
				enclaveProvider.GetEnclaveSession(enclaveSessionParameters, false, isRetry, out sqlEnclaveSession, out num, out array, out num2);
				if (sqlEnclaveSession == null)
				{
					enclaveProvider.CreateEnclaveSession(attestationInfo, attestationParameters.ClientDiffieHellmanKey, enclaveSessionParameters, customData, customDataLength, out sqlEnclaveSession, out num);
					if (sqlEnclaveSession == null)
					{
						throw SQL.NullEnclaveSessionReturnedFromProvider(enclaveType, enclaveSessionParameters.AttestationUrl);
					}
				}
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		internal void GetEnclaveSession(SqlConnectionAttestationProtocol attestationProtocol, string enclaveType, EnclaveSessionParameters enclaveSessionParameters, bool generateCustomData, bool isRetry, out SqlEnclaveSession sqlEnclaveSession, out byte[] customData, out int customDataLength)
		{
			long num;
			this.GetEnclaveSession(attestationProtocol, enclaveType, enclaveSessionParameters, generateCustomData, isRetry, out sqlEnclaveSession, out num, out customData, out customDataLength, false);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000DB70 File Offset: 0x0000BD70
		private void GetEnclaveSession(SqlConnectionAttestationProtocol attestationProtocol, string enclaveType, EnclaveSessionParameters enclaveSessionParameters, bool generateCustomData, bool isRetry, out SqlEnclaveSession sqlEnclaveSession, out long counter, out byte[] customData, out int customDataLength, bool throwIfNull)
		{
			SqlColumnEncryptionEnclaveProvider enclaveProvider = this.GetEnclaveProvider(attestationProtocol, enclaveType);
			enclaveProvider.GetEnclaveSession(enclaveSessionParameters, generateCustomData, isRetry, out sqlEnclaveSession, out counter, out customData, out customDataLength);
			if (throwIfNull && sqlEnclaveSession == null)
			{
				throw SQL.NullEnclaveSessionDuringQueryExecution(enclaveType, enclaveSessionParameters.AttestationUrl);
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		internal void InvalidateEnclaveSession(SqlConnectionAttestationProtocol attestationProtocol, string enclaveType, EnclaveSessionParameters enclaveSessionParameters, SqlEnclaveSession enclaveSession)
		{
			SqlColumnEncryptionEnclaveProvider enclaveProvider = this.GetEnclaveProvider(attestationProtocol, enclaveType);
			enclaveProvider.InvalidateEnclaveSession(enclaveSessionParameters, enclaveSession);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		private SqlColumnEncryptionEnclaveProvider GetEnclaveProvider(SqlConnectionAttestationProtocol attestationProtocol, string enclaveType)
		{
			SqlColumnEncryptionEnclaveProvider sqlColumnEncryptionEnclaveProvider;
			if (!EnclaveDelegate.s_enclaveProviders.TryGetValue(attestationProtocol, out sqlColumnEncryptionEnclaveProvider))
			{
				switch (attestationProtocol)
				{
				case SqlConnectionAttestationProtocol.AAS:
				{
					AzureAttestationEnclaveProvider azureAttestationEnclaveProvider = new AzureAttestationEnclaveProvider();
					EnclaveDelegate.s_enclaveProviders[attestationProtocol] = azureAttestationEnclaveProvider;
					sqlColumnEncryptionEnclaveProvider = EnclaveDelegate.s_enclaveProviders[attestationProtocol];
					break;
				}
				case SqlConnectionAttestationProtocol.None:
				{
					NoneAttestationEnclaveProvider noneAttestationEnclaveProvider = new NoneAttestationEnclaveProvider();
					EnclaveDelegate.s_enclaveProviders[attestationProtocol] = noneAttestationEnclaveProvider;
					sqlColumnEncryptionEnclaveProvider = EnclaveDelegate.s_enclaveProviders[attestationProtocol];
					break;
				}
				case SqlConnectionAttestationProtocol.HGS:
				{
					HostGuardianServiceEnclaveProvider hostGuardianServiceEnclaveProvider = new HostGuardianServiceEnclaveProvider();
					EnclaveDelegate.s_enclaveProviders[attestationProtocol] = hostGuardianServiceEnclaveProvider;
					sqlColumnEncryptionEnclaveProvider = EnclaveDelegate.s_enclaveProviders[attestationProtocol];
					break;
				}
				}
			}
			if (sqlColumnEncryptionEnclaveProvider == null)
			{
				throw SQL.EnclaveProviderNotFound(enclaveType, attestationProtocol.ToString());
			}
			return sqlColumnEncryptionEnclaveProvider;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000DC78 File Offset: 0x0000BE78
		internal EnclavePackage GenerateEnclavePackage(SqlConnectionAttestationProtocol attestationProtocol, ConcurrentDictionary<int, SqlTceCipherInfoEntry> keysToBeSentToEnclave, string queryText, string enclaveType, EnclaveSessionParameters enclaveSessionParameters, SqlConnection connection, SqlCommand command)
		{
			SqlEnclaveSession sqlEnclaveSession;
			long num;
			try
			{
				byte[] array;
				int num2;
				this.GetEnclaveSession(attestationProtocol, enclaveType, enclaveSessionParameters, false, false, out sqlEnclaveSession, out num, out array, out num2, true);
			}
			catch (Exception ex)
			{
				throw new EnclaveDelegate.RetryableEnclaveQueryExecutionException(ex.Message, ex);
			}
			List<ColumnEncryptionKeyInfo> decryptedKeysToBeSentToEnclave = this.GetDecryptedKeysToBeSentToEnclave(keysToBeSentToEnclave, enclaveSessionParameters.ServerName, connection, command);
			byte[] array2 = this.ComputeQueryStringHash(queryText);
			byte[] array3 = this.GenerateBytePackageForKeys(num, array2, decryptedKeysToBeSentToEnclave);
			byte[] sessionKey = sqlEnclaveSession.GetSessionKey();
			byte[] array4 = this.EncryptBytePackage(array3, sessionKey, enclaveSessionParameters.ServerName);
			byte[] bytes = BitConverter.GetBytes(sqlEnclaveSession.SessionId);
			byte[] array5 = this.CombineByteArrays(bytes, array4);
			return new EnclavePackage(array5, sqlEnclaveSession);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000DD20 File Offset: 0x0000BF20
		internal SqlEnclaveAttestationParameters GetAttestationParameters(SqlConnectionAttestationProtocol attestationProtocol, string enclaveType, string attestationUrl, byte[] customData, int customDataLength)
		{
			SqlColumnEncryptionEnclaveProvider enclaveProvider = this.GetEnclaveProvider(attestationProtocol, enclaveType);
			return enclaveProvider.GetAttestationParameters(attestationUrl, customData, customDataLength);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000DD44 File Offset: 0x0000BF44
		internal byte[] GetSerializedAttestationParameters(SqlEnclaveAttestationParameters sqlEnclaveAttestationParameters, string enclaveType)
		{
			int protocol = sqlEnclaveAttestationParameters.Protocol;
			byte[] uintBytes = this.GetUintBytes(enclaveType, protocol, "attestationProtocol");
			if (uintBytes == null)
			{
				throw SQL.NullArgumentInternal("attestationProtocolBytes", "EnclaveDelegate", "GetSerializedAttestationParameters");
			}
			byte[] input = sqlEnclaveAttestationParameters.GetInput();
			byte[] uintBytes2 = this.GetUintBytes(enclaveType, input.Length, "attestationProtocolInputLength");
			if (uintBytes2 == null)
			{
				throw SQL.NullArgumentInternal("attestationProtocolInputLengthBytes", "EnclaveDelegate", "GetSerializedAttestationParameters");
			}
			byte[] ecdiffieHellmanPublicKeyBlob = KeyConverter.GetECDiffieHellmanPublicKeyBlob(sqlEnclaveAttestationParameters.ClientDiffieHellmanKey);
			byte[] uintBytes3 = this.GetUintBytes(enclaveType, ecdiffieHellmanPublicKeyBlob.Length, "clientDHPublicKeyLength");
			if (uintBytes3 == null)
			{
				throw SQL.NullArgumentInternal("clientDHPublicKeyLengthBytes", "EnclaveDelegate", "GetSerializedAttestationParameters");
			}
			return this.CombineByteArrays(uintBytes, uintBytes2, input, uintBytes3, ecdiffieHellmanPublicKeyBlob);
		}

		// Token: 0x04000089 RID: 137
		private static readonly SqlAeadAes256CbcHmac256Factory s_sqlAeadAes256CbcHmac256Factory = new SqlAeadAes256CbcHmac256Factory();

		// Token: 0x0400008A RID: 138
		private static readonly EnclaveDelegate s_enclaveDelegate = new EnclaveDelegate();

		// Token: 0x0400008B RID: 139
		private readonly object _lock;

		// Token: 0x0400008C RID: 140
		private static readonly ConcurrentDictionary<SqlConnectionAttestationProtocol, SqlColumnEncryptionEnclaveProvider> s_enclaveProviders = new ConcurrentDictionary<SqlConnectionAttestationProtocol, SqlColumnEncryptionEnclaveProvider>();

		// Token: 0x020001A8 RID: 424
		internal class RetryableEnclaveQueryExecutionException : Exception
		{
			// Token: 0x06001D78 RID: 7544 RVA: 0x00079A17 File Offset: 0x00077C17
			internal RetryableEnclaveQueryExecutionException(string message, Exception innerException)
				: base(message, innerException)
			{
			}
		}
	}
}
