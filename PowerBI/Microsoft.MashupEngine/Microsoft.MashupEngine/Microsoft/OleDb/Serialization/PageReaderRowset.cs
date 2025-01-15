using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FA4 RID: 8100
	public class PageReaderRowset : Rowset, IRowset, IDBAsynchStatus, IDisposable
	{
		// Token: 0x0600C564 RID: 50532 RVA: 0x002751AF File Offset: 0x002733AF
		public PageReaderRowset(IPageReader reader, IManagedDataConvert dataConvert, Func<ISerializedException, Exception> pageExceptionHandler)
			: this(reader, dataConvert, null, pageExceptionHandler, null, null)
		{
		}

		// Token: 0x0600C565 RID: 50533 RVA: 0x002751BD File Offset: 0x002733BD
		public PageReaderRowset(IPageReader reader, IManagedDataConvert dataConvert, IDictionary<Type, DBTYPE> columnTypeProjection, Func<ISerializedException, Exception> pageExceptionHandler, bool cellErrorUnavailableStatus, IEvaluationTimeout evaluationTimeout)
			: this(reader, dataConvert, columnTypeProjection, pageExceptionHandler, null, evaluationTimeout)
		{
			this.cellErrorUnavailableStatus = cellErrorUnavailableStatus;
		}

		// Token: 0x0600C566 RID: 50534 RVA: 0x002751D8 File Offset: 0x002733D8
		public PageReaderRowset(IPageReader reader, IManagedDataConvert dataConvert, IDictionary<Type, DBTYPE> columnTypeProjection, Func<ISerializedException, Exception> pageExceptionHandler, Func<ISerializedException, Exception> cellErrorHandler, IEvaluationTimeout evaluationTimeout)
		{
			ColumnInfo[] columnInfos = this.GetColumnInfos(reader.Schema, columnTypeProjection);
			this.reader = reader;
			this.dataConvert = dataConvert;
			this.accessor = new Accessor();
			this.columnsInfo = new ColumnsInfo(columnInfos);
			this.rowsetInfo = new RowsetInfo(DbProperties.Create());
			this.ordinalIndices = PageReaderRowset.CreateIndices(columnInfos);
			this.pageExceptionHandler = pageExceptionHandler;
			this.cellErrorHandler = cellErrorHandler;
			this.evaluationTimeout = evaluationTimeout;
			this.cellErrorUnavailableStatus = false;
			this.page = reader.CreatePage();
		}

		// Token: 0x17002FD5 RID: 12245
		// (get) Token: 0x0600C567 RID: 50535 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override IRowset _Rowset
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002FD6 RID: 12246
		// (get) Token: 0x0600C568 RID: 50536 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override IDBAsynchStatus DbAsyncStatus
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002FD7 RID: 12247
		// (get) Token: 0x0600C569 RID: 50537 RVA: 0x00275265 File Offset: 0x00273465
		public override IColumnsInfo ColumnsInfo
		{
			get
			{
				return this.columnsInfo;
			}
		}

		// Token: 0x17002FD8 RID: 12248
		// (get) Token: 0x0600C56A RID: 50538 RVA: 0x0027526D File Offset: 0x0027346D
		public override IAccessor Accessor
		{
			get
			{
				return this.accessor;
			}
		}

		// Token: 0x17002FD9 RID: 12249
		// (get) Token: 0x0600C56B RID: 50539 RVA: 0x00275275 File Offset: 0x00273475
		public override IRowsetInfo RowsetInfo
		{
			get
			{
				return this.rowsetInfo;
			}
		}

		// Token: 0x0600C56C RID: 50540 RVA: 0x0027527D File Offset: 0x0027347D
		public override int InterfaceSupportsErrorInfo(ref Guid iid)
		{
			if (iid == IID.IRowset && (this.cellErrorHandler != null || this.pageExceptionHandler != null))
			{
				return 0;
			}
			return base.InterfaceSupportsErrorInfo(ref iid);
		}

		// Token: 0x17002FDA RID: 12250
		// (get) Token: 0x0600C56D RID: 50541 RVA: 0x0027526D File Offset: 0x0027346D
		protected Accessor InternalAccessor
		{
			get
			{
				return this.accessor;
			}
		}

		// Token: 0x17002FDB RID: 12251
		// (get) Token: 0x0600C56E RID: 50542 RVA: 0x00275265 File Offset: 0x00273465
		protected ColumnsInfo InternalColumnsInfo
		{
			get
			{
				return this.columnsInfo;
			}
		}

		// Token: 0x0600C56F RID: 50543 RVA: 0x002752AA File Offset: 0x002734AA
		public void ReadToEnd()
		{
			do
			{
				this.ReadNextPage();
			}
			while (this.page.RowCount > 0);
		}

		// Token: 0x0600C570 RID: 50544 RVA: 0x002752C0 File Offset: 0x002734C0
		unsafe void IRowset.AddRefRows(DBCOUNTITEM cRows, HROW* nativeRows, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			if (cRows.value > 2147483647UL)
			{
				throw new ArgumentException("Invalid row count");
			}
			int num = (int)cRows.value;
			this.refCounts += num;
			for (int i = 0; i < num; i++)
			{
				if (nativeRefCounts != null)
				{
					nativeRefCounts[i] = (uint)this.refCounts;
				}
				if (nativeRowStatus != null)
				{
					nativeRowStatus[i] = DBROWSTATUS.S_OK;
				}
			}
		}

		// Token: 0x0600C571 RID: 50545 RVA: 0x0027532C File Offset: 0x0027352C
		unsafe void IRowset.GetData(HROW hRow, HACCESSOR hAccessor, byte* destBuffer)
		{
			int row = (int)hRow.value;
			Binding[] bindings = this.accessor.GetBinder(hAccessor).Bindings;
			Func<Binding, IOleDbColumn> func = delegate(Binding binding)
			{
				int num = this.ordinalIndices[(int)binding.Ordinal.value];
				IExceptionRow exceptionRow;
				ISerializedException ex;
				if ((!this.cellErrorUnavailableStatus && this.cellErrorHandler == null) || !this.page.ExceptionRows.TryGetValue(row, out exceptionRow) || !exceptionRow.Exceptions.TryGetValue(num, out ex))
				{
					return (IOleDbColumn)this.page.GetColumn(num);
				}
				if (this.cellErrorHandler != null)
				{
					throw this.cellErrorHandler(ex);
				}
				return UnavailableColumn.Instance;
			};
			PageReaderRowset.GetData(row, bindings, destBuffer, this.dataConvert, func);
		}

		// Token: 0x0600C572 RID: 50546 RVA: 0x00275384 File Offset: 0x00273584
		protected unsafe static void GetData(int row, Binding[] bindings, byte* destBuffer, IManagedDataConvert dataConvert, Func<Binding, IOleDbColumn> getColumn)
		{
			for (int i = 0; i < bindings.Length; i++)
			{
				PageReaderRowset.ClearBinding(bindings[i], destBuffer);
			}
			try
			{
				foreach (Binding binding in bindings)
				{
					DBPART part = binding.Part;
					IOleDbColumn oleDbColumn = getColumn(binding);
					DBLENGTH dblength;
					DBSTATUS dbstatus;
					if ((part & DBPART.VALUE) == DBPART.VALUE)
					{
						dbstatus = oleDbColumn.GetValue(row, dataConvert, binding, destBuffer + binding.ValueOffset.value, out dblength);
					}
					else
					{
						dbstatus = (oleDbColumn.IsNull(row) ? DBSTATUS.S_ISNULL : DBSTATUS.S_OK);
						dblength = new DBLENGTH
						{
							value = 0UL
						};
					}
					if ((part & DBPART.LENGTH) == DBPART.LENGTH)
					{
						DBLENGTH* ptr = (DBLENGTH*)(destBuffer + binding.LengthOffset.value);
						*ptr = ((dbstatus == DBSTATUS.S_ISNULL) ? DbLength.Zero : dblength);
					}
					if ((part & DBPART.STATUS) == DBPART.STATUS)
					{
						DBSTATUS* ptr2 = (DBSTATUS*)(destBuffer + binding.StatusOffset.value);
						*ptr2 = dbstatus;
					}
				}
			}
			catch
			{
				for (int k = 0; k < bindings.Length; k++)
				{
					PageReaderRowset.FreeBinding(bindings[k], destBuffer);
				}
				throw;
			}
		}

		// Token: 0x0600C573 RID: 50547 RVA: 0x00275490 File Offset: 0x00273690
		public void Dispose()
		{
			this.page.Dispose();
			this.reader.Dispose();
		}

		// Token: 0x0600C574 RID: 50548 RVA: 0x002754A8 File Offset: 0x002736A8
		private unsafe static void ClearBinding(Binding binding, byte* destBuffer)
		{
			if ((binding.Part & DBPART.VALUE) == DBPART.VALUE)
			{
				void* ptr = (void*)(destBuffer + binding.ValueOffset.value);
				if (binding.DestType == DBTYPE.BSTR)
				{
					*(IntPtr*)ptr = (IntPtr)((UIntPtr)0);
					return;
				}
				if (binding.DestType == DBTYPE.VARIANT || binding.DestType == DBTYPE.TYPEDVARIANT)
				{
					Variant.Init((VARIANT*)ptr);
				}
			}
		}

		// Token: 0x0600C575 RID: 50549 RVA: 0x002754FC File Offset: 0x002736FC
		private unsafe static void FreeBinding(Binding binding, byte* destBuffer)
		{
			if ((binding.Part & DBPART.VALUE) == DBPART.VALUE)
			{
				void* ptr = (void*)(destBuffer + binding.ValueOffset.value);
				if (binding.DestType == DBTYPE.BSTR)
				{
					Marshal.FreeBSTR(new IntPtr(*(IntPtr*)ptr));
					return;
				}
				if (binding.DestType == DBTYPE.VARIANT || binding.DestType == DBTYPE.TYPEDVARIANT)
				{
					Variant.Clear((VARIANT*)ptr);
				}
			}
		}

		// Token: 0x0600C576 RID: 50550 RVA: 0x00275558 File Offset: 0x00273758
		unsafe int IRowset.GetNextRows(HCHAPTER hchapter, DBROWOFFSET lRowsOffset, DBROWCOUNT cRows, out DBCOUNTITEM countRowsObtained, HROW** pRows)
		{
			countRowsObtained = default(DBCOUNTITEM);
			if (hchapter.value != 0UL)
			{
				return -2147217914;
			}
			if (lRowsOffset.value != 0L)
			{
				return -2147217879;
			}
			if (cRows.value < 0L)
			{
				return -2147217884;
			}
			if (this.refCounts != 0)
			{
				return -2147217883;
			}
			if (this.offset == this.page.RowCount)
			{
				if (this.page.PageException != null)
				{
					if (this.pageExceptionHandler != null)
					{
						throw this.pageExceptionHandler(this.page.PageException);
					}
					this.page = new PageExceptionPage(this.columnsInfo.ColumnInfos.Length, this.page.PageException);
				}
				else
				{
					IEvaluationTimeout evaluationTimeout = this.evaluationTimeout;
					if (evaluationTimeout != null)
					{
						evaluationTimeout.EnableTimeout();
					}
					this.ReadNextPage();
					IEvaluationTimeout evaluationTimeout2 = this.evaluationTimeout;
					if (evaluationTimeout2 != null)
					{
						evaluationTimeout2.DisableTimeout();
					}
				}
				this.offset = 0;
			}
			int num = (int)Math.Min(cRows.value, (long)(this.page.RowCount - this.offset));
			if (num == 0)
			{
				return 265926;
			}
			if (*(IntPtr*)pRows != (IntPtr)((UIntPtr)0))
			{
				HROW* ptr = *(IntPtr*)pRows;
				for (int i = 0; i < num; i++)
				{
					ptr[i] = new HROW
					{
						value = (long)(this.offset + i)
					};
				}
			}
			else
			{
				using (ComHeap comHeap = new ComHeap())
				{
					HROW* ptr2 = (HROW*)comHeap.AllocArray(num, sizeof(HROW));
					for (int j = 0; j < num; j++)
					{
						ptr2[j] = new HROW
						{
							value = (long)(this.offset + j)
						};
					}
					comHeap.Commit();
					*(IntPtr*)pRows = ptr2;
				}
			}
			this.offset += num;
			this.refCounts += num;
			countRowsObtained = new DBCOUNTITEM
			{
				value = (ulong)num
			};
			return 0;
		}

		// Token: 0x0600C577 RID: 50551 RVA: 0x0027575C File Offset: 0x0027395C
		private void ReadNextPage()
		{
			try
			{
				this.reader.Read(this.page);
			}
			catch (OperationCanceledException)
			{
				IEvaluationTimeout evaluationTimeout = this.evaluationTimeout;
				if (evaluationTimeout != null && evaluationTimeout.TimedOut)
				{
					Marshal.ThrowExceptionForHR(-2147217871);
				}
				Marshal.ThrowExceptionForHR(-2147217842);
				throw;
			}
		}

		// Token: 0x0600C578 RID: 50552 RVA: 0x002757B8 File Offset: 0x002739B8
		unsafe void IRowset.ReleaseRows(DBCOUNTITEM cRows, HROW* nativeRows, void* nativeRowOptions, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			if (cRows.value > 2147483647UL)
			{
				throw new ArgumentException("Invalid row count");
			}
			if (nativeRowOptions != null)
			{
				throw new ArgumentException("Invalid row status");
			}
			if ((int)cRows.value > this.refCounts)
			{
				throw new ArgumentException("Invalid row count");
			}
			int num = (int)cRows.value;
			this.refCounts -= num;
			for (int i = 0; i < num; i++)
			{
				if (nativeRefCounts != null)
				{
					nativeRefCounts[i] = (uint)this.refCounts;
				}
				if (nativeRowStatus != null)
				{
					nativeRowStatus[i] = DBROWSTATUS.S_OK;
				}
			}
		}

		// Token: 0x0600C579 RID: 50553 RVA: 0x0027584D File Offset: 0x00273A4D
		void IRowset.RestartPosition(HCHAPTER hchapter)
		{
			throw new COMException("Restarting a rowset is not supported", -2147217896);
		}

		// Token: 0x0600C57A RID: 50554 RVA: 0x0000336E File Offset: 0x0000156E
		void IDBAsynchStatus.Abort(HCHAPTER hChapter, DBASYNCHOP eOperation)
		{
		}

		// Token: 0x0600C57B RID: 50555 RVA: 0x0027585E File Offset: 0x00273A5E
		unsafe void IDBAsynchStatus.GetStatus(HCHAPTER hChapter, DBASYNCHOP eOperation, DBCOUNTITEM* pulProgress, DBCOUNTITEM* pulProgressMax, out DBASYNCHPHASE peAsynchPhase, char** ppwszStatusText)
		{
			if (pulProgress != null)
			{
				*pulProgress = default(DBCOUNTITEM);
			}
			if (pulProgressMax != null)
			{
				*pulProgressMax = default(DBCOUNTITEM);
			}
			if (ppwszStatusText != null)
			{
				*(IntPtr*)ppwszStatusText = (IntPtr)((UIntPtr)0);
			}
			peAsynchPhase = DBASYNCHPHASE.COMPLETE;
		}

		// Token: 0x0600C57C RID: 50556 RVA: 0x0027588C File Offset: 0x00273A8C
		private static int[] CreateIndices(ColumnInfo[] columnInfos)
		{
			int[] array = new int[PageReaderRowset.GetMaxOrdinal(columnInfos) + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			for (int j = 0; j < columnInfos.Length; j++)
			{
				array[(int)(checked((IntPtr)columnInfos[j].Ordinal.value))] = j;
			}
			return array;
		}

		// Token: 0x0600C57D RID: 50557 RVA: 0x002758DC File Offset: 0x00273ADC
		private static int GetMaxOrdinal(ColumnInfo[] columnInfos)
		{
			int num = -1;
			for (int i = 0; i < columnInfos.Length; i++)
			{
				num = Math.Max(num, (int)columnInfos[i].Ordinal.value);
			}
			return num;
		}

		// Token: 0x0600C57E RID: 50558 RVA: 0x00275910 File Offset: 0x00273B10
		protected virtual ColumnInfo[] GetColumnInfos(TableSchema schema, IDictionary<Type, DBTYPE> columnTypeProjection)
		{
			ColumnInfo[] array = new ColumnInfo[schema.ColumnCount];
			for (int i = 0; i < array.Length; i++)
			{
				SchemaColumn column = schema.GetColumn(i);
				DBTYPE type = PageReaderRowset.GetType(column.DataType, columnTypeProjection);
				PageReaderRowset.TypeInfo typeInfo = PageReaderRowset.TypeInfo.GetTypeInfo(type);
				ColumnInfo columnInfo = new ColumnInfo(new ColumnID(column.Name), new DBORDINAL
				{
					value = (ulong)column.Ordinal.Value
				}, typeInfo.ColumnFlags | (column.Nullable ? ((DBCOLUMNFLAGS)96U) : DBCOLUMNFLAGS.NONE), typeInfo.Length, type, 0, 0);
				array[i] = columnInfo;
			}
			return array;
		}

		// Token: 0x0600C57F RID: 50559 RVA: 0x002759AC File Offset: 0x00273BAC
		protected static DBLENGTH GetTypeInfo(DBTYPE dbType, out DBCOLUMNFLAGS flags)
		{
			PageReaderRowset.TypeInfo typeInfo = PageReaderRowset.TypeInfo.GetTypeInfo(dbType);
			flags = typeInfo.ColumnFlags;
			return typeInfo.Length;
		}

		// Token: 0x0600C580 RID: 50560 RVA: 0x002759D0 File Offset: 0x00273BD0
		protected static DBTYPE GetType(Type type, IDictionary<Type, DBTYPE> columnTypeProjection)
		{
			ValueWithMetadata.HasMetadata(type, out type);
			DBTYPE dbtype;
			if (columnTypeProjection != null && columnTypeProjection.TryGetValue(type, out dbtype))
			{
				return dbtype;
			}
			return PageReaderRowset.GetType(type);
		}

		// Token: 0x0600C581 RID: 50561 RVA: 0x002759FC File Offset: 0x00273BFC
		private static DBTYPE GetType(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return DBTYPE.BOOL;
			case TypeCode.SByte:
				return DBTYPE.I1;
			case TypeCode.Byte:
				return DBTYPE.UI1;
			case TypeCode.Int16:
				return DBTYPE.I2;
			case TypeCode.UInt16:
				return DBTYPE.UI2;
			case TypeCode.Int32:
				return DBTYPE.I4;
			case TypeCode.UInt32:
				return DBTYPE.UI4;
			case TypeCode.Int64:
				return DBTYPE.I8;
			case TypeCode.UInt64:
				return DBTYPE.UI8;
			case TypeCode.Single:
				return DBTYPE.R4;
			case TypeCode.Double:
				return DBTYPE.R8;
			case TypeCode.Decimal:
				return DBTYPE.DECIMAL;
			case TypeCode.DateTime:
				return DBTYPE.DBTIMESTAMP;
			case TypeCode.String:
				return DBTYPE.WSTR;
			}
			if (type == typeof(Guid))
			{
				return DBTYPE.GUID;
			}
			if (type == typeof(DateTimeOffset))
			{
				return DBTYPE.DBTIMESTAMPOFFSET;
			}
			if (type == typeof(TimeSpan))
			{
				return DBTYPE.DBDURATION;
			}
			if (type == typeof(object))
			{
				return DBTYPE.VARIANT;
			}
			if (type == typeof(ErrorWrapper))
			{
				return DBTYPE.ERROR;
			}
			if (type == typeof(byte[]))
			{
				return DBTYPE.BYTES;
			}
			if (type == typeof(Currency))
			{
				return DBTYPE.CY;
			}
			if (type == typeof(Time))
			{
				return DBTYPE.DBTIME2;
			}
			if (type == typeof(Date))
			{
				return DBTYPE.DBDATE;
			}
			if (type == typeof(Number))
			{
				return DBTYPE.NUMERIC;
			}
			if (type == typeof(UnsupportedType))
			{
				return DBTYPE.VARIANT;
			}
			throw new NotSupportedException();
		}

		// Token: 0x040064EF RID: 25839
		private readonly IPageReader reader;

		// Token: 0x040064F0 RID: 25840
		private readonly IManagedDataConvert dataConvert;

		// Token: 0x040064F1 RID: 25841
		private readonly Accessor accessor;

		// Token: 0x040064F2 RID: 25842
		private readonly ColumnsInfo columnsInfo;

		// Token: 0x040064F3 RID: 25843
		private readonly IRowsetInfo rowsetInfo;

		// Token: 0x040064F4 RID: 25844
		private readonly int[] ordinalIndices;

		// Token: 0x040064F5 RID: 25845
		private readonly Func<ISerializedException, Exception> pageExceptionHandler;

		// Token: 0x040064F6 RID: 25846
		private readonly Func<ISerializedException, Exception> cellErrorHandler;

		// Token: 0x040064F7 RID: 25847
		private readonly IEvaluationTimeout evaluationTimeout;

		// Token: 0x040064F8 RID: 25848
		private readonly bool cellErrorUnavailableStatus;

		// Token: 0x040064F9 RID: 25849
		private IPage page;

		// Token: 0x040064FA RID: 25850
		private int offset;

		// Token: 0x040064FB RID: 25851
		private int refCounts;

		// Token: 0x02001FA5 RID: 8101
		private struct TypeInfo
		{
			// Token: 0x0600C582 RID: 50562 RVA: 0x00275B83 File Offset: 0x00273D83
			public TypeInfo(DBLENGTH length, DBCOLUMNFLAGS columnFlags)
			{
				this.length = length;
				this.columnFlags = columnFlags;
			}

			// Token: 0x17002FDC RID: 12252
			// (get) Token: 0x0600C583 RID: 50563 RVA: 0x00275B93 File Offset: 0x00273D93
			public DBLENGTH Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x17002FDD RID: 12253
			// (get) Token: 0x0600C584 RID: 50564 RVA: 0x00275B9B File Offset: 0x00273D9B
			public DBCOLUMNFLAGS ColumnFlags
			{
				get
				{
					return this.columnFlags;
				}
			}

			// Token: 0x17002FDE RID: 12254
			// (get) Token: 0x0600C585 RID: 50565 RVA: 0x00275BA3 File Offset: 0x00273DA3
			public static PageReaderRowset.TypeInfo Boolean
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.VariantBool, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FDF RID: 12255
			// (get) Token: 0x0600C586 RID: 50566 RVA: 0x00275BB1 File Offset: 0x00273DB1
			public static PageReaderRowset.TypeInfo UI1
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.One, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE0 RID: 12256
			// (get) Token: 0x0600C587 RID: 50567 RVA: 0x00275BB1 File Offset: 0x00273DB1
			public static PageReaderRowset.TypeInfo I1
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.One, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE1 RID: 12257
			// (get) Token: 0x0600C588 RID: 50568 RVA: 0x00275BBF File Offset: 0x00273DBF
			public static PageReaderRowset.TypeInfo I2
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Two, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE2 RID: 12258
			// (get) Token: 0x0600C589 RID: 50569 RVA: 0x00275BCD File Offset: 0x00273DCD
			public static PageReaderRowset.TypeInfo I4
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Four, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE3 RID: 12259
			// (get) Token: 0x0600C58A RID: 50570 RVA: 0x00275BDB File Offset: 0x00273DDB
			public static PageReaderRowset.TypeInfo I8
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Eight, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE4 RID: 12260
			// (get) Token: 0x0600C58B RID: 50571 RVA: 0x00275BCD File Offset: 0x00273DCD
			public static PageReaderRowset.TypeInfo R4
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Four, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE5 RID: 12261
			// (get) Token: 0x0600C58C RID: 50572 RVA: 0x00275BDB File Offset: 0x00273DDB
			public static PageReaderRowset.TypeInfo R8
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Eight, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE6 RID: 12262
			// (get) Token: 0x0600C58D RID: 50573 RVA: 0x00275BBF File Offset: 0x00273DBF
			public static PageReaderRowset.TypeInfo UI2
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Two, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE7 RID: 12263
			// (get) Token: 0x0600C58E RID: 50574 RVA: 0x00275BCD File Offset: 0x00273DCD
			public static PageReaderRowset.TypeInfo UI4
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Four, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE8 RID: 12264
			// (get) Token: 0x0600C58F RID: 50575 RVA: 0x00275BDB File Offset: 0x00273DDB
			public static PageReaderRowset.TypeInfo UI8
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Eight, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FE9 RID: 12265
			// (get) Token: 0x0600C590 RID: 50576 RVA: 0x00275BE9 File Offset: 0x00273DE9
			public static PageReaderRowset.TypeInfo Currency
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Currency, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FEA RID: 12266
			// (get) Token: 0x0600C591 RID: 50577 RVA: 0x00275BF7 File Offset: 0x00273DF7
			public static PageReaderRowset.TypeInfo Decimal
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Decimal, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FEB RID: 12267
			// (get) Token: 0x0600C592 RID: 50578 RVA: 0x00275C05 File Offset: 0x00273E05
			public static PageReaderRowset.TypeInfo Guid
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Guid, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FEC RID: 12268
			// (get) Token: 0x0600C593 RID: 50579 RVA: 0x00275C13 File Offset: 0x00273E13
			public static PageReaderRowset.TypeInfo TimeStamp
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.TimeStamp, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FED RID: 12269
			// (get) Token: 0x0600C594 RID: 50580 RVA: 0x00275C21 File Offset: 0x00273E21
			public static PageReaderRowset.TypeInfo TimeStampOffset
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.TimeStampOffset, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FEE RID: 12270
			// (get) Token: 0x0600C595 RID: 50581 RVA: 0x00275C2F File Offset: 0x00273E2F
			public static PageReaderRowset.TypeInfo Duration
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Duration, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FEF RID: 12271
			// (get) Token: 0x0600C596 RID: 50582 RVA: 0x00275C3D File Offset: 0x00273E3D
			public static PageReaderRowset.TypeInfo String
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.MaxValue, DBCOLUMNFLAGS.NONE);
				}
			}

			// Token: 0x17002FF0 RID: 12272
			// (get) Token: 0x0600C597 RID: 50583 RVA: 0x00275C4A File Offset: 0x00273E4A
			public static PageReaderRowset.TypeInfo Variant
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Variant, DBCOLUMNFLAGS.NONE);
				}
			}

			// Token: 0x17002FF1 RID: 12273
			// (get) Token: 0x0600C598 RID: 50584 RVA: 0x00275C57 File Offset: 0x00273E57
			public static PageReaderRowset.TypeInfo Error
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Error, DBCOLUMNFLAGS.NONE);
				}
			}

			// Token: 0x17002FF2 RID: 12274
			// (get) Token: 0x0600C599 RID: 50585 RVA: 0x00275C64 File Offset: 0x00273E64
			public static PageReaderRowset.TypeInfo Numeric
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Numeric, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FF3 RID: 12275
			// (get) Token: 0x0600C59A RID: 50586 RVA: 0x00275C72 File Offset: 0x00273E72
			public static PageReaderRowset.TypeInfo DbTime2
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.DbTime2, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FF4 RID: 12276
			// (get) Token: 0x0600C59B RID: 50587 RVA: 0x00275C80 File Offset: 0x00273E80
			public static PageReaderRowset.TypeInfo DbDate
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.DbDate, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FF5 RID: 12277
			// (get) Token: 0x0600C59C RID: 50588 RVA: 0x00275C8E File Offset: 0x00273E8E
			public static PageReaderRowset.TypeInfo Date
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.Date, DBCOLUMNFLAGS.ISFIXEDLENGTH);
				}
			}

			// Token: 0x17002FF6 RID: 12278
			// (get) Token: 0x0600C59D RID: 50589 RVA: 0x00275C3D File Offset: 0x00273E3D
			public static PageReaderRowset.TypeInfo Binary
			{
				get
				{
					return new PageReaderRowset.TypeInfo(DbLength.MaxValue, DBCOLUMNFLAGS.NONE);
				}
			}

			// Token: 0x0600C59E RID: 50590 RVA: 0x00275C9C File Offset: 0x00273E9C
			public static PageReaderRowset.TypeInfo GetTypeInfo(DBTYPE type)
			{
				if (type <= DBTYPE.DBTIMESTAMP)
				{
					switch (type)
					{
					case DBTYPE.I2:
						return PageReaderRowset.TypeInfo.I2;
					case DBTYPE.I4:
						return PageReaderRowset.TypeInfo.I4;
					case DBTYPE.R4:
						return PageReaderRowset.TypeInfo.R4;
					case DBTYPE.R8:
						return PageReaderRowset.TypeInfo.R8;
					case DBTYPE.CY:
						return PageReaderRowset.TypeInfo.Currency;
					case DBTYPE.DATE:
						return PageReaderRowset.TypeInfo.Date;
					case DBTYPE.BSTR:
					case DBTYPE.IDISPATCH:
					case DBTYPE.IUNKNOWN:
					case (DBTYPE)15:
						break;
					case DBTYPE.ERROR:
						return PageReaderRowset.TypeInfo.Error;
					case DBTYPE.BOOL:
						return PageReaderRowset.TypeInfo.Boolean;
					case DBTYPE.VARIANT:
						return PageReaderRowset.TypeInfo.Variant;
					case DBTYPE.DECIMAL:
						return PageReaderRowset.TypeInfo.Decimal;
					case DBTYPE.I1:
						return PageReaderRowset.TypeInfo.I1;
					case DBTYPE.UI1:
						return PageReaderRowset.TypeInfo.UI1;
					case DBTYPE.UI2:
						return PageReaderRowset.TypeInfo.UI2;
					case DBTYPE.UI4:
						return PageReaderRowset.TypeInfo.UI4;
					case DBTYPE.I8:
						return PageReaderRowset.TypeInfo.I8;
					case DBTYPE.UI8:
						return PageReaderRowset.TypeInfo.UI8;
					default:
						if (type == DBTYPE.GUID)
						{
							return PageReaderRowset.TypeInfo.Guid;
						}
						switch (type)
						{
						case DBTYPE.BYTES:
							return PageReaderRowset.TypeInfo.Binary;
						case DBTYPE.WSTR:
							return PageReaderRowset.TypeInfo.String;
						case DBTYPE.NUMERIC:
							return PageReaderRowset.TypeInfo.Numeric;
						case DBTYPE.DBDATE:
							return PageReaderRowset.TypeInfo.DbDate;
						case DBTYPE.DBTIMESTAMP:
							return PageReaderRowset.TypeInfo.TimeStamp;
						}
						break;
					}
				}
				else
				{
					if (type == DBTYPE.DBTIME2)
					{
						return PageReaderRowset.TypeInfo.DbTime2;
					}
					if (type == DBTYPE.DBTIMESTAMPOFFSET)
					{
						return PageReaderRowset.TypeInfo.TimeStampOffset;
					}
					if (type == DBTYPE.DBDURATION)
					{
						return PageReaderRowset.TypeInfo.Duration;
					}
				}
				throw new NotSupportedException();
			}

			// Token: 0x040064FC RID: 25852
			private readonly DBLENGTH length;

			// Token: 0x040064FD RID: 25853
			private readonly DBCOLUMNFLAGS columnFlags;
		}
	}
}
