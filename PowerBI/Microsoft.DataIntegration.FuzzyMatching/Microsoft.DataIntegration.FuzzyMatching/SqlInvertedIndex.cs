using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	internal class SqlInvertedIndex : ISqlInvertedIndex, ISqlInvertedIndexBuilder, IInvertedIndexUpdate, ISqlInvertedIndexReader, ISerializable
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00018C68 File Offset: 0x00016E68
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00018C70 File Offset: 0x00016E70
		public bool UseTemporaryTables { get; set; }

		// Token: 0x0600057A RID: 1402 RVA: 0x00018C79 File Offset: 0x00016E79
		public SqlInvertedIndex()
		{
			this.UseTemporaryTables = false;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00018C94 File Offset: 0x00016E94
		private SqlInvertedIndex(SerializationInfo info, StreamingContext context)
		{
			this.m_tableName = (SqlName)info.GetValue("m_tableName", typeof(SqlName));
			this.m_clusteredIndexesHaveBeenCreated = (bool)info.GetValue("m_clusteredIndexesHaveBeenCreated", typeof(bool));
			this.UseTemporaryTables = (bool)info.GetValue("UseTemporaryTables", typeof(bool));
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00018D12 File Offset: 0x00016F12
		public void Dispose()
		{
			if (this.m_tableWriter != null)
			{
				this.m_tableWriter.Dispose();
				this.m_tableWriter = null;
			}
			if (this.m_signaturesDataReader != null)
			{
				this.m_signaturesDataReader.Dispose();
				this.m_signaturesDataReader = null;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00018D48 File Offset: 0x00016F48
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this.m_tableWriter != null)
			{
				throw new SerializationException("Can not serialize while in update mode.  Call EndUpdate() first.");
			}
			info.AddValue("m_tableName", this.m_tableName);
			info.AddValue("m_clusteredIndexesHaveBeenCreated", this.m_clusteredIndexesHaveBeenCreated);
			info.AddValue("UseTemporaryTables", this.UseTemporaryTables);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00018D9B File Offset: 0x00016F9B
		public IDataReader CreateSignaturesReader(SqlConnection connection)
		{
			SqlCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandTimeout = connection.ConnectionTimeout;
			sqlCommand.CommandText = string.Format("select Signature, LookupId, HashTableId, RecordId  from {0} order by LookupId, HashTableId, Signature", this.m_tableName);
			return sqlCommand.ExecuteReader();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00018DCC File Offset: 0x00016FCC
		public void CreateTables(DataTable referenceSchema, SqlConnection connection, bool overwriteExistingTables, string sqlSchemaName, string tableNamePrefix)
		{
			this.m_tableName = new SqlName(sqlSchemaName, (this.UseTemporaryTables ? "#" : "") + tableNamePrefix + "_ETI");
			if (!overwriteExistingTables)
			{
				this.m_tableName = SqlUtils.CreateUniqueIdentifier(connection, this.m_tableName);
			}
			else
			{
				SqlUtils.TryDropTable(connection, this.m_tableName);
			}
			using (SqlCommand sqlCommand = SqlUtils.CreateSqlCommand(connection))
			{
				sqlCommand.CommandText = string.Format("create table {0} (Signature int not null, LookupId int not null, HashTableId int not null, RecordId int not null)", this.m_tableName);
				sqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00018E6C File Offset: 0x0001706C
		public void DropIndex(SqlConnection connection)
		{
			SqlUtils.TryDropTable(connection, this.m_tableName);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00018E7C File Offset: 0x0001707C
		public void AddRidSignatures(IUpdateContext _updateContext, int recordId, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			this.m_signaturesDataReader.AddRidSignatures(recordId, lookupId, hashTableIndex, signatures);
			if (this.m_signaturesDataReader.Count >= this.BatchSize)
			{
				while (this.m_signaturesDataReader.Read())
				{
					this.m_tableWriter.AddRecord(this.m_signaturesDataReader);
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00018ECD File Offset: 0x000170CD
		public void RemoveRidSignatures(IUpdateContext _updateContext, int entry, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00018ED4 File Offset: 0x000170D4
		public IUpdateContext BeginUpdate(DataTable schemaTable)
		{
			return new SqlInvertedIndex.UpdateContext();
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00018EDB File Offset: 0x000170DB
		public IUpdateContext BeginUpdate(SqlConnection connection)
		{
			this.m_updateConnection = connection;
			if (!SqlUtils.IsContextConnection(connection))
			{
				this.BeginUpdate(connection, new SqlBulkCopyTableWriter());
			}
			else
			{
				this.BeginUpdate(connection, new IsolatedStorageBcpTableWriter());
			}
			return new SqlInvertedIndex.UpdateContext();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00018F10 File Offset: 0x00017110
		public IUpdateContext BeginUpdate(SqlConnection connection, ISqlTableWriter tableWriter)
		{
			this.m_tableWriter = tableWriter;
			DataTable schemaTable;
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				sqlCommand.CommandText = string.Format("select top 0 * from {0}", this.m_tableName.QualifiedName);
				using (IDataReader dataReader = sqlCommand.ExecuteReader(2))
				{
					schemaTable = dataReader.GetSchemaTable();
				}
			}
			this.m_tableWriter.BeginUpdate(connection, this.m_tableName, schemaTable);
			this.m_signaturesDataReader = new RidSignaturesDataReader(schemaTable);
			return new SqlInvertedIndex.UpdateContext();
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00018FAC File Offset: 0x000171AC
		public void EndUpdate(IUpdateContext updateContext)
		{
			while (this.m_signaturesDataReader.Read())
			{
				this.m_tableWriter.AddRecord(this.m_signaturesDataReader);
			}
			this.m_tableWriter.EndUpdate();
			this.m_tableWriter = null;
			if (!this.m_clusteredIndexesHaveBeenCreated)
			{
				using (SqlCommand sqlCommand = SqlUtils.CreateSqlCommand(this.m_updateConnection))
				{
					sqlCommand.CommandText = string.Format("create clustered index i on {0}(Signature, LookupId, HashTableId, RecordId)", this.m_tableName);
					sqlCommand.ExecuteNonQuery();
				}
				this.m_clusteredIndexesHaveBeenCreated = true;
			}
			this.m_updateConnection = null;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00019048 File Offset: 0x00017248
		public ISession CreateReadSession(SqlConnection connection)
		{
			SqlCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandText = string.Format("select RecordId from {0} where LookupId=@LookupId and Signature=@Signature and HashTableId=@HashTableId", this.m_tableName.QualifiedName);
			SqlParameter sqlParameter = sqlCommand.CreateParameter();
			sqlParameter.ParameterName = "@LookupId";
			sqlParameter.DbType = 11;
			sqlCommand.Parameters.Add(sqlParameter);
			sqlParameter = sqlCommand.CreateParameter();
			sqlParameter.ParameterName = "@Signature";
			sqlParameter.DbType = 11;
			sqlCommand.Parameters.Add(sqlParameter);
			sqlParameter = sqlCommand.CreateParameter();
			sqlParameter.ParameterName = "@HashTableId";
			sqlParameter.DbType = 11;
			sqlCommand.Parameters.Add(sqlParameter);
			sqlCommand.Prepare();
			return new SqlInvertedIndex.SqlReadSession
			{
				m_fetchRidsCmd = sqlCommand
			};
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00019100 File Offset: 0x00017300
		public bool TryGetSignatureRidList(ISession _session, int lookupId, int hashTableIndex, int signature, ref int[] ridBuffer, out RidList ridList)
		{
			SqlInvertedIndex.SqlReadSession sqlReadSession = (SqlInvertedIndex.SqlReadSession)_session;
			sqlReadSession.m_fetchRidsCmd.Parameters[0].Value = lookupId;
			sqlReadSession.m_fetchRidsCmd.Parameters[1].Value = signature;
			sqlReadSession.m_fetchRidsCmd.Parameters[2].Value = hashTableIndex;
			ridList = default(RidList);
			using (IDataReader dataReader = sqlReadSession.m_fetchRidsCmd.ExecuteReader())
			{
				while (dataReader.Read())
				{
					if (ridList.Count == 0)
					{
						ridList.Array = new int[1];
					}
					else if (ridList.Count == ridList.Array.Length)
					{
						Array.Resize<int>(ref ridList.Array, Math.Max(1, ridList.Array.Length) * 2);
					}
					int[] array = ridList.Array;
					int count = ridList.Count;
					ridList.Count = count + 1;
					array[count] = dataReader.GetInt32(0);
				}
			}
			return ridList.Count > 0;
		}

		// Token: 0x040001D8 RID: 472
		protected int BatchSize = 5000;

		// Token: 0x040001D9 RID: 473
		private RidSignaturesDataReader m_signaturesDataReader;

		// Token: 0x040001DA RID: 474
		private ISqlTableWriter m_tableWriter;

		// Token: 0x040001DB RID: 475
		[NonSerialized]
		private SqlConnection m_updateConnection;

		// Token: 0x040001DC RID: 476
		internal SqlName m_tableName;

		// Token: 0x040001DD RID: 477
		private bool m_clusteredIndexesHaveBeenCreated;

		// Token: 0x02000167 RID: 359
		[Serializable]
		public class SqlReadSession : ISession, IDisposable
		{
			// Token: 0x06000CDE RID: 3294 RVA: 0x00037331 File Offset: 0x00035531
			public void Dispose()
			{
				if (this.m_fetchRidsCmd != null)
				{
					this.m_fetchRidsCmd.Dispose();
					this.m_fetchRidsCmd = null;
				}
			}

			// Token: 0x06000CDF RID: 3295 RVA: 0x0003734D File Offset: 0x0003554D
			public void Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040005C7 RID: 1479
			public SqlCommand m_fetchRidsCmd;
		}

		// Token: 0x02000168 RID: 360
		private class UpdateContext : IUpdateContext
		{
		}
	}
}
