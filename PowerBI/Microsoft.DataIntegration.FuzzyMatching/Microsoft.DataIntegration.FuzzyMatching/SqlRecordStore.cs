using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000082 RID: 130
	[Serializable]
	internal class SqlRecordStore : IDisposable, IRawSerializable, ISerializable
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00017FE3 File Offset: 0x000161E3
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00017FEB File Offset: 0x000161EB
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00017FF4 File Offset: 0x000161F4
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00017FFC File Offset: 0x000161FC
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00018005 File Offset: 0x00016205
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0001800D File Offset: 0x0001620D
		public bool UseTemporaryTables { get; set; }

		// Token: 0x06000533 RID: 1331 RVA: 0x00018016 File Offset: 0x00016216
		public SqlRecordStore()
		{
			this.UseTemporaryTables = false;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00018030 File Offset: 0x00016230
		private SqlRecordStore(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_tableName = (SqlName)info.GetValue("m_tableName", typeof(SqlName));
			this.m_preparedReferenceSchema = (DataTable)info.GetValue("m_preparedReferenceSchema", typeof(DataTable));
			this.m_ridColumnIndex = (int)info.GetValue("m_ridColumnIndex", typeof(int));
			this.m_ridColumnName = (SqlName)info.GetValue("m_ridColumnName", typeof(SqlName));
			this.m_clusteredIndexesHaveBeenCreated = (bool)info.GetValue("m_clusteredIndexesHaveBeenCreated", typeof(bool));
			this.UseTemporaryTables = (bool)info.GetValue("UseTemporaryTables", typeof(bool));
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018150 File Offset: 0x00016350
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
			info.AddValue("m_ridColumnIndex", this.m_ridColumnIndex);
			info.AddValue("m_ridColumnName", this.m_ridColumnName);
			info.AddValue("m_clusteredIndexesHaveBeenCreated", this.m_clusteredIndexesHaveBeenCreated);
			info.AddValue("UseTemporaryTables", this.UseTemporaryTables);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00018213 File Offset: 0x00016413
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001822D File Offset: 0x0001642D
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

		// Token: 0x06000538 RID: 1336 RVA: 0x00018261 File Offset: 0x00016461
		public void Dispose()
		{
			if (this.m_referenceWriter != null)
			{
				this.m_referenceWriter.Dispose();
				this.m_referenceWriter = null;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00018280 File Offset: 0x00016480
		public void CreateTables(DataTable referenceSchema, SqlConnection connection, bool overwriteExistingTables, string sqlSchemaName, string tableNamePrefix)
		{
			this.m_tableName = new SqlName(sqlSchemaName, (this.UseTemporaryTables ? "#" : "") + tableNamePrefix + "_REF");
			if (!overwriteExistingTables)
			{
				this.m_tableName = SqlUtils.CreateUniqueIdentifier(connection, this.m_tableName);
			}
			else
			{
				SqlUtils.TryDropTable(connection, this.m_tableName);
			}
			this.m_preparedReferenceSchema = Utilities.CloneDataTable(referenceSchema);
			DataRow dataRow = this.m_preparedReferenceSchema.NewRow();
			dataRow[SchemaTableColumn.ColumnName] = SchemaUtils.GenerateUniqueColumnName(referenceSchema, "RID");
			dataRow[SchemaTableColumn.ColumnOrdinal] = referenceSchema.Rows.Count;
			dataRow[SchemaTableColumn.ColumnSize] = 4;
			dataRow[SchemaTableColumn.DataType] = typeof(int);
			dataRow[SchemaTableColumn.IsKey] = false;
			dataRow[SchemaTableColumn.AllowDBNull] = false;
			this.m_preparedReferenceSchema.Rows.Add(dataRow);
			this.m_ridColumnIndex = referenceSchema.Rows.Count;
			this.m_ridColumnName.SetPart(SqlName.Part.Table, dataRow[SchemaTableColumn.ColumnName] as string);
			SqlUtils.CreateTableFromSchema(connection, this.m_preparedReferenceSchema, this.m_tableName, false, false);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000183C1 File Offset: 0x000165C1
		public void DropIndex(SqlConnection connection)
		{
			SqlUtils.TryDropTable(connection, this.m_tableName);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000183D0 File Offset: 0x000165D0
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
			return new SqlRecordStore.UpdateContext();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018480 File Offset: 0x00016680
		public void EndUpdate(IUpdateContext updateContext)
		{
			this.m_referenceWriter.EndUpdate();
			this.m_referenceWriter.Dispose();
			this.m_referenceWriter = null;
			if (!this.m_clusteredIndexesHaveBeenCreated)
			{
				using (SqlCommand sqlCommand = SqlUtils.CreateSqlCommand(this.m_updateConnection))
				{
					sqlCommand.CommandText = string.Format("create clustered index i on {0}({1})", this.m_tableName, this.m_ridColumnName);
					sqlCommand.ExecuteNonQuery();
				}
				this.m_clusteredIndexesHaveBeenCreated = true;
			}
			this.m_updateConnection = null;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001850C File Offset: 0x0001670C
		public ISession CreateReadSession(SqlConnection connection)
		{
			SqlCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandText = string.Format("select {2} from {0} where {1}=@RecordId", this.m_tableName.QualifiedName, this.m_ridColumnName.QualifiedName, Enumerable.Select<DataRow, string>(Enumerable.Where<DataRow>(Enumerable.Cast<DataRow>(this.m_preparedReferenceSchema.Rows), (DataRow r) => (int)r[SchemaTableColumn.ColumnOrdinal] != this.m_ridColumnIndex), (DataRow r) => SqlName.DelimitElement((string)r[SchemaTableColumn.ColumnName])).ToCsv(", "));
			SqlParameter sqlParameter = sqlCommand.CreateParameter();
			sqlParameter.ParameterName = "@RecordId";
			sqlParameter.DbType = 11;
			sqlCommand.Parameters.Add(sqlParameter);
			sqlCommand.Prepare();
			return new SqlRecordStore.SqlReadSession
			{
				m_fetchRefCmd = sqlCommand
			};
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000185D0 File Offset: 0x000167D0
		public void AddRecord(IDataRecord record, int rid)
		{
			object[] array = new object[record.FieldCount];
			record.GetValues(array);
			Array.Resize<object>(ref array, array.Length + 1);
			array[this.m_ridColumnIndex] = rid;
			this.m_referenceWriter.AddRecord(new SimpleDataRecord
			{
				Schema = this.m_preparedReferenceSchema,
				Values = array
			});
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018630 File Offset: 0x00016830
		public bool TryGetRecord(ISession _session, int rid, ref object[] values)
		{
			SqlRecordStore.SqlReadSession sqlReadSession = (SqlRecordStore.SqlReadSession)_session;
			sqlReadSession.m_fetchRefCmd.Parameters[0].Value = rid;
			using (IDataReader dataReader = sqlReadSession.m_fetchRefCmd.ExecuteReader())
			{
				if (values.Length != dataReader.FieldCount)
				{
					throw new Exception("Value array length does not match record length.");
				}
				if (!dataReader.Read())
				{
					Array.Clear(values, 0, values.Length);
					return false;
				}
				dataReader.GetValues(values);
			}
			return true;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000186C4 File Offset: 0x000168C4
		public bool TryRemoveRecord(IDataRecord record, out int rid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000186CB File Offset: 0x000168CB
		public bool TryRemoveRecord(int rid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001C8 RID: 456
		internal SqlName m_tableName;

		// Token: 0x040001C9 RID: 457
		internal DataTable m_preparedReferenceSchema;

		// Token: 0x040001CA RID: 458
		internal int m_ridColumnIndex;

		// Token: 0x040001CB RID: 459
		internal SqlName m_ridColumnName = new SqlName();

		// Token: 0x040001CC RID: 460
		[NonSerialized]
		private ISqlTableWriter m_referenceWriter;

		// Token: 0x040001CD RID: 461
		[NonSerialized]
		private SqlConnection m_updateConnection;

		// Token: 0x040001CE RID: 462
		private bool m_clusteredIndexesHaveBeenCreated;

		// Token: 0x02000162 RID: 354
		[Serializable]
		public class SqlReadSession : ISession, IDisposable
		{
			// Token: 0x06000CD6 RID: 3286 RVA: 0x000372AE File Offset: 0x000354AE
			public void Reset()
			{
			}

			// Token: 0x06000CD7 RID: 3287 RVA: 0x000372B0 File Offset: 0x000354B0
			public void Dispose()
			{
				if (this.m_fetchRefCmd != null)
				{
					this.m_fetchRefCmd.Dispose();
					this.m_fetchRefCmd = null;
				}
			}

			// Token: 0x040005BB RID: 1467
			public SqlCommand m_fetchRefCmd;
		}

		// Token: 0x02000163 RID: 355
		private class UpdateContext : IUpdateContext
		{
		}
	}
}
