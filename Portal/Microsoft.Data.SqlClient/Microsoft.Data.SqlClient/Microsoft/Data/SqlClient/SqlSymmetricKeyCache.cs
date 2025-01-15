using System;
using System.Runtime.Caching;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000A2 RID: 162
	internal sealed class SqlSymmetricKeyCache
	{
		// Token: 0x06000CC0 RID: 3264 RVA: 0x00026E2A File Offset: 0x0002502A
		private SqlSymmetricKeyCache()
		{
			this._cache = new MemoryCache("ColumnEncryptionKeyCache", null);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00026E43 File Offset: 0x00025043
		internal static SqlSymmetricKeyCache GetInstance()
		{
			return SqlSymmetricKeyCache._singletonInstance;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00026E4C File Offset: 0x0002504C
		internal SqlClientSymmetricKey GetKey(SqlEncryptionKeyInfo keyInfo, SqlConnection connection, SqlCommand command)
		{
			string dataSource = connection.DataSource;
			StringBuilder stringBuilder = new StringBuilder(dataSource, dataSource.Length + SqlSecurityUtility.GetBase64LengthFromByteLength(keyInfo.encryptedKey.Length) + keyInfo.keyStoreName.Length + 2);
			stringBuilder.Append(":");
			stringBuilder.Append(Convert.ToBase64String(keyInfo.encryptedKey));
			stringBuilder.Append(":");
			stringBuilder.Append(keyInfo.keyStoreName);
			string text = stringBuilder.ToString();
			SqlClientSymmetricKey sqlClientSymmetricKey = this._cache.Get(text, null) as SqlClientSymmetricKey;
			if (sqlClientSymmetricKey == null)
			{
				SqlSecurityUtility.ThrowIfKeyPathIsNotTrustedForServer(dataSource, keyInfo.keyPath);
				SqlColumnEncryptionKeyStoreProvider sqlColumnEncryptionKeyStoreProvider;
				if (!SqlSecurityUtility.TryGetColumnEncryptionKeyStoreProvider(keyInfo.keyStoreName, out sqlColumnEncryptionKeyStoreProvider, connection, command))
				{
					throw SQL.UnrecognizedKeyStoreProviderName(keyInfo.keyStoreName, SqlConnection.GetColumnEncryptionSystemKeyStoreProvidersNames(), SqlSecurityUtility.GetListOfProviderNamesThatWereSearched(connection, command));
				}
				byte[] array;
				try
				{
					sqlColumnEncryptionKeyStoreProvider.ColumnEncryptionKeyCacheTtl = new TimeSpan?(new TimeSpan(0L));
					array = sqlColumnEncryptionKeyStoreProvider.DecryptColumnEncryptionKey(keyInfo.keyPath, keyInfo.algorithmName, keyInfo.encryptedKey);
				}
				catch (Exception ex)
				{
					string bytesAsString = SqlSecurityUtility.GetBytesAsString(keyInfo.encryptedKey, true, 10);
					throw SQL.KeyDecryptionFailed(keyInfo.keyStoreName, bytesAsString, ex);
				}
				sqlClientSymmetricKey = new SqlClientSymmetricKey(array);
				if (SqlConnection.ColumnEncryptionKeyCacheTtl != TimeSpan.Zero)
				{
					DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow.Add(SqlConnection.ColumnEncryptionKeyCacheTtl);
					this._cache.Add(text, sqlClientSymmetricKey, dateTimeOffset, null);
				}
			}
			return sqlClientSymmetricKey;
		}

		// Token: 0x04000362 RID: 866
		private readonly MemoryCache _cache;

		// Token: 0x04000363 RID: 867
		private static readonly SqlSymmetricKeyCache _singletonInstance = new SqlSymmetricKeyCache();
	}
}
