using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000C3 RID: 195
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class RowsetPageReaderBase<T> : IPageReader, IDisposable where T : class
	{
		// Token: 0x06000354 RID: 852 RVA: 0x00009A08 File Offset: 0x00007C08
		protected RowsetPageReaderBase(T source)
		{
			ColumnInfo[] columnInfos = RowsetPageReaderBase<T>.GetColumnInfos(source);
			this.source = source;
			this.schemaTable = RowsetPageReaderBase<T>.GetSchemaTable(columnInfos);
			this.progress = new ReaderWriterProgress();
			this.bindings = RowsetPageReaderBase<T>.CreateBindings(columnInfos);
			this.accessor = RowsetPageReaderBase<T>.CreateAccessor(source, this.bindings);
			this.cancelIssued = false;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00009A67 File Offset: 0x00007C67
		public DataTable SchemaTable
		{
			get
			{
				return this.schemaTable;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00009A6F File Offset: 0x00007C6F
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00009A79 File Offset: 0x00007C79
		public bool CancelIssued
		{
			get
			{
				return this.cancelIssued;
			}
			set
			{
				this.cancelIssued = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00009A84 File Offset: 0x00007C84
		public IProgress Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00009A8C File Offset: 0x00007C8C
		internal ReaderWriterProgress ReaderWriterProgress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00009A94 File Offset: 0x00007C94
		internal BindingsInfo Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00009A9C File Offset: 0x00007C9C
		protected T Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00009AA4 File Offset: 0x00007CA4
		protected HACCESSOR Accessor
		{
			get
			{
				return this.accessor;
			}
		}

		// Token: 0x0600035D RID: 861
		public abstract IPage CreatePage();

		// Token: 0x0600035E RID: 862
		public abstract void Read(IPage page);

		// Token: 0x0600035F RID: 863 RVA: 0x00009AAC File Offset: 0x00007CAC
		public void Dispose()
		{
			if (this.accessor.Value != 0L)
			{
				((IAccessor)((object)this.source)).ReleaseAccessor(this.accessor, null);
				this.accessor.Value = 0L;
			}
			if (this.source != null)
			{
				IDisposable disposable = this.source as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this.source = default(T);
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009B24 File Offset: 0x00007D24
		private static DataTable GetSchemaTable(ColumnInfo[] columnInfos)
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("ColumnName", typeof(string));
			dataTable.Columns.Add("ColumnGuid", typeof(Guid));
			dataTable.Columns.Add("ColumnPropId", typeof(DBPROPID));
			dataTable.Columns.Add("ColumnOrdinal", typeof(int));
			dataTable.Columns.Add("DataType", typeof(Type));
			dataTable.Columns.Add("AllowDBNull", typeof(bool));
			foreach (ColumnInfo columnInfo in columnInfos)
			{
				string text = (columnInfo.ColumnID.HasName ? columnInfo.ColumnID.Name : string.Empty);
				Guid guid = (columnInfo.ColumnID.HasGuid ? columnInfo.ColumnID.Guid : Guid.Empty);
				DBPROPID dbpropid = (columnInfo.ColumnID.HasPropertyID ? columnInfo.ColumnID.PropertyID : ((DBPROPID)0U));
				int num = checked((int)columnInfo.Ordinal.Value);
				Type type = RowsetPageReaderBase<T>.TypeInfo.GetTypeInfo(columnInfo.Type, columnInfo.ColumnSize).Type;
				bool flag = RowsetPageReaderBase<T>.IsNullable(columnInfo.Flags);
				dataTable.Rows.Add(new object[] { text, guid, dbpropid, num, type, flag });
			}
			return dataTable;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009CD9 File Offset: 0x00007ED9
		private static ColumnInfo[] GetColumnInfos(T source)
		{
			return new ColumnsInfo((IColumnsInfo)((object)source)).ColumnInfos;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009CF0 File Offset: 0x00007EF0
		private static bool IsNullable(DBCOLUMNFLAGS flags)
		{
			return (flags & DBCOLUMNFLAGS.ISNULLABLE) == DBCOLUMNFLAGS.ISNULLABLE || (flags & DBCOLUMNFLAGS.MAYBENULL) == DBCOLUMNFLAGS.MAYBENULL;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00009D04 File Offset: 0x00007F04
		private static BindingsInfo CreateBindings(ColumnInfo[] columnInfos)
		{
			DBBINDING[] array = new DBBINDING[columnInfos.Length];
			DBBYTEOFFSET dbbyteoffset = new DBBYTEOFFSET
			{
				Value = 0UL
			};
			for (int i = 0; i < columnInfos.Length; i++)
			{
				RowsetPageReaderBase<T>.TypeInfo typeInfo = RowsetPageReaderBase<T>.TypeInfo.GetTypeInfo(columnInfos[i].Type, columnInfos[i].ColumnSize);
				DBLENGTH length = typeInfo.Length;
				if (length.Value > DBLENGTH.MaxValue.Value - DBLENGTH.Size.Value - 4UL - dbbyteoffset.Value)
				{
					throw new ArgumentException("Row is too large");
				}
				DBLENGTH dblength = length;
				if ((typeInfo.NativeType & DBTYPE.BYREF) == DBTYPE.BYREF)
				{
					dblength = DbLength.MaxValue;
				}
				array[i].Ordinal = columnInfos[i].Ordinal;
				array[i].Part = (DBPART)7U;
				array[i].MemOwner = DBMEMOWNER.CLIENTOWNED;
				array[i].ParamIO = DBPARAMIO.NOTPARAM;
				array[i].MaxLen = dblength;
				array[i].Flags = 0U;
				array[i].Type = typeInfo.NativeType;
				array[i].Precision = columnInfos[i].Precision;
				array[i].Scale = columnInfos[i].Scale;
				array[i].Length = dbbyteoffset;
				dbbyteoffset.Value += DBLENGTH.Size.Value;
				array[i].Status = dbbyteoffset;
				dbbyteoffset.Value += 4UL;
				array[i].Value = dbbyteoffset;
				dbbyteoffset.Value += length.Value;
				dbbyteoffset.Value += (ulong)IntPtr.Size - dbbyteoffset.Value % (ulong)IntPtr.Size;
			}
			return new BindingsInfo
			{
				Bindings = array,
				RowLength = (int)dbbyteoffset.Value
			};
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00009EE0 File Offset: 0x000080E0
		private unsafe static HACCESSOR CreateAccessor(T source, BindingsInfo bindings)
		{
			IAccessor accessor = (IAccessor)((object)source);
			DBBINDSTATUS[] array = new DBBINDSTATUS[bindings.Bindings.Length];
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
			DBBINDING[] array3;
			DBBINDING* ptr2;
			if ((array3 = bindings.Bindings) == null || array3.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array3[0];
			}
			HACCESSOR haccessor;
			accessor.CreateAccessor(DBACCESSORFLAGS.ROWDATA, new DBCOUNTITEM
			{
				Value = (ulong)bindings.Bindings.Length
			}, ptr2, new DBLENGTH
			{
				Value = (ulong)bindings.RowLength
			}, out haccessor, ptr);
			array3 = null;
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

		// Token: 0x0400037B RID: 891
		private readonly DataTable schemaTable;

		// Token: 0x0400037C RID: 892
		private readonly ReaderWriterProgress progress;

		// Token: 0x0400037D RID: 893
		private readonly BindingsInfo bindings;

		// Token: 0x0400037E RID: 894
		private T source;

		// Token: 0x0400037F RID: 895
		private HACCESSOR accessor;

		// Token: 0x04000380 RID: 896
		private volatile bool cancelIssued;

		// Token: 0x020000F2 RID: 242
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal struct TypeInfo
		{
			// Token: 0x060004C6 RID: 1222 RVA: 0x0000E740 File Offset: 0x0000C940
			private TypeInfo(Type type, DBTYPE nativeType, uint length)
			{
				this = new RowsetPageReaderBase<T>.TypeInfo(type, nativeType, new DBLENGTH
				{
					Value = (ulong)length
				});
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x0000E767 File Offset: 0x0000C967
			private TypeInfo(Type type, DBTYPE nativeType, DBLENGTH length)
			{
				this.type = type;
				this.nativeType = nativeType;
				this.length = length;
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000E77E File Offset: 0x0000C97E
			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000E786 File Offset: 0x0000C986
			public DBTYPE NativeType
			{
				get
				{
					return this.nativeType;
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000E78E File Offset: 0x0000C98E
			public DBLENGTH Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x0000E798 File Offset: 0x0000C998
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			public static RowsetPageReaderBase<T>.TypeInfo GetTypeInfo(DBTYPE type, DBLENGTH columnSize)
			{
				if (type <= DBTYPE.DBTIMESTAMPOFFSET)
				{
					switch (type)
					{
					case DBTYPE.I2:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(short), DBTYPE.I2, 2U);
					case DBTYPE.I4:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(int), DBTYPE.I4, 4U);
					case DBTYPE.R4:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(float), DBTYPE.R4, 4U);
					case DBTYPE.R8:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(double), DBTYPE.R8, 8U);
					case DBTYPE.CY:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(Currency), DBTYPE.CY, DbLength.Currency);
					case DBTYPE.DATE:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(DateTime), DBTYPE.R8, 8U);
					case DBTYPE.BSTR:
						break;
					case DBTYPE.IDISPATCH:
					case DBTYPE.ERROR:
					case (DBTYPE)15:
						goto IL_0369;
					case DBTYPE.BOOL:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(bool), DBTYPE.BOOL, DbLength.VariantBool);
					case DBTYPE.VARIANT:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(object), DBTYPE.VARIANT, DbLength.Variant);
					case DBTYPE.IUNKNOWN:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(object), DBTYPE.VARIANT, DbLength.Variant);
					case DBTYPE.DECIMAL:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(decimal), DBTYPE.DECIMAL, DbLength.Decimal);
					case DBTYPE.I1:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(sbyte), DBTYPE.I1, 1U);
					case DBTYPE.UI1:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(byte), DBTYPE.UI1, 1U);
					case DBTYPE.UI2:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(ushort), DBTYPE.UI2, 2U);
					case DBTYPE.UI4:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(uint), DBTYPE.UI4, 4U);
					case DBTYPE.I8:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(long), DBTYPE.I8, 8U);
					case DBTYPE.UI8:
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(ulong), DBTYPE.UI8, 8U);
					default:
						if (type == DBTYPE.GUID)
						{
							return new RowsetPageReaderBase<T>.TypeInfo(typeof(Guid), DBTYPE.GUID, DbLength.Guid);
						}
						switch (type)
						{
						case DBTYPE.BYTES:
							goto IL_028F;
						case DBTYPE.STR:
						case DBTYPE.WSTR:
							if (columnSize.Value < 16UL)
							{
								return new RowsetPageReaderBase<T>.TypeInfo(typeof(string), DBTYPE.WSTR, (uint)(columnSize.Value + 1UL) * 2U);
							}
							return new RowsetPageReaderBase<T>.TypeInfo(typeof(string), (DBTYPE)16514, DbLength.Pointer);
						case DBTYPE.NUMERIC:
							return new RowsetPageReaderBase<T>.TypeInfo(typeof(Number), DBTYPE.NUMERIC, DbLength.Numeric);
						case DBTYPE.UDT:
						case (DBTYPE)137:
						case DBTYPE.PROPVARIANT:
						case DBTYPE.VARNUMERIC:
						case (DBTYPE)140:
						case (DBTYPE)142:
						case (DBTYPE)143:
						case (DBTYPE)144:
							goto IL_0369;
						case DBTYPE.DBDATE:
							return new RowsetPageReaderBase<T>.TypeInfo(typeof(Date), DBTYPE.DBDATE, DbLength.DbDate);
						case DBTYPE.DBTIME:
						case DBTYPE.DBTIME2:
							return new RowsetPageReaderBase<T>.TypeInfo(typeof(Time), DBTYPE.DBTIME2, DbLength.DbTime2);
						case DBTYPE.DBTIMESTAMP:
							return new RowsetPageReaderBase<T>.TypeInfo(typeof(DateTime), DBTYPE.DATE, 8U);
						case DBTYPE.HCHAPTER:
							return new RowsetPageReaderBase<T>.TypeInfo(typeof(ulong), DBTYPE.HCHAPTER, 8U);
						case DBTYPE.XML:
							break;
						case DBTYPE.DBTIMESTAMPOFFSET:
							return new RowsetPageReaderBase<T>.TypeInfo(typeof(DateTimeOffset), DBTYPE.DBTIMESTAMPOFFSET, DbLength.TimeStampOffset);
						default:
							goto IL_0369;
						}
						break;
					}
				}
				else
				{
					if (type == DBTYPE.DBDURATION)
					{
						return new RowsetPageReaderBase<T>.TypeInfo(typeof(TimeSpan), DBTYPE.DBDURATION, DbLength.Duration);
					}
					if (type == (DBTYPE)16512)
					{
						goto IL_028F;
					}
					if (type - (DBTYPE)16513 > 1)
					{
						goto IL_0369;
					}
				}
				return new RowsetPageReaderBase<T>.TypeInfo(typeof(string), (DBTYPE)16514, DbLength.Pointer);
				IL_028F:
				return new RowsetPageReaderBase<T>.TypeInfo(typeof(byte[]), (DBTYPE)16512, DbLength.Pointer);
				IL_0369:
				return new RowsetPageReaderBase<T>.TypeInfo(typeof(object), DBTYPE.VARIANT, DbLength.Variant);
			}

			// Token: 0x04000410 RID: 1040
			private Type type;

			// Token: 0x04000411 RID: 1041
			private DBTYPE nativeType;

			// Token: 0x04000412 RID: 1042
			private DBLENGTH length;
		}

		// Token: 0x020000F3 RID: 243
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal abstract class RowsetPageBase : IPage, IDisposable
		{
			// Token: 0x060004CC RID: 1228 RVA: 0x0000EB24 File Offset: 0x0000CD24
			protected RowsetPageBase(DataTable schemaTable, BindingsInfo bindings)
				: this(schemaTable, bindings, SchemaTableHelper.MaxRowCount(schemaTable))
			{
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x0000EB34 File Offset: 0x0000CD34
			protected RowsetPageBase(DataTable schemaTable, BindingsInfo bindings, int maxRowCount)
			{
				this.maxRowCount = maxRowCount;
				this.columnCount = schemaTable.Rows.Count;
				this.bindings = bindings;
				this.buffer = new byte[checked(bindings.RowLength * this.maxRowCount)];
				this.columnsPage = new ColumnsPage(schemaTable);
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000EB8A File Offset: 0x0000CD8A
			public int ColumnCount
			{
				get
				{
					return this.columnCount;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000EB92 File Offset: 0x0000CD92
			// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000EB9A File Offset: 0x0000CD9A
			public int RowCount { get; protected set; }

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000EBA3 File Offset: 0x0000CDA3
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return RowsetPageReaderBase<T>.RowsetPageBase.exceptionRows;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000EBAA File Offset: 0x0000CDAA
			protected int MaxRowCount
			{
				get
				{
					return this.maxRowCount;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000EBB2 File Offset: 0x0000CDB2
			protected BindingsInfo Bindings
			{
				get
				{
					return this.bindings;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000EBBA File Offset: 0x0000CDBA
			protected byte[] Buffer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x0000EBC2 File Offset: 0x0000CDC2
			public IColumn GetColumn(int ordinal)
			{
				this.SyncColumnsPage();
				return this.columnsPage.GetColumn(ordinal);
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
			private unsafe void SyncColumnsPage()
			{
				if (this.columnsPage.RowCount != this.RowCount)
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
					for (int i = 0; i < this.RowCount; i++)
					{
						for (int j = 0; j < this.bindings.Bindings.Length; j++)
						{
							Column column = this.columnsPage.GetColumn(j);
							DBSTATUS dbstatus = (DBSTATUS)(*(uint*)(ptr2 + this.bindings.Bindings[j].Status.Value));
							if (dbstatus == DBSTATUS.S_OK)
							{
								DBTYPE type = this.bindings.Bindings[j].Type;
								void* ptr3 = (void*)(ptr2 + this.bindings.Bindings[j].Value.Value);
								DBLENGTH dblength = *(DBLENGTH*)(ptr2 + this.bindings.Bindings[j].Length.Value);
								column.AddValue(type, ptr3, (int)dblength.Value);
							}
							else
							{
								if (dbstatus != DBSTATUS.S_ISNULL)
								{
									throw new InvalidOperationException("Unknown status");
								}
								column.AddNull();
							}
						}
						this.columnsPage.AddRow();
						ptr2 += this.bindings.RowLength;
					}
					array = null;
				}
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x0000ED34 File Offset: 0x0000CF34
			public unsafe void Clear()
			{
				this.columnsPage.Clear();
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
				for (int i = 0; i < this.bindings.Bindings.Length; i++)
				{
					DBTYPE type = this.bindings.Bindings[i].Type;
					if ((type & DBTYPE.BYREF) == DBTYPE.BYREF || type == DBTYPE.VARIANT)
					{
						byte* ptr2 = ptr;
						for (int j = 0; j < this.RowCount; j++)
						{
							if (*(uint*)(ptr2 + this.bindings.Bindings[i].Status.Value) == 0U)
							{
								if (type == DBTYPE.VARIANT)
								{
									void* ptr3 = (void*)(ptr2 + this.bindings.Bindings[i].Value.Value);
									Variant.Clear((VARIANT*)ptr3);
								}
								else
								{
									Marshal.FreeCoTaskMem(new IntPtr(*(IntPtr*)(ptr2 + this.bindings.Bindings[i].Value.Value)));
								}
							}
							ptr2 += this.bindings.RowLength;
						}
					}
				}
				this.RowCount = 0;
				array = null;
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x0000EE69 File Offset: 0x0000D069
			public void Dispose()
			{
				this.columnsPage.Dispose();
				this.Clear();
			}

			// Token: 0x04000413 RID: 1043
			private static readonly IDictionary<int, IExceptionRow> exceptionRows = new Dictionary<int, IExceptionRow>();

			// Token: 0x04000414 RID: 1044
			private readonly int maxRowCount;

			// Token: 0x04000415 RID: 1045
			private readonly int columnCount;

			// Token: 0x04000416 RID: 1046
			private readonly BindingsInfo bindings;

			// Token: 0x04000417 RID: 1047
			private readonly byte[] buffer;

			// Token: 0x04000418 RID: 1048
			private readonly ColumnsPage columnsPage;
		}
	}
}
