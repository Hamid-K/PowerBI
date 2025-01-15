using System;
using System.Collections.Concurrent;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000059 RID: 89
	internal sealed class SqlClientEncryptionAlgorithmFactoryList
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x0001336C File Offset: 0x0001156C
		private SqlClientEncryptionAlgorithmFactoryList()
		{
			this._encryptionAlgoFactoryList = new ConcurrentDictionary<string, SqlClientEncryptionAlgorithmFactory>(4 * Environment.ProcessorCount, 2);
			this._encryptionAlgoFactoryList.TryAdd("AEAD_AES_256_CBC_HMAC_SHA256", new SqlAeadAes256CbcHmac256Factory());
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001339D File Offset: 0x0001159D
		internal static SqlClientEncryptionAlgorithmFactoryList GetInstance()
		{
			return SqlClientEncryptionAlgorithmFactoryList._singletonInstance;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000133A4 File Offset: 0x000115A4
		internal string GetRegisteredCipherAlgorithmNames()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string text in this._encryptionAlgoFactoryList.Keys)
			{
				if (flag)
				{
					stringBuilder.Append("'");
					flag = false;
				}
				else
				{
					stringBuilder.Append(", '");
				}
				stringBuilder.Append(text);
				stringBuilder.Append("'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00013430 File Offset: 0x00011630
		internal void GetAlgorithm(SqlClientSymmetricKey key, byte type, string algorithmName, out SqlClientEncryptionAlgorithm encryptionAlgorithm)
		{
			encryptionAlgorithm = null;
			SqlClientEncryptionAlgorithmFactory sqlClientEncryptionAlgorithmFactory = null;
			if (!this._encryptionAlgoFactoryList.TryGetValue(algorithmName, out sqlClientEncryptionAlgorithmFactory))
			{
				throw SQL.UnknownColumnEncryptionAlgorithm(algorithmName, SqlClientEncryptionAlgorithmFactoryList.GetInstance().GetRegisteredCipherAlgorithmNames());
			}
			encryptionAlgorithm = sqlClientEncryptionAlgorithmFactory.Create(key, (SqlClientEncryptionType)type, algorithmName);
		}

		// Token: 0x0400013A RID: 314
		private readonly ConcurrentDictionary<string, SqlClientEncryptionAlgorithmFactory> _encryptionAlgoFactoryList;

		// Token: 0x0400013B RID: 315
		private static readonly SqlClientEncryptionAlgorithmFactoryList _singletonInstance = new SqlClientEncryptionAlgorithmFactoryList();
	}
}
