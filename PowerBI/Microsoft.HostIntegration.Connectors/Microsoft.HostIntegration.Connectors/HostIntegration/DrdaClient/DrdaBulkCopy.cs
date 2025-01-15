using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009B2 RID: 2482
	public sealed class DrdaBulkCopy : IDisposable
	{
		// Token: 0x06004CC2 RID: 19650 RVA: 0x00132A4E File Offset: 0x00130C4E
		public DrdaBulkCopy(DrdaConnection connection, DrdaTransaction externalTransaction)
			: this(connection)
		{
			this._externalTransaction = externalTransaction;
			this._connection.Transaction = this._externalTransaction;
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x00132A6F File Offset: 0x00130C6F
		public DrdaBulkCopy(string connectionString)
			: this(new DrdaConnection(connectionString))
		{
			this._ownConnection = true;
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x00132A84 File Offset: 0x00130C84
		public DrdaBulkCopy(DrdaConnection connection)
		{
			this._connection = connection;
			this._originalConnectionState = this._connection.State;
			this._ownConnection = false;
			this._batchSize = 100;
			this._notifyAfter = 0;
			this._destinationTableName = null;
			this._timeoutValue = 30;
			this._externalTransaction = null;
			this._dataList = new List<object[]>();
			this._isDestinationTableSchemaDirty = true;
			this._mappingsType = DrdaBulkCopyMappingsType.Unknown;
			this._columnMappings = new DrdaBulkCopyColumnMappingCollection();
			this._destinationFieldsMap = new SortedDictionary<string, int>();
			this._destinationBindingList = new List<DrdaColumnBinding>();
			this._statement = null;
			this._prepared = false;
			this._copiedRows = 0;
			this._totalCopiedRows = 0;
			this._fieldMap = new SortedDictionary<int, BulkCopyFieldInfo>();
			this._timeout = new Timeout((double)(this._timeoutValue * 1000), new TimeoutCallback(this.SendTimeout), null);
			this._connection.TransactionStateEventHandler += this.TransactionStateChanged;
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x00132B78 File Offset: 0x00130D78
		public void Close()
		{
			this.CloseInternalAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x00132B9E File Offset: 0x00130D9E
		public Task CloseAsync(CancellationToken cancellationToken)
		{
			return this.CloseInternalAsync(true, cancellationToken);
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x00132BA8 File Offset: 0x00130DA8
		private void InsertRow(object[] values)
		{
			List<object[]> dataList = this._dataList;
			lock (dataList)
			{
				this._dataList.Add(values);
			}
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x00132BF0 File Offset: 0x00130DF0
		private void ReleaseBuffer()
		{
			this._dataList.Clear();
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x00132C00 File Offset: 0x00130E00
		private async Task CommitAsync(bool isAsync, CancellationToken cancellationToken)
		{
			int numRows = this._dataList.Count;
			if (numRows != 0)
			{
				this._timeout.Start();
				try
				{
					await this._statement.InsertRowsAsync(this._dataList, isAsync, cancellationToken);
				}
				finally
				{
					this._timeout.Stop();
					this.ReleaseBuffer();
				}
				this._copiedRows += numRows;
				this._totalCopiedRows += numRows;
				if (this._copiedRows >= this._notifyAfter && this._notifyAfter > 0)
				{
					DrdaRowsCopiedEventArgs drdaRowsCopiedEventArgs = new DrdaRowsCopiedEventArgs((long)this._totalCopiedRows);
					this.DrdaRowsCopied(this, drdaRowsCopiedEventArgs);
					this._copiedRows = 0;
				}
			}
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x00132C58 File Offset: 0x00130E58
		private async Task InternalPrepareAsync(IBulkCopySourceInfo info, bool isAsync, CancellationToken cancellationToken)
		{
			this.UpdateDestinationTableSchema();
			this.ValidateMappings();
			this._fieldMap.Clear();
			this._prepared = false;
			switch (this._mappingsType)
			{
			case DrdaBulkCopyMappingsType.Empty:
				goto IL_0363;
			case DrdaBulkCopyMappingsType.OrdinalToString:
			{
				using (IEnumerator enumerator = this._columnMappings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DrdaBulkCopyColumnMapping drdaBulkCopyColumnMapping = (DrdaBulkCopyColumnMapping)obj;
						if (drdaBulkCopyColumnMapping.SourceOrdinal >= info.FieldCount)
						{
							throw DrdaException.InvalidBulkCopySourceOrdinal(drdaBulkCopyColumnMapping.SourceOrdinal);
						}
						string text = drdaBulkCopyColumnMapping.DestinationColumn.ToUpperInvariant();
						if (!this._destinationFieldsMap.ContainsKey(text))
						{
							throw DrdaException.InvalidBulkCopyDestinationColumn(drdaBulkCopyColumnMapping.DestinationColumn);
						}
						this._fieldMap.Add(drdaBulkCopyColumnMapping.SourceOrdinal, new BulkCopyFieldInfo(this._destinationBindingList[this._destinationFieldsMap[text]]));
					}
					goto IL_039C;
				}
				break;
			}
			case DrdaBulkCopyMappingsType.StringToOrdinal:
				goto IL_01D4;
			case DrdaBulkCopyMappingsType.StringToString:
				goto IL_0294;
			case DrdaBulkCopyMappingsType.OrdinalToOrdinal:
				break;
			default:
				goto IL_039C;
			}
			using (IEnumerator enumerator = this._columnMappings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj2 = enumerator.Current;
					DrdaBulkCopyColumnMapping drdaBulkCopyColumnMapping2 = (DrdaBulkCopyColumnMapping)obj2;
					if (drdaBulkCopyColumnMapping2.SourceOrdinal >= info.FieldCount)
					{
						throw DrdaException.InvalidBulkCopySourceOrdinal(drdaBulkCopyColumnMapping2.SourceOrdinal);
					}
					if (drdaBulkCopyColumnMapping2.DestinationOrdinal >= this._destinationBindingList.Count)
					{
						throw DrdaException.InvalidBulkCopyDestinationOrdinal(drdaBulkCopyColumnMapping2.DestinationOrdinal);
					}
					this._fieldMap.Add(drdaBulkCopyColumnMapping2.SourceOrdinal, new BulkCopyFieldInfo(this._destinationBindingList[drdaBulkCopyColumnMapping2.DestinationOrdinal]));
				}
				goto IL_039C;
			}
			IL_01D4:
			SortedDictionary<string, int> sortedDictionary = this.BuildSourceColumnDictionary(info);
			using (IEnumerator enumerator = this._columnMappings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj3 = enumerator.Current;
					DrdaBulkCopyColumnMapping drdaBulkCopyColumnMapping3 = (DrdaBulkCopyColumnMapping)obj3;
					if (!sortedDictionary.ContainsKey(drdaBulkCopyColumnMapping3.SourceColumn))
					{
						throw DrdaException.InvalidBulkCopySourceColumn(drdaBulkCopyColumnMapping3.SourceColumn);
					}
					if (drdaBulkCopyColumnMapping3.DestinationOrdinal >= this._destinationBindingList.Count)
					{
						throw DrdaException.InvalidBulkCopyDestinationOrdinal(drdaBulkCopyColumnMapping3.DestinationOrdinal);
					}
					this._fieldMap.Add(sortedDictionary[drdaBulkCopyColumnMapping3.SourceColumn], new BulkCopyFieldInfo(this._destinationBindingList[drdaBulkCopyColumnMapping3.DestinationOrdinal]));
				}
				goto IL_039C;
			}
			IL_0294:
			sortedDictionary = this.BuildSourceColumnDictionary(info);
			using (IEnumerator enumerator = this._columnMappings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj4 = enumerator.Current;
					DrdaBulkCopyColumnMapping drdaBulkCopyColumnMapping4 = (DrdaBulkCopyColumnMapping)obj4;
					if (!sortedDictionary.ContainsKey(drdaBulkCopyColumnMapping4.SourceColumn))
					{
						throw DrdaException.InvalidBulkCopySourceColumn(drdaBulkCopyColumnMapping4.SourceColumn);
					}
					string text2 = drdaBulkCopyColumnMapping4.DestinationColumn.ToUpperInvariant();
					if (!this._destinationFieldsMap.ContainsKey(text2))
					{
						throw DrdaException.InvalidBulkCopyDestinationColumn(drdaBulkCopyColumnMapping4.DestinationColumn);
					}
					this._fieldMap.Add(sortedDictionary[drdaBulkCopyColumnMapping4.SourceColumn], new BulkCopyFieldInfo(this._destinationBindingList[this._destinationFieldsMap[text2]]));
				}
				goto IL_039C;
			}
			IL_0363:
			for (int i = 0; i < this._destinationBindingList.Count; i++)
			{
				this._fieldMap.Add(i, new BulkCopyFieldInfo(this._destinationBindingList[i]));
			}
			IL_039C:
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			bool flag = true;
			ushort num = 0;
			this._statement = this._connection.CheckOutStatement();
			bool flag2 = this._connection.Requester.Flavor == DrdaFlavor.DB2;
			for (int j = 0; j < info.FieldCount; j++)
			{
				if (this._fieldMap.ContainsKey(j))
				{
					BulkCopyFieldInfo bulkCopyFieldInfo = this._fieldMap[j];
					num += 1;
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(',');
						stringBuilder2.Append(',');
					}
					if (flag2)
					{
						stringBuilder.Append('"');
					}
					stringBuilder.Append(bulkCopyFieldInfo._binding.Name);
					if (flag2)
					{
						stringBuilder.Append('"');
					}
					stringBuilder2.Append('?');
					DrdaMetaType metaTypeForType = DrdaMetaType.GetMetaTypeForType(info.GetFieldType(j));
					bulkCopyFieldInfo._fieldType = metaTypeForType;
					this._fieldMap[j] = bulkCopyFieldInfo;
				}
			}
			string text3 = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", this._destinationTableName, stringBuilder.ToString(), stringBuilder2.ToString());
			DrdaCommand.SetSqlAttribute(this._connection, this._statement, SqlTypeEnum.NonQueryBatch);
			await this._statement.PrepareAsync(text3, isAsync, cancellationToken);
			this._prepared = true;
			this._copiedRows = 0;
			this._totalCopiedRows = 0;
			this.ReleaseBuffer();
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x00132CB8 File Offset: 0x00130EB8
		private SortedDictionary<string, int> BuildSourceColumnDictionary(IBulkCopySourceInfo info)
		{
			SortedDictionary<string, int> sortedDictionary = new SortedDictionary<string, int>();
			for (int i = 0; i < info.FieldCount; i++)
			{
				sortedDictionary.Add(info.GetFieldName(i), i);
			}
			return sortedDictionary;
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x00132CEC File Offset: 0x00130EEC
		private void UpdateDestinationTableSchema()
		{
			if (!this._isDestinationTableSchemaDirty || string.IsNullOrWhiteSpace(this._destinationTableName))
			{
				return;
			}
			this._destinationFieldsMap.Clear();
			this._destinationBindingList.Clear();
			if (this._connection.State == global::System.Data.ConnectionState.Closed)
			{
				this._connection.Open();
			}
			string text = this._connection.DefaultSchema;
			string text2 = this._destinationTableName;
			if (this._connection.BulkCopySchema)
			{
				int num = this._destinationTableName.IndexOf(".");
				if (num != -1)
				{
					text = this._destinationTableName.Substring(0, num);
					text2 = this._destinationTableName.Substring(num + 1, this._destinationTableName.Length - num - 1);
				}
				else
				{
					text = this._connection.DefaultSchema;
					text2 = this._destinationTableName;
				}
			}
			string[] array = new string[4];
			array[0] = this._connection.InitialCatalog;
			array[1] = text;
			array[2] = text2;
			string[] array2 = array;
			DataTable schema = this._connection.GetSchema("Columns", array2);
			for (int i = 0; i < schema.Rows.Count; i++)
			{
				DrdaColumnBinding drdaColumnBinding = this.ToBindign(schema.Rows[i]);
				this._destinationFieldsMap.Add(drdaColumnBinding.Name.ToUpperInvariant(), i);
				this._destinationBindingList.Add(drdaColumnBinding);
			}
			this._isDestinationTableSchemaDirty = false;
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x00132E44 File Offset: 0x00131044
		private DrdaColumnBinding ToBindign(DataRow row)
		{
			DrdaColumnBinding drdaColumnBinding = new DrdaColumnBinding();
			if (row == null)
			{
				return drdaColumnBinding;
			}
			drdaColumnBinding.BaseTable = (string)row["TableName"];
			drdaColumnBinding.Schema = (string)row["TableSchema"];
			drdaColumnBinding.Catalog = (string)row["TableCatalog"];
			drdaColumnBinding.Precision = (short)((int)row["Precision"]);
			drdaColumnBinding.Scale = (short)((int)row["Scale"]);
			drdaColumnBinding.Size = (int)row["Length"];
			DrdaType drdaType = (DrdaType)row["DataType"];
			drdaColumnBinding.IsLob = drdaType == DrdaType.BLOB || drdaType == DrdaType.CLOB || drdaType == DrdaType.DBCLOB || drdaType == DrdaType.Xml;
			drdaColumnBinding.Type = DrdaMetaType.GetMetaTypeForType(DataTypeConverter.ToDrdaClientType(drdaType));
			drdaColumnBinding.IsNullable = (bool)row["Nullable"];
			drdaColumnBinding.Name = (string)row["ColumnName"];
			return drdaColumnBinding;
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x00132F50 File Offset: 0x00131150
		private void ValidateMappings()
		{
			this._mappingsType = DrdaBulkCopyMappingsType.Unknown;
			if (this._columnMappings.Count == 0)
			{
				this._mappingsType = DrdaBulkCopyMappingsType.Empty;
				return;
			}
			this._mappingsType = this._columnMappings[0].GetMappingType();
			for (int i = 1; i < this._columnMappings.Count; i++)
			{
				if (this._mappingsType != this._columnMappings[i].GetMappingType())
				{
					this._mappingsType = DrdaBulkCopyMappingsType.Invalid;
					throw DrdaException.InvalidBulkCopyMappingCollectionTypes();
				}
			}
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x00132FCC File Offset: 0x001311CC
		internal void SendTimeout(object state)
		{
			try
			{
				Trace.MessageTrace(Trace.GetTracePoint(this._connection), "BulkCopy Timeout. Sending Cancel command...");
				Task.Factory.StartNew<Task>(() => this._connection.Requester.InterruptAsync(true, CancellationToken.None));
				Trace.MessageTrace(Trace.GetTracePoint(this._connection), "BulkCopy Timeout. Cancel command has been sent.");
			}
			catch
			{
			}
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x00133030 File Offset: 0x00131230
		public void WriteToServer(DataRow[] rows)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.InternalWriteToServerAsync(rows, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x00133067 File Offset: 0x00131267
		public Task WriteToServerAsync(DataRow[] rows)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalWriteToServerAsync(rows, true, CancellationToken.None);
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x00133086 File Offset: 0x00131286
		public Task WriteToServerAsync(DataRow[] rows, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalWriteToServerAsync(rows, true, cancellationToken);
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x001330A4 File Offset: 0x001312A4
		private async Task InternalWriteToServerAsync(DataRow[] rows, bool isAsync, CancellationToken cancellationToken)
		{
			bool isPrepared = false;
			int numRows = 1;
			foreach (DataRow row in rows)
			{
				if (!isPrepared)
				{
					await this.InternalPrepareAsync(new BulkCopyDataTableInfo(row.Table), isAsync, cancellationToken);
					isPrepared = true;
				}
				this._dataList.Add(row.ItemArray);
				if (this._batchSize > 0 && numRows % this._batchSize == 0)
				{
					await this.CommitAsync(isAsync, cancellationToken);
				}
				numRows++;
				row = null;
			}
			DataRow[] array = null;
			if (numRows > 1 && this._dataList.Count<object[]>() > 0)
			{
				await this.CommitAsync(isAsync, cancellationToken);
			}
			await this.ReleaseStatementAsync(isAsync, cancellationToken);
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x00133104 File Offset: 0x00131304
		public void WriteToServer(DataTable table, DataRowState rowState)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.InternalWriteToServerAsync(table, rowState, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x0013313C File Offset: 0x0013133C
		public Task WriteToServerAsync(DataTable table, DataRowState rowState)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalWriteToServerAsync(table, rowState, true, CancellationToken.None);
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x0013315C File Offset: 0x0013135C
		public Task WriteToServerAsync(DataTable table, DataRowState rowState, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalWriteToServerAsync(table, rowState, true, cancellationToken);
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x00133178 File Offset: 0x00131378
		private async Task InternalWriteToServerAsync(DataTable table, DataRowState rowState, bool isAsync, CancellationToken cancellationToken)
		{
			await this.InternalPrepareAsync(new BulkCopyDataTableInfo(table), isAsync, cancellationToken);
			int numRows = 1;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((dataRow.RowState & rowState) != (DataRowState)0)
				{
					this._dataList.Add(dataRow.ItemArray);
					if (this._batchSize > 0 && numRows % this._batchSize == 0)
					{
						await this.CommitAsync(isAsync, cancellationToken);
					}
					numRows++;
				}
			}
			IEnumerator enumerator = null;
			if (numRows > 1 && this._dataList.Count<object[]>() > 0)
			{
				await this.CommitAsync(isAsync, cancellationToken);
			}
			await this.ReleaseStatementAsync(isAsync, cancellationToken);
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x001331E0 File Offset: 0x001313E0
		public void WriteToServer(DataTable table)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.InternalWriteToServerAsync(table, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x00133217 File Offset: 0x00131417
		public Task WriteToServerAsync(DataTable table)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalWriteToServerAsync(table, true, CancellationToken.None);
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x00133236 File Offset: 0x00131436
		public Task WriteToServerAsync(DataTable table, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalWriteToServerAsync(table, true, cancellationToken);
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x00133254 File Offset: 0x00131454
		private async Task InternalWriteToServerAsync(DataTable table, bool isAsync, CancellationToken cancellationToken)
		{
			await this.InternalPrepareAsync(new BulkCopyDataTableInfo(table), isAsync, cancellationToken);
			int numRows = 1;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				this._dataList.Add(dataRow.ItemArray);
				if (this._batchSize > 0 && numRows % this._batchSize == 0)
				{
					await this.CommitAsync(isAsync, cancellationToken);
				}
				numRows++;
			}
			IEnumerator enumerator = null;
			if (numRows > 1 && this._dataList.Count<object[]>() > 0)
			{
				await this.CommitAsync(isAsync, cancellationToken);
			}
			await this.ReleaseStatementAsync(isAsync, cancellationToken);
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x001332B4 File Offset: 0x001314B4
		public void WriteToServer(IDataReader reader)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.InternalWriteToServerAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x001332EB File Offset: 0x001314EB
		public Task WriteToServerAsync(IDataReader reader)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalWriteToServerAsync(reader, true, CancellationToken.None);
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x0013330A File Offset: 0x0013150A
		public Task WriteToServerAsync(IDataReader reader, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalWriteToServerAsync(reader, true, cancellationToken);
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x00133328 File Offset: 0x00131528
		private async Task InternalWriteToServerAsync(IDataReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			await this.InternalPrepareAsync(new BulkCopyReaderInfo(reader), isAsync, cancellationToken);
			int numRows = 1;
			while (reader.Read())
			{
				object[] array = new object[reader.FieldCount];
				reader.GetValues(array);
				this._dataList.Add(array);
				if (this._batchSize > 0 && numRows % this._batchSize == 0)
				{
					await this.CommitAsync(isAsync, cancellationToken);
				}
				numRows++;
			}
			if (numRows > 1 && this._dataList.Count<object[]>() > 0)
			{
				await this.CommitAsync(isAsync, cancellationToken);
			}
			await this.ReleaseStatementAsync(isAsync, cancellationToken);
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x00133385 File Offset: 0x00131585
		// (set) Token: 0x06004CE1 RID: 19681 RVA: 0x0013338D File Offset: 0x0013158D
		public int BatchSize
		{
			get
			{
				return this._batchSize;
			}
			set
			{
				this._batchSize = value;
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06004CE3 RID: 19683 RVA: 0x001333BD File Offset: 0x001315BD
		// (set) Token: 0x06004CE2 RID: 19682 RVA: 0x00133396 File Offset: 0x00131596
		public int BulkCopyTimeout
		{
			get
			{
				return this._timeoutValue;
			}
			set
			{
				if (value < 0)
				{
					throw DrdaException.InvalidBulkCopyTimeout(value);
				}
				this._timeoutValue = value;
				this._timeout.Interval = (double)(value * 1000);
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x001333C5 File Offset: 0x001315C5
		public DrdaBulkCopyColumnMappingCollection ColumnMappings
		{
			get
			{
				return this._columnMappings;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06004CE6 RID: 19686 RVA: 0x001333F4 File Offset: 0x001315F4
		// (set) Token: 0x06004CE5 RID: 19685 RVA: 0x001333CD File Offset: 0x001315CD
		public string DestinationTableName
		{
			get
			{
				return this._destinationTableName;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value) || !string.Equals(this._destinationTableName, value, StringComparison.OrdinalIgnoreCase))
				{
					this._isDestinationTableSchemaDirty = true;
				}
				this._destinationTableName = value;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x001333FC File Offset: 0x001315FC
		public int RowsCopied
		{
			get
			{
				return this._totalCopiedRows;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x0013340D File Offset: 0x0013160D
		// (set) Token: 0x06004CE8 RID: 19688 RVA: 0x00133404 File Offset: 0x00131604
		public int NotifyAfter
		{
			get
			{
				return this._notifyAfter;
			}
			set
			{
				this._notifyAfter = value;
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06004CEA RID: 19690 RVA: 0x00133418 File Offset: 0x00131618
		// (remove) Token: 0x06004CEB RID: 19691 RVA: 0x00133450 File Offset: 0x00131650
		public event DrdaRowsCopiedEventHandler DrdaRowsCopied;

		// Token: 0x06004CEC RID: 19692 RVA: 0x00133488 File Offset: 0x00131688
		public void Dispose()
		{
			this._timeout.Dispose();
			if (this._ownConnection)
			{
				if (this._connection != null)
				{
					this._connection.Dispose();
					this._connection = null;
				}
			}
			else
			{
				this.CloseInternalAsync(true, CancellationToken.None).GetAwaiter().GetResult();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x001334E4 File Offset: 0x001316E4
		private async Task ReleaseStatementAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this._statement != null)
			{
				if (this._connection != null)
				{
					if (this._prepared)
					{
						await this._statement.CloseAsync(isAsync, cancellationToken);
						this._prepared = false;
					}
					else
					{
						await this._connection.CheckInStatementAsync(this._statement, isAsync, cancellationToken);
					}
				}
				this._statement = null;
			}
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x0013353C File Offset: 0x0013173C
		private async Task CloseInternalAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.ReleaseStatementAsync(isAsync, cancellationToken);
			if (this._connection != null)
			{
				this._connection.TransactionStateEventHandler -= this.TransactionStateChanged;
				if (this._originalConnectionState == global::System.Data.ConnectionState.Closed && !this._connection.IsClosing)
				{
					this._connection.Close();
				}
				this._connection = null;
			}
			this.ReleaseBuffer();
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x00133594 File Offset: 0x00131794
		private void TransactionStateChanged(object sender, EventArgs e)
		{
			TransactionState state = ((TransactionStateEventArgs)e).State;
			if (state != TransactionState.Commit && state == TransactionState.RollBack)
			{
				this._totalCopiedRows = 0;
			}
		}

		// Token: 0x04003CB0 RID: 15536
		private DrdaTransaction _externalTransaction;

		// Token: 0x04003CB1 RID: 15537
		private DrdaConnection _connection;

		// Token: 0x04003CB2 RID: 15538
		private DrdaBulkCopyColumnMappingCollection _columnMappings;

		// Token: 0x04003CB3 RID: 15539
		private bool _ownConnection;

		// Token: 0x04003CB4 RID: 15540
		private global::System.Data.ConnectionState _originalConnectionState;

		// Token: 0x04003CB5 RID: 15541
		private string _destinationTableName;

		// Token: 0x04003CB6 RID: 15542
		private int _batchSize;

		// Token: 0x04003CB7 RID: 15543
		private int _notifyAfter;

		// Token: 0x04003CB8 RID: 15544
		private int _timeoutValue;

		// Token: 0x04003CB9 RID: 15545
		private int _copiedRows;

		// Token: 0x04003CBA RID: 15546
		private int _totalCopiedRows;

		// Token: 0x04003CBB RID: 15547
		private Timeout _timeout;

		// Token: 0x04003CBC RID: 15548
		private ISqlStatement _statement;

		// Token: 0x04003CBD RID: 15549
		private bool _prepared;

		// Token: 0x04003CBE RID: 15550
		private bool _isDestinationTableSchemaDirty;

		// Token: 0x04003CBF RID: 15551
		private SortedDictionary<int, BulkCopyFieldInfo> _fieldMap;

		// Token: 0x04003CC0 RID: 15552
		private DrdaBulkCopyMappingsType _mappingsType;

		// Token: 0x04003CC1 RID: 15553
		private List<object[]> _dataList;

		// Token: 0x04003CC2 RID: 15554
		private SortedDictionary<string, int> _destinationFieldsMap;

		// Token: 0x04003CC3 RID: 15555
		private List<DrdaColumnBinding> _destinationBindingList;
	}
}
