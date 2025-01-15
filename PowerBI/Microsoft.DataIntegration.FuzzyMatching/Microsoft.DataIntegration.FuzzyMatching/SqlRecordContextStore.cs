using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200007C RID: 124
	[Serializable]
	internal class SqlRecordContextStore : IDisposable, IRawSerializable, ISerializable
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x000172AA File Offset: 0x000154AA
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x000172B2 File Offset: 0x000154B2
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000172BB File Offset: 0x000154BB
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x000172C3 File Offset: 0x000154C3
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000172CC File Offset: 0x000154CC
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x000172D4 File Offset: 0x000154D4
		public bool UseTemporaryTables { get; set; }

		// Token: 0x06000506 RID: 1286 RVA: 0x000172DD File Offset: 0x000154DD
		public SqlRecordContextStore()
		{
			this.UseTemporaryTables = false;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000172EC File Offset: 0x000154EC
		private SqlRecordContextStore(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_tableName = (SqlName)info.GetValue("m_tableName", typeof(SqlName));
			this.m_preparedReferenceSchema = (DataTable)info.GetValue("m_preparedReferenceSchema", typeof(DataTable));
			this.m_clusteredIndexesHaveBeenCreated = (bool)info.GetValue("m_clusteredIndexesHaveBeenCreated", typeof(bool));
			this.UseTemporaryTables = (bool)info.GetValue("UseTemporaryTables", typeof(bool));
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000173C0 File Offset: 0x000155C0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this.m_referenceWriter != null)
			{
				throw new SerializationException("Can not serialize while in update mode.  Call EndUpdate() first.");
			}
			if (!((IRawSerializable)this).EnableRawSerialization && this.UseTemporaryTables)
			{
				throw new NotImplementedException("EnableRawSerialization must be true.  Use FuzzyLookupFormatter to perform serialization or set SqlIndexOptions.Temporary=false.");
			}
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("m_tableName", this.m_tableName);
			info.AddValue("m_preparedReferenceSchema", this.m_preparedReferenceSchema);
			info.AddValue("m_clusteredIndexesHaveBeenCreated", this.m_clusteredIndexesHaveBeenCreated);
			info.AddValue("UseTemporaryTables", this.UseTemporaryTables);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00017461 File Offset: 0x00015661
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001747B File Offset: 0x0001567B
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000174AF File Offset: 0x000156AF
		public void Dispose()
		{
			if (this.m_referenceWriter != null)
			{
				this.m_referenceWriter.Dispose();
				this.m_referenceWriter = null;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000174CC File Offset: 0x000156CC
		public void CreateTables(DataTable referenceSchema, SqlConnection connection, bool overwriteExistingTables, string sqlSchemaName, string tableNamePrefix)
		{
			this.m_tableName = new SqlName(sqlSchemaName, (this.UseTemporaryTables ? "#" : "") + tableNamePrefix + "_RCTXT");
			if (!overwriteExistingTables)
			{
				this.m_tableName = SqlUtils.CreateUniqueIdentifier(connection, this.m_tableName);
			}
			else
			{
				SqlUtils.TryDropTable(connection, this.m_tableName);
			}
			this.m_preparedReferenceSchema = SchemaUtils.CreateSchemaTable("RecordContextTable");
			int num = 0;
			DataRow dataRow = this.m_preparedReferenceSchema.NewRow();
			dataRow[SchemaTableColumn.ColumnName] = SchemaUtils.GenerateUniqueColumnName(referenceSchema, SqlRecordContextStore.LookupIdColumnName);
			dataRow[SchemaTableColumn.ColumnOrdinal] = num++;
			dataRow[SchemaTableColumn.ColumnSize] = 4;
			dataRow[SchemaTableColumn.DataType] = typeof(int);
			dataRow[SchemaTableColumn.IsKey] = false;
			dataRow[SchemaTableColumn.AllowDBNull] = false;
			this.m_preparedReferenceSchema.Rows.Add(dataRow);
			dataRow = this.m_preparedReferenceSchema.NewRow();
			dataRow[SchemaTableColumn.ColumnName] = SchemaUtils.GenerateUniqueColumnName(referenceSchema, SqlRecordContextStore.RecordIdColumnName);
			dataRow[SchemaTableColumn.ColumnOrdinal] = num++;
			dataRow[SchemaTableColumn.ColumnSize] = 4;
			dataRow[SchemaTableColumn.DataType] = typeof(int);
			dataRow[SchemaTableColumn.IsKey] = false;
			dataRow[SchemaTableColumn.AllowDBNull] = false;
			this.m_preparedReferenceSchema.Rows.Add(dataRow);
			dataRow = this.m_preparedReferenceSchema.NewRow();
			dataRow[SchemaTableColumn.ColumnName] = SchemaUtils.GenerateUniqueColumnName(referenceSchema, SqlRecordContextStore.RecordContextColumnName);
			dataRow[SchemaTableColumn.ColumnOrdinal] = num++;
			dataRow[SchemaTableColumn.ColumnSize] = SqlMetaData.Max;
			dataRow[SchemaTableColumn.DataType] = typeof(byte[]);
			dataRow[SchemaTableColumn.IsKey] = false;
			dataRow[SchemaTableColumn.AllowDBNull] = true;
			this.m_preparedReferenceSchema.Rows.Add(dataRow);
			SqlUtils.CreateTableFromSchema(connection, this.m_preparedReferenceSchema, this.m_tableName, false);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00017703 File Offset: 0x00015903
		public void DropIndex(SqlConnection connection)
		{
			SqlUtils.TryDropTable(connection, this.m_tableName);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00017714 File Offset: 0x00015914
		public IUpdateContext BeginUpdate(SqlConnection connection)
		{
			this.m_updateConnection = connection;
			if (!SqlUtils.IsContextConnection(connection))
			{
				this.m_referenceWriter = new SqlBulkCopyTableWriter();
			}
			else
			{
				this.m_referenceWriter = new IsolatedStorageBcpTableWriter();
			}
			DataTable schemaTable;
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				sqlCommand.CommandText = string.Format("select top 0 * from {0}", this.m_tableName.QualifiedName);
				using (IDataReader dataReader = sqlCommand.ExecuteReader(2))
				{
					schemaTable = dataReader.GetSchemaTable();
				}
			}
			this.m_referenceWriter.BeginUpdate(connection, this.m_tableName, schemaTable);
			return new SqlRecordContextStore.UpdateContext();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000177C4 File Offset: 0x000159C4
		public void EndUpdate(IUpdateContext updateContext)
		{
			this.m_referenceWriter.EndUpdate();
			this.m_referenceWriter.Dispose();
			this.m_referenceWriter = null;
			if (!this.m_clusteredIndexesHaveBeenCreated)
			{
				using (SqlCommand sqlCommand = SqlUtils.CreateSqlCommand(this.m_updateConnection))
				{
					sqlCommand.CommandText = string.Format("create clustered index i on {0}({1}, {2})", this.m_tableName, SqlName.DelimitElement((string)this.m_preparedReferenceSchema.Rows[0][SchemaTableColumn.ColumnName]), SqlName.DelimitElement((string)this.m_preparedReferenceSchema.Rows[1][SchemaTableColumn.ColumnName]));
					sqlCommand.ExecuteNonQuery();
				}
				this.m_clusteredIndexesHaveBeenCreated = true;
			}
			this.m_updateConnection = null;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00017898 File Offset: 0x00015A98
		public ISession CreateReadSession(SqlConnection connection)
		{
			SqlCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandText = string.Format("select {3} from {0} where {1}=@LookupId and {2}=@RecordId", new object[]
			{
				this.m_tableName.QualifiedName,
				SqlName.DelimitElement((string)this.m_preparedReferenceSchema.Rows[0][SchemaTableColumn.ColumnName]),
				SqlName.DelimitElement((string)this.m_preparedReferenceSchema.Rows[1][SchemaTableColumn.ColumnName]),
				SqlName.DelimitElement((string)this.m_preparedReferenceSchema.Rows[2][SchemaTableColumn.ColumnName])
			});
			SqlParameter sqlParameter = sqlCommand.CreateParameter();
			sqlParameter.ParameterName = "@LookupId";
			sqlParameter.DbType = 11;
			sqlCommand.Parameters.Add(sqlParameter);
			sqlParameter = sqlCommand.CreateParameter();
			sqlParameter.ParameterName = "@RecordId";
			sqlParameter.DbType = 11;
			sqlCommand.Parameters.Add(sqlParameter);
			sqlCommand.Prepare();
			return new SqlRecordContextStore.SqlReadSession
			{
				m_fetchRefCmd = sqlCommand
			};
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000179A8 File Offset: 0x00015BA8
		public void AddRecordContext(int lookupId, int rid, RecordContext recordContext)
		{
			object[] array = new object[3];
			array[0] = lookupId;
			array[1] = rid;
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			recordContext.Write(binaryWriter);
			array[2] = memoryStream.ToArray();
			this.m_referenceWriter.AddRecord(new SimpleDataRecord
			{
				Schema = this.m_preparedReferenceSchema,
				Values = array
			});
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00017A10 File Offset: 0x00015C10
		public bool TryGetRecordContext(ISession _session, int lookupId, int rid, out RecordContext recordContext)
		{
			SqlRecordContextStore.SqlReadSession sqlReadSession = (SqlRecordContextStore.SqlReadSession)_session;
			sqlReadSession.m_fetchRefCmd.Parameters[0].Value = lookupId;
			sqlReadSession.m_fetchRefCmd.Parameters[1].Value = rid;
			using (IDataReader dataReader = sqlReadSession.m_fetchRefCmd.ExecuteReader())
			{
				if (!dataReader.Read())
				{
					recordContext = null;
					return false;
				}
				recordContext = new RecordContext(new BinaryReader(new MemoryStream((byte[])dataReader[0])), HeapSegmentAllocator<int>.Instance, HeapSegmentAllocator<byte>.Instance);
			}
			return true;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00017AC0 File Offset: 0x00015CC0
		public bool TryRemoveRecord(IDataRecord record, out int rid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00017AC7 File Offset: 0x00015CC7
		public bool TryRemoveRecord(int rid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001AF RID: 431
		private static readonly string LookupIdColumnName = "LID";

		// Token: 0x040001B0 RID: 432
		private static readonly string RecordIdColumnName = "RID";

		// Token: 0x040001B1 RID: 433
		private static readonly string RecordContextColumnName = "RecordContext";

		// Token: 0x040001B2 RID: 434
		internal SqlName m_tableName;

		// Token: 0x040001B3 RID: 435
		internal DataTable m_preparedReferenceSchema;

		// Token: 0x040001B4 RID: 436
		[NonSerialized]
		private ISqlTableWriter m_referenceWriter;

		// Token: 0x040001B5 RID: 437
		[NonSerialized]
		private SqlConnection m_updateConnection;

		// Token: 0x040001B6 RID: 438
		private bool m_clusteredIndexesHaveBeenCreated;

		// Token: 0x02000160 RID: 352
		[Serializable]
		public class SqlReadSession : ISession, IDisposable
		{
			// Token: 0x06000CD2 RID: 3282 RVA: 0x00037280 File Offset: 0x00035480
			public void Reset()
			{
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x00037282 File Offset: 0x00035482
			public void Dispose()
			{
				if (this.m_fetchRefCmd != null)
				{
					this.m_fetchRefCmd.Dispose();
					this.m_fetchRefCmd = null;
				}
			}

			// Token: 0x040005BA RID: 1466
			public SqlCommand m_fetchRefCmd;
		}

		// Token: 0x02000161 RID: 353
		private class UpdateContext : IUpdateContext
		{
		}
	}
}
