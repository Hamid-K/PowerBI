using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001F26 RID: 7974
	public class RowsetPageReader : IPageReader, IDisposable
	{
		// Token: 0x0600C35E RID: 50014 RVA: 0x00271F10 File Offset: 0x00270110
		public RowsetPageReader(IRowset rowset, IOleDbCustomErrorHandler errorHandler = null, Func<DBSTATUS, ISerializedException> cellErrorHandler = null, Func<Exception, ISerializedException> pageExceptionHandler = null)
		{
			ColumnInfo[] columnInfos = RowsetPageReader.GetColumnInfos(rowset);
			this.rowset = rowset;
			this.schema = RowsetPageReader.GetSchema(columnInfos);
			this.progress = new ReaderWriterProgress();
			this.bindings = RowsetPageReader.CreateBindings(columnInfos, out this.rowLength);
			this.hAccessor = RowsetPageReader.CreateAccessor(rowset, this.bindings, this.rowLength);
			this.first = true;
			this.errorHandler = errorHandler;
			this.cellErrorHandler = cellErrorHandler;
			this.pageExceptionHandler = pageExceptionHandler;
			this.maxPageRowCount = SchemaTableHelper.MaxRowCount(this.schema);
		}

		// Token: 0x17002FB5 RID: 12213
		// (get) Token: 0x0600C35F RID: 50015 RVA: 0x00271FA0 File Offset: 0x002701A0
		public TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17002FB6 RID: 12214
		// (get) Token: 0x0600C360 RID: 50016 RVA: 0x00271FA8 File Offset: 0x002701A8
		public IProgress Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x17002FB7 RID: 12215
		// (get) Token: 0x0600C361 RID: 50017 RVA: 0x00271FB0 File Offset: 0x002701B0
		public int MaxPageRowCount
		{
			get
			{
				return this.maxPageRowCount;
			}
		}

		// Token: 0x0600C362 RID: 50018 RVA: 0x00271FB8 File Offset: 0x002701B8
		public IPage CreatePage()
		{
			return new RowsetPageReader.RowsetPage(this.schema, this.maxPageRowCount, this.rowLength, this.bindings, this.progress, this.errorHandler, this.cellErrorHandler, this.pageExceptionHandler);
		}

		// Token: 0x0600C363 RID: 50019 RVA: 0x00271FEF File Offset: 0x002701EF
		public void Read(IPage page)
		{
			this.Read((RowsetPageReader.RowsetPage)page);
		}

		// Token: 0x0600C364 RID: 50020 RVA: 0x00272000 File Offset: 0x00270200
		private void Read(RowsetPageReader.RowsetPage page)
		{
			if (this.pageException != null)
			{
				page.Clear(this.pageException);
				return;
			}
			page.Read(this.rowset, this.hAccessor, this.first);
			this.first = false;
			this.pageException = page.PageException;
		}

		// Token: 0x0600C365 RID: 50021 RVA: 0x000020FA File Offset: 0x000002FA
		public IPageReader NextResult()
		{
			return null;
		}

		// Token: 0x0600C366 RID: 50022 RVA: 0x00272050 File Offset: 0x00270250
		public void Dispose()
		{
			if (this.hAccessor.value != 0L)
			{
				((IAccessor)this.rowset).ReleaseAccessor(this.hAccessor, null);
				this.hAccessor.value = 0L;
			}
			if (this.rowset != null)
			{
				IDisposable disposable = this.rowset as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this.rowset = null;
			}
		}

		// Token: 0x0600C367 RID: 50023 RVA: 0x002720B4 File Offset: 0x002702B4
		private static TableSchema GetSchema(ColumnInfo[] columnInfos)
		{
			TableSchema tableSchema = new TableSchema(columnInfos.Length);
			for (int i = 0; i < columnInfos.Length; i++)
			{
				ColumnInfo columnInfo = columnInfos[i];
				string name = columnInfo.ColumnID.Name;
				int num = i + 1;
				Type type = RowsetPageReader.TypeInfo.GetTypeInfo(columnInfo.Type, columnInfo.ColumnSize).Type;
				bool flag = RowsetPageReader.IsNullable(columnInfo.Flags);
				tableSchema.AddColumn(name, num, type, flag);
			}
			return tableSchema;
		}

		// Token: 0x0600C368 RID: 50024 RVA: 0x00272125 File Offset: 0x00270325
		private static ColumnInfo[] GetColumnInfos(IRowset rowset)
		{
			return new ColumnsInfo((IColumnsInfo)rowset).ColumnInfos;
		}

		// Token: 0x0600C369 RID: 50025 RVA: 0x00272137 File Offset: 0x00270337
		private static bool IsNullable(DBCOLUMNFLAGS flags)
		{
			return (flags & DBCOLUMNFLAGS.ISNULLABLE) == DBCOLUMNFLAGS.ISNULLABLE || (flags & DBCOLUMNFLAGS.MAYBENULL) == DBCOLUMNFLAGS.MAYBENULL;
		}

		// Token: 0x0600C36A RID: 50026 RVA: 0x0027214C File Offset: 0x0027034C
		private static DBBINDING[] CreateBindings(ColumnInfo[] columnInfos, out int rowLength)
		{
			DBBINDING[] array = new DBBINDING[columnInfos.Length];
			DBBYTEOFFSET dbbyteoffset = new DBBYTEOFFSET
			{
				value = 0UL
			};
			for (int i = 0; i < columnInfos.Length; i++)
			{
				RowsetPageReader.TypeInfo typeInfo = RowsetPageReader.TypeInfo.GetTypeInfo(columnInfos[i].Type, columnInfos[i].ColumnSize);
				DBLENGTH length = typeInfo.Length;
				if (length.value > DBLENGTH.MaxValue.value - DBLENGTH.Size.value - 4UL - dbbyteoffset.value)
				{
					throw new ArgumentException("Row is too large");
				}
				DBLENGTH dblength = length;
				if ((typeInfo.NativeType & DBTYPE.BYREF) == DBTYPE.BYREF)
				{
					dblength = DbLength.MaxValue;
				}
				array[i].iOrdinal = columnInfos[i].Ordinal;
				array[i].dwPart = (DBPART)7U;
				array[i].dwMemOwner = DBMEMOWNER.CLIENTOWNED;
				array[i].eParamIO = DBPARAMIO.NOTPARAM;
				array[i].cbMaxLen = dblength;
				array[i].dwFlags = 0U;
				array[i].wType = typeInfo.NativeType;
				array[i].bPrecision = columnInfos[i].Precision;
				array[i].bScale = columnInfos[i].Scale;
				array[i].obLength = dbbyteoffset;
				dbbyteoffset.value += DBLENGTH.Size.value;
				array[i].obStatus = dbbyteoffset;
				dbbyteoffset.value += 4UL;
				array[i].obValue = dbbyteoffset;
				dbbyteoffset.value += length.value;
				dbbyteoffset.value += (ulong)IntPtr.Size - dbbyteoffset.value % (ulong)IntPtr.Size;
			}
			rowLength = (int)dbbyteoffset.value;
			return array;
		}

		// Token: 0x0600C36B RID: 50027 RVA: 0x00272310 File Offset: 0x00270510
		private unsafe static HACCESSOR CreateAccessor(IRowset rowset, DBBINDING[] bindings, int rowLength)
		{
			IAccessor accessor = (IAccessor)rowset;
			DBBINDSTATUS[] array = new DBBINDSTATUS[bindings.Length];
			DBBINDSTATUS[] array2;
			DBBINDSTATUS* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			HACCESSOR haccessor;
			fixed (DBBINDING[] array3 = bindings)
			{
				DBBINDING* ptr2;
				if (bindings == null || array3.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array3[0];
				}
				accessor.CreateAccessor(DBACCESSORFLAGS.ROWDATA, new DBCOUNTITEM
				{
					value = (ulong)bindings.Length
				}, ptr2, new DBLENGTH
				{
					value = (ulong)rowLength
				}, out haccessor, ptr);
			}
			array2 = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != DBBINDSTATUS.OK)
				{
					throw new Exception("Couldn't bind");
				}
			}
			return haccessor;
		}

		// Token: 0x0600C36C RID: 50028 RVA: 0x002723C0 File Offset: 0x002705C0
		private static bool IsSafeException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is AccessViolationException) && !(e is SEHException) && !typeof(SecurityException).IsAssignableFrom(e.GetType());
		}

		// Token: 0x04006478 RID: 25720
		private readonly IOleDbCustomErrorHandler errorHandler;

		// Token: 0x04006479 RID: 25721
		private readonly Func<DBSTATUS, ISerializedException> cellErrorHandler;

		// Token: 0x0400647A RID: 25722
		private readonly Func<Exception, ISerializedException> pageExceptionHandler;

		// Token: 0x0400647B RID: 25723
		private readonly TableSchema schema;

		// Token: 0x0400647C RID: 25724
		private readonly ReaderWriterProgress progress;

		// Token: 0x0400647D RID: 25725
		private readonly DBBINDING[] bindings;

		// Token: 0x0400647E RID: 25726
		private readonly int rowLength;

		// Token: 0x0400647F RID: 25727
		private readonly int maxPageRowCount;

		// Token: 0x04006480 RID: 25728
		private IRowset rowset;

		// Token: 0x04006481 RID: 25729
		private HACCESSOR hAccessor;

		// Token: 0x04006482 RID: 25730
		private bool first;

		// Token: 0x04006483 RID: 25731
		private ISerializedException pageException;

		// Token: 0x02001F27 RID: 7975
		private struct TypeInfo
		{
			// Token: 0x0600C36D RID: 50029 RVA: 0x00272410 File Offset: 0x00270610
			private TypeInfo(Type type, DBTYPE nativeType, uint length)
			{
				this = new RowsetPageReader.TypeInfo(type, nativeType, new DBLENGTH
				{
					value = (ulong)length
				});
			}

			// Token: 0x0600C36E RID: 50030 RVA: 0x00272437 File Offset: 0x00270637
			private TypeInfo(Type type, DBTYPE nativeType, DBLENGTH length)
			{
				this.type = type;
				this.nativeType = nativeType;
				this.length = length;
			}

			// Token: 0x17002FB8 RID: 12216
			// (get) Token: 0x0600C36F RID: 50031 RVA: 0x0027244E File Offset: 0x0027064E
			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002FB9 RID: 12217
			// (get) Token: 0x0600C370 RID: 50032 RVA: 0x00272456 File Offset: 0x00270656
			public DBTYPE NativeType
			{
				get
				{
					return this.nativeType;
				}
			}

			// Token: 0x17002FBA RID: 12218
			// (get) Token: 0x0600C371 RID: 50033 RVA: 0x0027245E File Offset: 0x0027065E
			public DBLENGTH Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x0600C372 RID: 50034 RVA: 0x00272468 File Offset: 0x00270668
			public static RowsetPageReader.TypeInfo GetTypeInfo(DBTYPE type, DBLENGTH columnSize)
			{
				if (type <= DBTYPE.DBTIMESTAMPOFFSET)
				{
					switch (type)
					{
					case DBTYPE.I2:
						return new RowsetPageReader.TypeInfo(typeof(short), DBTYPE.I2, 2U);
					case DBTYPE.I4:
						return new RowsetPageReader.TypeInfo(typeof(int), DBTYPE.I4, 4U);
					case DBTYPE.R4:
						return new RowsetPageReader.TypeInfo(typeof(float), DBTYPE.R4, 4U);
					case DBTYPE.R8:
						return new RowsetPageReader.TypeInfo(typeof(double), DBTYPE.R8, 8U);
					case DBTYPE.CY:
						return new RowsetPageReader.TypeInfo(typeof(Currency), DBTYPE.CY, DbLength.Currency);
					case DBTYPE.DATE:
						return new RowsetPageReader.TypeInfo(typeof(DateTime), DBTYPE.DATE, DbLength.Date);
					case DBTYPE.BSTR:
						break;
					case DBTYPE.IDISPATCH:
					case DBTYPE.ERROR:
					case (DBTYPE)15:
						goto IL_0376;
					case DBTYPE.BOOL:
						return new RowsetPageReader.TypeInfo(typeof(bool), DBTYPE.BOOL, DbLength.VariantBool);
					case DBTYPE.VARIANT:
						return new RowsetPageReader.TypeInfo(typeof(object), DBTYPE.VARIANT, DbLength.Variant);
					case DBTYPE.IUNKNOWN:
						return new RowsetPageReader.TypeInfo(typeof(object), DBTYPE.VARIANT, DbLength.Variant);
					case DBTYPE.DECIMAL:
						return new RowsetPageReader.TypeInfo(typeof(decimal), DBTYPE.DECIMAL, DbLength.Decimal);
					case DBTYPE.I1:
						return new RowsetPageReader.TypeInfo(typeof(sbyte), DBTYPE.I1, 1U);
					case DBTYPE.UI1:
						return new RowsetPageReader.TypeInfo(typeof(byte), DBTYPE.UI1, 1U);
					case DBTYPE.UI2:
						return new RowsetPageReader.TypeInfo(typeof(ushort), DBTYPE.UI2, 2U);
					case DBTYPE.UI4:
						return new RowsetPageReader.TypeInfo(typeof(uint), DBTYPE.UI4, 4U);
					case DBTYPE.I8:
						return new RowsetPageReader.TypeInfo(typeof(long), DBTYPE.I8, 8U);
					case DBTYPE.UI8:
						return new RowsetPageReader.TypeInfo(typeof(ulong), DBTYPE.UI8, 8U);
					default:
						if (type == DBTYPE.GUID)
						{
							return new RowsetPageReader.TypeInfo(typeof(Guid), DBTYPE.GUID, DbLength.Guid);
						}
						switch (type)
						{
						case DBTYPE.BYTES:
							goto IL_0293;
						case DBTYPE.STR:
						case DBTYPE.WSTR:
							if (columnSize.value < 16UL)
							{
								return new RowsetPageReader.TypeInfo(typeof(string), DBTYPE.WSTR, (uint)(columnSize.value + 1UL) * 2U);
							}
							return new RowsetPageReader.TypeInfo(typeof(string), (DBTYPE)16514, DbLength.Pointer);
						case DBTYPE.NUMERIC:
							return new RowsetPageReader.TypeInfo(typeof(Number), DBTYPE.NUMERIC, DbLength.Numeric);
						case DBTYPE.UDT:
						case (DBTYPE)136:
						case (DBTYPE)137:
						case (DBTYPE)138:
						case (DBTYPE)140:
						case (DBTYPE)142:
						case (DBTYPE)143:
						case (DBTYPE)144:
							goto IL_0376;
						case DBTYPE.DBDATE:
							return new RowsetPageReader.TypeInfo(typeof(Date), DBTYPE.DBDATE, DbLength.DbDate);
						case DBTYPE.DBTIME:
						case DBTYPE.DBTIME2:
							return new RowsetPageReader.TypeInfo(typeof(Time), DBTYPE.DBTIME2, DbLength.DbTime2);
						case DBTYPE.DBTIMESTAMP:
							return new RowsetPageReader.TypeInfo(typeof(DateTime), DBTYPE.DBTIMESTAMP, DbLength.TimeStamp);
						case DBTYPE.VARNUMERIC:
							return new RowsetPageReader.TypeInfo(typeof(decimal), DBTYPE.DECIMAL, DbLength.Decimal);
						case DBTYPE.XML:
							break;
						case DBTYPE.DBTIMESTAMPOFFSET:
							return new RowsetPageReader.TypeInfo(typeof(DateTimeOffset), DBTYPE.DBTIMESTAMPOFFSET, DbLength.TimeStampOffset);
						default:
							goto IL_0376;
						}
						break;
					}
				}
				else
				{
					if (type == DBTYPE.DBDURATION)
					{
						return new RowsetPageReader.TypeInfo(typeof(TimeSpan), DBTYPE.DBDURATION, DbLength.Duration);
					}
					if (type == (DBTYPE)16512)
					{
						goto IL_0293;
					}
					if (type - (DBTYPE)16513 > 1)
					{
						goto IL_0376;
					}
				}
				return new RowsetPageReader.TypeInfo(typeof(string), (DBTYPE)16514, DbLength.Pointer);
				IL_0293:
				return new RowsetPageReader.TypeInfo(typeof(byte[]), (DBTYPE)16512, DbLength.Pointer);
				IL_0376:
				return new RowsetPageReader.TypeInfo(typeof(object), DBTYPE.VARIANT, DbLength.Variant);
			}

			// Token: 0x04006484 RID: 25732
			private Type type;

			// Token: 0x04006485 RID: 25733
			private DBTYPE nativeType;

			// Token: 0x04006486 RID: 25734
			private DBLENGTH length;
		}

		// Token: 0x02001F28 RID: 7976
		private class RowsetPage : IPage, IDisposable
		{
			// Token: 0x0600C373 RID: 50035 RVA: 0x00272804 File Offset: 0x00270A04
			public RowsetPage(TableSchema schema, int maxRowCount, int rowLength, DBBINDING[] bindings, ReaderWriterProgress progress, IOleDbCustomErrorHandler errorHandler, Func<DBSTATUS, ISerializedException> cellErrorHandler, Func<Exception, ISerializedException> pageExceptionHandler)
			{
				this.columnCount = schema.ColumnCount;
				this.rowLength = rowLength;
				this.bindings = bindings;
				this.progress = progress;
				this.hrows = new HROW[maxRowCount];
				this.buffer = new byte[rowLength * maxRowCount];
				this.columnsPage = new ColumnsPage(schema);
				this.errorHandler = errorHandler;
				this.cellErrorHandler = cellErrorHandler;
				this.pageExceptionHandler = pageExceptionHandler;
				this.exceptionRows = RowsetPageReader.RowsetPage.emptyExceptionRows;
			}

			// Token: 0x17002FBB RID: 12219
			// (get) Token: 0x0600C374 RID: 50036 RVA: 0x00272883 File Offset: 0x00270A83
			public int ColumnCount
			{
				get
				{
					return this.columnCount;
				}
			}

			// Token: 0x17002FBC RID: 12220
			// (get) Token: 0x0600C375 RID: 50037 RVA: 0x0027288B File Offset: 0x00270A8B
			public int RowCount
			{
				get
				{
					return this.rowCount;
				}
			}

			// Token: 0x17002FBD RID: 12221
			// (get) Token: 0x0600C376 RID: 50038 RVA: 0x00272893 File Offset: 0x00270A93
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					this.SyncExceptionRows();
					return this.exceptionRows;
				}
			}

			// Token: 0x17002FBE RID: 12222
			// (get) Token: 0x0600C377 RID: 50039 RVA: 0x002728A1 File Offset: 0x00270AA1
			public ISerializedException PageException
			{
				get
				{
					return this.pageException;
				}
			}

			// Token: 0x0600C378 RID: 50040 RVA: 0x002728A9 File Offset: 0x00270AA9
			public IColumn GetColumn(int ordinal)
			{
				this.SyncColumnsPage();
				return this.columnsPage.GetColumn(ordinal);
			}

			// Token: 0x0600C379 RID: 50041 RVA: 0x002728C0 File Offset: 0x00270AC0
			private unsafe void SyncColumnsPage()
			{
				if (this.columnsPage.RowCount != this.rowCount)
				{
					byte[] array;
					byte* ptr;
					if ((array = this.buffer) == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr;
					for (int i = 0; i < this.rowCount; i++)
					{
						for (int j = 0; j < this.bindings.Length; j++)
						{
							Column column = this.columnsPage.GetColumn(j);
							if (*(uint*)(ptr2 + this.bindings[j].obStatus.value) == 0U)
							{
								DBTYPE wType = this.bindings[j].wType;
								void* ptr3 = (void*)(ptr2 + this.bindings[j].obValue.value);
								DBLENGTH dblength = *(DBLENGTH*)(ptr2 + this.bindings[j].obLength.value);
								column.AddValue(wType, ptr3, (int)dblength.value);
							}
							else
							{
								column.AddNull();
							}
						}
						this.columnsPage.AddRow();
						ptr2 += this.rowLength;
					}
					array = null;
					this.progress.OnRows(this.rowCount, this.ExceptionRows.Count);
				}
			}

			// Token: 0x0600C37A RID: 50042 RVA: 0x00272A04 File Offset: 0x00270C04
			private unsafe void SyncExceptionRows()
			{
				if (this.cellErrorHandler != null && this.exceptionRows == RowsetPageReader.RowsetPage.emptyExceptionRows)
				{
					this.exceptionRows = new Dictionary<int, IExceptionRow>();
					byte[] array;
					byte* ptr;
					if ((array = this.buffer) == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr;
					for (int i = 0; i < this.rowCount; i++)
					{
						Dictionary<int, ISerializedException> dictionary = null;
						for (int j = 0; j < this.bindings.Length; j++)
						{
							DBSTATUS dbstatus = (DBSTATUS)(*(uint*)(ptr2 + this.bindings[j].obStatus.value));
							if (dbstatus != DBSTATUS.S_OK && dbstatus != DBSTATUS.S_ISNULL)
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<int, ISerializedException>();
								}
								dictionary[j] = this.cellErrorHandler(dbstatus);
							}
						}
						if (dictionary != null)
						{
							this.exceptionRows[i] = new ExceptionRow(dictionary);
						}
						ptr2 += this.rowLength;
					}
					array = null;
				}
			}

			// Token: 0x0600C37B RID: 50043 RVA: 0x00272AF4 File Offset: 0x00270CF4
			public unsafe void Clear(ISerializedException pageException = null)
			{
				this.columnsPage.Clear(null);
				this.exceptionRows = RowsetPageReader.RowsetPage.emptyExceptionRows;
				byte[] array;
				byte* ptr;
				if ((array = this.buffer) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				for (int i = 0; i < this.bindings.Length; i++)
				{
					DBTYPE wType = this.bindings[i].wType;
					if ((wType & DBTYPE.BYREF) == DBTYPE.BYREF || wType == DBTYPE.VARIANT)
					{
						byte* ptr2 = ptr;
						for (int j = 0; j < this.rowCount; j++)
						{
							if (*(uint*)(ptr2 + this.bindings[i].obStatus.value) == 0U)
							{
								if (wType == DBTYPE.VARIANT)
								{
									void* ptr3 = (void*)(ptr2 + this.bindings[i].obValue.value);
									Variant.Clear((VARIANT*)ptr3);
								}
								else
								{
									Marshal.FreeCoTaskMem(new IntPtr(*(IntPtr*)(ptr2 + this.bindings[i].obValue.value)));
								}
							}
							ptr2 += this.rowLength;
						}
					}
				}
				this.rowCount = 0;
				array = null;
				this.pageException = pageException;
			}

			// Token: 0x0600C37C RID: 50044 RVA: 0x00272C1E File Offset: 0x00270E1E
			public void Dispose()
			{
				this.columnsPage.Dispose();
				this.Clear(null);
			}

			// Token: 0x0600C37D RID: 50045 RVA: 0x00272C34 File Offset: 0x00270E34
			public unsafe void Read(IRowset rowset, HACCESSOR hAccessor, bool first)
			{
				this.Clear(null);
				int i = (first ? Math.Min(100, this.hrows.Length) : this.hrows.Length);
				HROW[] array;
				HROW* ptr;
				if ((array = this.hrows) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				byte[] array2;
				byte* ptr2;
				if ((array2 = this.buffer) == null || array2.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array2[0];
				}
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(rowset);
				try
				{
					HROW* ptr3 = ptr;
					byte* ptr4 = ptr2;
					while (i > 0)
					{
						DBCOUNTITEM dbcountitem;
						int data = RowsetPageReader.RowsetPage.GetData(iunknownForObject, hAccessor, ptr3, new DBROWCOUNT
						{
							value = (long)i
						}, ptr4, (uint)this.rowLength, out dbcountitem);
						int num = (int)dbcountitem.value;
						i -= num;
						ptr3 += num;
						ptr4 += num * this.rowLength;
						this.rowCount += num;
						try
						{
							OleDbException.ThrowExceptionForHR(data, rowset, typeof(IRowset), this.errorHandler);
						}
						catch (Exception ex)
						{
							if (!RowsetPageReader.IsSafeException(ex) || this.pageExceptionHandler == null)
							{
								throw;
							}
							this.pageException = this.pageExceptionHandler(ex);
						}
						if (num == 0)
						{
							break;
						}
					}
				}
				finally
				{
					Marshal.Release(iunknownForObject);
				}
				array2 = null;
				array = null;
			}

			// Token: 0x0600C37E RID: 50046
			[DllImport("Microsoft.Mashup.OleDbInterop.dll")]
			private unsafe static extern int GetData(IntPtr rowset, HACCESSOR hAccessor, HROW* hrows, DBROWCOUNT hrowCount, byte* buffer, uint rowLength, out DBCOUNTITEM readCount);

			// Token: 0x04006487 RID: 25735
			private static readonly IDictionary<int, IExceptionRow> emptyExceptionRows = new Dictionary<int, IExceptionRow>();

			// Token: 0x04006488 RID: 25736
			private readonly int columnCount;

			// Token: 0x04006489 RID: 25737
			private readonly int rowLength;

			// Token: 0x0400648A RID: 25738
			private readonly DBBINDING[] bindings;

			// Token: 0x0400648B RID: 25739
			private readonly HROW[] hrows;

			// Token: 0x0400648C RID: 25740
			private readonly byte[] buffer;

			// Token: 0x0400648D RID: 25741
			private readonly ColumnsPage columnsPage;

			// Token: 0x0400648E RID: 25742
			private readonly ReaderWriterProgress progress;

			// Token: 0x0400648F RID: 25743
			private readonly IOleDbCustomErrorHandler errorHandler;

			// Token: 0x04006490 RID: 25744
			private readonly Func<DBSTATUS, ISerializedException> cellErrorHandler;

			// Token: 0x04006491 RID: 25745
			private readonly Func<Exception, ISerializedException> pageExceptionHandler;

			// Token: 0x04006492 RID: 25746
			private IDictionary<int, IExceptionRow> exceptionRows;

			// Token: 0x04006493 RID: 25747
			private int rowCount;

			// Token: 0x04006494 RID: 25748
			private ISerializedException pageException;
		}
	}
}
