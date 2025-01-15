using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Data.SqlTypes;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000EB RID: 235
	internal sealed class SqlDataReaderSmi : SqlDataReader
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x000468B2 File Offset: 0x00044AB2
		public override int FieldCount
		{
			get
			{
				this.ThrowIfClosed("FieldCount");
				return this.InternalFieldCount;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x000468C5 File Offset: 0x00044AC5
		public override int VisibleFieldCount
		{
			get
			{
				this.ThrowIfClosed("VisibleFieldCount");
				if (this.FNotInResults())
				{
					return 0;
				}
				return this._visibleColumnCount;
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x000468E2 File Offset: 0x00044AE2
		public override string GetName(int ordinal)
		{
			this.EnsureCanGetMetaData("GetName");
			return this._currentMetaData[ordinal].Name;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000468FC File Offset: 0x00044AFC
		public override string GetDataTypeName(int ordinal)
		{
			this.EnsureCanGetMetaData("GetDataTypeName");
			SmiExtendedMetaData smiExtendedMetaData = this._currentMetaData[ordinal];
			if (SqlDbType.Udt == smiExtendedMetaData.SqlDbType)
			{
				return string.Concat(new string[] { smiExtendedMetaData.TypeSpecificNamePart1, ".", smiExtendedMetaData.TypeSpecificNamePart2, ".", smiExtendedMetaData.TypeSpecificNamePart3 });
			}
			return smiExtendedMetaData.TypeName;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00046964 File Offset: 0x00044B64
		public override Type GetFieldType(int ordinal)
		{
			this.EnsureCanGetMetaData("GetFieldType");
			if (this._currentMetaData[ordinal].SqlDbType == SqlDbType.Udt)
			{
				return this._currentMetaData[ordinal].Type;
			}
			return MetaType.GetMetaTypeFromSqlDbType(this._currentMetaData[ordinal].SqlDbType, this._currentMetaData[ordinal].IsMultiValued).ClassType;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000469C0 File Offset: 0x00044BC0
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			this.EnsureCanGetMetaData("GetProviderSpecificFieldType");
			if (SqlDbType.Udt == this._currentMetaData[ordinal].SqlDbType)
			{
				return this._currentMetaData[ordinal].Type;
			}
			return MetaType.GetMetaTypeFromSqlDbType(this._currentMetaData[ordinal].SqlDbType, this._currentMetaData[ordinal].IsMultiValued).SqlType;
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00046A1B File Offset: 0x00044C1B
		public override int Depth
		{
			get
			{
				this.ThrowIfClosed("Depth");
				return 0;
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00046A2C File Offset: 0x00044C2C
		public override object GetValue(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetValue");
			SmiQueryMetaData smiQueryMetaData = this._currentMetaData[ordinal];
			if (this._currentConnection.Is2008OrNewer)
			{
				return ValueUtilsSmi.GetValue200(this._readerEventSink, (SmiTypedGetterSetter)this._currentColumnValuesV3, ordinal, smiQueryMetaData, this._currentConnection.InternalContext);
			}
			return ValueUtilsSmi.GetValue(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData, this._currentConnection.InternalContext);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00046AA0 File Offset: 0x00044CA0
		public override T GetFieldValue<T>(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetFieldValue");
			SmiQueryMetaData smiQueryMetaData = this._currentMetaData[ordinal];
			if (typeof(INullable).IsAssignableFrom(typeof(T)))
			{
				if (this._currentConnection.Is2008OrNewer)
				{
					return (T)((object)ValueUtilsSmi.GetSqlValue200(this._readerEventSink, (SmiTypedGetterSetter)this._currentColumnValuesV3, ordinal, smiQueryMetaData, this._currentConnection.InternalContext));
				}
				return (T)((object)ValueUtilsSmi.GetSqlValue(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData, this._currentConnection.InternalContext));
			}
			else
			{
				if (this._currentConnection.Is2008OrNewer)
				{
					return (T)((object)ValueUtilsSmi.GetValue200(this._readerEventSink, (SmiTypedGetterSetter)this._currentColumnValuesV3, ordinal, smiQueryMetaData, this._currentConnection.InternalContext));
				}
				return (T)((object)ValueUtilsSmi.GetValue(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData, this._currentConnection.InternalContext));
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00046B90 File Offset: 0x00044D90
		public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			return ADP.CreatedTaskWithException<T>(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00046BA4 File Offset: 0x00044DA4
		internal override SqlBuffer.StorageType GetVariantInternalStorageType(int ordinal)
		{
			if (this.IsDBNull(ordinal))
			{
				return SqlBuffer.StorageType.Empty;
			}
			SmiMetaData variantType = this._currentColumnValuesV3.GetVariantType(this._readerEventSink, ordinal);
			if (variantType == null)
			{
				return SqlBuffer.StorageType.Empty;
			}
			return ValueUtilsSmi.SqlDbTypeToStorageType(variantType.SqlDbType);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00046BE0 File Offset: 0x00044DE0
		public override int GetValues(object[] values)
		{
			this.EnsureCanGetCol(0, "GetValues");
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this._visibleColumnCount) ? values.Length : this._visibleColumnCount);
			for (int i = 0; i < num; i++)
			{
				values[this._indexMap[i]] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00046C3C File Offset: 0x00044E3C
		public override int GetOrdinal(string name)
		{
			this.EnsureCanGetMetaData("GetOrdinal");
			if (this._fieldNameLookup == null)
			{
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x17000884 RID: 2180
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x17000885 RID: 2181
		public override object this[string strName]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(strName));
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00046C6A File Offset: 0x00044E6A
		public override bool IsDBNull(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "IsDBNull");
			return ValueUtilsSmi.IsDBNull(this._readerEventSink, this._currentColumnValuesV3, ordinal);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00046C8A File Offset: 0x00044E8A
		public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00046C9B File Offset: 0x00044E9B
		public override bool GetBoolean(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetBoolean");
			return ValueUtilsSmi.GetBoolean(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00046CC3 File Offset: 0x00044EC3
		public override byte GetByte(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetByte");
			return ValueUtilsSmi.GetByte(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00046CEC File Offset: 0x00044EEC
		public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureCanGetCol(ordinal, "GetBytes");
			return ValueUtilsSmi.GetBytes(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], fieldOffset, buffer, bufferOffset, length, true);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00046D28 File Offset: 0x00044F28
		internal override long GetBytesInternal(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureCanGetCol(ordinal, "GetBytesInternal");
			return ValueUtilsSmi.GetBytesInternal(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], fieldOffset, buffer, bufferOffset, length, false);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00025577 File Offset: 0x00023777
		public override char GetChar(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00046D64 File Offset: 0x00044F64
		public override long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureCanGetCol(ordinal, "GetChars");
			SmiExtendedMetaData smiExtendedMetaData = this._currentMetaData[ordinal];
			if (base.IsCommandBehavior(CommandBehavior.SequentialAccess) && smiExtendedMetaData.SqlDbType == SqlDbType.Xml)
			{
				return base.GetStreamingXmlChars(ordinal, fieldOffset, buffer, bufferOffset, length);
			}
			return ValueUtilsSmi.GetChars(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiExtendedMetaData, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00046DC1 File Offset: 0x00044FC1
		public override Guid GetGuid(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetGuid");
			return ValueUtilsSmi.GetGuid(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00046DE9 File Offset: 0x00044FE9
		public override short GetInt16(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetInt16");
			return ValueUtilsSmi.GetInt16(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00046E11 File Offset: 0x00045011
		public override int GetInt32(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetInt32");
			return ValueUtilsSmi.GetInt32(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00046E39 File Offset: 0x00045039
		public override long GetInt64(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetInt64");
			return ValueUtilsSmi.GetInt64(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00046E61 File Offset: 0x00045061
		public override float GetFloat(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetFloat");
			return ValueUtilsSmi.GetSingle(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00046E89 File Offset: 0x00045089
		public override double GetDouble(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetDouble");
			return ValueUtilsSmi.GetDouble(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00046EB1 File Offset: 0x000450B1
		public override string GetString(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetString");
			return ValueUtilsSmi.GetString(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00046ED9 File Offset: 0x000450D9
		public override decimal GetDecimal(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetDecimal");
			return ValueUtilsSmi.GetDecimal(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00046F01 File Offset: 0x00045101
		public override DateTime GetDateTime(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetDateTime");
			return ValueUtilsSmi.GetDateTime(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00046F29 File Offset: 0x00045129
		public override bool IsClosed
		{
			get
			{
				return this.IsReallyClosed();
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00046F31 File Offset: 0x00045131
		public override int RecordsAffected
		{
			get
			{
				return base.Command.InternalRecordsAffected;
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00046F3E File Offset: 0x0004513E
		internal override void CloseReaderFromConnection()
		{
			this.CloseInternal(false);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00046F47 File Offset: 0x00045147
		public override void Close()
		{
			this.CloseInternal(base.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00046F58 File Offset: 0x00045158
		private void CloseInternal(bool closeConnection)
		{
			using (TryEventScope.Create<int>("<sc.SqlDataReaderSmi.Close|API> {0}", this.ObjectID))
			{
				bool flag = true;
				try
				{
					if (!this.IsClosed)
					{
						this._hasRows = false;
						while (this._eventStream.HasEvents)
						{
							this._eventStream.ProcessEvent(this._readerEventSink);
							this._readerEventSink.ProcessMessagesAndThrow(true);
						}
						this._requestExecutor.Close(this._readerEventSink);
						this._readerEventSink.ProcessMessagesAndThrow(true);
					}
				}
				catch (Exception ex)
				{
					flag = ADP.IsCatchableExceptionType(ex);
					throw;
				}
				finally
				{
					if (flag)
					{
						this._isOpen = false;
						if (closeConnection && base.Connection != null)
						{
							base.Connection.Close();
						}
					}
				}
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00047030 File Offset: 0x00045230
		public override bool NextResult()
		{
			this.ThrowIfClosed("NextResult");
			return this.InternalNextResult(false);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00046C8A File Offset: 0x00044E8A
		public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00047054 File Offset: 0x00045254
		internal bool InternalNextResult(bool ignoreNonFatalMessages)
		{
			long num = SqlClientEventSource.Log.TryAdvancedScopeEnterEvent<int>("<sc.SqlDataReaderSmi.InternalNextResult|ADV> {0}", this.ObjectID);
			bool flag;
			try
			{
				this._hasRows = false;
				if (SqlDataReaderSmi.PositionState.AfterResults != this._currentPosition)
				{
					while (this.InternalRead(ignoreNonFatalMessages))
					{
					}
					this.ResetResultSet();
					while (this._currentMetaData == null && this._eventStream.HasEvents)
					{
						this._eventStream.ProcessEvent(this._readerEventSink);
						this._readerEventSink.ProcessMessagesAndThrow(ignoreNonFatalMessages);
					}
				}
				flag = SqlDataReaderSmi.PositionState.AfterResults != this._currentPosition;
			}
			finally
			{
				SqlClientEventSource.Log.TryAdvanceScopeLeave(num);
			}
			return flag;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000470F8 File Offset: 0x000452F8
		public override bool Read()
		{
			this.ThrowIfClosed("Read");
			return this.InternalRead(false);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00046C8A File Offset: 0x00044E8A
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004711C File Offset: 0x0004531C
		internal bool InternalRead(bool ignoreNonFatalErrors)
		{
			long num = SqlClientEventSource.Log.TryAdvancedScopeEnterEvent<int>("<sc.SqlDataReaderSmi.InternalRead|ADV> {0}", this.ObjectID);
			bool flag;
			try
			{
				if (this.FInResults())
				{
					this._currentColumnValues = null;
					this._currentColumnValuesV3 = null;
					if (this._currentStream != null)
					{
						this._currentStream.SetClosed();
						this._currentStream = null;
					}
					if (this._currentTextReader != null)
					{
						this._currentTextReader.SetClosed();
						this._currentTextReader = null;
					}
					while (this._currentColumnValues == null && this._currentColumnValuesV3 == null && this.FInResults() && SqlDataReaderSmi.PositionState.AfterRows != this._currentPosition && this._eventStream.HasEvents)
					{
						this._eventStream.ProcessEvent(this._readerEventSink);
						this._readerEventSink.ProcessMessagesAndThrow(ignoreNonFatalErrors);
					}
				}
				flag = SqlDataReaderSmi.PositionState.OnRow == this._currentPosition;
			}
			finally
			{
				SqlClientEventSource.Log.TryAdvanceScopeLeave(num);
			}
			return flag;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00047204 File Offset: 0x00045404
		public override DataTable GetSchemaTable()
		{
			this.ThrowIfClosed("GetSchemaTable");
			if (this._schemaTable == null && this.FInResults())
			{
				DataTable dataTable = new DataTable("SchemaTable")
				{
					Locale = CultureInfo.InvariantCulture,
					MinimumCapacity = this.InternalFieldCount
				};
				DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
				DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
				DataColumn dataColumn3 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
				DataColumn dataColumn4 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
				DataColumn dataColumn5 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
				DataColumn dataColumn6 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
				DataColumn dataColumn7 = new DataColumn(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
				DataColumn dataColumn8 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
				DataColumn dataColumn9 = new DataColumn(SchemaTableColumn.NonVersionedProviderType, typeof(int));
				DataColumn dataColumn10 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
				DataColumn dataColumn11 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
				DataColumn dataColumn12 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
				DataColumn dataColumn13 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
				DataColumn dataColumn14 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
				DataColumn dataColumn15 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
				DataColumn dataColumn16 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
				DataColumn dataColumn17 = new DataColumn(SchemaTableOptionalColumn.IsHidden, typeof(bool));
				DataColumn dataColumn18 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
				DataColumn dataColumn19 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
				DataColumn dataColumn20 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
				DataColumn dataColumn21 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
				DataColumn dataColumn22 = new DataColumn(SchemaTableOptionalColumn.BaseServerName, typeof(string));
				DataColumn dataColumn23 = new DataColumn(SchemaTableColumn.IsAliased, typeof(bool));
				DataColumn dataColumn24 = new DataColumn(SchemaTableColumn.IsExpression, typeof(bool));
				DataColumn dataColumn25 = new DataColumn("IsIdentity", typeof(bool));
				DataColumn dataColumn26 = new DataColumn("DataTypeName", typeof(string));
				DataColumn dataColumn27 = new DataColumn("UdtAssemblyQualifiedName", typeof(string));
				DataColumn dataColumn28 = new DataColumn("XmlSchemaCollectionDatabase", typeof(string));
				DataColumn dataColumn29 = new DataColumn("XmlSchemaCollectionOwningSchema", typeof(string));
				DataColumn dataColumn30 = new DataColumn("XmlSchemaCollectionName", typeof(string));
				DataColumn dataColumn31 = new DataColumn("IsColumnSet", typeof(bool));
				dataColumn2.DefaultValue = 0;
				dataColumn10.DefaultValue = false;
				DataColumnCollection columns = dataTable.Columns;
				columns.Add(dataColumn);
				columns.Add(dataColumn2);
				columns.Add(dataColumn3);
				columns.Add(dataColumn4);
				columns.Add(dataColumn5);
				columns.Add(dataColumn14);
				columns.Add(dataColumn15);
				columns.Add(dataColumn22);
				columns.Add(dataColumn18);
				columns.Add(dataColumn21);
				columns.Add(dataColumn19);
				columns.Add(dataColumn20);
				columns.Add(dataColumn6);
				columns.Add(dataColumn11);
				columns.Add(dataColumn8);
				columns.Add(dataColumn23);
				columns.Add(dataColumn24);
				columns.Add(dataColumn25);
				columns.Add(dataColumn16);
				columns.Add(dataColumn13);
				columns.Add(dataColumn17);
				columns.Add(dataColumn10);
				columns.Add(dataColumn12);
				columns.Add(dataColumn7);
				columns.Add(dataColumn26);
				columns.Add(dataColumn28);
				columns.Add(dataColumn29);
				columns.Add(dataColumn30);
				columns.Add(dataColumn27);
				columns.Add(dataColumn9);
				columns.Add(dataColumn31);
				int i = 0;
				while (i < this.InternalFieldCount)
				{
					SmiQueryMetaData smiQueryMetaData = this._currentMetaData[i];
					long num = smiQueryMetaData.MaxLength;
					MetaType metaType = MetaType.GetMetaTypeFromSqlDbType(smiQueryMetaData.SqlDbType, smiQueryMetaData.IsMultiValued);
					if (-1L == num)
					{
						metaType = MetaType.GetMaxMetaTypeFromMetaType(metaType);
						num = ((metaType.IsSizeInCharacters && !metaType.IsPlp) ? 1073741823L : 2147483647L);
					}
					DataRow dataRow = dataTable.NewRow();
					if (SqlDbType.Decimal == smiQueryMetaData.SqlDbType)
					{
						num = 17L;
					}
					else if (SqlDbType.Variant == smiQueryMetaData.SqlDbType)
					{
						num = 8009L;
					}
					dataRow[dataColumn] = smiQueryMetaData.Name;
					dataRow[dataColumn2] = i;
					dataRow[dataColumn3] = num;
					dataRow[dataColumn8] = (int)smiQueryMetaData.SqlDbType;
					dataRow[dataColumn9] = (int)smiQueryMetaData.SqlDbType;
					if (smiQueryMetaData.SqlDbType != SqlDbType.Udt)
					{
						dataRow[dataColumn6] = metaType.ClassType;
						dataRow[dataColumn7] = metaType.SqlType;
					}
					else
					{
						dataRow[dataColumn27] = smiQueryMetaData.Type.AssemblyQualifiedName;
						dataRow[dataColumn6] = smiQueryMetaData.Type;
						dataRow[dataColumn7] = smiQueryMetaData.Type;
					}
					byte b;
					switch (smiQueryMetaData.SqlDbType)
					{
					case SqlDbType.BigInt:
					case SqlDbType.DateTime:
					case SqlDbType.Decimal:
					case SqlDbType.Int:
					case SqlDbType.Money:
					case SqlDbType.SmallDateTime:
					case SqlDbType.SmallInt:
					case SqlDbType.SmallMoney:
					case SqlDbType.TinyInt:
						b = smiQueryMetaData.Precision;
						break;
					case SqlDbType.Binary:
					case SqlDbType.Bit:
					case SqlDbType.Char:
					case SqlDbType.Image:
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
					case SqlDbType.UniqueIdentifier:
					case SqlDbType.Text:
					case SqlDbType.Timestamp:
						goto IL_05E1;
					case SqlDbType.Float:
						b = 15;
						break;
					case SqlDbType.Real:
						b = 7;
						break;
					default:
						goto IL_05E1;
					}
					IL_05E8:
					dataRow[dataColumn4] = b;
					if (SqlDbType.Decimal == smiQueryMetaData.SqlDbType || SqlDbType.Time == smiQueryMetaData.SqlDbType || SqlDbType.DateTime2 == smiQueryMetaData.SqlDbType || SqlDbType.DateTimeOffset == smiQueryMetaData.SqlDbType)
					{
						dataRow[dataColumn5] = smiQueryMetaData.Scale;
					}
					else
					{
						dataRow[dataColumn5] = MetaType.GetMetaTypeFromSqlDbType(smiQueryMetaData.SqlDbType, smiQueryMetaData.IsMultiValued).Scale;
					}
					dataRow[dataColumn11] = smiQueryMetaData.AllowsDBNull;
					if (!smiQueryMetaData.IsAliased.IsNull)
					{
						dataRow[dataColumn23] = smiQueryMetaData.IsAliased.Value;
					}
					if (!smiQueryMetaData.IsKey.IsNull)
					{
						dataRow[dataColumn15] = smiQueryMetaData.IsKey.Value;
					}
					if (!smiQueryMetaData.IsHidden.IsNull)
					{
						dataRow[dataColumn17] = smiQueryMetaData.IsHidden.Value;
					}
					if (!smiQueryMetaData.IsExpression.IsNull)
					{
						dataRow[dataColumn24] = smiQueryMetaData.IsExpression.Value;
					}
					dataRow[dataColumn12] = smiQueryMetaData.IsReadOnly;
					dataRow[dataColumn25] = smiQueryMetaData.IsIdentity;
					dataRow[dataColumn31] = smiQueryMetaData.IsColumnSet;
					dataRow[dataColumn16] = smiQueryMetaData.IsIdentity;
					dataRow[dataColumn10] = metaType.IsLong;
					if (SqlDbType.Timestamp == smiQueryMetaData.SqlDbType)
					{
						dataRow[dataColumn14] = true;
						dataRow[dataColumn13] = true;
					}
					else
					{
						dataRow[dataColumn14] = false;
						dataRow[dataColumn13] = false;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.ColumnName))
					{
						dataRow[dataColumn21] = smiQueryMetaData.ColumnName;
					}
					else if (!ADP.IsEmpty(smiQueryMetaData.Name))
					{
						dataRow[dataColumn21] = smiQueryMetaData.Name;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.TableName))
					{
						dataRow[dataColumn20] = smiQueryMetaData.TableName;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.SchemaName))
					{
						dataRow[dataColumn19] = smiQueryMetaData.SchemaName;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.CatalogName))
					{
						dataRow[dataColumn18] = smiQueryMetaData.CatalogName;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.ServerName))
					{
						dataRow[dataColumn22] = smiQueryMetaData.ServerName;
					}
					if (SqlDbType.Udt == smiQueryMetaData.SqlDbType)
					{
						dataRow[dataColumn26] = string.Concat(new string[] { smiQueryMetaData.TypeSpecificNamePart1, ".", smiQueryMetaData.TypeSpecificNamePart2, ".", smiQueryMetaData.TypeSpecificNamePart3 });
					}
					else
					{
						dataRow[dataColumn26] = metaType.TypeName;
					}
					if (SqlDbType.Xml == smiQueryMetaData.SqlDbType)
					{
						dataRow[dataColumn28] = smiQueryMetaData.TypeSpecificNamePart1;
						dataRow[dataColumn29] = smiQueryMetaData.TypeSpecificNamePart2;
						dataRow[dataColumn30] = smiQueryMetaData.TypeSpecificNamePart3;
					}
					dataTable.Rows.Add(dataRow);
					dataRow.AcceptChanges();
					i++;
					continue;
					IL_05E1:
					b = byte.MaxValue;
					goto IL_05E8;
				}
				foreach (object obj in columns)
				{
					DataColumn dataColumn32 = (DataColumn)obj;
					dataColumn32.ReadOnly = true;
				}
				this._schemaTable = dataTable;
			}
			return this._schemaTable;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00047BD0 File Offset: 0x00045DD0
		public override SqlBinary GetSqlBinary(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlBinary");
			return ValueUtilsSmi.GetSqlBinary(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00047BF8 File Offset: 0x00045DF8
		public override SqlBoolean GetSqlBoolean(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlBoolean");
			return ValueUtilsSmi.GetSqlBoolean(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00047C20 File Offset: 0x00045E20
		public override SqlByte GetSqlByte(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlByte");
			return ValueUtilsSmi.GetSqlByte(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00047C48 File Offset: 0x00045E48
		public override SqlInt16 GetSqlInt16(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlInt16");
			return ValueUtilsSmi.GetSqlInt16(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00047C70 File Offset: 0x00045E70
		public override SqlInt32 GetSqlInt32(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlInt32");
			return ValueUtilsSmi.GetSqlInt32(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00047C98 File Offset: 0x00045E98
		public override SqlInt64 GetSqlInt64(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlInt64");
			return ValueUtilsSmi.GetSqlInt64(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00047CC0 File Offset: 0x00045EC0
		public override SqlSingle GetSqlSingle(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlSingle");
			return ValueUtilsSmi.GetSqlSingle(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00047CE8 File Offset: 0x00045EE8
		public override SqlDouble GetSqlDouble(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlDouble");
			return ValueUtilsSmi.GetSqlDouble(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00047D10 File Offset: 0x00045F10
		public override SqlMoney GetSqlMoney(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlMoney");
			return ValueUtilsSmi.GetSqlMoney(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00047D38 File Offset: 0x00045F38
		public override SqlDateTime GetSqlDateTime(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlDateTime");
			return ValueUtilsSmi.GetSqlDateTime(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00047D60 File Offset: 0x00045F60
		public override SqlDecimal GetSqlDecimal(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlDecimal");
			return ValueUtilsSmi.GetSqlDecimal(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00047D88 File Offset: 0x00045F88
		public override SqlString GetSqlString(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlString");
			return ValueUtilsSmi.GetSqlString(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00047DB0 File Offset: 0x00045FB0
		public override SqlGuid GetSqlGuid(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlGuid");
			return ValueUtilsSmi.GetSqlGuid(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00047DD8 File Offset: 0x00045FD8
		public override SqlChars GetSqlChars(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlChars");
			return ValueUtilsSmi.GetSqlChars(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.InternalContext);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00047E0B File Offset: 0x0004600B
		public override SqlBytes GetSqlBytes(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlBytes");
			return ValueUtilsSmi.GetSqlBytes(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.InternalContext);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00047E3E File Offset: 0x0004603E
		public override SqlXml GetSqlXml(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlXml");
			return ValueUtilsSmi.GetSqlXml(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.InternalContext);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00047E71 File Offset: 0x00046071
		public override TimeSpan GetTimeSpan(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetTimeSpan");
			return ValueUtilsSmi.GetTimeSpan(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.Is2008OrNewer);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00047EA4 File Offset: 0x000460A4
		public override DateTimeOffset GetDateTimeOffset(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetDateTimeOffset");
			return ValueUtilsSmi.GetDateTimeOffset(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.Is2008OrNewer);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00047ED8 File Offset: 0x000460D8
		public override object GetSqlValue(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetSqlValue");
			SmiMetaData smiMetaData = this._currentMetaData[ordinal];
			if (this._currentConnection.Is2008OrNewer)
			{
				return ValueUtilsSmi.GetSqlValue200(this._readerEventSink, (SmiTypedGetterSetter)this._currentColumnValuesV3, ordinal, smiMetaData, this._currentConnection.InternalContext);
			}
			return ValueUtilsSmi.GetSqlValue(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiMetaData, this._currentConnection.InternalContext);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00047F4C File Offset: 0x0004614C
		public override int GetSqlValues(object[] values)
		{
			this.EnsureCanGetCol(0, "GetSqlValues");
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this._visibleColumnCount) ? values.Length : this._visibleColumnCount);
			for (int i = 0; i < num; i++)
			{
				values[this._indexMap[i]] = this.GetSqlValue(i);
			}
			return num;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00047FA8 File Offset: 0x000461A8
		public override bool HasRows
		{
			get
			{
				return this._hasRows;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00047FB0 File Offset: 0x000461B0
		public override Stream GetStream(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetStream");
			SmiQueryMetaData smiQueryMetaData = this._currentMetaData[ordinal];
			if (smiQueryMetaData.SqlDbType == SqlDbType.Variant || !base.IsCommandBehavior(CommandBehavior.SequentialAccess) || ValueUtilsSmi.IsDBNull(this._readerEventSink, this._currentColumnValuesV3, ordinal))
			{
				return ValueUtilsSmi.GetStream(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData, false);
			}
			if (this.HasActiveStreamOrTextReaderOnColumn(ordinal))
			{
				throw ADP.NonSequentialColumnAccess(ordinal, ordinal + 1);
			}
			this._currentStream = ValueUtilsSmi.GetSequentialStream(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData, false);
			return this._currentStream;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00048044 File Offset: 0x00046244
		public override TextReader GetTextReader(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetTextReader");
			SmiQueryMetaData smiQueryMetaData = this._currentMetaData[ordinal];
			if (smiQueryMetaData.SqlDbType == SqlDbType.Variant || !base.IsCommandBehavior(CommandBehavior.SequentialAccess) || ValueUtilsSmi.IsDBNull(this._readerEventSink, this._currentColumnValuesV3, ordinal))
			{
				return ValueUtilsSmi.GetTextReader(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData);
			}
			if (this.HasActiveStreamOrTextReaderOnColumn(ordinal))
			{
				throw ADP.NonSequentialColumnAccess(ordinal, ordinal + 1);
			}
			this._currentTextReader = ValueUtilsSmi.GetSequentialTextReader(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData);
			return this._currentTextReader;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000480D4 File Offset: 0x000462D4
		public override XmlReader GetXmlReader(int ordinal)
		{
			this.EnsureCanGetCol(ordinal, "GetXmlReader");
			if (this._currentMetaData[ordinal].SqlDbType != SqlDbType.Xml)
			{
				throw ADP.InvalidCast();
			}
			Stream stream;
			if (base.IsCommandBehavior(CommandBehavior.SequentialAccess) && !ValueUtilsSmi.IsDBNull(this._readerEventSink, this._currentColumnValuesV3, ordinal))
			{
				if (this.HasActiveStreamOrTextReaderOnColumn(ordinal))
				{
					throw ADP.NonSequentialColumnAccess(ordinal, ordinal + 1);
				}
				this._currentStream = ValueUtilsSmi.GetSequentialStream(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], true);
				stream = this._currentStream;
			}
			else
			{
				stream = ValueUtilsSmi.GetStream(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], true);
			}
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(stream, false, false);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00048184 File Offset: 0x00046384
		internal SqlDataReaderSmi(SmiEventStream eventStream, SqlCommand parent, CommandBehavior behavior, SqlInternalConnectionSmi connection, SmiEventSink parentSink, SmiRequestExecutor requestExecutor)
			: base(parent, behavior)
		{
			this._eventStream = eventStream;
			this._currentConnection = connection;
			this._readerEventSink = new SqlDataReaderSmi.ReaderEventSink(this, parentSink);
			this._currentPosition = SqlDataReaderSmi.PositionState.BeforeResults;
			this._isOpen = true;
			this._indexMap = null;
			this._visibleColumnCount = 0;
			this._currentStream = null;
			this._currentTextReader = null;
			this._requestExecutor = requestExecutor;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000481E8 File Offset: 0x000463E8
		internal override SmiExtendedMetaData[] GetInternalSmiMetaData()
		{
			if (this._currentMetaData == null || this._visibleColumnCount == this.InternalFieldCount)
			{
				return this._currentMetaData;
			}
			SmiExtendedMetaData[] array = new SmiExtendedMetaData[this._visibleColumnCount];
			for (int i = 0; i < this._visibleColumnCount; i++)
			{
				array[i] = this._currentMetaData[this._indexMap[i]];
			}
			return array;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00048244 File Offset: 0x00046444
		internal override int GetLocaleId(int ordinal)
		{
			this.EnsureCanGetMetaData("GetLocaleId");
			return (int)this._currentMetaData[ordinal].LocaleId;
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004825F File Offset: 0x0004645F
		private int InternalFieldCount
		{
			get
			{
				if (this.FNotInResults())
				{
					return 0;
				}
				return this._currentMetaData.Length;
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00048273 File Offset: 0x00046473
		private bool IsReallyClosed()
		{
			return !this._isOpen;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0004827E File Offset: 0x0004647E
		internal void ThrowIfClosed([CallerMemberName] string operationName = null)
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed(operationName);
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004828F File Offset: 0x0004648F
		private void EnsureCanGetCol(int ordinal, [CallerMemberName] string operationName = null)
		{
			this.EnsureOnRow(operationName);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00048298 File Offset: 0x00046498
		internal void EnsureOnRow(string operationName)
		{
			this.ThrowIfClosed(operationName);
			if (this._currentPosition != SqlDataReaderSmi.PositionState.OnRow)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000482B0 File Offset: 0x000464B0
		internal void EnsureCanGetMetaData([CallerMemberName] string operationName = null)
		{
			this.ThrowIfClosed(operationName);
			if (this.FNotInResults())
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000482C7 File Offset: 0x000464C7
		private bool FInResults()
		{
			return !this.FNotInResults();
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000482D2 File Offset: 0x000464D2
		private bool FNotInResults()
		{
			return SqlDataReaderSmi.PositionState.AfterResults == this._currentPosition || this._currentPosition == SqlDataReaderSmi.PositionState.BeforeResults;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000482E8 File Offset: 0x000464E8
		private void MetaDataAvailable(SmiQueryMetaData[] md, bool nextEventIsRow)
		{
			this._currentMetaData = md;
			this._hasRows = nextEventIsRow;
			this._fieldNameLookup = null;
			this._schemaTable = null;
			this._currentPosition = SqlDataReaderSmi.PositionState.BeforeRows;
			this._indexMap = new int[this._currentMetaData.Length];
			int num = 0;
			for (int i = 0; i < this._currentMetaData.Length; i++)
			{
				if (!this._currentMetaData[i].IsHidden.IsTrue)
				{
					this._indexMap[num] = i;
					num++;
				}
			}
			this._visibleColumnCount = num;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0004836C File Offset: 0x0004656C
		private bool HasActiveStreamOrTextReaderOnColumn(int columnIndex)
		{
			bool flag = false;
			flag |= this._currentStream != null && this._currentStream.ColumnIndex == columnIndex;
			return flag | (this._currentTextReader != null && this._currentTextReader.ColumnIndex == columnIndex);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000483B4 File Offset: 0x000465B4
		private void RowAvailable(ITypedGetters row)
		{
			this._currentColumnValues = row;
			this._currentPosition = SqlDataReaderSmi.PositionState.OnRow;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000483C4 File Offset: 0x000465C4
		private void RowAvailable(ITypedGettersV3 row)
		{
			this._currentColumnValuesV3 = row;
			this._currentPosition = SqlDataReaderSmi.PositionState.OnRow;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000483D4 File Offset: 0x000465D4
		private void StatementCompleted()
		{
			this._currentPosition = SqlDataReaderSmi.PositionState.AfterRows;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000483DD File Offset: 0x000465DD
		private void ResetResultSet()
		{
			this._currentMetaData = null;
			this._visibleColumnCount = 0;
			this._schemaTable = null;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000483F4 File Offset: 0x000465F4
		private void BatchCompleted()
		{
			this.ResetResultSet();
			this._currentPosition = SqlDataReaderSmi.PositionState.AfterResults;
			this._eventStream.Close(this._readerEventSink);
		}

		// Token: 0x04000767 RID: 1895
		private SqlDataReaderSmi.PositionState _currentPosition;

		// Token: 0x04000768 RID: 1896
		private bool _isOpen;

		// Token: 0x04000769 RID: 1897
		private SmiQueryMetaData[] _currentMetaData;

		// Token: 0x0400076A RID: 1898
		private int[] _indexMap;

		// Token: 0x0400076B RID: 1899
		private int _visibleColumnCount;

		// Token: 0x0400076C RID: 1900
		private DataTable _schemaTable;

		// Token: 0x0400076D RID: 1901
		private ITypedGetters _currentColumnValues;

		// Token: 0x0400076E RID: 1902
		private ITypedGettersV3 _currentColumnValuesV3;

		// Token: 0x0400076F RID: 1903
		private bool _hasRows;

		// Token: 0x04000770 RID: 1904
		private SmiEventStream _eventStream;

		// Token: 0x04000771 RID: 1905
		private SmiRequestExecutor _requestExecutor;

		// Token: 0x04000772 RID: 1906
		private SqlInternalConnectionSmi _currentConnection;

		// Token: 0x04000773 RID: 1907
		private SqlDataReaderSmi.ReaderEventSink _readerEventSink;

		// Token: 0x04000774 RID: 1908
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000775 RID: 1909
		private SqlSequentialStreamSmi _currentStream;

		// Token: 0x04000776 RID: 1910
		private SqlSequentialTextReaderSmi _currentTextReader;

		// Token: 0x02000242 RID: 578
		internal enum PositionState
		{
			// Token: 0x0400166D RID: 5741
			BeforeResults,
			// Token: 0x0400166E RID: 5742
			BeforeRows,
			// Token: 0x0400166F RID: 5743
			OnRow,
			// Token: 0x04001670 RID: 5744
			AfterRows,
			// Token: 0x04001671 RID: 5745
			AfterResults
		}

		// Token: 0x02000243 RID: 579
		private sealed class ReaderEventSink : SmiEventSink_Default
		{
			// Token: 0x06001EAB RID: 7851 RVA: 0x0007D784 File Offset: 0x0007B984
			internal ReaderEventSink(SqlDataReaderSmi reader, SmiEventSink parent)
				: base(parent)
			{
				this._reader = reader;
			}

			// Token: 0x06001EAC RID: 7852 RVA: 0x0007D794 File Offset: 0x0007B994
			internal override void MetaDataAvailable(SmiQueryMetaData[] md, bool nextEventIsRow)
			{
				int num = ((md != null) ? md.Length : (-1));
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int, bool>("<sc.SqlDataReaderSmi.ReaderEventSink.MetaDataAvailable|ADV> {0}, md.Length={1} nextEventIsRow={2}.", this._reader.ObjectID, num, nextEventIsRow);
				if (SqlClientEventSource.Log.IsAdvancedTraceOn() && md != null)
				{
					for (int i = 0; i < md.Length; i++)
					{
						SqlClientEventSource.Log.TraceEvent<int, int, Type, string>("<sc.SqlDataReaderSmi.ReaderEventSink.MetaDataAvailable|ADV> {0}, metaData[{1}] is {2}{3}", this._reader.ObjectID, i, md[i].GetType(), md[i].TraceString());
					}
				}
				this._reader.MetaDataAvailable(md, nextEventIsRow);
			}

			// Token: 0x06001EAD RID: 7853 RVA: 0x0007D81C File Offset: 0x0007BA1C
			internal override void RowAvailable(ITypedGetters row)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlDataReaderSmi.ReaderEventSink.RowAvailable|ADV> {0} (v2).", this._reader.ObjectID);
				this._reader.RowAvailable(row);
			}

			// Token: 0x06001EAE RID: 7854 RVA: 0x0007D844 File Offset: 0x0007BA44
			internal override void RowAvailable(ITypedGettersV3 row)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlDataReaderSmi.ReaderEventSink.RowAvailable|ADV> {0} (ITypedGettersV3).", this._reader.ObjectID);
				this._reader.RowAvailable(row);
			}

			// Token: 0x06001EAF RID: 7855 RVA: 0x0007D86C File Offset: 0x0007BA6C
			internal override void RowAvailable(SmiTypedGetterSetter rowData)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlDataReaderSmi.ReaderEventSink.RowAvailable|ADV> {0} (SmiTypedGetterSetter).", this._reader.ObjectID);
				this._reader.RowAvailable(rowData);
			}

			// Token: 0x06001EB0 RID: 7856 RVA: 0x0007D894 File Offset: 0x0007BA94
			internal override void StatementCompleted(int recordsAffected)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.SqlDataReaderSmi.ReaderEventSink.StatementCompleted|ADV> {0} recordsAffected= {1}.", this._reader.ObjectID, recordsAffected);
				base.StatementCompleted(recordsAffected);
				this._reader.StatementCompleted();
			}

			// Token: 0x06001EB1 RID: 7857 RVA: 0x0007D8C3 File Offset: 0x0007BAC3
			internal override void BatchCompleted()
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlDataReaderSmi.ReaderEventSink.BatchCompleted|ADV> {0}.", this._reader.ObjectID);
				base.BatchCompleted();
				this._reader.BatchCompleted();
			}

			// Token: 0x04001672 RID: 5746
			private readonly SqlDataReaderSmi _reader;
		}
	}
}
