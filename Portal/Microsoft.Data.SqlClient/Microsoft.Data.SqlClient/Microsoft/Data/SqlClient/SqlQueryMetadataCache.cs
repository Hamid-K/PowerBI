using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000093 RID: 147
	internal sealed class SqlQueryMetadataCache
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x000246B0 File Offset: 0x000228B0
		private SqlQueryMetadataCache()
		{
			this._cache = new MemoryCache("SqlQueryMetadataCache", null);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000246C9 File Offset: 0x000228C9
		internal static SqlQueryMetadataCache GetInstance()
		{
			return SqlQueryMetadataCache.s_singletonInstance;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000246D0 File Offset: 0x000228D0
		internal bool GetQueryMetadataIfExists(SqlCommand sqlCommand)
		{
			if (!SqlConnection.ColumnEncryptionQueryMetadataCacheEnabled)
			{
				return false;
			}
			global::System.ValueTuple<string, string> cacheLookupKeysFromSqlCommand = this.GetCacheLookupKeysFromSqlCommand(sqlCommand);
			string item = cacheLookupKeysFromSqlCommand.Item1;
			string item2 = cacheLookupKeysFromSqlCommand.Item2;
			if (item == null)
			{
				this.IncrementCacheMisses();
				return false;
			}
			Dictionary<string, SqlCipherMetadata> dictionary = this._cache.Get(item, null) as Dictionary<string, SqlCipherMetadata>;
			if (dictionary == null)
			{
				this.IncrementCacheMisses();
				return false;
			}
			foreach (object obj in sqlCommand.Parameters)
			{
				SqlParameter sqlParameter = (SqlParameter)obj;
				SqlCipherMetadata sqlCipherMetadata;
				if (!dictionary.TryGetValue(sqlParameter.ParameterNameFixed, out sqlCipherMetadata))
				{
					foreach (object obj2 in sqlCommand.Parameters)
					{
						SqlParameter sqlParameter2 = (SqlParameter)obj2;
						sqlParameter2.CipherMetadata = null;
					}
					this.IncrementCacheMisses();
					return false;
				}
				sqlParameter.CipherMetadata = sqlCipherMetadata;
			}
			foreach (object obj3 in sqlCommand.Parameters)
			{
				SqlParameter sqlParameter3 = (SqlParameter)obj3;
				SqlCipherMetadata sqlCipherMetadata2 = null;
				if (sqlParameter3.CipherMetadata != null)
				{
					sqlCipherMetadata2 = new SqlCipherMetadata(sqlParameter3.CipherMetadata.EncryptionInfo, 0, sqlParameter3.CipherMetadata.CipherAlgorithmId, sqlParameter3.CipherMetadata.CipherAlgorithmName, sqlParameter3.CipherMetadata.EncryptionType, sqlParameter3.CipherMetadata.NormalizationRuleVersion);
				}
				sqlParameter3.CipherMetadata = sqlCipherMetadata2;
				if (sqlCipherMetadata2 != null)
				{
					try
					{
						SqlSecurityUtility.DecryptSymmetricKey(sqlCipherMetadata2, sqlCommand.Connection, sqlCommand);
					}
					catch (Exception ex)
					{
						this.InvalidateCacheEntry(sqlCommand);
						if (ex is SqlException || ex is ArgumentException || ex is ArgumentNullException)
						{
							foreach (object obj4 in sqlCommand.Parameters)
							{
								SqlParameter sqlParameter4 = (SqlParameter)obj4;
								sqlParameter4.CipherMetadata = null;
							}
							this.IncrementCacheMisses();
							return false;
						}
						throw;
					}
				}
			}
			ConcurrentDictionary<int, SqlTceCipherInfoEntry> concurrentDictionary = this._cache.Get(item2, null) as ConcurrentDictionary<int, SqlTceCipherInfoEntry>;
			if (concurrentDictionary != null)
			{
				sqlCommand.keysToBeSentToEnclave = this.CreateCopyOfEnclaveKeys(concurrentDictionary);
			}
			this.IncrementCacheHits();
			return true;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000249A8 File Offset: 0x00022BA8
		internal void AddQueryMetadata(SqlCommand sqlCommand, bool ignoreQueriesWithReturnValueParams)
		{
			if (!SqlConnection.ColumnEncryptionQueryMetadataCacheEnabled)
			{
				return;
			}
			if (sqlCommand.CommandType == CommandType.StoredProcedure)
			{
				foreach (object obj in sqlCommand.Parameters)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					if (sqlParameter.Direction == ParameterDirection.ReturnValue && ignoreQueriesWithReturnValueParams)
					{
						sqlCommand.CachingQueryMetadataPostponed = true;
						return;
					}
				}
			}
			global::System.ValueTuple<string, string> cacheLookupKeysFromSqlCommand = this.GetCacheLookupKeysFromSqlCommand(sqlCommand);
			string item = cacheLookupKeysFromSqlCommand.Item1;
			string item2 = cacheLookupKeysFromSqlCommand.Item2;
			if (item == null)
			{
				return;
			}
			Dictionary<string, SqlCipherMetadata> dictionary = new Dictionary<string, SqlCipherMetadata>(sqlCommand.Parameters.Count);
			foreach (object obj2 in sqlCommand.Parameters)
			{
				SqlParameter sqlParameter2 = (SqlParameter)obj2;
				SqlCipherMetadata sqlCipherMetadata = null;
				if (sqlParameter2.CipherMetadata != null)
				{
					sqlCipherMetadata = new SqlCipherMetadata(sqlParameter2.CipherMetadata.EncryptionInfo, 0, sqlParameter2.CipherMetadata.CipherAlgorithmId, sqlParameter2.CipherMetadata.CipherAlgorithmName, sqlParameter2.CipherMetadata.EncryptionType, sqlParameter2.CipherMetadata.NormalizationRuleVersion);
				}
				dictionary.Add(sqlParameter2.ParameterNameFixed, sqlCipherMetadata);
			}
			long count = this._cache.GetCount(null);
			if (count > 2300L && Interlocked.CompareExchange(ref this._inTrim, 1, 0) == 0)
			{
				try
				{
					this._cache.Trim((int)((double)(count - 2000L) / (double)count * 100.0));
				}
				finally
				{
					Interlocked.CompareExchange(ref this._inTrim, 0, 1);
				}
			}
			this._cache.Set(item, dictionary, DateTimeOffset.UtcNow.AddHours(10.0), null);
			if (sqlCommand.requiresEnclaveComputations)
			{
				ConcurrentDictionary<int, SqlTceCipherInfoEntry> concurrentDictionary = this.CreateCopyOfEnclaveKeys(sqlCommand.keysToBeSentToEnclave);
				this._cache.Set(item2, concurrentDictionary, DateTimeOffset.UtcNow.AddHours(10.0), null);
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00024BC4 File Offset: 0x00022DC4
		internal void InvalidateCacheEntry(SqlCommand sqlCommand)
		{
			global::System.ValueTuple<string, string> cacheLookupKeysFromSqlCommand = this.GetCacheLookupKeysFromSqlCommand(sqlCommand);
			string item = cacheLookupKeysFromSqlCommand.Item1;
			string item2 = cacheLookupKeysFromSqlCommand.Item2;
			if (item == null)
			{
				return;
			}
			this._cache.Remove(item, null);
			this._cache.Remove(item2, null);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00024C05 File Offset: 0x00022E05
		private void IncrementCacheHits()
		{
			Interlocked.Increment(ref this._cacheHits);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00024C13 File Offset: 0x00022E13
		private void IncrementCacheMisses()
		{
			Interlocked.Increment(ref this._cacheMisses);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00024C24 File Offset: 0x00022E24
		private global::System.ValueTuple<string, string> GetCacheLookupKeysFromSqlCommand(SqlCommand sqlCommand)
		{
			SqlConnection connection = sqlCommand.Connection;
			if (connection == null)
			{
				return new global::System.ValueTuple<string, string>(null, null);
			}
			StringBuilder stringBuilder = new StringBuilder(connection.DataSource, connection.DataSource.Length + 128 + sqlCommand.CommandText.Length + 6);
			stringBuilder.Append(":::");
			stringBuilder.Append(connection.Database.PadRight(128));
			stringBuilder.Append(":::");
			stringBuilder.Append(sqlCommand.CommandText);
			string text = stringBuilder.ToString();
			string text2 = stringBuilder.Append(":::enclaveKeys").ToString();
			return new global::System.ValueTuple<string, string>(text, text2);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00024CCC File Offset: 0x00022ECC
		private ConcurrentDictionary<int, SqlTceCipherInfoEntry> CreateCopyOfEnclaveKeys(ConcurrentDictionary<int, SqlTceCipherInfoEntry> keysToBeSentToEnclave)
		{
			ConcurrentDictionary<int, SqlTceCipherInfoEntry> concurrentDictionary = new ConcurrentDictionary<int, SqlTceCipherInfoEntry>();
			foreach (KeyValuePair<int, SqlTceCipherInfoEntry> keyValuePair in keysToBeSentToEnclave)
			{
				int key = keyValuePair.Key;
				SqlTceCipherInfoEntry value = keyValuePair.Value;
				SqlTceCipherInfoEntry sqlTceCipherInfoEntry = new SqlTceCipherInfoEntry(key);
				foreach (SqlEncryptionKeyInfo sqlEncryptionKeyInfo in value.ColumnEncryptionKeyValues)
				{
					sqlTceCipherInfoEntry.Add(sqlEncryptionKeyInfo.encryptedKey, sqlEncryptionKeyInfo.databaseId, sqlEncryptionKeyInfo.cekId, sqlEncryptionKeyInfo.cekVersion, sqlEncryptionKeyInfo.cekMdVersion, sqlEncryptionKeyInfo.keyPath, sqlEncryptionKeyInfo.keyStoreName, sqlEncryptionKeyInfo.algorithmName);
				}
				concurrentDictionary.TryAdd(key, sqlTceCipherInfoEntry);
			}
			return concurrentDictionary;
		}

		// Token: 0x0400031A RID: 794
		private const int CacheSize = 2000;

		// Token: 0x0400031B RID: 795
		private const int CacheTrimThreshold = 300;

		// Token: 0x0400031C RID: 796
		private readonly MemoryCache _cache;

		// Token: 0x0400031D RID: 797
		private static readonly SqlQueryMetadataCache s_singletonInstance = new SqlQueryMetadataCache();

		// Token: 0x0400031E RID: 798
		private int _inTrim;

		// Token: 0x0400031F RID: 799
		private long _cacheHits;

		// Token: 0x04000320 RID: 800
		private long _cacheMisses;
	}
}
