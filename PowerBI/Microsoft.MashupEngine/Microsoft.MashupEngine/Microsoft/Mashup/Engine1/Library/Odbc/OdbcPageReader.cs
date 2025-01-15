using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005A2 RID: 1442
	internal class OdbcPageReader : IPageReader, IDisposable
	{
		// Token: 0x06002D96 RID: 11670 RVA: 0x0008A986 File Offset: 0x00088B86
		private OdbcPageReader(OdbcStatementHandle statement, OdbcBuffer parameterBuffer, RowRange rowRange, OdbcFetchPlan fetchPlan, TableSchema schema)
		{
			this.statement = statement;
			this.parameterBuffer = parameterBuffer;
			this.progress = new ReaderWriterProgress();
			this.rowRange = rowRange;
			this.isFirst = true;
			this.fetchPlan = fetchPlan;
			this.schema = schema;
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x0008A9C5 File Offset: 0x00088BC5
		public TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x0008A9CD File Offset: 0x00088BCD
		public IProgress Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x0008A9D5 File Offset: 0x00088BD5
		public int MaxPageRowCount
		{
			get
			{
				return this.fetchPlan.MaxRowCount;
			}
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x0008A9E2 File Offset: 0x00088BE2
		public IPage CreatePage()
		{
			return new OdbcPageReader.OdbcPage(this.statement, this.schema, this.fetchPlan);
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x0008A9FB File Offset: 0x00088BFB
		public void Read(IPage page)
		{
			this.Read((OdbcPageReader.OdbcPage)page);
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x0008AA0C File Offset: 0x00088C0C
		private void Read(OdbcPageReader.OdbcPage page)
		{
			RowRange rowRange = RowRange.All;
			if (this.isFirst)
			{
				this.isFirst = false;
				rowRange = rowRange.Skip(this.rowRange.SkipCount);
			}
			if (!this.rowRange.TakeCount.IsInfinite)
			{
				rowRange = rowRange.Take(new RowCount(Math.Min(this.rowRange.TakeCount.Value - (long)this.rowsFetched, (long)this.fetchPlan.MaxRowCount)));
			}
			else
			{
				rowRange = rowRange.Take(new RowCount((long)this.fetchPlan.MaxRowCount));
			}
			if (this.pageException != null)
			{
				page.Clear(this.pageException);
				return;
			}
			if (this.eof)
			{
				page.Clear(null);
				return;
			}
			try
			{
				page.Read(this.statement, rowRange);
				this.rowsFetched += page.RowCount;
				this.pageException = page.PageException;
				this.eof = page.Eof;
			}
			catch (Exception ex) when (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
			{
				this.statement.Dispose();
				this.statement = null;
				throw;
			}
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000020FA File Offset: 0x000002FA
		public IPageReader NextResult()
		{
			return null;
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x0008AB54 File Offset: 0x00088D54
		public void Dispose()
		{
			if (this.statement != null)
			{
				Odbc32.RetCode retCode;
				do
				{
					retCode = this.statement.MoreResults();
				}
				while (retCode == Odbc32.RetCode.SUCCESS || retCode == Odbc32.RetCode.SUCCESS_WITH_INFO);
				this.statement.Dispose();
				this.statement = null;
			}
			if (this.parameterBuffer != null)
			{
				this.parameterBuffer.Dispose();
				this.parameterBuffer = null;
			}
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x0008ABA8 File Offset: 0x00088DA8
		public static IPageReader New(OdbcConnectionHandle connection, OdbcStatementHandle statement, OdbcBuffer parameterBuffer, RowRange rowRange, OdbcFetchPlanFactory fetchPlanFactory)
		{
			short num;
			Odbc32.RetCode retCode;
			do
			{
				OdbcUtils.HandleError(statement, statement.NumberOfResultColumns(out num));
				if (num != 0)
				{
					break;
				}
				retCode = statement.MoreResults();
			}
			while (retCode == Odbc32.RetCode.SUCCESS || retCode == Odbc32.RetCode.SUCCESS_WITH_INFO);
			OdbcTypeMap[] array;
			TableSchema tableSchema = OdbcPageReader.CreateSchema(statement, (int)num, out array);
			int num2 = SchemaTableHelper.MaxRowCount(tableSchema);
			OdbcFetchPlan odbcFetchPlan = fetchPlanFactory.NewFetchPlan(connection, statement, num, array, tableSchema, num2);
			return new OdbcPageReader(statement, parameterBuffer, rowRange, odbcFetchPlan, tableSchema);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x0008AC04 File Offset: 0x00088E04
		private static TableSchema CreateSchema(OdbcStatementHandle statement, int columnCount, out OdbcTypeMap[] typeMaps)
		{
			TableSchema tableSchema = new TableSchema(columnCount);
			typeMaps = new OdbcTypeMap[columnCount];
			using (OdbcBuffer odbcBuffer = new OdbcBuffer(4096))
			{
				for (int i = 0; i < columnCount; i++)
				{
					int num = i + 1;
					Odbc32.SQL_TYPE sql_TYPE = (Odbc32.SQL_TYPE)statement.GetColumnAttribute(num, Odbc32.SQL_DESC.CONCISE_TYPE, Odbc32.SQL_COLUMN.TYPE);
					OdbcTypeMap odbcTypeMap = OdbcTypeMap.FromSqlType(sql_TYPE);
					bool? unsigned = OdbcPageReader.GetUnsigned(odbcTypeMap, statement, num, out odbcTypeMap);
					typeMaps[i] = odbcTypeMap;
					short columnAttribute = statement.GetColumnAttribute(num, Odbc32.SQL_DESC.NAME, Odbc32.SQL_COLUMN.NAME, odbcBuffer);
					SchemaColumn schemaColumn = new SchemaColumn(odbcBuffer.PtrToStringUni(0, (int)(columnAttribute / 2)));
					schemaColumn.Ordinal = new int?(i);
					schemaColumn.DataType = odbcTypeMap.Type;
					schemaColumn.ProviderType = new int?((int)sql_TYPE);
					long num2;
					schemaColumn.Nullable = !statement.TryGetColumnAttribute(num, Odbc32.SQL_DESC.NULLABLE, Odbc32.SQL_COLUMN.NULLABLE, out num2) || (ushort)num2 > 0;
					if (statement.TryGetColumnAttribute(num, Odbc32.SQL_DESC.TYPE_NAME, Odbc32.SQL_COLUMN.TYPE_NAME, odbcBuffer, out columnAttribute) && columnAttribute > 1)
					{
						schemaColumn.DataTypeName = odbcBuffer.PtrToStringUni(0, (int)(columnAttribute / 2));
					}
					if (statement.TryGetColumnAttribute(num, Odbc32.SQL_DESC.LENGTH, Odbc32.SQL_COLUMN.LENGTH, out num2) && num2 < 2147483647L && num2 > 0L)
					{
						schemaColumn.ColumnSize = new long?(num2);
					}
					if (statement.TryGetColumnAttribute(num, Odbc32.SQL_DESC.NUM_PREC_RADIX, out num2) && num2 < 2147483647L && num2 > 0L)
					{
						schemaColumn.NumericBase = new int?((int)num2);
					}
					if (statement.TryGetColumnAttribute(num, Odbc32.SQL_DESC.SCALE, Odbc32.SQL_COLUMN.SCALE, out num2) && num2 < 2147483647L && num2 >= 0L)
					{
						schemaColumn.NumericScale = new int?((int)num2);
					}
					if (unsigned != null)
					{
						schemaColumn.IsUnsigned = new bool?(unsigned.Value);
					}
					tableSchema.AddColumn(schemaColumn);
				}
			}
			return tableSchema;
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x0008ADD4 File Offset: 0x00088FD4
		private static bool? GetUnsigned(OdbcTypeMap typeMap, OdbcStatementHandle statement, int columnNumber, out OdbcTypeMap newTypeMap)
		{
			newTypeMap = typeMap;
			bool? flag = null;
			OdbcTypeMap odbcTypeMap = typeMap.GetUnsigned();
			if (odbcTypeMap == null)
			{
				flag = new bool?(false);
			}
			else
			{
				if (odbcTypeMap.SqlType == Odbc32.SQL_TYPE.BIGINT && !statement.IsBigIntSupportedByDriver)
				{
					odbcTypeMap = OdbcTypeMap.Decimal;
				}
				long num;
				if (statement.TryGetColumnAttribute(columnNumber, Odbc32.SQL_DESC.UNSIGNED, Odbc32.SQL_COLUMN.UNSIGNED, out num))
				{
					if (num != 0L)
					{
						flag = new bool?(true);
						newTypeMap = odbcTypeMap;
					}
					else
					{
						flag = new bool?(false);
					}
				}
			}
			return flag;
		}

		// Token: 0x040013DB RID: 5083
		private readonly TableSchema schema;

		// Token: 0x040013DC RID: 5084
		private readonly ReaderWriterProgress progress;

		// Token: 0x040013DD RID: 5085
		private readonly OdbcFetchPlan fetchPlan;

		// Token: 0x040013DE RID: 5086
		private readonly RowRange rowRange;

		// Token: 0x040013DF RID: 5087
		private ISerializedException pageException;

		// Token: 0x040013E0 RID: 5088
		private int rowsFetched;

		// Token: 0x040013E1 RID: 5089
		private OdbcStatementHandle statement;

		// Token: 0x040013E2 RID: 5090
		private OdbcBuffer parameterBuffer;

		// Token: 0x040013E3 RID: 5091
		private bool isFirst;

		// Token: 0x040013E4 RID: 5092
		private bool eof;

		// Token: 0x020005A3 RID: 1443
		private class OdbcPage : IPage, IDisposable
		{
			// Token: 0x06002DA2 RID: 11682 RVA: 0x0008AE40 File Offset: 0x00089040
			public OdbcPage(OdbcStatementHandle statement, TableSchema schema, OdbcFetchPlan fetchPlan)
			{
				this.statement = statement;
				this.schema = schema;
				this.fetchPlan = fetchPlan;
				this.columnsPage = new ColumnsPage(schema, fetchPlan.MaxRowCount);
				this.truncatedCells = new HashSet<OdbcPageReader.CellLocation>();
				int num = 0;
				this.loaders = new Loader[fetchPlan.ColumnInfos.Length];
				for (int i = 0; i < this.loaders.Length; i++)
				{
					OdbcPageReaderColumnInfo odbcPageReaderColumnInfo = fetchPlan.ColumnInfos[i];
					this.loaders[i] = odbcPageReaderColumnInfo.NewLoader(odbcPageReaderColumnInfo, this.columnsPage.GetColumn(i));
					if (odbcPageReaderColumnInfo.IsColumnBound)
					{
						num += odbcPageReaderColumnInfo.BoundColumnLength;
					}
				}
				this.boundColumnsData = new byte[num];
				this.boundColumnsCellLength = new IntPtr[this.columnsPage.MaxRowCount * this.columnsPage.ColumnCount];
			}

			// Token: 0x170010DB RID: 4315
			// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x0008AF1B File Offset: 0x0008911B
			public int ColumnCount
			{
				get
				{
					return this.schema.ColumnCount;
				}
			}

			// Token: 0x170010DC RID: 4316
			// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x0008AF28 File Offset: 0x00089128
			public int RowCount
			{
				get
				{
					return (int)this.rowCount;
				}
			}

			// Token: 0x06002DA5 RID: 11685 RVA: 0x0008AF31 File Offset: 0x00089131
			public IColumn GetColumn(int ordinal)
			{
				return this.columnsPage.GetColumn(ordinal);
			}

			// Token: 0x170010DD RID: 4317
			// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x0008AF3F File Offset: 0x0008913F
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.exceptionRows;
				}
			}

			// Token: 0x170010DE RID: 4318
			// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x0008AF47 File Offset: 0x00089147
			public ISerializedException PageException
			{
				get
				{
					return this.pageException;
				}
			}

			// Token: 0x170010DF RID: 4319
			// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x0008AF4F File Offset: 0x0008914F
			public bool Eof
			{
				get
				{
					return this.eof;
				}
			}

			// Token: 0x06002DA9 RID: 11689 RVA: 0x0008AF57 File Offset: 0x00089157
			public void Dispose()
			{
				if (this.statement != null)
				{
					this.statement = null;
				}
			}

			// Token: 0x06002DAA RID: 11690 RVA: 0x0008AF68 File Offset: 0x00089168
			public unsafe void Read(OdbcStatementHandle statement, RowRange rowRange)
			{
				this.Clear(null);
				if (!rowRange.TakeCount.IsZero && !rowRange.SkipCount.IsInfinite && this.ColumnCount > 0)
				{
					byte[] array;
					byte* ptr;
					if ((array = this.boundColumnsData) == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					IntPtr[] array2;
					IntPtr* ptr2;
					if ((array2 = this.boundColumnsCellLength) == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					fixed (long* ptr3 = &this.rowCount)
					{
						long* ptr4 = ptr3;
						this.BindColumns(ptr, ptr2);
						if (this.Skip(rowRange.SkipCount))
						{
							if (this.fetchPlan.UseMultipleRowFetch)
							{
								OdbcUtils.HandleError(this.statement, this.statement.SetStatementAttribute(Odbc32.SQL_ATTR.ROWS_FETCHED_PTR, (IntPtr)((void*)ptr4), (Odbc32.SQL_IS)0));
								OdbcUtils.HandleError(statement, statement.SetStatementAttribute(Odbc32.SQL_ATTR.ROW_ARRAY_SIZE, (IntPtr)rowRange.TakeCount.Value, (Odbc32.SQL_IS)0));
								if (!this.Fetch())
								{
									this.rowCount = 0L;
									return;
								}
							}
							else
							{
								OdbcUtils.HandleError(this.statement, this.statement.SetStatementAttribute(Odbc32.SQL_ATTR.ROWS_FETCHED_PTR, IntPtr.Zero, (Odbc32.SQL_IS)0));
								this.rowCount = rowRange.TakeCount.Value;
							}
							this.SyncColumnsPage(ptr);
						}
					}
					array2 = null;
					array = null;
				}
			}

			// Token: 0x06002DAB RID: 11691 RVA: 0x0008B0BC File Offset: 0x000892BC
			public void Clear(ISerializedException pageException = null)
			{
				Array.Clear(this.boundColumnsCellLength, 0, this.boundColumnsCellLength.Length);
				this.exceptionRows.Clear();
				this.columnsPage.Clear(null);
				this.rowCount = 0L;
				this.pageException = pageException;
				this.eof = false;
			}

			// Token: 0x06002DAC RID: 11692 RVA: 0x0008B10C File Offset: 0x0008930C
			private bool Skip(RowCount rowCount)
			{
				if (!rowCount.IsZero)
				{
					if (this.fetchPlan.UseMultipleRowFetch)
					{
						OdbcUtils.HandleError(this.statement, this.statement.SetStatementAttribute(Odbc32.SQL_ATTR.ROWS_FETCHED_PTR, IntPtr.Zero, (Odbc32.SQL_IS)0));
						OdbcUtils.HandleError(this.statement, this.statement.SetStatementAttribute(Odbc32.SQL_ATTR.RETRIEVE_DATA, (IntPtr)0, (Odbc32.SQL_IS)0));
						OdbcUtils.HandleError(this.statement, this.statement.SetStatementAttribute(Odbc32.SQL_ATTR.ROW_ARRAY_SIZE, new IntPtr(rowCount.Value), (Odbc32.SQL_IS)0));
						if (!this.Fetch())
						{
							return false;
						}
						OdbcUtils.HandleError(this.statement, this.statement.SetStatementAttribute(Odbc32.SQL_ATTR.RETRIEVE_DATA, (IntPtr)1, (Odbc32.SQL_IS)0));
					}
					else
					{
						for (long num = 0L; num < rowCount.Value; num += 1L)
						{
							if (!this.Fetch())
							{
								return false;
							}
						}
					}
				}
				return true;
			}

			// Token: 0x06002DAD RID: 11693 RVA: 0x0008B1E8 File Offset: 0x000893E8
			private bool Fetch()
			{
				Odbc32.RetCode retCode = this.statement.Fetch();
				if (retCode == Odbc32.RetCode.SUCCESS)
				{
					return true;
				}
				if (retCode == Odbc32.RetCode.SUCCESS_WITH_INFO)
				{
					this.LoadDiagnosticRecords(retCode);
					return true;
				}
				if (retCode != Odbc32.RetCode.NO_DATA)
				{
					Exception ex = OdbcUtils.HandleErrorNoThrow(this.statement, retCode);
					this.pageException = OdbcUtils.SerializeException(ex);
					return false;
				}
				this.eof = true;
				return false;
			}

			// Token: 0x06002DAE RID: 11694 RVA: 0x0008B240 File Offset: 0x00089440
			private void LoadDiagnosticRecords(Odbc32.RetCode fetchRetcode)
			{
				this.truncatedCells.Clear();
				StringBuilder stringBuilder = new StringBuilder(6);
				StringBuilder stringBuilder2 = null;
				List<OdbcError> list = null;
				for (int i = 1; i <= 32767; i++)
				{
					Odbc32.RetCode diagnosticField = this.statement.GetDiagnosticField(i, Odbc32.SQL_DIAG.SQLSTATE, stringBuilder);
					if (diagnosticField == Odbc32.RetCode.NO_DATA)
					{
						break;
					}
					if (diagnosticField != Odbc32.RetCode.SUCCESS)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unexpected return code from SQLGetDiagField '{0}' for record number '{1}'.", diagnosticField, i));
					}
					OdbcUtils.HandleError(this.statement, diagnosticField);
					long num;
					long num2;
					if (OdbcUtils.IsSuccess(this.statement.GetDiagnosticField(i, Odbc32.SQL_DIAG.ROW_NUMBER, out num)) && OdbcUtils.IsSuccess(this.statement.GetDiagnosticField(i, Odbc32.SQL_DIAG.COLUMN_NUMBER, out num2)) && num >= 0L && num2 >= 0L && num <= (long)this.RowCount && num2 <= (long)this.ColumnCount)
					{
						string text = stringBuilder.ToString();
						if (text == "01004" || text == "01S07")
						{
							this.truncatedCells.Add(new OdbcPageReader.CellLocation(num - 1L, num2 - 1L));
						}
						else
						{
							OdbcError diagnosticRecord = this.GetDiagnosticRecord(i, stringBuilder, ref stringBuilder2);
							checked
							{
								this.AddException((int)num, (int)num2, diagnosticRecord.ToString(fetchRetcode));
							}
						}
					}
					else
					{
						OdbcError diagnosticRecord2 = this.GetDiagnosticRecord(i, stringBuilder, ref stringBuilder2);
						if (list == null)
						{
							list = new List<OdbcError>();
						}
						list.Add(diagnosticRecord2);
					}
				}
				if (list != null)
				{
					throw new OdbcException(fetchRetcode, list);
				}
			}

			// Token: 0x06002DAF RID: 11695 RVA: 0x0008B3B0 File Offset: 0x000895B0
			private OdbcError GetDiagnosticRecord(int recordNumber, StringBuilder sqlStateBuffer, ref StringBuilder errorMessageBuffer)
			{
				if (errorMessageBuffer == null)
				{
					errorMessageBuffer = new StringBuilder(1024);
				}
				OdbcError odbcError;
				Odbc32.RetCode diagnosticRecord = this.statement.GetDiagnosticRecord(recordNumber, errorMessageBuffer, sqlStateBuffer, out odbcError);
				if (!OdbcUtils.IsSuccess(diagnosticRecord))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unexpected return code from SQLGetDiagRec '{0}' for record number '{1}'.", diagnosticRecord, recordNumber));
				}
				return odbcError;
			}

			// Token: 0x06002DB0 RID: 11696 RVA: 0x0008B40C File Offset: 0x0008960C
			private void AddException(int row, int col, string errorMessage)
			{
				IExceptionRow exceptionRow;
				if (!this.exceptionRows.TryGetValue(row, out exceptionRow))
				{
					exceptionRow = new ExceptionRow(new Dictionary<int, ISerializedException>());
					this.exceptionRows[row] = exceptionRow;
				}
				ExceptionRow exceptionRow2 = (ExceptionRow)exceptionRow;
				ISerializedException ex;
				if (exceptionRow2.Exceptions.TryGetValue(col, out ex))
				{
					ISerializedException ex2 = ex;
					ex2["Message"] = ex2["Message"] + Environment.NewLine + errorMessage;
					return;
				}
				ISerializedException ex3 = new SerializedException();
				ex3["Reason"] = "DataSource.Error";
				ex3["Message"] = errorMessage;
				exceptionRow2.Exceptions[col] = ex3;
			}

			// Token: 0x06002DB1 RID: 11697 RVA: 0x0008B4B0 File Offset: 0x000896B0
			private unsafe void BindColumns(byte* dataBuffer, IntPtr* dataLengthBuffer)
			{
				for (int i = 0; i < this.fetchPlan.ColumnInfos.Length; i++)
				{
					OdbcPageReaderColumnInfo odbcPageReaderColumnInfo = this.fetchPlan.ColumnInfos[i];
					if (odbcPageReaderColumnInfo.IsColumnBound)
					{
						int boundCellLength = odbcPageReaderColumnInfo.BoundCellLength;
						Exception ex = OdbcUtils.HandleError(this.statement, this.statement.BindColumn(i + 1, odbcPageReaderColumnInfo.TypeMap.CType, (IntPtr)((void*)(dataBuffer + odbcPageReaderColumnInfo.BoundColumnOffset)), (long)boundCellLength, (IntPtr)((void*)(dataLengthBuffer + (IntPtr)(odbcPageReaderColumnInfo.BoundColumnIndex * this.columnsPage.MaxRowCount) * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)))));
						if (ex != null)
						{
							this.fetchPlan.BindColumnFailureHandler(odbcPageReaderColumnInfo, ex);
						}
					}
				}
			}

			// Token: 0x06002DB2 RID: 11698 RVA: 0x0008B560 File Offset: 0x00089760
			private unsafe void SyncColumnsPage(byte* boundColumnsDataPtr)
			{
				int num = 0;
				while ((long)num < this.rowCount)
				{
					int num2;
					if (!this.fetchPlan.UseMultipleRowFetch)
					{
						if (!this.Fetch())
						{
							this.rowCount = (long)num;
							return;
						}
						num2 = 0;
					}
					else
					{
						num2 = num;
					}
					for (int i = 0; i < this.ColumnCount; i++)
					{
						OdbcPageReaderColumnInfo odbcPageReaderColumnInfo = this.fetchPlan.ColumnInfos[i];
						Column column = this.columnsPage.GetColumn(i);
						try
						{
							if (odbcPageReaderColumnInfo.IsColumnBound)
							{
								long num3 = this.boundColumnsCellLength[odbcPageReaderColumnInfo.BoundColumnIndex * this.columnsPage.MaxRowCount + num2].ToInt64();
								if (num3 == -1L)
								{
									column.AddNull();
								}
								else if (num3 == -4L || num3 > (long)odbcPageReaderColumnInfo.BoundCellLength || this.truncatedCells.Contains(new OdbcPageReader.CellLocation((long)num2, (long)i)))
								{
									this.LoadCellData(odbcPageReaderColumnInfo, column, num, num2, i);
								}
								else
								{
									if (num3 < 0L)
									{
										throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unexpected length indicator value {0}.", num3));
									}
									num3 = Math.Min(num3, (long)odbcPageReaderColumnInfo.MaxBoundColumnFetch);
									byte* ptr = boundColumnsDataPtr + odbcPageReaderColumnInfo.BoundColumnOffset + odbcPageReaderColumnInfo.BoundCellLength * num2;
									string text;
									if (!this.loaders[i].TryLoad(odbcPageReaderColumnInfo.TypeMap.OleDbType, ptr, (int)num3, out text))
									{
										this.AddException(num, i, text);
										column.AddNull();
									}
								}
							}
							else
							{
								this.LoadCellData(odbcPageReaderColumnInfo, column, num, num2, i);
							}
						}
						catch (ArgumentOutOfRangeException)
						{
							this.AddException(num, i, Strings.OdbcInvalidValue);
							column.AddNull();
						}
					}
					this.columnsPage.AddRow();
					num++;
				}
			}

			// Token: 0x06002DB3 RID: 11699 RVA: 0x0008B724 File Offset: 0x00089924
			private unsafe void LoadCellData(OdbcPageReaderColumnInfo columnInfo, Column column, int rowIndex, int rowSetIndex, int columnIndex)
			{
				if (this.getDataBuffer == null)
				{
					this.getDataBuffer = new byte[this.fetchPlan.MaxCellByteLength * 2];
				}
				int num = 0;
				long num3;
				DBTYPE dbtype;
				if (columnInfo.TypeMap.SqlType == Odbc32.SQL_TYPE.SS_VARIANT)
				{
					int num2 = columnIndex + 1;
					byte b;
					OdbcUtils.HandleError(this.statement, this.statement.GetData(num2, Odbc32.SQL_C.BINARY, (IntPtr)((void*)(&b)), 0L, out num3));
					if (num3 >= 0L)
					{
						OdbcTypeMap odbcTypeMap = OdbcTypeMap.FromSqlType((Odbc32.SQL_TYPE)((int)this.statement.GetColumnAttribute(num2, (Odbc32.SQL_DESC)1216, (Odbc32.SQL_COLUMN)65535)));
						OdbcPageReader.GetUnsigned(odbcTypeMap, this.statement, num2, out odbcTypeMap);
						dbtype = odbcTypeMap.OleDbType;
						num3 = this.GetData(odbcTypeMap.CType, rowSetIndex, columnIndex, (long)num);
					}
					else
					{
						if (num3 != -1L)
						{
							throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unexpected SQLGetData StrLen_or_IndPtr value '{0}'.", num3));
						}
						dbtype = DBTYPE.IUNKNOWN;
					}
				}
				else
				{
					num3 = this.GetData(columnInfo.TypeMap.CType, rowSetIndex, columnIndex, (long)num);
					dbtype = columnInfo.TypeMap.OleDbType;
				}
				if (num3 == -1L)
				{
					column.AddNull();
					return;
				}
				Loader loader = this.loaders[columnIndex];
				byte[] array;
				byte* ptr;
				if ((array = this.getDataBuffer) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				string text = null;
				if (!loader.TryLoad(dbtype, ptr, (int)num3, out text))
				{
					this.AddException(rowIndex, columnIndex, text);
					column.AddNull();
				}
				array = null;
			}

			// Token: 0x06002DB4 RID: 11700 RVA: 0x0008B88C File Offset: 0x00089A8C
			private unsafe long GetData(Odbc32.SQL_C cType, int rowIndex, int columnIndex, long pos)
			{
				long num = -1L;
				for (;;)
				{
					long num2 = (long)this.getDataBuffer.Length - pos;
					byte[] array;
					byte* ptr;
					if ((array = this.getDataBuffer) == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					Odbc32.RetCode data = this.statement.GetData(columnIndex + 1, cType, (IntPtr)((void*)(ptr + pos)), num2, out num);
					array = null;
					if (data == Odbc32.RetCode.NO_DATA && (cType == Odbc32.SQL_C.WCHAR || cType == Odbc32.SQL_C.BINARY))
					{
						break;
					}
					OdbcUtils.HandleError(this.statement, data);
					if (cType == Odbc32.SQL_C.WCHAR)
					{
						num2 -= 2L;
					}
					if (num == -1L)
					{
						goto Block_5;
					}
					long num3;
					if (num == -4L)
					{
						pos += num2;
						num3 = (long)this.getDataBuffer.Length * 2L;
					}
					else
					{
						if (num < 0L)
						{
							goto Block_7;
						}
						if (num <= num2)
						{
							goto Block_8;
						}
						pos += num2;
						long num4 = Math.Max((long)this.getDataBuffer.Length * 2L, num - num2);
						num3 = (long)this.getDataBuffer.Length + num4;
					}
					if (num3 > 2147483647L)
					{
						goto Block_9;
					}
					byte[] array2 = new byte[num3];
					Array.Copy(this.getDataBuffer, 0L, array2, 0L, (long)this.getDataBuffer.Length);
					this.getDataBuffer = array2;
				}
				return 0L;
				Block_5:
				return -1L;
				Block_7:
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unexpected SQLGetData StrLen_or_IndPtr value '{0}'.", num));
				Block_8:
				pos += num;
				return pos;
				Block_9:
				if (num == (long)((ulong)(-1)))
				{
					return -1L;
				}
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "SQLGetData returned exceedingly large value '{0}'.", num));
			}

			// Token: 0x040013E5 RID: 5093
			private readonly IDictionary<int, IExceptionRow> exceptionRows = new Dictionary<int, IExceptionRow>();

			// Token: 0x040013E6 RID: 5094
			private readonly ColumnsPage columnsPage;

			// Token: 0x040013E7 RID: 5095
			private readonly TableSchema schema;

			// Token: 0x040013E8 RID: 5096
			private readonly OdbcFetchPlan fetchPlan;

			// Token: 0x040013E9 RID: 5097
			private readonly byte[] boundColumnsData;

			// Token: 0x040013EA RID: 5098
			private readonly IntPtr[] boundColumnsCellLength;

			// Token: 0x040013EB RID: 5099
			private readonly HashSet<OdbcPageReader.CellLocation> truncatedCells;

			// Token: 0x040013EC RID: 5100
			private readonly Loader[] loaders;

			// Token: 0x040013ED RID: 5101
			private OdbcStatementHandle statement;

			// Token: 0x040013EE RID: 5102
			private long rowCount;

			// Token: 0x040013EF RID: 5103
			private byte[] getDataBuffer;

			// Token: 0x040013F0 RID: 5104
			private ISerializedException pageException;

			// Token: 0x040013F1 RID: 5105
			private bool eof;
		}

		// Token: 0x020005A4 RID: 1444
		private class CellLocation : IEquatable<OdbcPageReader.CellLocation>
		{
			// Token: 0x06002DB5 RID: 11701 RVA: 0x0008B9E9 File Offset: 0x00089BE9
			public CellLocation(long row, long col)
			{
				this.Row = row;
				this.Col = col;
			}

			// Token: 0x06002DB6 RID: 11702 RVA: 0x0008B9FF File Offset: 0x00089BFF
			public override bool Equals(object obj)
			{
				return base.Equals(obj as OdbcPageReader.CellLocation);
			}

			// Token: 0x06002DB7 RID: 11703 RVA: 0x0008BA0D File Offset: 0x00089C0D
			public bool Equals(OdbcPageReader.CellLocation location)
			{
				return location != null && location.Row == this.Row && location.Col == this.Col;
			}

			// Token: 0x06002DB8 RID: 11704 RVA: 0x0008BA30 File Offset: 0x00089C30
			public override int GetHashCode()
			{
				return this.Row.GetHashCode() ^ this.Col.GetHashCode();
			}

			// Token: 0x040013F2 RID: 5106
			public readonly long Row;

			// Token: 0x040013F3 RID: 5107
			public readonly long Col;
		}
	}
}
