using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000CD RID: 205
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class PageReaderRowset : Rowset, IRowset, IDBAsynchStatus, IDisposable
	{
		// Token: 0x06000398 RID: 920 RVA: 0x0000AC23 File Offset: 0x00008E23
		public PageReaderRowset(IPageReader reader)
			: this(reader, DataConvert.GetInstance(), null)
		{
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000AC34 File Offset: 0x00008E34
		public PageReaderRowset(IPageReader reader, IDataConvert dataConvert, IDictionary<Type, DBTYPE> columnTypeProjection)
		{
			ColumnInfo[] array = reader.SchemaTable.ToColumnInfos(columnTypeProjection);
			this.reader = reader;
			this.dataConvert = dataConvert;
			this.accessor = new Accessor();
			this.columnsInfo = new ColumnsInfo(array);
			this.rowsetInfo = new RowsetInfo(DbProperties.Create());
			this.ordinalIndices = array.CreateOrdinalIndices();
			this.page = reader.CreatePage();
			this.isEndOfRowset = false;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		public override IRowset _Rowset
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000ACAB File Offset: 0x00008EAB
		public override IDBAsynchStatus DbAsyncStatus
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000ACAE File Offset: 0x00008EAE
		public IPageReader PageReader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000ACB6 File Offset: 0x00008EB6
		public override IColumnsInfo ColumnsInfo
		{
			get
			{
				return this.columnsInfo;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000ACBE File Offset: 0x00008EBE
		public override IAccessor Accessor
		{
			get
			{
				return this.accessor;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000ACC6 File Offset: 0x00008EC6
		public override IRowsetInfo RowsetInfo
		{
			get
			{
				return this.rowsetInfo;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000ACCE File Offset: 0x00008ECE
		public bool IsEndOfRowset
		{
			get
			{
				return this.isEndOfRowset;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000ACD6 File Offset: 0x00008ED6
		public bool IsLastRow
		{
			get
			{
				return this.offset == this.page.RowCount;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000ACEB File Offset: 0x00008EEB
		public int CurrentPageRowCount
		{
			get
			{
				return this.page.RowCount;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe void IRowset.AddRefRows(DBCOUNTITEM rowCount, HROW* nativeRows, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			if (rowCount.Value > 2147483647UL)
			{
				throw new ArgumentException("Invalid row count");
			}
			int num = (int)rowCount.Value;
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

		// Token: 0x060003A4 RID: 932 RVA: 0x0000AD64 File Offset: 0x00008F64
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe void IRowset.GetData(HROW hrow, HACCESSOR accessor, byte* destBuffer)
		{
			int offsetFromRowHandle = PageReaderRowset.GetOffsetFromRowHandle(hrow);
			Binder binder = this.accessor.GetBinder(accessor);
			Binding[] bindings = binder.Bindings;
			RowsetUtils.GetData(this.page, this.ordinalIndices, binder.Bindings, this.dataConvert, offsetFromRowHandle, destBuffer);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000ADAB File Offset: 0x00008FAB
		public void Dispose()
		{
			this.page.Dispose();
			this.reader.Dispose();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000ADC3 File Offset: 0x00008FC3
		public void ReadPage()
		{
			this.reader.Read(this.page);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe int IRowset.GetNextRows(HCHAPTER hchapter, DBROWOFFSET rowsOffset, DBROWCOUNT rowCount, out DBCOUNTITEM countRowsObtained, HROW** rowsPointer)
		{
			countRowsObtained = default(DBCOUNTITEM);
			if (hchapter.Value != 0UL)
			{
				return -2147217914;
			}
			if (rowsOffset.Value != 0L)
			{
				return -2147217879;
			}
			if (rowCount.Value < 0L)
			{
				return -2147217884;
			}
			if (this.refCounts != 0)
			{
				return -2147217883;
			}
			if (this.isEndOfRowset)
			{
				return 265926;
			}
			if (this.offset == this.page.RowCount)
			{
				this.reader.Read(this.page);
				this.offset = 0;
			}
			int num = (int)Math.Min(rowCount.Value, (long)(this.page.RowCount - this.offset));
			if (num == 0)
			{
				this.isEndOfRowset = true;
				return 265926;
			}
			if (*(IntPtr*)rowsPointer != (IntPtr)((UIntPtr)0))
			{
				PageReaderRowset.BuildRowHandles(*(IntPtr*)rowsPointer, this.offset, num);
			}
			else
			{
				using (ComHeap comHeap = new ComHeap())
				{
					HROW* ptr = (HROW*)comHeap.AllocArray(num, sizeof(HROW));
					PageReaderRowset.BuildRowHandles(ptr, this.offset, num);
					comHeap.Commit();
					*(IntPtr*)rowsPointer = ptr;
				}
			}
			this.offset += num;
			this.refCounts += num;
			countRowsObtained = new DBCOUNTITEM
			{
				Value = (ulong)num
			};
			return 0;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000AF28 File Offset: 0x00009128
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe void IRowset.ReleaseRows(DBCOUNTITEM rowCount, HROW* nativeRows, void* nativeRowOptions, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			if (rowCount.Value > 2147483647UL)
			{
				throw new ArgumentException("Invalid row count");
			}
			if (nativeRowOptions != null)
			{
				throw new ArgumentException("Invalid row status");
			}
			if ((int)rowCount.Value > this.refCounts)
			{
				throw new ArgumentException("Invalid row count");
			}
			int num = (int)rowCount.Value;
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

		// Token: 0x060003A9 RID: 937 RVA: 0x0000AFBD File Offset: 0x000091BD
		void IRowset.RestartPosition(HCHAPTER hchapter)
		{
			throw new COMException("Restarting a rowset is not supported", -2147217896);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000AFCE File Offset: 0x000091CE
		void IDBAsynchStatus.Abort(HCHAPTER chapter, DBASYNCHOP operation)
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000AFD0 File Offset: 0x000091D0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe void IDBAsynchStatus.GetStatus(HCHAPTER chapter, DBASYNCHOP operation, DBCOUNTITEM* progress, DBCOUNTITEM* progressMax, out DBASYNCHPHASE asynchPhase, char** statusText)
		{
			if (progress != null)
			{
				*progress = default(DBCOUNTITEM);
			}
			if (progressMax != null)
			{
				*progressMax = default(DBCOUNTITEM);
			}
			if (statusText != null)
			{
				*(IntPtr*)statusText = (IntPtr)((UIntPtr)0);
			}
			asynchPhase = DBASYNCHPHASE.COMPLETE;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000AFFC File Offset: 0x000091FC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private unsafe static void BuildRowHandles(HROW* rows, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				rows[i] = PageReaderRowset.GetRowHandleFromOffset(offset + i);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000B02D File Offset: 0x0000922D
		private static int GetOffsetFromRowHandle(HROW hrow)
		{
			return (int)hrow.Value - 1;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000B038 File Offset: 0x00009238
		private static HROW GetRowHandleFromOffset(int offset)
		{
			return new HROW
			{
				Value = (long)(offset + 1)
			};
		}

		// Token: 0x04000390 RID: 912
		private readonly IPageReader reader;

		// Token: 0x04000391 RID: 913
		private readonly IDataConvert dataConvert;

		// Token: 0x04000392 RID: 914
		private readonly Accessor accessor;

		// Token: 0x04000393 RID: 915
		private readonly ColumnsInfo columnsInfo;

		// Token: 0x04000394 RID: 916
		private readonly IRowsetInfo rowsetInfo;

		// Token: 0x04000395 RID: 917
		private readonly int[] ordinalIndices;

		// Token: 0x04000396 RID: 918
		private readonly IPage page;

		// Token: 0x04000397 RID: 919
		private int offset;

		// Token: 0x04000398 RID: 920
		private int refCounts;

		// Token: 0x04000399 RID: 921
		private bool isEndOfRowset;
	}
}
