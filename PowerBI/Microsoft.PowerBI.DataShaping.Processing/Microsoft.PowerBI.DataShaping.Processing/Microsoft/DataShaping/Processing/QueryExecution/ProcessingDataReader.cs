using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.Processing.Analytics;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000063 RID: 99
	internal sealed class ProcessingDataReader : IRowSource, IResultSet, IResultSetSource
	{
		// Token: 0x0600024F RID: 591 RVA: 0x000068D8 File Offset: 0x00004AD8
		internal ProcessingDataReader(Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService, Microsoft.DataShaping.ServiceContracts.ITracer tracer, DataSet dataSet, IDataReader dataReader)
		{
			this._telemetryService = telemetryService;
			this._tracer = tracer;
			this._dataSet = dataSet;
			this._dataReader = dataReader;
			this._diagnosticRowBuffer = new DiagnosticDataRowBuffer(3);
			this._rowIndexInResultSets = new List<long>(1);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00006915 File Offset: 0x00004B15
		public bool NextResultSet()
		{
			return !this.IsClosed() && !this._isOutOfResultSets && this._telemetryService.RunInActivity<bool>(ActivityKind.NextResultSet, delegate
			{
				if (!this._dataReader.NextResult())
				{
					this._isOutOfResultSets = true;
					return false;
				}
				this._isCurrentResultExhausted = false;
				this._resultSetIndex++;
				this._rowIndexInResultSets.Add(this._rowIndexInCurrentResultSet);
				this._rowIndexInCurrentResultSet = 0L;
				this.BuildColumnMetadata();
				return true;
			});
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00006941 File Offset: 0x00004B41
		public int CurrentResultSetIndex
		{
			get
			{
				return this._resultSetIndex;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00006949 File Offset: 0x00004B49
		public long RowCount
		{
			get
			{
				return this._rowIndexInResultSets.Sum() + this._rowIndexInCurrentResultSet;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000695D File Offset: 0x00004B5D
		public long GetRowCount(int resultSetIndex)
		{
			if (resultSetIndex == this._rowIndexInResultSets.Count)
			{
				return this._rowIndexInCurrentResultSet;
			}
			return this._rowIndexInResultSets[resultSetIndex];
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00006980 File Offset: 0x00004B80
		internal DiagnosticDataRowBuffer DiagnosticRowBuffer
		{
			get
			{
				return this._diagnosticRowBuffer;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00006988 File Offset: 0x00004B88
		internal void BuildColumnMetadata()
		{
			IReadOnlyList<ResultTable> resultTables = this._dataSet.ResultTables;
			if (this._resultSetIndex >= resultTables.Count)
			{
				this.HandleIndexOutOfRange();
			}
			IList<Field> fields = resultTables[this._resultSetIndex].Fields;
			this._columnMetadataByDefnIndex = new ProcessingDataReader.ColumnMetadata[fields.Count];
			for (int i = 0; i < fields.Count; i++)
			{
				Field field = fields[i];
				if (field.IsRowIndex)
				{
					this._columnMetadataByDefnIndex[i] = new ProcessingDataReader.ColumnMetadata
					{
						IsRowIndex = new bool?(true)
					};
				}
				else
				{
					string dataField = field.DataField;
					int ordinal = this._dataReader.GetOrdinal(dataField);
					Contract.RetailAssert(ordinal >= 0, "Could not find result set column for field {0} at index {1} of result table at index {2}", dataField.MarkAsCustomerContent(), i, this._resultSetIndex);
					this._columnMetadataByDefnIndex[i] = new ProcessingDataReader.ColumnMetadata
					{
						Ordinal = new int?(ordinal)
					};
				}
			}
			this._currentSchema = null;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00006A7C File Offset: 0x00004C7C
		private void HandleIndexOutOfRange()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this._dataReader.ColumnCount; i++)
			{
				stringBuilder.Append(this._dataReader.GetColumnName(i) + ",");
			}
			throw new ProcessingException("ArgumentOutOfRangeExceptionInDataReader", StringUtil.FormatInvariant("More result sets than result tables show up. Current resultSetIndex: {0}. ResultTable count: {1}. DataSet id: {2}. Result set column count: {3}. Result set header: {4}.", new object[]
			{
				this._resultSetIndex,
				this._dataSet.ResultTables.Count,
				this._dataSet.Id,
				this._dataReader.ColumnCount,
				stringBuilder.ToString().MarkAsCustomerContent()
			}), null, Microsoft.PowerBI.Query.Contracts.ErrorSource.PowerBI);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006B33 File Offset: 0x00004D33
		internal void Close()
		{
			if (this.IsClosed())
			{
				return;
			}
			this._dataReader.Dispose();
			this._dataReader = null;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00006B50 File Offset: 0x00004D50
		private bool IsOpen()
		{
			return !this.IsClosed() && this._dataReader.IsOpen;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006B67 File Offset: 0x00004D67
		private bool IsClosed()
		{
			return this._dataReader == null;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00006B74 File Offset: 0x00004D74
		public IDataRow ReadRow()
		{
			if (this.IsClosed() || this._isCurrentResultExhausted)
			{
				return null;
			}
			IDataRow dataRow;
			try
			{
				if (!this._dataReader.MoveNext())
				{
					this._isCurrentResultExhausted = true;
					dataRow = null;
				}
				else
				{
					int num = this._columnMetadataByDefnIndex.Length;
					object[] array = new object[num];
					for (int i = 0; i < num; i++)
					{
						if (this._columnMetadataByDefnIndex[i].IsRowIndex != null)
						{
							array[i] = this._rowIndexInCurrentResultSet;
						}
						else
						{
							int value = this._columnMetadataByDefnIndex[i].Ordinal.Value;
							array[i] = this._dataReader.GetValue(value);
						}
					}
					DataRow dataRow2 = new DataRow(array);
					this._diagnosticRowBuffer.PutRow(dataRow2);
					this._rowIndexInCurrentResultSet += 1L;
					dataRow = dataRow2;
				}
			}
			catch (DataExtensionException ex)
			{
				this._tracer.TraceSanitizedError(ex, "An error occurred reading the next row.");
				QueryExecutionUtils.HandleKnownProviderErrors(ex, this._dataSet, this._tracer);
				throw;
			}
			return dataRow;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00006C84 File Offset: 0x00004E84
		public ResultSetSchema Schema
		{
			get
			{
				if (this._currentSchema == null)
				{
					Type[] array = new Type[this._columnMetadataByDefnIndex.Length];
					try
					{
						for (int i = 0; i < this._columnMetadataByDefnIndex.Length; i++)
						{
							if (this._columnMetadataByDefnIndex[i].IsRowIndex != null)
							{
								array[i] = typeof(long);
							}
							else
							{
								int value = this._columnMetadataByDefnIndex[i].Ordinal.Value;
								array[i] = this._dataReader.GetColumnType(value);
							}
						}
					}
					catch (DataExtensionException ex)
					{
						this._tracer.TraceSanitizedError(ex, "An error occurred calling GetColumnType.");
						throw;
					}
					this._currentSchema = new ResultSetSchema(array);
				}
				return this._currentSchema;
			}
		}

		// Token: 0x04000164 RID: 356
		private const int DiagnosticRowBufferSize = 3;

		// Token: 0x04000165 RID: 357
		private readonly Microsoft.DataShaping.ServiceContracts.ITelemetryService _telemetryService;

		// Token: 0x04000166 RID: 358
		private readonly Microsoft.DataShaping.ServiceContracts.ITracer _tracer;

		// Token: 0x04000167 RID: 359
		private readonly DataSet _dataSet;

		// Token: 0x04000168 RID: 360
		private readonly DiagnosticDataRowBuffer _diagnosticRowBuffer;

		// Token: 0x04000169 RID: 361
		private IDataReader _dataReader;

		// Token: 0x0400016A RID: 362
		private ProcessingDataReader.ColumnMetadata[] _columnMetadataByDefnIndex;

		// Token: 0x0400016B RID: 363
		private int _resultSetIndex;

		// Token: 0x0400016C RID: 364
		private bool _isCurrentResultExhausted;

		// Token: 0x0400016D RID: 365
		private bool _isOutOfResultSets;

		// Token: 0x0400016E RID: 366
		private long _rowIndexInCurrentResultSet;

		// Token: 0x0400016F RID: 367
		private List<long> _rowIndexInResultSets;

		// Token: 0x04000170 RID: 368
		private ResultSetSchema _currentSchema;

		// Token: 0x020000C9 RID: 201
		private class ColumnMetadata
		{
			// Token: 0x1700018E RID: 398
			// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000EE12 File Offset: 0x0000D012
			// (set) Token: 0x060004DA RID: 1242 RVA: 0x0000EE1A File Offset: 0x0000D01A
			internal int? Ordinal { get; set; }

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000EE23 File Offset: 0x0000D023
			// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000EE2B File Offset: 0x0000D02B
			internal bool? IsRowIndex { get; set; }
		}
	}
}
