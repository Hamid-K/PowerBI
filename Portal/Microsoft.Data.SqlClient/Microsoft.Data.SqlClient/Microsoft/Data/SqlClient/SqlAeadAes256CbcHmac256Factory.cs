using System;
using System.Collections.Concurrent;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200004C RID: 76
	internal class SqlAeadAes256CbcHmac256Factory : SqlClientEncryptionAlgorithmFactory
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		internal override SqlClientEncryptionAlgorithm Create(SqlClientSymmetricKey encryptionKey, SqlClientEncryptionType encryptionType, string encryptionAlgorithm)
		{
			if (encryptionType != SqlClientEncryptionType.Deterministic && encryptionType != SqlClientEncryptionType.Randomized)
			{
				throw SQL.InvalidEncryptionType("AEAD_AES_256_CBC_HMAC_SHA256", encryptionType, new SqlClientEncryptionType[]
				{
					SqlClientEncryptionType.Deterministic,
					SqlClientEncryptionType.Randomized
				});
			}
			StringBuilder stringBuilder = new StringBuilder(Convert.ToBase64String(encryptionKey.RootKey), SqlSecurityUtility.GetBase64LengthFromByteLength(encryptionKey.RootKey.Length) + 4);
			stringBuilder.Append(":");
			stringBuilder.Append((int)encryptionType);
			stringBuilder.Append(":");
			stringBuilder.Append(1);
			string text = stringBuilder.ToString();
			SqlAeadAes256CbcHmac256Algorithm sqlAeadAes256CbcHmac256Algorithm;
			if (!this._encryptionAlgorithms.TryGetValue(text, out sqlAeadAes256CbcHmac256Algorithm))
			{
				SqlAeadAes256CbcHmac256EncryptionKey sqlAeadAes256CbcHmac256EncryptionKey = new SqlAeadAes256CbcHmac256EncryptionKey(encryptionKey.RootKey, "AEAD_AES_256_CBC_HMAC_SHA256");
				sqlAeadAes256CbcHmac256Algorithm = new SqlAeadAes256CbcHmac256Algorithm(sqlAeadAes256CbcHmac256EncryptionKey, encryptionType, 1);
				this._encryptionAlgorithms.TryAdd(text, sqlAeadAes256CbcHmac256Algorithm);
			}
			return sqlAeadAes256CbcHmac256Algorithm;
		}

		// Token: 0x0400010D RID: 269
		private readonly ConcurrentDictionary<string, SqlAeadAes256CbcHmac256Algorithm> _encryptionAlgorithms = new ConcurrentDictionary<string, SqlAeadAes256CbcHmac256Algorithm>(4 * Environment.ProcessorCount, 2);
	}
}
